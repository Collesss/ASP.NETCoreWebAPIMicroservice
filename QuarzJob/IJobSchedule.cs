using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzJobMetricAgent
{
    public interface IJobSchedule
    {
        Type Type { get; }
        IJobDetail GetJobDetail();
        ITrigger GetTrigger();
    }
}
