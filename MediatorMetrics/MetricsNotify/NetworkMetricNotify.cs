using AutoMapper;
using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class NetworkMetricNotify : BaseMetricNotify<NetworkMetric>
    {
        public NetworkMetricNotify(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, new PerformanceCounter("Network interface", "Bytes Total/sec", "Intel[R] Dual Band Wireless-AC 3165"))
        {

        }
    }
}
