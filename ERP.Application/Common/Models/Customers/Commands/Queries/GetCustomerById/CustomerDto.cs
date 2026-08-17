using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Customers.Commands.Queries.GetCustomerById
{
    public record CustomerDto(
    int Id,
    string Name,
    string Email,
    string PhoneNumber,
    string Address,
    decimal CreditLimit
        );
}
