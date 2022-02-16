using _01_Query.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

		public LatestArticlesViewComponent(IArticleQuery articleQuery)
		{
			_articleQuery = articleQuery;
		}

		public IViewComponentResult Invoke() 
        {
            var model = _articleQuery.GetLatestArticles();
            return View(model);
        }
    }
}
