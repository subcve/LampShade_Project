using _01_Query.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems = new List<CartItem>();
        JavaScriptSerializer serializer = new();
        const string cookieName = "cart-items";

        private readonly IProductQuery _productQuery;

		public CartModel(IProductQuery productQuery)
		{
			_productQuery = productQuery;
		}

		public void OnGet()
        {
            var value = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalPrice(item.UnitPrice, item.Count);

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }
        public IActionResult RemoveFromCart(long id)
		{
            var value = Request.Cookies[cookieName];
            Response.Cookies.Delete(cookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = CartItems.FirstOrDefault(c => c.Id == id);
            CartItems.Remove(itemToRemove);
			var cookieOptions = new CookieOptions {  Expires = DateTime.Now.AddDays(1),Path="/",};
            Response.Cookies.Append(cookieName, serializer.Serialize(CartItems),cookieOptions);
            return RedirectToPage("/Cart");
		}
        public IActionResult OnGetGoToCheckOut()
		{
            var value = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalPrice(item.UnitPrice, item.Count);

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
            if(CartItems.Any(c=>!c.IsInStock))
                return RedirectToPage("/Cart");
            return RedirectToPage("/CheckOut");
        }
	}
}
