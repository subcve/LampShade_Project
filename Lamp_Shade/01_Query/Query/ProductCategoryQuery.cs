using _01_Query.Contracts.Product;
using _01_Query.Contracts.ProductCategory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore;

namespace _01_Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(c => new ProductCategoryQueryModel
            {
                Id = c.Id,
                Name = c.Name,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Slug = c.Slug,
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(c => new { c.ProductId, c.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts
            .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
            .Select(c => new { c.DiscountRate, c.ProductId }).ToList();

            var result = _context.ProductCategories.Include(c => c.Products).ThenInclude(c => c.Category)
            .Select(c => new ProductCategoryQueryModel
            {
                Id = c.Id,
                Name = c.Name,
                Products = MapProducts(c.Products)
            }).AsNoTracking().ToList();
            foreach (var category in result)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(c => c.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();
                        var productDiscount = discount.FirstOrDefault(c => c.ProductId == product.Id);
                        if (productDiscount != null)
                        {
                            var discountRate = productDiscount.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                }
            }

            //result.ForEach(a=>a.Products.ForEach(x=>x.Price = inventory.FirstOrDefault(c => c.ProductId == x.Id)?.UnitPrice.ToMoney()));
            
            return result;
        }
        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).ToList();
        }
    }
}
