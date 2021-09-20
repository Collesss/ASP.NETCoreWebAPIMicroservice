using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models.Dto
{
    public class MetricAgentCreateOrUpdateRequestDto
    {
        public Uri AddressAgent { get; set; }
        public int TestValue { get; set; }
    }
}
