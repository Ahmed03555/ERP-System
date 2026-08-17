using ERP.Application.Common.Models.Suppliers.GetSupplierById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.GetSupplierByList
{
    public record GetSuppliersListQuery : IRequest<Result<List<SupplierDto>>>;

}
