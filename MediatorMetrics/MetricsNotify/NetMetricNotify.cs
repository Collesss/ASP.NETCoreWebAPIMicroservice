using AutoMapper;
using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class NetMetricNotify : BaseMetricNotify<NetMetric>
    {
        public NetMetricNotify(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_"))
        {

        }
    }
}
