using DB;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMetricsAgent.Extension
{
    public static class DBMetricsAgentExtension
    {
        public static void AddDBMetricsAgent(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            serviceCollection.AddDbContext<MetricsDbContext>(optionsBuilder);

            serviceCollection.AddScoped<IRepository<CpuMetric>, Repository<CpuMetric>>();
            serviceCollection.AddScoped<IRepository<HardDriveMetric>, Repository<HardDriveMetric>>();
            serviceCollection.AddScoped<IRepository<NetMetric>, Repository<NetMetric>>();
            serviceCollection.AddScoped<IRepository<NetworkMetric>, Repository<NetworkMetric>>();
            serviceCollection.AddScoped<IRepository<RamMetric>, Repository<RamMetric>>();
        }
    }
}
