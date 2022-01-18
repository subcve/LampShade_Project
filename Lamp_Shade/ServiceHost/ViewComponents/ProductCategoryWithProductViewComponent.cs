using _01_Query.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productQueryModel;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productQueryModel)
        {
            _productQueryModel = productQueryModel;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productQueryModel.GetProductCategoriesWithProducts();
            return View(model);
        }
    }
}