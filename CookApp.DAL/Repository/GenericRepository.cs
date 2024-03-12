using CookApp.Entity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookApp.Context;
using CookApp.DAL.IGenericRepository;

namespace CookApp.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly CookDbContext _context;
        protected DbSet<TEntity> _dbSet;
        public GenericRepository(CookDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> Update(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkUpdate(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<TEntity?> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(t => t.ID == id);
        }

        public async Task<TEntity> SetValues(TEntity existingEntity, TEntity editedEntity)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(editedEntity);
            return existingEntity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var toDelete = await _dbSet.FindAsync(id);

                return await DeleteAsync(toDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<TEntity> GetAllAsyncQuery(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = this._dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return query;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();

                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
