﻿using Academy.Model.Models.IdentityModels;
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
    }
}