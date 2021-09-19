using DBMetricsManager;
using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzJobMetricManager
{
    public class RemoveOldRegistrations : IJob
    {
        private IServiceProvider _serviceProvider;
        private readonly TimeSpan _timeDelete;

        public RemoveOldRegistrations(IServiceProvider serviceProvider, TimeSpan timeDelete)
        {
            _serviceProvider = serviceProvider;
            _timeDelete = timeDelete;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            //DateTime dateTimeDelete = DateTime.Now - (context.NextFireTimeUtc.Value.DateTime - DateTime.Now);
            DateTime dateTimeDelete = DateTime.Now - _timeDelete;
            
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IRepository<MetricAgent> repository = scope.ServiceProvider.GetService<IRepository<MetricAgent>>();
                
                await Task.WhenAll(
                    (await repository.GetAll().Where(agent => agent.LastUpdateTime < dateTimeDelete)
                    .ToListAsync(context.CancellationToken)).Select(agent => repository.DeleteAsync(agent)).ToArray());
            }
        }
    }
}
