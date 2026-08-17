using ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomersList
{
    public record GetCustomersListQuery() : IRequest<Result<List<CustomerDto>>>;
}
