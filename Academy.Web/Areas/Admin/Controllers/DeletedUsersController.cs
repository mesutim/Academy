using Academy.Core.Services.Interfaces;
using Academy.Model.Models.IdentityModels;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeletedUsersController : Controller
    {
        private IUserService _userService;
        public DeletedUsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pegeId = 1, int showCount = 5, string filterEmail = "", string filterUserName = "")
        {
            DeleteUsersForAdminViewModel users = _userService.GetDeletedUsers(pegeId, showCount, filterEmail, filterUserName);
            return View(users);
        }

        public IActionResult RecoveryUser(int id)
        {
            DeletedUserInformationViewModel deletedUser= _userService.GetDeleteUserInformation(id);
            return View(deletedUser);
        }

        [HttpPost, ActionName("RecoveryUser")]
        public IActionResult RecoveryUserPOST(int UserId)
        {
            _userService.RecoverUser(UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
