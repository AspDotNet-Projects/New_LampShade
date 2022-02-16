using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopMangement.Configuration
{
    public class ShopManagementBootstrapper
    {
        /// <summary>
        /// wire up har majol dakhek khodesh
        /// </summary>
        /// <param name="services"></param>
        public static void Confgure(IServiceCollection services,string connectionstring)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionstring));

        }
    }
}
