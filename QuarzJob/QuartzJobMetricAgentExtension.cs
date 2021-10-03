using AutoMapper;
using MediatorMetrics.Extension;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuarzJob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace QuartzJobMetricAgent.Extension
{
    public static class QuartzJobMetricAgentExtension
    {
        public static void AddQuartzJobMetricAgentHostedService(this IServiceCollection serviceCollection, IMapperConfigurationExpression mapperConfigurationExpression, string RegisterHost)
        {
            serviceCollection.AddMediatorMetrics(mapperConfigurationExpression);

            serviceCollection.AddSingleton<IJob>(ser => new RegisterAgentJob(ser.GetService<IHttpClientFactory>(), new Uri(RegisterHost)));
            serviceCollection.AddSingleton<IJobSchedule>(new JobSchedule(typeof(RegisterAgentJob), "0 0/10 * * * ?"));

            serviceCollection.AddSingleton<IJob, MetricJob>();
            serviceCollection.AddSingleton<IJobSchedule>(new JobSchedule(typeof(MetricJob), "0/5 * * * * ?"));

            serviceCollection.AddSingleton<IJobFactory, SingletonJobFactory>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            serviceCollection.AddHostedService<QuartzHostedService>();
        }
    }
}
