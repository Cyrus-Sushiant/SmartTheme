namespace SmartTheme.Core.Services.Identity
{
    public interface IIdentityService
    {
        bool IsAuthenticated { get; }
        long UserId { get; }

        bool IsInRole(string role);
        bool HasPermission(string actionName, string controllerName, string areaName = null);
    }
}
