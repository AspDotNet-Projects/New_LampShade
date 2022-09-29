using InventoryManagement.Domain.InventoryAgg;
using Microsoft.Extensions.DependencyInjection;
using _0_Framework.Infrastructure;
using _01_LampShadeQuery.Contracts.Inventory;
using _01_LampShadeQuery.Query;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Applicataion;


namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configur(IServiceCollection services,string connectionstring)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();


            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
