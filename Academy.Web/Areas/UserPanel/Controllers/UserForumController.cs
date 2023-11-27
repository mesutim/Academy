using Academy.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAcademy.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserForumController : Controller
    {
        IForumService _forumService;

        public UserForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index(int userId, string filter = "")
        {
            ViewBag.UserId = userId;
           
            return View(_forumService.GetQuestionsForMaster(userId, filter));
        }
    }
}
