using EntitiesMetricsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMetricsManager
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private ManagerDbContext _managerDbContext;
        public Repository(ManagerDbContext managerDbContext)
        {
            _managerDbContext = managerDbContext;
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAll()
        {
            return _managerDbContext.Set<TEntity>().AsQueryable();
        }

        async Task IRepository<TEntity>.CreateAsync(TEntity entity)
        {
            await _managerDbContext.Set<TEntity>().AddAsync(entity);
            await _managerDbContext.SaveChangesAsync();
        }

        async Task IRepository<TEntity>.DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _managerDbContext.Set<TEntity>().Remove(entity));
            await _managerDbContext.SaveChangesAsync();
        }

        async Task IRepository<TEntity>.UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _managerDbContext.Set<TEntity>().Update(entity));
            await _managerDbContext.SaveChangesAsync();
        }
    }
}
