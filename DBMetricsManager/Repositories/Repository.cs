using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        async Task<TEntity> IRepository<TEntity>.CreateAsync(TEntity entity)
        {
            TEntity returnEntity = (await _managerDbContext.Set<TEntity>().AddAsync(entity)).Entity;
            await _managerDbContext.SaveChangesAsync();
            return returnEntity;
        }

        async Task<TEntity> IRepository<TEntity>.DeleteAsync(TEntity entity)
        {
            TEntity returnEntity = (await Task.Run(() => _managerDbContext.Set<TEntity>().Remove(entity))).Entity;
            await _managerDbContext.SaveChangesAsync();
            return returnEntity;
        }

        async Task IRepository<TEntity>.DeleteRangeAsync(IEnumerable<TEntity> entitys)
        {
            await Task.Run(() => _managerDbContext.Set<TEntity>().RemoveRange(entitys));
            await _managerDbContext.SaveChangesAsync();
        }

        async Task<TEntity> IRepository<TEntity>.UpdateAsync(TEntity entity)
        {
            TEntity returnEntity = (await Task.Run(() => _managerDbContext.Set<TEntity>().Update(entity))).Entity;
            await _managerDbContext.SaveChangesAsync();
            return returnEntity;
        }
    }
}
