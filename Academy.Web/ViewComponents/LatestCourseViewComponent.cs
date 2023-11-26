using Microsoft.AspNetCore.Mvc;
using Academy.Core.Services.Interfaces;

namespace MyAcademy.Web.ViewComponents
{
    public class LatestCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public LatestCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatestCourse", _courseService.GetLatestCourseList()));
        }
    }
}
