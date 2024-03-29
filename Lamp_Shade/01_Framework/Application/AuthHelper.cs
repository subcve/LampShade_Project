﻿using System.Security.Claims;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

#nullable disable

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
				return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
			return "";
		}

		public List<int> GetAccountPermissions()
		{
			if (!IsAuthenticated())
				return new List<int>();

			var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "permissions").Value;
			return JsonConvert.DeserializeObject<List<int>>(permissions);
		}

		public bool IsAuthenticated()
		{
			return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
			//var clamis = _contextAccessor.HttpContext.User.Claims.ToList();
			//return clamis.Count > 0;
		}

		public void SignIn(AuthViewModel account)
		{
			var permissions = JsonConvert.SerializeObject(account.Permissions);
			var claims = new List<Claim>
			{
				new Claim("AccountId", account.Id.ToString()),
				new Claim(ClaimTypes.Name, account.Fullname),
				new Claim(ClaimTypes.Role, account.RoleId.ToString()),
				new Claim("Username", account.Username),
				new Claim("Permissions",permissions),
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			
			var expireDate = DateTimeOffset.UtcNow.AddDays(1);
			if (account.RememberMe == true)
				expireDate = DateTimeOffset.UtcNow.AddDays(7);

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = expireDate
			};

		_contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

	public void SignOut()
	{
		_contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	}

		public long GetCurrentAccountId()
		{
			if (IsAuthenticated())
				return long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccountId").Value);
			return 0;
		}
	}
}
