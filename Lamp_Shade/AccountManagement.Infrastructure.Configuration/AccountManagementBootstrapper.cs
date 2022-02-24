using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domian.AccountAgg;
using AccountManagement.Domian.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
	public class AccountManagementBootstrapper
	{
		public static void Configure(IServiceCollection services , string connectionString)
		{
			services.AddTransient<IAccountApplication, AccountApplication>();
			services.AddTransient<IAccountRepository, AccountRepository>();
			services.AddTransient<IRoleApplication, RoleApplication>();
			services.AddTransient<IRoleRepository, RoleRepository>();

			services.AddDbContext<AccountContext>(c 
				=> c.UseSqlServer(connectionString));
		}
	}
}