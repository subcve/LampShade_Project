using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
	{
		[TempData]
		public string LoginMessage { get; set; }

		[TempData]
		public string RegisterMessage { get; set; }
		
		private readonly IAccountApplication _accountApplication;

		public AccountModel(IAccountApplication accountApplication)
		{
			_accountApplication = accountApplication;
		}

		public void OnGet()
		{
		}
		//logining
		public IActionResult OnPostLogin(Login command)
		{
			var result = _accountApplication.Login(command);
			if (result.IsSucceed)
				return RedirectToPage("/Index");

			LoginMessage = result.Message;
			return RedirectToPage("/Account");
		}
		//Registering
		public IActionResult OnPostRegister(RegisterAccount command)
		{
			var result = _accountApplication.Register(command);
			if (result.IsSucceed)
				return RedirectToPage("/Index");

			RegisterMessage = result.Message;
			return RedirectToPage("/Account");
		}
		//LogOuting
		public IActionResult OnGetLogOut()
		{
			_accountApplication.LogOut();

			return RedirectToPage("/Index");

		}
	}
}
