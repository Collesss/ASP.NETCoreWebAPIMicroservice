using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Models
{
    public class NetMetricRepository<T> : Repository<NetMetric, T> where T : IDbConnection, new()
    {
        protected override string TableName => "NetMetric";
    }
}
