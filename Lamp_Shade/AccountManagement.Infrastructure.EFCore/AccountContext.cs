using AccountManagement.Domian.AccountAgg;
using AccountManagement.Domian.RoleAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
	public class AccountContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Role> Roles { get; set; }
		public AccountContext(DbContextOptions<AccountContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assembly = typeof(AccountMapping).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
