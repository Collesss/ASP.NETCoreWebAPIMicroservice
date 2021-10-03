using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzJobMetricManager
{
    public class JobSchedule : IJobSchedule
    {
        public Type Type { get; }
        public string CronExpression { get; }
        public JobSchedule(Type type, string cronExpression)
        {
            Type = type;
            CronExpression = cronExpression;
        }

        public IJobDetail GetJobDetail()
        {
            return JobBuilder
                .Create(Type)
                .WithIdentity(Type.FullName)
                .WithDescription(Type.Name)
                .Build();
        }
        public ITrigger GetTrigger()
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{Type.FullName}.trigger")
                .WithCronSchedule(CronExpression)
                .WithDescription(Type.Name)
                .Build();
        }
    }
}
