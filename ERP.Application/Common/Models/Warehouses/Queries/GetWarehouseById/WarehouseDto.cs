using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Warehouses.Queries.GetWarehouseById
{
    public record WarehouseDto(
        int Id,
        string Name,
        string Location
        );
}
