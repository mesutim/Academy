using Academy.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyAcademy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        ApplicationDbContext context;

        public CourseApiController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Produces("application/json")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle = context.Courses
                    .Include(c => c.CourseCategories)
                    .Where(c => c.CourseTitle.Contains(term))
                    .Select(c => c.CourseTitle
                    )
                    .ToList();
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
