using AccountManagement.Domian.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
			builder.HasKey(c => c.Id);

            builder.Property(c=>c.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(c=>c.Accounts)
                .WithOne(c=>c.Role)
                .HasForeignKey(c=>c.RoleId);

            builder.OwnsMany(c => c.Permissions, modelBuilder =>
               {
                   modelBuilder.ToTable("RolePermissions");
                   modelBuilder.HasKey(c => c.Id);
                   modelBuilder.Ignore(c => c.Name);
                   modelBuilder.WithOwner(c => c.Role);
               });
        }
    }
}