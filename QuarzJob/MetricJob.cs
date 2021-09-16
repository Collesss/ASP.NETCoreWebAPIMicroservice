using MediatorMetrics;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuarzJob
{
    public class MetricJob : IJob
    {
        private IMediator _mediator;

        public MetricJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            await _mediator.Notify();
        }
    }
}
