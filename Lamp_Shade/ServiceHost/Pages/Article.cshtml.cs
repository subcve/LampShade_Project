using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryViewModel Article;
        public List<ArticleQueryViewModel> LatestArticles;
        public List<ArticleCategoryQueryViewModel> ArticleCategories;

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;
		public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
		{
			_articleQuery = articleQuery;
			_articleCategoryQuery = articleCategoryQuery;
			_commentApplication = commentApplication;
		}

		public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }
        public IActionResult OnPost(AddComment command , string articleSlug) 
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
