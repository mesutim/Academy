using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.IdentityModels;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public UserController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public IActionResult Index(int pegeId = 1, int showCount = 5, string filterEmail = "", string filterUserName = "")
        {
            UserForAdminViewModel users = _userService.GetUsers(pegeId, showCount, filterEmail, filterUserName);
            return View(users);
        }

        public IActionResult Create()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel user, List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return View(user);
            int userId = _userService.AddUserFromAdmin(user);

            //Add Roles
            _permissionService.AddRolesToUser(userId, SelectedRoles);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int userId)
        {
            EditUserViewModel user = _userService.GetUserForShowInEditMode(userId);
            ViewData["Roles"] = _permissionService.GetRoles();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel user, List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = SelectedRoles;
                return View(user);
            }

            _userService.EditUserFromAdmin(user);

            //Edit Roles
            _permissionService.EditRolesUser(user.UserId, SelectedRoles);
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int userId)
        {
            _userService.DeleteUser(userId);
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }

}
