using System.Security.Claims;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _01_Framework.Application
{
	public class AuthHelper : IAuthHelper
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public AuthHelper(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public AuthViewModel GetCurrentAccountInfo()
		{
			AuthViewModel result = new();

			if (IsAuthenticated())
			{
				result.Id = long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccountId").Value);
				result.RoleId = long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
				result.Fullname = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
				result.Username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Username").Value;
				result.Role = Roles.GetRoleBy(result.RoleId);
			}

			return result;
		}

		public string GetCurrentAccountRole()
		{
			if (IsAuthenticated())
				return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Role).Value;
			return "";
		}

		public bool IsAuthenticated()
		{
			var clamis = _contextAccessor.HttpContext.User.Claims.ToList();
			return clamis.Count > 0;
		}

		public void SignIn(AuthViewModel account)
		{
			var claims = new List<Claim>
			{
				new Claim("AccountId", account.Id.ToString()),
				new Claim(ClaimTypes.Name, account.Fullname),
				new Claim(ClaimTypes.Role, account.RoleId.ToString()),
				new Claim("Username", account.Username)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
			};

			_contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

		public void SignOut()
		{
			_contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
