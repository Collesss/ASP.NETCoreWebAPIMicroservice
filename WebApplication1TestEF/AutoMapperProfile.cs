using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            CreateMap<MetricAgentCreateOrUpdateRequestDto, MetricAgent>();
            CreateMap<EntityEntry<MetricAgent>, MetricAgentCreateOrUpdateResponseDto>()
                .ForMember(response => response.MetricAgent, opts => opts.MapFrom(source => source.Entity))
                .ForMember(response => response.State, opts => opts.MapFrom(source => source.State));
        }
    }
}
