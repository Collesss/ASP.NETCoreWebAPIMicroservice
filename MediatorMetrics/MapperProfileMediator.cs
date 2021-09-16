using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediatorMetrics
{
    public class MapperProfileMediator : Profile
    {
        public MapperProfileMediator()
        {
            CreateMap<PerformanceCounter, BaseMetricEntity>()
                .Include<PerformanceCounter, CpuMetric>()
                .Include<PerformanceCounter, HardDriveMetric>()
                .Include<PerformanceCounter, NetMetric>()
                .Include<PerformanceCounter, NetworkMetric>()
                .Include<PerformanceCounter, RamMetric>()
                .ForMember("Id", opt => opt.Ignore())
                .ForMember("Value", opt => opt.MapFrom(pc => (int)MathF.Round(pc.NextValue())))
                .ForMember("Time", opt => opt.MapFrom(pc => DateTime.Now));

            CreateMap<PerformanceCounter, CpuMetric>();
            CreateMap<PerformanceCounter, HardDriveMetric>();
            CreateMap<PerformanceCounter, NetMetric>();
            CreateMap<PerformanceCounter, NetworkMetric>();
            CreateMap<PerformanceCounter, RamMetric>();
        }
    }
}
