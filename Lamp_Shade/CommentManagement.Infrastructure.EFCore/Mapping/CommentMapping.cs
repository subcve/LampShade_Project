using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
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
		}
	}
}
