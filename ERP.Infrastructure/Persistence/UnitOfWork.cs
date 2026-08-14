using ERP.Domain.Entities.Common;
using ERP.Domain.Interfaces;
using ERP.Infrastructure.Date;
using ERP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ErbDbContext _dbContext;
        private readonly Dictionary<string, object> _repositories = [];
        public UnitOfWork(ErbDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
           return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            
            var typeName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(typeName, out var repository))

                return (IRepository<TEntity>)repository;

            var repo = new Repository<TEntity>(_dbContext);

            _repositories[typeName] = repo;

            return repo;
        
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
             
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
