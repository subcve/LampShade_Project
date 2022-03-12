using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
	public class OrderMapping : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");
			builder.HasKey(c => c.Id);

			builder.Property(c => c.IssueTrackingNo).HasMaxLength(8);

			builder.OwnsMany(c => c.OrderItems,modelBuilder =>
			{
				modelBuilder.ToTable("OrderItems");
				modelBuilder.HasKey(c => c.Id);
				modelBuilder.WithOwner(c => c.Order).HasForeignKey(c => c.OrderId);
			});
		}
	}
}
