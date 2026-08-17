using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.Commands.DeleteSupplier
{
    public record DeleteSupplierCommand(int Id) : IRequest<Result<bool>>;

}
