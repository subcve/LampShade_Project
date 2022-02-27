using _0_Framework.Application;
using _01_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
	public class RegisterAccount
	{
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(100,ErrorMessage = ValidationMessages.MaxLength)]
		public string Fullname { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
		public string UserName { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MinLength(8, ErrorMessage = "تعداد کارکتر وارد شده باید حداقل 8  حرف باشد")]
		public string Password { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(20, ErrorMessage = ValidationMessages.MaxLength)]
		public string Mobile { get; set; }
		public long RoleId { get; set; }
		[MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
		public IFormFile ProfilePhoto { get; set; }
		public List<RoleViewModel> Roles { get; set; }
	}
}
