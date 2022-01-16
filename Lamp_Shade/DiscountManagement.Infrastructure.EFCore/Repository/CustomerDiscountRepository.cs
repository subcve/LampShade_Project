using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts.Select(c => new EditCustomerDiscount
            {
                Id = c.Id,
                ProductId = c.ProductId,
                DiscountRate = c.DiscountRate,
                StartDate = c.StartDate.ToFarsi(),
                EndDate = c.EndDate.ToFarsi(),
                Reason = c.Reason
            }).FirstOrDefault(c => c.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(c => new { c.Id, c.Name }).ToList();
            var query = _discountContext.CustomerDiscounts.Select(c => new CustomerDiscountViewModel
            {
                Id = c.Id,
                ProductId = c.ProductId,
                DiscountRate = c.DiscountRate,
                StartDate = c.StartDate.ToFarsi(),
                StartDateGR = c.StartDate,
                EndDate = c.EndDate.ToFarsi(),
                EndDateGR = c.EndDate,
                Reason = c.Reason,
                CreationDate = c.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(c => c.ProductId == searchModel.ProductId);

            if (!string.IsNullOrEmpty(searchModel.StartDate))
            {
                query = query.Where(c => c.StartDateGR > searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrEmpty(searchModel.EndDate))
            {
                query = query.Where(c => c.EndDateGR < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(c => c.Id).ToList();

            discounts.ForEach(z =>
            z.Product = products.FirstOrDefault(c => c.Id  == z.ProductId)?.Name);

            return discounts;
        }
    }
}
