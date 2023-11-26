using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.CourseModels;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            List<ShowCourseForAdminViewModel> courseList = _courseService.GetCoursesForAdmin();
            return View(courseList);
        }

        public IActionResult Create()
        {
            CreateCourseAdminViewModel newUser = new CreateCourseAdminViewModel();

            var groups = _courseService.GetCategoriesForManageCourse();
            newUser.Group = new SelectList(groups, "Value", "Text");

            var subGrous = _courseService.GetSubCategoriesForManageCourse(int.Parse(groups.First().Value));
            newUser.SubGroup = new SelectList(subGrous, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            newUser.Teacher = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            newUser.Level = new SelectList(levels, "Value", "Text");

            var statues = _courseService.GetStatues();
            newUser.Status = new SelectList(statues, "Value", "Text");

            return View(newUser);
        }

        [HttpPost]
        public IActionResult Create(CreateCourseAdminViewModel newCourse ,IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return View(newCourse);

            _courseService.AddCourse(newCourse, imgCourseUp, demoUp);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            CreateCourseAdminViewModel newUser = new CreateCourseAdminViewModel();
            newUser.Course = _courseService.GetCourseForEditByAdmin(id);

            var groups = _courseService.GetCategoriesForManageCourse();
            newUser.Group = new SelectList(groups, "Value", "Text");

            var subGrous = _courseService.GetSubCategoriesForManageCourse(newUser.Course.CategoryId);
            newUser.SubGroup = new SelectList(subGrous, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            newUser.Teacher = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            newUser.Level = new SelectList(levels, "Value", "Text");

            var statues = _courseService.GetStatues();
            newUser.Status = new SelectList(statues, "Value", "Text");

            return View(newUser);
        }

        [HttpPost]
        public IActionResult Edit(CreateCourseAdminViewModel newCourse, IFormFile? imgCourseUp, IFormFile? demoUp)
        {
            if (!ModelState.IsValid)
                return View(newCourse);

            _courseService.UpdateCourse(newCourse, imgCourseUp, demoUp);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetSubCategories(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(_courseService.GetSubCategoriesForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
            //return Json(new SelectList(_courseService.GetSubGroupForManageCourse(id),"Value", "Text"));
        }

        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _courseService.DeleteCourse(id);
            return Json(new { success = true, message = "حذف با موفقیت انجام شد" });
        }
        #endregion
    }
}
