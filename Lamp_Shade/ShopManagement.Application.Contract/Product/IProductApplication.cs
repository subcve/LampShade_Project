using System.Collections;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
    }
}
