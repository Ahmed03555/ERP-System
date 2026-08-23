using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Inventory.Queries.GetStockMovements
{
    public record GetStockMovementsQuery : IRequest<Result<List<MovementDto>>>;
}
