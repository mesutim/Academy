using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseCategoryController : Controller
    {
        private ICourseService _courseService;
        public CourseCategoryController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            List<Category> categories = _courseService.GetAllCategories();
            return View(categories);
        }

        public IActionResult Create(int? id)
        {
            Category newCategory = new Category()
            {
                ParentId = id
            };
            return View(newCategory);
        }
        
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (!ModelState.IsValid)
                return View();

            _courseService.AddCategory(newCategory);
            TempData["success"] = $"دسته {newCategory.CategoryTitle} با موفقیت ایجاد شد";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Category newCategory = _courseService.GetCategoryById(id);
            return View(newCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            _courseService.UpdateCategory(category);
            TempData["success"] = $"دسته {category.CategoryTitle} با موفقیت ویرایش شد";
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int categoryId)
        {
            _courseService.DeleteCategoryById(categoryId);
            return Json(new { success = true, message = "دسته مورد نظر با موفقیت حذف شد." });
        }
        #endregion
    }
}
