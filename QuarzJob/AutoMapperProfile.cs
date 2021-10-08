using AutoMapper;
using QuartzJobMetricAgent.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuartzJobMetricAgent
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Uri, AgentCreateOrUpdateRequestDto>()
                .ForMember(a => a.AddressAgent, mce => mce.MapFrom(u => u));
        }
    }
}
