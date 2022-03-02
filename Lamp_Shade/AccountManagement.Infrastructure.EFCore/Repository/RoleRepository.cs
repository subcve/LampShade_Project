using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_Framework.Infrastructure;
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
            var role = _context.Roles.Select(c => new EditRole
            {
                Id = c.Id,
                Name = c.Name,
                MappedPermissions = MapPermissoins(c.Permissions),
            }).AsNoTracking().FirstOrDefault(c=>c.Id == id);

            role.Permissions = role.MappedPermissions.Select(c => c.Code).ToList();

            return role;
        }

		private static List<PermissionDto> MapPermissoins(List<Permission> permissions)
		{
			return permissions.Select(c=> new PermissionDto(c.Code,c.Name)).ToList();
		}

		public List<RoleViewModel> List()
        {
            return _context.Roles.Select(c=>new RoleViewModel
            {Id = c.Id,Name=c.Name,CreationDate = c.CreationDate.ToFarsi()})
            .AsNoTracking().ToList();
        }
    }
}