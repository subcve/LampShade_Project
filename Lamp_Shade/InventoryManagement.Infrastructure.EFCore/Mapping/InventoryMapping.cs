using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Inventory");

            builder.OwnsMany(c => c.Operations, modelBuilder =>
            {
                modelBuilder.HasKey(c => c.Id);
                modelBuilder.ToTable("InventoryOperations");
                modelBuilder.Property(c => c.Description).HasMaxLength(1000).IsRequired();
                modelBuilder.WithOwner(c => c.Inventory).HasForeignKey(c => c.InventoryId);
            });
        }
    }
}
