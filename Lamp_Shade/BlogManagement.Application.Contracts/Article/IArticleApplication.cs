using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.Article
{
	public interface IArticleApplication
	{
		OperationResult Create(CreateArticle command);
		OperationResult Edit(EditArticle command);
		EditArticle GetDetails(long id);
		List<ArticleViewModel> Search(ArticleSearchModel searchModel);
	}
}
