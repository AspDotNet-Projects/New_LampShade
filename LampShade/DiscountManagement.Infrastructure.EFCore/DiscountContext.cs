using System;
using DiscountManagement.Infrastructure.EFCore.Mapping;
using DiscountMangement.Domain.ColleagueDiscountAgg;
using DiscountMangement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
