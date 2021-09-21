using AutoMapper;
using EntitiesMetricsManager;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentCreateOrUpdateRequestDto, MetricAgent>();
                //.ForMember("LastUpdateTime", opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
