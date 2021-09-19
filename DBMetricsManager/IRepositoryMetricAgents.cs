using EntitiesMetricsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMetricsManager
{
    public interface IRepositoryMetricAgents<TEntity> where TEntity : BaseMetricAgent
    {
        public IEnumerable<TEntity> GetMetricFromAgent(int id, DateTime from, DateTime to);
        public IEnumerable<TEntity> GetMetricFromAgents(DateTime from, DateTime to);
    }
}
