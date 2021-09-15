using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Entities;
using System.Threading.Tasks;

namespace DB
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
