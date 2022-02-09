using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
	public class CommentMapping : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comments");
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
			builder.Property(c => c.Email).HasMaxLength(150).IsRequired();
			builder.Property(c => c.Message).HasMaxLength(500).IsRequired();

			builder.HasOne(c => c.Product).WithMany(c => c.Comments).HasForeignKey(c => c.ProductId);
		}
	}
}
