using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBMetricsManager;
using EntitiesMetricsManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using QuartzJobMetricManager.Extension;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient("MetricAgent")
                .AddTransientHttpErrorPolicy(p => 
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            
            //services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

            AutoMapper.Configuration.MapperConfigurationExpression mapperConfigurationExpression = new AutoMapper.Configuration.MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile<MapperProfile>();

            services.AddDBMetricsManager(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), mapperConfigurationExpression);

            services.AddQuartzJobMetricManagerHostedService(
                Configuration["QuartzSection:CronTimeDelete"], 
                TimeSpan.FromSeconds(Configuration.GetValue<double>("QuartzSection:TimeDelete")));

            services.AddSingleton(new MapperConfiguration(mapperConfigurationExpression).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
