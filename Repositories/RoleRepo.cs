using DeckMaster.Data;
using DeckMaster.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeckMaster.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            this._context = context;
            CreateInitialRole();
        }

        public IEnumerable<RoleVM> GetAllRoles()
        {
            return _context.Roles.Select(r => new RoleVM { RoleName = r.Name });
        }

        public RoleVM GetRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);

            return role != null ? new RoleVM { RoleName = role.Name } : null;
        }

        public bool CreateRole(string roleName)
        {
            var normalizedRoleName = roleName.ToUpper();

            if (_context.Roles.Any(r => r.NormalizedName == normalizedRoleName))
            {
                return false; // Role already exists
            }

            _context.Roles.Add(new IdentityRole
            {
                Id = normalizedRoleName,
                Name = roleName,
                NormalizedName = normalizedRoleName
            });

            _context.SaveChanges();

            return true;
        }

        public SelectList GetRoleSelectList()
        {
            return new SelectList(GetAllRoles().Select(r => new SelectListItem
            {
                Value = r.RoleName,
                Text = r.RoleName
            }), "Value", "Text");
        }

        public void CreateInitialRole()
        {
            const string ADMIN = "Admin";

            if (GetRole(ADMIN) == null)
            {
                CreateRole(ADMIN);
            }
        }

        public bool DeleteRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r =>r.Name == roleName);

            if (role == null)
            {
                return false; // Role not found
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return true;
        }

        public bool IsRoleAssigned(string roleName)
        {
            return _context.UserRoles.Any(ur => ur.RoleId == roleName);
        }
    }
}
