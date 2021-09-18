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

            services.AddAutoMapper(cfg => {
                cfg.AddProfile<MapperProfile>();
                cfg.AddProfile<MapperProfileMediator>();
            });

            services.AddSingleton<INotify, CpuMetricNotify>();
            services.AddSingleton<INotify, HardDriveMetricNotify>();
            services.AddSingleton<INotify, NetMetricNotify>();
            services.AddSingleton<INotify, NetworkMetricNotify>();
            services.AddSingleton<INotify, RamMetricNotify>();

            
            services.AddSingleton<IMediator, MetricMediator>();

            services.AddSingleton<RegisterAgentJob>(ser => 
                new RegisterAgentJob(
                    ser.GetService<IHttpClientFactory>(), 
                    new Uri(Configuration.GetSection("QuartzSection").GetValue<string>("RegisterHost"))
                    )
                );

            services.AddSingleton(new JobSchedule(typeof(RegisterAgentJob), "0 0/10 * * * ?"));

            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(typeof(MetricJob), "0/5 * * * * ?"));
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            
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
