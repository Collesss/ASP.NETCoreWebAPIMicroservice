using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzJobMetricAgent
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        IJob IJobFactory.NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetServices<IJob>().Single(job => job.GetType() == bundle.JobDetail.JobType);
            //return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        void IJobFactory.ReturnJob(IJob job)
        {
        }
    }
}
