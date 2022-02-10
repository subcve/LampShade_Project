using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
	public interface IArticleCategoryReposiroty : IRepository<long, ArticleCategory>
	{
		EditArticleCategory GetDetails(long id);
		List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
	}
	 
}
