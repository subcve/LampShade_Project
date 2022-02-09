using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
	{
		private readonly ShopContext _context;

		public CommentRepository(ShopContext context) : base(context)
		{
			_context = context;
		}

		public List<CommentViewModel> Search(CommentSearchModel searchModel)
		{
			var query = _context.Comments.Include(x=>x.Product).Select(c => new CommentViewModel
			{
				Id = c.Id,
				Name = c.Name,
				Email = c.Email,
				Message = c.Message,
				IsConfirmed = c.IsConfirmed,
				IsCanceled = c.IsCanceled,
				CommentDate = c.CreationDate.ToFarsi(),
				ProductId = c.ProductId,
				ProductName = c.Product.Name
			}).AsNoTracking();

			if (!string.IsNullOrWhiteSpace(searchModel.Name))
				query = query.Where(c => c.Name.Contains(searchModel.Name));

			if (!string.IsNullOrWhiteSpace(searchModel.Email))
				query = query.Where(c => c.Name.Contains(searchModel.Email));

			return query.OrderByDescending(c => c.Id).ToList();
		}
	}
}
