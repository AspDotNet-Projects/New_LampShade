using CommentManagement.Domain.ProductCommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(500);
            builder.Property(x => x.Message).HasMaxLength(1000);
            builder.Property(x => x.Website).HasMaxLength(500);


            /////.OnDelete(DeleteBehavior.NoAction);
            ///// برای اینککه ارتباط وجود داره بین آنها میگه در هنگگام تعریف مایگریشت
            ///// حتما باد مشحص کنیم که باید تعیین تکلیف بشه
            //builder.HasOne(x => x.Parent)
            //    .WithMany(x => x.Children)
            //    .HasForeignKey(x => x.ParentId)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
