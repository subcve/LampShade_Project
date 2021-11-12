using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Code).HasMaxLength(15).IsRequired();
            builder.Property(c => c.ShortDescription).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.Picture).HasMaxLength(1000);
            builder.Property(c => c.PictureAlt).HasMaxLength(255);
            builder.Property(c => c.PictureTitle).HasMaxLength(500);

            

            builder.Property(c => c.Keywords).HasMaxLength(100).IsRequired();
            builder.Property(c => c.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(500).IsRequired();

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);

            builder.HasMany(c => c.ProductPictures)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
