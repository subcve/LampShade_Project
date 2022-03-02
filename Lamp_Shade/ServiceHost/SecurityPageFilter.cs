using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace ServiceHost
{
	public class SecurityPageFilter : IPageFilter
	{
		private readonly IAuthHelper _authHelper;

		public SecurityPageFilter(IAuthHelper authHelper)
		{
			_authHelper = authHelper;
		}

		public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
		{
		}

		public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
		{
			var handlerPermission = context.HandlerMethod
				.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute)) as NeedsPermissionAttribute;

			if (handlerPermission == null) return;

			var accountPermissions = _authHelper.GetAccountPermissions();

			if (!accountPermissions.Contains(handlerPermission.Permission))
				context.HttpContext.Response.Redirect("/Account");

		}

		public void OnPageHandlerSelected(PageHandlerSelectedContext context)
		{
		}
	}
}
