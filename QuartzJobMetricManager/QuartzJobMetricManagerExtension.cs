using DBMetricsManager;
using EntitiesMetricsManager;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzJobMetricManager.Extension
{
    public static class QuartzJobMetricManagerExtension
    {
        public static void AddQuartzJobMetricManagerHostedService(this IServiceCollection serviceCollection, string cronTimeDelete, TimeSpan timeDelete)
        {
            serviceCollection.AddSingleton<IJob>(src => new RemoveOldRegistrations(src, timeDelete));
            serviceCollection.AddSingleton(new JobSchedule(typeof(RemoveOldRegistrations), cronTimeDelete));

            serviceCollection.AddSingleton<IJobFactory, SingletonJobFactory>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            serviceCollection.AddHostedService<QuartzHostedService>();
        }
    }
}
