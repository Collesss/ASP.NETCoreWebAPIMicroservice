using AutoMapper;
using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class HardDriveMetricNotify : BaseMetricNotify<HardDriveMetric>
    {
        public HardDriveMetricNotify(IServiceProvider serviceProvider, IMapper mapper)
            : base(serviceProvider, mapper, new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total"))
        {

        }
    }
}
