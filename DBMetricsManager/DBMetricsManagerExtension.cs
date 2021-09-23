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

            serviceCollection.AddScoped<IRepositoryMetricAgents<CpuMetricAgent>, RepositoryMetricAgentsCpu>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<HardDriveMetricAgent>, RepositoryMetricAgentsHardDrive>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<NetMetricAgent>, RepositoryMetricAgentsNet>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<NetworkMetricAgent>, RepositoryMetricAgentsNetwork>();
            serviceCollection.AddScoped<IRepositoryMetricAgents<RamMetricAgent>, RepositoryMetricAgentsRam>();
        }
    }
}
