using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(int Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    decimal CreditLimit
) : IRequest<Result<bool>>;

}
