using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{

    public class ProductPictureRepository : RepositoryBase<long,ProductPicture> , IProductPictureRepository
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x=>new EditProductPicture
            {
                Id = x.Id,
                Picture =x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(c => c.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(c => c.Product)
                .Select(c => new ProductPictureViewModel
            {
                Id = c.Id,
                Picture = c.Picture,
                Product = c.Product.Name,
                CreationDate = c.CreationDate.ToFarsi(),
                ProductId = c.ProductId,
                IsRemoved = c.IsRemoved
            });

            if (searchModel.ProductId != 0)
                query = query.Where(c => c.ProductId == searchModel.ProductId);

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
