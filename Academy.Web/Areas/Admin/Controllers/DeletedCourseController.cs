using Academy.Core.Services.Interfaces;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeletedCourseController : Controller
    {
        private ICourseService _courseService;
        public DeletedCourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            List<ShowCourseForAdminViewModel> courseList = _courseService.GetDeletedCoursesForAdmin();
            return View(courseList);
        }

        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Recover(int id)
        {
            _courseService.RecoverCourse(id);
            return Json(new { success = true, message = "بازیابی دوره با موفقیت انجام شد" });
        }
        #endregion
    }
}
