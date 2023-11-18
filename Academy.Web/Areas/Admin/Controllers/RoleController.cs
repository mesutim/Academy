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
        [BindProperty]
        public Role Role { get; set; }

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
        public IActionResult Create(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Role.IsDelete = false;
            int roleId = _permissionService.AddRole(Role);
            _permissionService.AddPermissionsToRole(roleId, SelectedPermission);

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int roleID)
        {
            Role role = _permissionService.GetRoleById(roleID);
            _permissionService.DeleteRole(role);
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
