using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Cross_cutting
{
    public class Notifications:BaseEntity
    {
        public bool IsRead { get; set; }
        public string Body { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int UserId { get; set; }
        public Users Users { get; set; } = default!;

    }
}
