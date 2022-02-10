using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore
{
	public class BlogContext : DbContext
	{
		public DbSet<ArticleCategory> ArticleCategories { get; set; }
		public BlogContext(DbContextOptions<BlogContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assmbly = typeof(ArticleCategoryMapping).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assmbly);
			base.OnModelCreating(modelBuilder);
		}
	}
}