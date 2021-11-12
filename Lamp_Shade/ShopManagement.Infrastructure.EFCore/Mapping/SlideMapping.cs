using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Sliders");
            
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(c => c.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(c => c.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Heading).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Title).HasMaxLength(255);
            builder.Property(c => c.Text).HasMaxLength(255);
            builder.Property(c => c.BtnText).HasMaxLength(50).IsRequired();
        }
    }
}
