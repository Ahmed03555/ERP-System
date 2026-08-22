using ERP.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerDto>>, ICacheableQuery
    {
        public string CacheKey => $"customers:byid:{Id}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }
}
