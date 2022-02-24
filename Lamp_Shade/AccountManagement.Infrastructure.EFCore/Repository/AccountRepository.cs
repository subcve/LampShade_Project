using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domian.AccountAgg;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccountManagement.Infrastructure.EFCore.Repository
{
	public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
	{
		private readonly AccountContext _context;

		public AccountRepository(AccountContext context) : base(context)
		{
			_context = context;
		}

		public Account GetBy(string userName)
		{
			return _context.Accounts.AsNoTracking().FirstOrDefault(c => c.UserName == userName);
		}

		public EditAccount GetDetails(long id)
		{
			return _context.Accounts.Select(c => new EditAccount
			{
				Id = c.Id,
				Fullname = c.Fullname,
				UserName = c.UserName,
				Mobile = c.Mobile,
				RoleId = c.RoleId
			}).AsNoTracking().FirstOrDefault(x => x.Id == id);
		}

		public List<AccountViewModel> Search(AccountSearchModel searchModel)
		{
			var query = _context.Accounts.Include(c=>c.Role).Select(c => new AccountViewModel
			{
				Id = c.Id,
				Fullname = c.Fullname,
				UserName = c.UserName,
				Mobile = c.Mobile,
				ProfilePhoto = c.ProfilePhoto,
				Role = c.Role.Name,
				RoleId = c.RoleId,
				CreationDate = c.CreationDate.ToFarsi()
			}).AsNoTracking();

			if (!string.IsNullOrWhiteSpace(searchModel.FullName))
				query = query.Where(c => c.Fullname == searchModel.FullName);

			if (!string.IsNullOrWhiteSpace(searchModel.UserName))
				query = query.Where(c => c.UserName == searchModel.UserName);

			if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
				query = query.Where(c => c.Mobile == searchModel.Mobile);
			
			if (searchModel.RoleId > 0)
				query = query.Where(c => c.RoleId == searchModel.RoleId);

			return query.OrderByDescending(x => x.Id).ToList();
		}
	}
}