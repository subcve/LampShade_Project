using BlogManagement.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
	public class BlogManagementBootstrapper
	{
		public static void Configure(IServiceCollection services,string connectionString) 
		{
			services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
			services.AddTransient<IArticleCategoryReposiroty,ArticleCategoryRepository>();

			services.AddDbContext<BlogContext>(c => c.UseSqlServer(connectionString));
		}
	}
}