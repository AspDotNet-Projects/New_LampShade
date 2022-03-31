using InventoryManagement.Domain.InventoryAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using InventoryManagement.Applicataion;

namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configur(IServiceCollection services,string connectionstring)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
