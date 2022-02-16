namespace _01_Query.Contracts.ArticleCategory
{
	public interface IArticleCategoryQuery
	{
		List<ArticleCategoryQueryViewModel> GetArticleCategories();
		ArticleCategoryQueryViewModel GetArticleCategory(string slug);
	}
}
