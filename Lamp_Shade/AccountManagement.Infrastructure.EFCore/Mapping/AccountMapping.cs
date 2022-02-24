using AccountManagement.Domian.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
	public class AccountMapping : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToTable("Accounts");
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Fullname).HasMaxLength(100);
			builder.Property(c => c.UserName).HasMaxLength(100);
			builder.Property(c => c.Mobile).HasMaxLength(20);
			builder.Property(c => c.Password).HasMaxLength(1000);
			builder.Property(c => c.ProfilePhoto).HasMaxLength(500);

			builder.HasOne(c=>c.Role)
				.WithMany(c=>c.Accounts)
				.HasForeignKey(c=>c.RoleId);
		}
	}
}
