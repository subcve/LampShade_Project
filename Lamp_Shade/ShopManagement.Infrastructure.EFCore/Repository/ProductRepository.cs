using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(c => new EditProduct
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Slug = c.Slug,
                CategoryId = c.CategoryId,
                Description = c.Description,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Keywords = c.Keywords,
                MetaDescription = c.MetaDescription,
                ShortDescription = c.ShortDescription,

            }).FirstOrDefault(c => c.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products
                .Include(c => c.Category)
                .Select(c => new ProductViewModel
                {
                    Id =c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    Picture = c.Picture,
                    Category = c.Category.Name,
                    CategoryId = c.CategoryId,
                    CreationDate = c.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(c=>c.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(c => c.Code.Contains(searchModel.Code));
            if (searchModel.CategoryId != 0)
                query = query.Where(c => c.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(c => c.Id).ToList();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(c=>new ProductViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}
