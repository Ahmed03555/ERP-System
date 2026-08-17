using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Suppliers.GetSupplierById
{
    public record SupplierDto(
            int Id,
            string Name,
            string Email,
            string Phone,
            string Address
        );
}
