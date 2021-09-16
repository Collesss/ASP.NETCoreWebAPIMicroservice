using AutoMapper;
using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class RamMetricNotify : BaseMetricNotify<RamMetric>
    {
        public RamMetricNotify(IServiceProvider serviceProvider, IMapper mapper) 
            : base(serviceProvider, mapper, new PerformanceCounter("Memory", "Available MBytes"))
        {
            
        }
    }
}
