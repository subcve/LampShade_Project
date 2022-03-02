using _01_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account.Role
{
    public class EditModel : PageModel
    {
		private readonly IRoleApplication _roleApplication;
		private readonly IEnumerable<IPermissionExposer> _exposers;
		public EditRole Command;
		public List<SelectListItem> Permissions = new List<SelectListItem>();
		public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
		{
			_roleApplication = roleApplication;
			_exposers = exposers;
		}

		public void OnGet(long id)
		{
			Command = _roleApplication.GetDetails(id);

			var permissions = new List<PermissionDto>();
			foreach (var exposer in _exposers)
			{
				var exposendPermissions = exposer.Expose();
				foreach (var (key, value) in exposendPermissions)
				{
					permissions.AddRange(value);
					var group = new SelectListGroup
					{
						Name = key,
					};
					foreach (var permission in value)
					{
						var item = new SelectListItem(permission.Name,permission.Code.ToString())
						{
							Group = group,
						};
						if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
							item.Selected = true;
						Permissions.Add(item);
					}
				}
			}
		}

		public IActionResult OnPost(EditRole command)
		{
			var result = _roleApplication.Edit(command);
			return RedirectToPage("Index");
		}
	}
}
