using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById
{
    public record GetWarehouseById(int Id) : IRequest<Result<WarehouseDto>>, ICacheableQuery
    {
        public string CacheKey => $"warehouses:byid:{Id}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
