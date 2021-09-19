using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using DB;
using DBMetricsAgent.Extension;
using Entities;
using MediatorMetrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzJob.Extension;
using QuarzJob;

namespace MetricsAgent
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
            services.AddHttpClient();
            
            services.AddDBMetricsAgent(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            AutoMapper.Configuration.MapperConfigurationExpression mapperConfigurationExpression = new AutoMapper.Configuration.MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile<MapperProfile>();
            
            services.AddQuartzJobMetricAgentHostedService(mapperConfigurationExpression, Configuration["QuartzSection:RegisterHost"]);

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
