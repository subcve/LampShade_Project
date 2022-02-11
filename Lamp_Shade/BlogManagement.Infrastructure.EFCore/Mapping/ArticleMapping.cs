using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
	public class ArticleMapping : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
            builder.ToTable("Articles");
            builder.HasKey(c => c.Id);

            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(500);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();
            builder.Property(x => x.KeyWords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);

            builder.HasOne(c => c.Category)
                .WithMany(x => x.Articles)
                .HasForeignKey(z => z.CategoryId);
        }
	}
}
