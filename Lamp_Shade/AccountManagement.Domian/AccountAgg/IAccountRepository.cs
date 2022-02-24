using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domian.AccountAgg
{
	public interface IAccountRepository : IRepository<long,Account>
	{
		EditAccount GetDetails(long id);
		Account GetBy(string userName);
		List<AccountViewModel> Search(AccountSearchModel searchModel);
	}
}
