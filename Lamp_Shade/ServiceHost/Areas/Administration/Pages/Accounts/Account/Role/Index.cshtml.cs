using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account.Role
{
    public class IndexModel : PageModel
    {
		[TempData]
		public string Message { get; set; }
		public List<RoleViewModel> Roles;

		private readonly IRoleApplication _roleApplication;
		public IndexModel(IRoleApplication roleApplication)
		{
			_roleApplication = roleApplication;
		}
		public void OnGet()
		{
			Roles = _roleApplication.List();
		}
		public IActionResult OnGetCreate()
		{
			var command = new CreateRole
			{
			};
			return Partial("./Create", command);
		}

		public JsonResult OnPostCreate(CreateRole command)
		{
			var result = _roleApplication.Create(command);
			return new JsonResult(result);
		}

		public IActionResult OnGetEdit(long id)
		{
			var account = _roleApplication.GetDetails(id);
			return Partial("Edit", account);
		}

		public JsonResult OnPostEdit(EditRole command)
		{
			var result = _roleApplication.Edit(command);
			return new JsonResult(result);
		}
	}
}
