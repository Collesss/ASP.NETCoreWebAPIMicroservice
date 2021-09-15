using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private MetricsDbContext _metricsDbContext;
        public Repository(MetricsDbContext metricsDbContext)
        {
            _metricsDbContext = metricsDbContext;
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAll()
        {
            return _metricsDbContext.Set<TEntity>().AsQueryable();
        }

        async Task IRepository<TEntity>.CreateAsync(TEntity entity)
        {
            await _metricsDbContext.Set<TEntity>().AddAsync(entity);
            await _metricsDbContext.SaveChangesAsync();
        }

        async Task IRepository<TEntity>.DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _metricsDbContext.Set<TEntity>().Remove(entity));
            await _metricsDbContext.SaveChangesAsync();
        }

        async Task IRepository<TEntity>.UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _metricsDbContext.Set<TEntity>().Update(entity));
            await _metricsDbContext.SaveChangesAsync();
        }
    }
}
