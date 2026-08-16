using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.UpdateWarehouse
{
    public record UpdateWarehouseCommand(int Id,
        string Name,
        string Location
        ) : IRequest<Result<bool>>;
}
