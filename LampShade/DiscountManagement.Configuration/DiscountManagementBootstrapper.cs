using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.Extensions.DependencyInjection;
using System;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Configuration.Permissions;
using DiscountManagement.Infrastructure.EFCore;
using DiscountMangement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using DiscountMangement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string Connectionstring)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddTransient<IPermissionExposer, DiscountPermissionExposer>();

            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(Connectionstring));


        }
    }
}
