using Academy.Model.Models.IdentityModels;
using Academy.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetRoles();

        Role GetRoleById(int roleId);

        void DeleteRole(Role role);

        List<Permission> GetAllPermission();

        int AddRole(Role role);

        void AddPermissionsToRole(int roleId, List<int> permission);

        List<int> PermissionsRole(int roleId);

        public void UpdateRole(Role role);

        void UpdatePermissionsRole(int roleId, List<int> permissions);

        void AddRolesToUser(int userId, List<int> roleIds);

        public void EditRolesUser(int userId, List<int> rolesId);
        bool CheckPermission(int permissionId, string userName);
    }
}
