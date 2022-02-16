using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryViewModel ArticleCategory;
        public List<ArticleCategoryQueryViewModel> ArticleCategories;
        public List<ArticleQueryViewModel> LatestArticles;

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoryModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
		{
			_articleQuery = articleQuery;
			_articleCategoryQuery = articleCategoryQuery;
		}

		public void OnGet(string id)
        {
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategory = _articleCategoryQuery.GetArticleCategory(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }
    }
}
