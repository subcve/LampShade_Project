using _01_Framework.Application;
using _01_Framework.Infrastructure;
using _01_Query.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_Query.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {

            _discountContext = discountContext;
            _authHelper = authHelper;
        }
        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var CurrentAccountRole = _authHelper.GetCurrentAccountRole();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(c => new { c.ProductId, c.DiscountRate }).ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(c=>c.StartDate < DateTime.Now &&c.EndDate > DateTime.Now)
                .Select(c => new { c.ProductId, c.DiscountRate }).ToList();

            foreach (var item in cartItems)
            {
                if (CurrentAccountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(c => c.ProductId == item.Id);
                    if (colleagueDiscount != null) 
                        item.DiscountRate = colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(c => c.ProductId == item.Id);
                    if (customerDiscount != null)
                        item.DiscountRate = customerDiscount.DiscountRate;
                }
                item.DiscountAmount = ((item.TotalPrice * item.DiscountRate) / 100);
                item.ItemPayAmout = (item.TotalPrice - item.DiscountAmount);
                cart.Add(item);
            }
            return cart;
        }
    }
}
