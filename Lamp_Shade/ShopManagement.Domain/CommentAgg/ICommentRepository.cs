using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.CommentAgg
{
	public interface ICommentRepository : IRepository<long,Comment>
	{
		List<CommentViewModel> Search(CommentSearchModel searchModel);
	}
}
