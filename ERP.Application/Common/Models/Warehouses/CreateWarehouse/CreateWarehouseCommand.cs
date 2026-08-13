using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.CreateWarehouse
{
    public record CreateWarehouseCommand(string Name, string Location) : IRequest<Result<int>>;
}
