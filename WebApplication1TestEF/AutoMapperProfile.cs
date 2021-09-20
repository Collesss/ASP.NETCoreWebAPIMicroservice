using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1TestEF.Models;
using WebApplication1TestEF.Models.Dto;

namespace WebApplication1TestEF
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MetricAgentCreateRequestDto, MetricAgent>();
            CreateMap<MetricAgentUpdateRequestDto, MetricAgent>();
        }
    }
}
