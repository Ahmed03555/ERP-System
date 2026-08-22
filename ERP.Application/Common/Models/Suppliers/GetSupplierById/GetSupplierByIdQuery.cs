using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.GetSupplierById
{
    public record GetSupplierByIdQuery(int Id) : IRequest<Result<SupplierDto>>,ICacheableQuery
    {
        public string CacheKey => $"supplieres:byid:{Id}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
