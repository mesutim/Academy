using Microsoft.AspNetCore.Mvc;
using Academy.Core.Services.Interfaces;

namespace MyAcademy.Web.ViewComponents
{
    public class PopularCourseViewComponent: ViewComponent
    {
        ICourseService _courseService;

        public PopularCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("PopularCourse", _courseService.GetPopularCourseList()));
        }
    }
}
