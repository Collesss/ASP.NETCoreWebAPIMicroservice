using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public abstract class BaseMetricEntity : BaseEntity
    {
        public int Value { get; set; }
        public DateTime Time { get; set; }
    }
}
