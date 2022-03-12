using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(c => new EditColleagueDiscount
            {
                Id = c.Id,
                ProductId = c.ProductId,
                DiscountRate = c.DiscountRate
            }).FirstOrDefault(z => z.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(c => new { c.Id, c.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(c => new ColleagueDiscountViewModel
            {
                Id = c.Id,
                DiscountRate = c.DiscountRate,
                CreationDate = c.CreationDate.ToFarsi(),
                ProductId = c.ProductId,
                IsRemoved = c.IsRemoved
             });

            if (searchModel.ProductId > 0)
                query = query.Where(c => c.ProductId == searchModel.ProductId);

            var discount = query.OrderByDescending(c => c.Id).ToList();
            discount.ForEach(z =>
            z.Product = products.FirstOrDefault(c => c.Id == z.ProductId)?.Name);

            return discount;
        }
    }
}
