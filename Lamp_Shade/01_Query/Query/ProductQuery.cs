using _0_Framework.Application;
using _01_Query.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }
        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(c => new { c.ProductId, c.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts
            .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
            .Select(c => new { c.DiscountRate, c.ProductId }).ToList();
            
            var products = _context.Products.Select(product=>new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).OrderByDescending(c=>c.Id).Take(6).AsNoTracking().ToList();

            foreach (var product in products)
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
            return products;
            
        }
    }
}