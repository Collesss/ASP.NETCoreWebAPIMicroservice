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
        }
    }
}
