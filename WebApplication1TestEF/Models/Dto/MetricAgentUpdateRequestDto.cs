using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models.Dto
{
    public class MetricAgentUpdateRequestDto
    {
        public int Id { get; set; }
        public Uri AddressAgent { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
