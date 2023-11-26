using Microsoft.AspNetCore.Mvc;
using Academy.Core.Services.Interfaces;

namespace MyAcademy.Web.ViewComponents
{
    public class CourseCategoryViewComponent : ViewComponent
    {
        ICourseService _courseService;

        public CourseCategoryViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseCategory", _courseService.GetAllGroup()));
        }


    }
}
