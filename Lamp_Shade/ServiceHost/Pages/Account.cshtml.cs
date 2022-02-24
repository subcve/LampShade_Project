using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData]
		public string Message { get; set; }

        private readonly IAccountApplication _accountApplication;

		public AccountModel(IAccountApplication accountApplication)
		{
			_accountApplication = accountApplication;
		}

		public void OnGet()
        {
        }
        public IActionResult OnPostLogin(Login command)
		{
			var result = _accountApplication.Login(command);
			if (result.IsSucceed)
				return RedirectToPage("/Index");

			Message = result.Message;
			return RedirectToPage("/Account");
		}
		public IActionResult OnGetLogin()
		{
			 _accountApplication.LogOut();

			return RedirectToPage("/Index");

		}
	}
}
