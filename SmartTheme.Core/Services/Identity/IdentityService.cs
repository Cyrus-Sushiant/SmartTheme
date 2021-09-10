using Microsoft.AspNetCore.Http;
using SmartTheme.Core.Domain.Identity;
using SmartTheme.Core.Extensions;
using SmartTheme.Core.Services.Identity.Helper;

namespace SmartTheme.Core.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _accessor;

        public long UserId => _accessor.HttpContext.User.GetUserId<long>();
        public bool IsAuthenticated => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public IdentityService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public bool HasPermission(string actionName, string controllerName, string areaName = null)
        {
            var claimValue = $"{areaName}:{controllerName.Replace("Controller", null)}:{actionName}";
            return this.IsAuthenticated && (this.IsInRole(DefaultRoles.Admin) || _accessor.HttpContext.User.HasClaim(c => c.Type == ConstantPolicies.DynamicPermission && c.Value == claimValue));
        }
    }
}
