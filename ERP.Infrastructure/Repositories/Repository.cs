using ERP.Domain.Entities.Common;
using ERP.Domain.Interfaces;
using ERP.Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ErbDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public Repository(ErbDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
          return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public  IQueryable<T> Query()
        {
           return _dbSet.AsQueryable();
        }

        public void RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void  UpdateAsync(T entity)
        {
              _dbSet.Update(entity);
        }
    }
}
