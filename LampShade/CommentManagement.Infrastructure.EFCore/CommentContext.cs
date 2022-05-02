using CommentManagement.Domain.ProductCommentAgg;
using CommentManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore
{
    public class CommentContext:DbContext

    {
        public DbSet<ProductComment>  ProductComments{ get; set; }


        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var assembly = typeof(ProductCommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
