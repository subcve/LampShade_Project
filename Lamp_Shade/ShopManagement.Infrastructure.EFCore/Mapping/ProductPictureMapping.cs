using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(c=>c.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(c=>c.PictureTitle).HasMaxLength(500).IsRequired();

            builder.HasOne(c => c.Product)
                .WithMany(c => c.ProductPictures)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
