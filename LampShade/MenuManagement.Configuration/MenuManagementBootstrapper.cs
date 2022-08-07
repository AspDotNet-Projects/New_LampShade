using _0_Framework.Infrastructure;
using MenuManagement.Configuration.Permissions;
using Microsoft.Extensions.DependencyInjection;

namespace MenuManagement.Configuration
{
    public class MenuManagementBootstrapper
    {
        public static void configur(IServiceCollection services)
        {
            services.AddTransient<IPermissionExposer, MenuPermissionExposer>();

        }
    }
}
