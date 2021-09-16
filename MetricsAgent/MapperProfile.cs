using AutoMapper;
using Entities;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricCreateRequestDto, CpuMetric>();
            CreateMap<int, CpuMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<RamMetricCreateRequestDto, RamMetric>();
            CreateMap<int, RamMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<NetworkMetricCreateRequestDto, NetMetric>();
            CreateMap<int, NetMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<NetworkMetricCreateRequestDto, NetworkMetric>();
            CreateMap<int, NetworkMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<HardDriveMetricCreateRequestDto, HardDriveMetric>();
            CreateMap<int, HardDriveMetric>().ForMember("Id", opt => opt.MapFrom(src => src));
        }
    }
}
