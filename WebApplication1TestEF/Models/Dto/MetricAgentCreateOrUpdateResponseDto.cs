using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models.Dto
{
    public class MetricAgentCreateOrUpdateResponseDto
    {
        public int Id { get; set; }
        public Uri AddressAgent { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int TestValue { get; set; }
    }
}
