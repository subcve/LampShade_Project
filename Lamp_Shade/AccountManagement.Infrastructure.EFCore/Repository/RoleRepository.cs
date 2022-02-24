using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domian.RoleAgg;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;

		public RoleRepository(AccountContext context) : base(context)
		{
			_context = context;
		}

        public EditRole GetDetails(long id)
        {
            return _context.Roles.Select(c=>new EditRole{Id = c.Id,Name=c.Name})
            .AsNoTracking().FirstOrDefault(c=>c.Id == id);
        }

        public List<RoleViewModel> List()
        {
            return _context.Roles.Select(c=>new RoleViewModel
            {Id = c.Id,Name=c.Name,CreationDate = c.CreationDate.ToFarsi()})
            .AsNoTracking().ToList();
        }
    }
}