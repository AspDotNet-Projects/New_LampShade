using AccountManagement.Application;
using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionstring)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository,AccountRepository>();


            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionstring));

        }
    }
}
