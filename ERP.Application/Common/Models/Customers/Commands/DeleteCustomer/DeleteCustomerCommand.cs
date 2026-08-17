using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(int Id) : IRequest<Result<bool>>;

}
