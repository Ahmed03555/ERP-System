using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.Commands.UpdateSupplier
{
    public record UpdateSupplierCommand( int Id,
    string Name,
    string Email,
    string Phone,
    string Address
) : IRequest<Result<bool>>;
}
