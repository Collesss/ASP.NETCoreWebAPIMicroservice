using AutoMapper;
using Entities;
using EntitiesMetricsManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMetricsManager
{
    public class DBMetricsManagerAutoMapperProfile : Profile
    {
        public DBMetricsManagerAutoMapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricAgent>();
            CreateMap<HardDriveMetric, HardDriveMetricAgent>();
            CreateMap<NetMetric, NetMetricAgent>();
            CreateMap<NetworkMetric, NetworkMetricAgent>();
            CreateMap<RamMetric, RamMetricAgent>();
        }
    }
}
