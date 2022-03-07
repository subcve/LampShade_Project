using _01_Query.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart? Cart;
        JavaScriptSerializer serializer = new();
        const string cookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckOutModel(ICartCalculatorService cartCalculatorService)
        {
            _cartCalculatorService = cartCalculatorService;
        }

        public void OnGet()
        {
            var value = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalPrice(item.UnitPrice, item.Count);
            Cart = _cartCalculatorService.ComputeCart(cartItems);
        }
    }
}
