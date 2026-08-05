using ERP.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Auth___User
{
    public class RefreshTokens : BaseEntity
    {
        public int UserId { get; set; }
        public Users User { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
    }
}
