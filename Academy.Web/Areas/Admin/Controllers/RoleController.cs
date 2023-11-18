using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.IdentityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private IPermissionService _permissionService;
        public RoleController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            List<Role> roleList = _permissionService.GetRoles();
            return View(roleList);
        }

        public IActionResult Create()
        {
            ViewData["Permission"] = _permissionService.GetAllPermission();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Role role, List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            role.IsDelete = false;
            int roleId = _permissionService.AddRole(role);
            _permissionService.AddPermissionsToRole(roleId, SelectedPermission);
            TempData["success"] = $"نقش {role.RoleTitle} با موفقیت ایجاد شد";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewData["Permission"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = _permissionService.PermissionsRole(id);
            Role role = _permissionService.GetRoleById(id);
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(Role role, List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Permission"] = _permissionService.GetAllPermission();
                ViewData["SelectedPermission"] = SelectedPermission;
                return View(role);
            }
            _permissionService.UpdateRole(role);

            _permissionService.UpdatePermissionsRole(role.RoleId, SelectedPermission);

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int roleID)
        {
            Role role = _permissionService.GetRoleById(roleID);
            _permissionService.DeleteRole(role);
            return Json(new { success = true, message = $"نقش {role.RoleTitle} با موفقیت حذف شد." });
        }
        #endregion
    }
}
