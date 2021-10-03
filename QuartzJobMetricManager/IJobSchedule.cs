using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzJobMetricManager
{
    public interface IJobSchedule
    {
        Type Type { get; }
        IJobDetail GetJobDetail();
        ITrigger GetTrigger();
    }
}
