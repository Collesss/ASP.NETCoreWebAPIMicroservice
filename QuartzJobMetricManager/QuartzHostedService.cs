using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzJobMetricManager
{
    public class QuartzHostedService : IHostedService
    {
        private readonly IJobFactory _jobFactory;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        private IScheduler Scheduler { get; set; }

        public QuartzHostedService(IJobFactory jobFactory, ISchedulerFactory schedulerFactory, IEnumerable<JobSchedule> jobSchedules)
        {
            _jobFactory = jobFactory;
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
        }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            (Scheduler = await _schedulerFactory.GetScheduler(cancellationToken)).JobFactory = _jobFactory;

            foreach (JobSchedule jobSchedule in _jobSchedules)
                await Scheduler.ScheduleJob(jobSchedule.GetJobDetail(), jobSchedule.GetTrigger(), cancellationToken);

            await Scheduler.Start(cancellationToken);
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
