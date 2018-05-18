using BVirtual.FaTi.Business.Providers;
using BVirtual.FaTi.Business.Routes;
using Microsoft.Extensions.DependencyInjection;

namespace BVirtual.FaTi.Services
{
    public static class ServiceRoutes
    {
        public static void AddFareServices(this IServiceCollection services)
        {
            services.AddSingleton<UserManagerActorProvider>();

            services.AddSingleton<CreateUser>();
        }
    }
}
