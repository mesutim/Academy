﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.Services.Interfaces;
using Academy.Model.ViewModels;

namespace MyAcademy.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    //[PermissionChecker(12)] //پنل مدرس
    public class MasterController : Controller
    {
        #region Ctor
        private ICourseService _courseService;
        private IUserService _userService;
        public MasterController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        #endregion

        [HttpGet("master-courses")]
        public IActionResult MasterCoursesList()
        {
            var courses = _courseService.GetAllMasterCourses(User.Identity.Name);

            return View(courses);
        }

        [Route("UserCash-Courses")]
        public IActionResult UserCashCoursesList()
        {
            return View(_courseService.GetAllCashCoursesForUser(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [Route("UserFree-Courses")]
        public IActionResult UserFreeCoursesList()
        {
            return View(_courseService.GetAllFreeCoursesForUser(User.Identity.Name));
        }

        [HttpGet("course-episodes/{courseId}")]
        public IActionResult EpisodesList(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            var userId = _userService.GetUserIdByUserName(User.Identity.Name);

            if (course.TeacherId != userId)
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }

            var episodes = _courseService.GetCourseEpisodesByCourseId(courseId);

            ViewBag.CourseId = courseId;

            return View(episodes);
        }

        [HttpGet("add-episode/{courseId}")]
        public IActionResult AddEpisode(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            var userId = _userService.GetUserIdByUserName(User.Identity.Name);

            if (course.TeacherId != userId)
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }

            var result = new AddEpisodeViewModel
            {
                CourseId = courseId,
                IsFree = true
            };

            return View(result);
        }

        [HttpPost("add-episode/{courseId}")]
        public IActionResult AddEpisode(AddEpisodeViewModel episodeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(episodeViewModel);
            }

            if (string.IsNullOrEmpty(episodeViewModel.EpisodeFileName))
            {
                return View(episodeViewModel);
            }

            var result = _courseService.AddEpisode(episodeViewModel, User.Identity.Name);

            if (result)
            {
                return RedirectToAction("EpisodesList", "Master", new {courseId = episodeViewModel.CourseId});
            }

            return View(episodeViewModel);
        }

        public IActionResult DropzoneTarget(List<IFormFile> files, int courseId)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = $"{courseId}-{Guid.NewGuid().ToString()}" + Path.GetExtension(file.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var finalPath = path + fileName;

                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new JsonResult(new {data = fileName, status = "Success"});
                }
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}
