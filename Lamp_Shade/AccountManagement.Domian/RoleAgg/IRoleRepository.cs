using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domian.RoleAgg
{
    public interface IRoleRepository : IRepository<long,Role>
    {
         List<RoleViewModel> List();
		EditRole GetDetails(long id);
    }
}