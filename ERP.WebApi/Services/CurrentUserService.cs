using ERP.Application.Common.Interfaces;
using System.Security.Claims;

namespace ERP.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
        public int? UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value is string id ? int.Parse(id) : null;

        public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public IReadOnlyList<string> Roles => User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList() ?? new List<string>();
    }
}
