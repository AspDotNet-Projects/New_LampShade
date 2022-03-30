using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.Id);


            ////OwnsMany با Hasmany
            /// فرق میکنه که هز منی برای دو جدول جدا هستند
            /// و آنها را اینکلود می کنیم
            /// ولی واز منی دو جدول مرتبط هستند که نیاز به اینکلود نیست
            builder.OwnsMany(x => x.Operations, ModelBuilder =>
            {
                ModelBuilder.HasKey(x => x.Id);
                ModelBuilder.ToTable("InventoryOperations");
                ModelBuilder.Property(x => x.Description).HasMaxLength(1000);
                ModelBuilder.WithOwner(x => x.Inventory)
                    .HasForeignKey(x => x.InventoryId);
            });
        }
    }
}
