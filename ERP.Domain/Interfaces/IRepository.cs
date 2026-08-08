using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id,CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        IQueryable<T> Query();
        Task AddAsync(T entity,CancellationToken cancellationToken = default);
        void UpdateAsync(T entity);
        void RemoveAsync(T entity);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    }
}
