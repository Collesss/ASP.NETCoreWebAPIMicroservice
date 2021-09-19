using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatorMetrics.Extension
{
    public static class AddMediatorMetricsExtension
    {
        public static void AddMediatorMetrics(this IServiceCollection serviceCollection, IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.AddProfile<MapperProfileMediator>();

            serviceCollection.AddSingleton<INotify, CpuMetricNotify>();
            serviceCollection.AddSingleton<INotify, HardDriveMetricNotify>();
            serviceCollection.AddSingleton<INotify, NetMetricNotify>();
            serviceCollection.AddSingleton<INotify, NetworkMetricNotify>();
            serviceCollection.AddSingleton<INotify, RamMetricNotify>();

            serviceCollection.AddSingleton<IMediator, MetricMediator>();
        }
    }
}
