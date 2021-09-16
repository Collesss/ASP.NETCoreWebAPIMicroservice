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
            CreateMap<BaseMetricCreateRequestDto, BaseMetricEntity>()
                .Include<CpuMetricCreateRequestDto, CpuMetric>()
                .Include<HardDriveMetricCreateRequestDto, HardDriveMetric>()
                .Include<NetworkMetricCreateRequestDto, NetMetric>()
                .Include<NetworkMetricCreateRequestDto, NetworkMetric>()
                .Include<RamMetricCreateRequestDto, RamMetric>();

            CreateMap<CpuMetricCreateRequestDto, CpuMetric>();
            CreateMap<HardDriveMetricCreateRequestDto, HardDriveMetric>();
            CreateMap<NetworkMetricCreateRequestDto, NetMetric>();
            CreateMap<NetworkMetricCreateRequestDto, NetworkMetric>();
            CreateMap<RamMetricCreateRequestDto, RamMetric>();

            CreateMap<int, BaseMetricEntity>()
                .Include<int, CpuMetric>()
                .Include<int, HardDriveMetric>()
                .Include<int, NetMetric>()
                .Include<int, NetworkMetric>()
                .Include<int, RamMetric>()
                .ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<int, CpuMetric>();
            CreateMap<int, HardDriveMetric>();
            CreateMap<int, NetMetric>();
            CreateMap<int, NetworkMetric>();
            CreateMap<int, RamMetric>();

            /*
            CreateMap<int, CpuMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<RamMetricCreateRequestDto, RamMetric>();
            CreateMap<int, RamMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<NetworkMetricCreateRequestDto, NetMetric>();
            CreateMap<int, NetMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<NetworkMetricCreateRequestDto, NetworkMetric>();
            CreateMap<int, NetworkMetric>().ForMember("Id", opt => opt.MapFrom(src => src));

            CreateMap<HardDriveMetricCreateRequestDto, HardDriveMetric>();
            CreateMap<int, HardDriveMetric>().ForMember("Id", opt => opt.MapFrom(src => src));
            */
        }
    }
}
