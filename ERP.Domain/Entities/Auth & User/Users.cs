using ERP.Domain.Entities.Common;
using ERP.Domain.Entities.Cross_cutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities.Auth___User
{
    public class Users : BaseEntity
    {
        public bool IsActive { get; set; } = true;
        public string? PhoneNumber { get; set; } = null;
        public string? PasswordHash { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? FullName { get; set; } = null;

        public ICollection<UserRoles> UserRoles { get; set; } = [];
        public ICollection<Notifications> Notifications { get; set; } = [];

        public ICollection<RefreshTokens> RefreshTokens { get; set; } = [];
    }
}
