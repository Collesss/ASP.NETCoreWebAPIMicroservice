using AutoMapper.Configuration;
using Entities;
using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMetricsManager
{
    public static class DBMetricsManagerExtension
    {
        public static void AddDBMetricsManager(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsBuilder, MapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.AddProfile<DBMetricsManagerAutoMapperProfile>();

            serviceCollection.AddDbContext<ManagerDbContext>(optionsBuilder);

            serviceCollection.AddScoped<IRepository<MetricAgent>, Repository<MetricAgent>>();

            serviceCollection.AddScoped<IRepositoryMetricAgents<CpuMetricAgent>, RepositryMetricAgents<CpuMetricAgent, CpuMetric>>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<HardDriveMetricAgent>, RepositryMetricAgents<HardDriveMetricAgent, HardDriveMetric>>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<NetMetricAgent>, RepositryMetricAgents<NetMetricAgent, NetMetric>>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<NetworkMetricAgent>, RepositryMetricAgents<NetworkMetricAgent, NetworkMetric>>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<RamMetricAgent>, RepositryMetricAgents<RamMetricAgent, RamMetric>>();
        }
    }
}
