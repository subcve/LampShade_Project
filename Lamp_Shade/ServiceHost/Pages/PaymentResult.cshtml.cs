using _01_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
	public class PaymentResultModel : PageModel
    {
		public PaymentResultService? Result { get; set; }
		public void OnGet(PaymentResultService result)
        {
            Result = result;
        }
    }
}
