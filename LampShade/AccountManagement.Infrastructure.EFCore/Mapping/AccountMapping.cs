using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fullname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ProfilePhoto).HasMaxLength(500);
            builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);
            //تعیین ارتباط ببین دو جدول 
            //ابتدا باید اونو اینجوری 
            //builder.OwnsMany<RollPermissions>(.......).....
            //نوشت تا تمام پرارامتر ها رو بیاره
            builder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.HasKey(x => x.ID);
                navigationBuilder.ToTable("AccountPermissions");
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(x => x.Account);
            });

        }
    }
}
