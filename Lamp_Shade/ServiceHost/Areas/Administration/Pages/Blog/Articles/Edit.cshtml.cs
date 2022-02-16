using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public EditArticle Command;
        public SelectList ArticleCategories;
		public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
		{
			_articleApplication = articleApplication;
			_articleCategoryApplication = articleCategoryApplication;
		}

		public void OnGet(long id)
        {
            Command = _articleApplication.GetDetails(id);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
        public RedirectToPageResult OnPost(EditArticle command)
		{
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
		}
    }
}
