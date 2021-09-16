using AutoMapper;
using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class CpuMetricNotify : BaseMetricNotify<CpuMetric>
    {
        public CpuMetricNotify(IServiceProvider serviceProvider, IMapper mapper) 
            : base(serviceProvider, mapper, new PerformanceCounter("Processor", "% Processor Time", "_Total"))
        {
            
        }
    }
}
