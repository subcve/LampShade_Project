using _01_Framework.Application;
using _01_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
	public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
	{
		private readonly CommentContext _context;

		public CommentRepository(CommentContext context) : base(context)
		{
			_context = context;
		}

		public List<CommentViewModel> Search(CommentSearchModel searchModel)
		{
			var query = _context.Comments
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Name = c.Name,
					Email = c.Email,
					Message = c.Message,
					PhoneNumber = c.PhoneNumber,
					IsConfirmed = c.IsConfirmed,
					IsCanceled = c.IsCanceled,
					Type = c.Type,
					OwnerRecordId = c.OwnerRecordId,
					CommentDate = c.CreationDate.ToFarsi(),
				}).AsNoTracking();

			if (!string.IsNullOrWhiteSpace(searchModel.Name))
				query = query.Where(c => c.Name.Contains(searchModel.Name));

			if (!string.IsNullOrWhiteSpace(searchModel.Email))
				query = query.Where(c => c.Name.Contains(searchModel.Email));

			return query.OrderByDescending(c => c.Id).ToList();
		}
	}
}
