using _0_Framework.Application;
using _01_Query.Contracts.Comments;
using _01_Query.Contracts.Product;
using _01_Query.Contracts.ProductPicture;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductPictureAgg;
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

		public ProductQueryModel GetDetails(string slug)
		{
            var inventory = _inventoryContext.Inventory.Select(c => new { c.ProductId, c.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts
            .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
            .Select(c => new { c.DiscountRate, c.ProductId , c.EndDate}).ToList();

            var product = _context.Products
                .Include(c => c.Category)
                .Include(c => c.ProductPictures)
                .Include(c=>c.Comments)
                .Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                CategorySlug = product.Category.Slug,
                Code = product.Code,
                Description = product.Description,
                Keywords = product.Keywords,
                MetaDescription = product.MetaDescription,
                ShortDescription = product.ShortDescription,
                Pictures = MapProductPictures(product.ProductPictures),
                Comments = MapComments(product.Comments)
            }).FirstOrDefault(c => c.Slug == slug);

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
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
            }
            return product;
        }

		private static List<CommentQueryModel> MapComments(List<Comment> comments)
		{
            return comments.Where(x=>!x.IsCanceled && x.IsConfirmed).Select(c => new CommentQueryModel
            {
                Id = c.Id,
                Message = c.Message,
                Name = c.Name,
                ProductId = c.ProductId
            }).ToList();
		}

		private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> pictures)
		{
            return pictures.Select(c => new ProductPictureQueryModel
            {
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                ProductId = c.ProductId,
                IsRemoved = c.IsRemoved
            }).Where(x => !x.IsRemoved).ToList();
		}

		public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(c => new { c.ProductId, c.UnitPrice , c.InStock}).ToList();
            var discount = _discountContext.CustomerDiscounts
            .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
            .Select(c => new { c.DiscountRate, c.ProductId }).ToList();
            
            var products = _context.Products.Include(c=>c.Category).Select(product=>new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                CategorySlug = product.Category.Slug
            }).OrderByDescending(c=>c.Id).Take(6).AsNoTracking().ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(c => c.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    product.InStock = productInventory.InStock;
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

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(c => new { c.ProductId, c.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts
            .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
            .Select(c => new { c.DiscountRate, c.ProductId }).ToList();
            
            var query = _context.Products
            .Include(c=>c.Category)
            .Select(product=>new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                ShortDescription = product.ShortDescription,
                CategorySlug = product.Category.Slug
            }).AsNoTracking();

            if(!string.IsNullOrWhiteSpace(value))
                query = query.Where(c=>c.Name.Contains(value) || c.ShortDescription.Contains(value));

            var products = query.OrderByDescending(c=>c.Id).ToList();

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