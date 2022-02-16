using _01_Query;
using _01_Query.Contracts.ArticleCategory;
using _01_Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
		public MenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
		{
			_productCategoryQuery = productCategoryQuery;
			_articleCategoryQuery = articleCategoryQuery;
		}

		public IViewComponentResult Invoke()
        {
            var model = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View(model);
        }
    }
}