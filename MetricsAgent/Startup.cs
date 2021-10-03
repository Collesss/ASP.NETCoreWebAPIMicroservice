using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
using Microsoft.OpenApi.Models;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzJobMetricAgent.Extension;
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
            services.AddHttpClient("RegisterAgent")
                .AddTransientHttpErrorPolicy(p => 
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            
            services.AddDBMetricsAgent(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            AutoMapper.Configuration.MapperConfigurationExpression mapperConfigurationExpression = new AutoMapper.Configuration.MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile<MapperProfile>();
            
            services.AddQuartzJobMetricAgentHostedService(mapperConfigurationExpression, Configuration["QuartzSection:RegisterHost"]);

            services.AddSingleton(new MapperConfiguration(mapperConfigurationExpression).CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API agent",
                    Description = "API сервиса агента сбора метрик"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
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

            app.UseSwagger();

            app.UseSwaggerUI(c => 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса агента сбора метрик"));
        }
    }
}
