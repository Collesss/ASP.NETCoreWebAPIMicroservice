using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models.Dto
{
    public class MetricAgentCreateOrUpdateResponseDto
    {
        public MetricAgent MetricAgent { get; set; }

        public EntityState State { get; set; }
    }
}
