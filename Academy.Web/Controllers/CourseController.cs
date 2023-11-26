using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using Academy.Core.Convertors;
using Academy.Core.Services.Interfaces;
using Academy.Model.Models.CourseModels;

namespace Academy.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IOrderService _orderService;
        private IUserService _userService;
        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0,int? selectedCategory = null)
        {
            ViewBag.pageId=pageId;
            ViewData["SelectedCategory"] = selectedCategory;
            ViewData["Groups"] = _courseService.GetAllGroup();
            return View(_courseService.GetCourse(pageId,filter,getType,orderByType,startPrice,endPrice, selectedCategory, 1));
        }

        [Route("ShowCourse/{id}/{title}")]
        public IActionResult ShowCourse(int id,string title, int episodeId = 0)
        {
            var course = _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();

            }

            if (episodeId != 0 && User.Identity.IsAuthenticated)
            {
                if (course.CourseEpisodes.All(e => e.EpisodeId != episodeId))
                {
                    return NotFound();
                }

                if (!course.CourseEpisodes.First(e => e.EpisodeId == episodeId).IsFree)
                {
                    if (!_orderService.IsUserInCourse(User.Identity.Name, id))
                    {
                        return NotFound();
                    }
                }

                var ep = course.CourseEpisodes.First(e => e.EpisodeId == episodeId);
                ViewBag.Episode = ep;
                string filePath = "";
                string checkFilePath = "";
                if (ep.IsFree)
                {
                    filePath = "/courseOnline/" + ep.EpisodeFileName.Replace(".rar", ".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseOnline",
                        ep.EpisodeFileName.Replace(".rar", ".mp4"));
                }
                else
                {
                    filePath = "/CourseFilesOnline/" + ep.EpisodeFileName.Replace(".rar", ".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFilesOnline",
                        ep.EpisodeFileName.Replace(".rar", ".mp4"));
                }


                if (!System.IO.File.Exists(checkFilePath))
                {
                    string targetPath = Directory.GetCurrentDirectory();
                    if (ep.IsFree)
                    {
                        targetPath = System.IO.Path.Combine(targetPath, "wwwroot/courseOnline");
                    }
                    else
                    {
                        targetPath = System.IO.Path.Combine(targetPath, "wwwroot/CourseFilesOnline");
                    }

                    string rarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                        ep.EpisodeFileName);
                    var archive = ArchiveFactory.Open(rarPath);

                    var Entries = archive.Entries.OrderBy(x => x.Key.Length);
                    foreach (var en in Entries)
                    {
                        if (Path.GetExtension(en.Key) == ".mp4")
                        {
                            en.WriteTo(System.IO.File.Create(Path.Combine(targetPath, ep.EpisodeFileName.Replace(".rar", ".mp4"))));
                        }
                    }
                }

                ViewBag.filePath = filePath;
            }





            return View(course);
        }

        [Route("c/{key}")]
        public IActionResult ShortKeyRedirect(string key)
        {
            var course = _courseService.GetCourseByShortKey(key);

            if (course == null)
            {
                return NotFound();
            }
            Uri uri = new Uri("https://localhost:7287" + "/ShowCourse/" + course.CourseId + "/" + course.CourseTitle.FixText());
            return Redirect(uri.AbsoluteUri);
        }
       
        [Authorize]
        public ActionResult BuyCourse(int id)
        {
            _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/ShowCourse/" + id);
        }

        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);
            string filepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;

            if (episode.IsFree)
            {
                byte[] file=System.IO.File.ReadAllBytes(filepath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserInCourse(User.Identity.Name,episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    return File(file, "application/force-download", fileName);
                }
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult CreateComment(CourseComment comment)
        {
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseService.AddComment(comment);

            return View("ShowComment", _courseService.GetCourseComment(comment.CourseId));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_courseService.GetCourseComment(id, pageId));
        }

        public IActionResult CourseVote(int Id)
        {
            if (!_courseService.IsFree(Id))
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name, Id))
                {
                    ViewBag.NotAccess = true;
                }
            }
            return PartialView(_courseService.GetCourseVotes(Id));
        }

        [Authorize]
        public IActionResult AddVote(int id, bool vote)
        {
            _courseService.AddsVote(_userService.GetUserIdByUserName(User.Identity.Name), id, vote);

            return PartialView("CourseVote", _courseService.GetCourseVotes(id));
        }
    }
}
