using Microsoft.Extensions.DependencyInjection;
using SmartTheme.Core.Services.Identity;

namespace SmartTheme.Core.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void AddSmartThemeCoreServices(this IServiceCollection services)
        {
            // services
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
