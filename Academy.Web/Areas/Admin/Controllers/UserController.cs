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
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pegeId = 1, int showCount = 5, string filterEmail = "", string filterUserName = "")
        {
            UserForAdminViewModel users = _userService.GetUsers(pegeId, showCount, filterEmail, filterUserName);
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
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
