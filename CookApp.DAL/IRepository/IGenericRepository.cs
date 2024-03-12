using CookApp.Entity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.DAL.IRepository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> BulkUpdate(IEnumerable<TEntity> entities);
        Task<bool> Update(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> SetValues(TEntity existingEntity, TEntity editedEntity);
        IQueryable<TEntity> GetAllAsyncQuery(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
