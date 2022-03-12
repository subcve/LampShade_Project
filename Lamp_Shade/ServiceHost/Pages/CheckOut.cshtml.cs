using _01_Framework.Application;
using _01_Framework.Application.ZarinPal;
using _01_Query.Contracts;
using _01_Query.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart? Cart;
        JavaScriptSerializer serializer = new();
        const string cookieName = "cart-items";
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;
		public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
		{
			_cartCalculatorService = cartCalculatorService;
			_cartService = cartService;
			_productQuery = productQuery;
			_orderApplication = orderApplication;
			_zarinPalFactory = zarinPalFactory;
			_authHelper = authHelper;
		}

		public void OnGet()
        {
            var value = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalPrice(item.UnitPrice, item.Count);
            Cart = _cartCalculatorService.ComputeCart(cartItems);

            _cartService.Set(Cart);
        }
        public IActionResult OnPostPay(int methodId)
		{
            var cart = _cartService.Get();
            cart.SetPaymentMethod(methodId);
            var res = _productQuery.CheckInventoryStatus(cart.Items);
            if (res.Any(c =>!c.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);
            if(cart.PaymentMethod == 1) //onlinePay
			{

                var userName = _authHelper.GetCurrentAccountInfo().Username;
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "", "",
                    "خرید از درگاه پرداخت سایت لوازم دکوری", orderId);

                return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
			}
			else
			{
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد",issueTrackingNo:""));
			}
		}
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
		{
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());
            
            var result = new PaymentResultService();
            if(status == "OK" && verificationResponse.Status >= 100)
			{
                var issueTrackingNo = _orderApplication.PaymentSucceed(oId, verificationResponse.RefID);
                Response.Cookies.Delete(cookieName);
                result = result.Succeed("پرداخت شما با موفقیت انجام شد", issueTrackingNo);
                return RedirectToPage("/PaymentResult",result);
			}
			else
			{
                result = result.Failed("پرداخت با شکست مواجه شد ! درصورت کسر وجه از حساب , مبلغ تا 24 ساعت بعد عودت داده خواهد شد");
                return RedirectToPage("/PaymentResult", result);
            }
		}
    }
}
