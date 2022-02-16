namespace _01_Query.Contracts.Article
{
	public interface IArticleQuery 
	{
		ArticleQueryViewModel GetArticleDetails(string slug);
		List<ArticleQueryViewModel> GetLatestArticles();
	}
}
