using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Cross_cutting
{
    public class AuditLogs : BaseEntity
    {
        public int? UserId { get; set; }

        public Users? Users { get; set; } = default!;
        public string Entity { get; set; } = default!;
        public int EntityId { get; set; }
        public string Action { get; set; } = default!;
        public string OldValues { get; set; } = default!;
        public string NewValues { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}
