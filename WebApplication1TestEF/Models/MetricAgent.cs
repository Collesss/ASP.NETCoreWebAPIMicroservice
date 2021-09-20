using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models
{
    public class MetricAgent : BaseEntity
    {
        public Uri AddressAgent { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int TestValue { get; set; }
    }
}
