using Academy.Core.Services.Interfaces;
using Academy.Model.Models.CourseModels;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EpisodeController : Controller
    {
        private ICourseService _courseService;
        public EpisodeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index(int id)
        {
            ViewData["CourseId"] = id;
            List<CourseEpisode> episodes = _courseService.GetListEpisodeCourse(id);
            return View(episodes);
        }

        public IActionResult Create(int id)
        {
            CourseEpisode episode = new CourseEpisode();
            episode.CourseId = id;
            return View(episode);
        }
        [HttpPost]
        public IActionResult Create(CourseEpisode episode, IFormFile fileEpisode)
        {
            if (!ModelState.IsValid || fileEpisode == null)
                return View(episode);


            _courseService.AddEpisode(episode, fileEpisode);

            return Redirect("/Admin/Episode/Index/" + episode.CourseId);
        }

        public IActionResult Edit(int id)
        {
            CourseEpisode episode = _courseService.GetEpisodeById(id);
            return View(episode);
        }
        [HttpPost]
        public IActionResult Edit(CourseEpisode episode, IFormFile? fileEpisode)
        {
            if (!ModelState.IsValid)
                return View(episode);

            _courseService.EditEpisode(episode, fileEpisode);

            return Redirect("/Admin/Episode/Index/" + episode.CourseId);
        }
        #region API CALLS
        //POST
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _courseService.DeleteEpisode(id);
            return Json(new { success = true, message = "حذف با موفقیت انجام شد" });
        }
        #endregion
    }
}
