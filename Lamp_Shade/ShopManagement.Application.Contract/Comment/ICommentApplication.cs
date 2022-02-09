using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Comment
{
	public interface ICommentApplication
	{
		OperationResult Add(AddComment command);
		OperationResult Confirm(long id);
		OperationResult Cancel(long id);
		List<CommentViewModel> Search(CommentSearchModel searchModel);

	}
}
