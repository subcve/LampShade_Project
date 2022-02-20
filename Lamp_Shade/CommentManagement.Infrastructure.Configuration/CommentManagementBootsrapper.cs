using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.Configuration
{
	public class CommentManagementBootsrapper
	{
		public static void Configure(IServiceCollection services, string connectionString) 
		{
			services.AddTransient<ICommentApplication, CommentApplication>();
			services.AddTransient<ICommentRepository, CommentRepository>();

			services.AddDbContext<CommentContext>(c=>
			c.UseSqlServer(connectionString));
		}
	}
}