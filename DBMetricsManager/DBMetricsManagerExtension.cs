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
        public static void AddDBMetricsManager(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            serviceCollection.AddDbContext<ManagerDbContext>(optionsBuilder);

            serviceCollection.AddScoped<IRepository<MetricAgent>, Repository<MetricAgent>>();
        }
    }
}
