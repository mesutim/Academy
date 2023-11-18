using Academy.Core.Services.Interfaces;
using Academy.DataAccess.Context;
using Academy.Model.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private ApplicationDbContext _context;
        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permissions.ToList();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermissionMap.Add(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }
    }
}
