using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        
    }
}
