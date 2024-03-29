﻿using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            //تعیین ارتباط ببین دو جدول 
            //ابتدا باید اونو اینجوری 
            //builder.OwnsMany<RollPermissions>(.......).....
            //نوشت تا تمام پرارامتر ها رو بیاره
            builder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.HasKey(x => x.ID);
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(x => x.Role);
            });

        }
    }
}
