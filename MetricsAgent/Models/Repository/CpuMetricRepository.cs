using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Models
{
    public class CpuMetricRepository<T> : Repository<CpuMetric, T> where T : IDbConnection, new()
    {
        protected override string TableName => "CpuMetric";

    }
}
