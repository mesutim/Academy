using Academy.Core.Convertors;
using Academy.Core.Generator;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;
using Academy.DataAccess.Context;
using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Services
{
    public class CourseService : ICourseService
    {
        private ApplicationDbContext _context;
        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public void AddCategory(Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
        }
        public Category GetCategoryById(int catId)
        {
            return _context.Categories.Find(catId);
        }
        public void UpdateCategory(Category cat)
        {
            _context.Categories.Update(cat);
            _context.SaveChanges();
        }
        public void DeleteCategoryById(int id)
        {
            Category cat = _context.Categories.Find(id);
            cat.IsDelete = true;
            _context.SaveChanges();
        }
        public List<ShowCourseForAdminViewModel> GetCoursesForAdmin()
        {
            return _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToList();
        }
        public List<SelectListItem> GetCategoriesForManageCourse()
        {
            return _context.Categories.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.CategoryTitle,
                    Value = g.CategoryId.ToString()
                }).ToList();
        }
        public List<SelectListItem> GetSubCategoriesForManageCourse(int groupId)
        {
            return _context.Categories.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.CategoryTitle,
                    Value = g.CategoryId.ToString()
                }).ToList();
        }
        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoleMap.Where(r => r.RoleId == 14).Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToList();
        }
        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();

        }
        public List<SelectListItem> GetStatues()
        {
            return _context.CourseStatus.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }
        public int AddCourse(CreateCourseAdminViewModel newCourse, IFormFile imgCourse, IFormFile courseDemo)
        {
            Course course = new Course();
            course.CourseTitle = newCourse.Course.Title;
            course.TeacherId = newCourse.Course.TeacherId;
            course.CourseDescription = newCourse.Course.Description;
            course.CoursePrice = newCourse.Course.Price;
            course.Tags = newCourse.Course.Tags;
            course.LevelId = newCourse.Course.LevelId;
            course.StatusId = newCourse.Course.StatusId;
            course.CreateDate = DateTime.Now;
            course.ShortKey = GenerateShortKey();
            course.CourseImageName = "no-photo.jpg";

            //TODO Check Image
            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Image/", course.CourseImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }
                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Thumb/", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            //ToDO Upload Demo 
            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Demo/", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            


            _context.Add(course);
            _context.SaveChanges();

            _context.courseCategoryMap.Add(new CourseCategory()
            {
                CourseId = course.CourseId,
                CategoryId = newCourse.Course.CategoryId
            });

            if (newCourse.Course.SubCategoryId != null)
            {
                _context.courseCategoryMap.Add(new CourseCategory()
                {
                    CourseId = course.CourseId,
                    CategoryId = newCourse.Course.SubCategoryId
                });
            }
            _context.SaveChanges();

            return course.CourseId;
        }
        private string GenerateShortKey(int lenght = 5)
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);

            while (_context.Courses.Any(c => c.ShortKey == key))
            {
                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);
            }

            return key;
        }
        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }
        public CreateCourseViewModel GetCourseForEditByAdmin(int courseId)
        {
            Course course = _context.Courses.Find(courseId);
            List<CourseCategory> categories = _context.courseCategoryMap.Where(c=>c.CourseId==courseId).Include(c => c.Category).ToList();
            CreateCourseViewModel editedCourse = new CreateCourseViewModel()
            {
                Id = courseId,
                Title = course.CourseTitle,
                Description = course.CourseDescription,
                TeacherId = course.TeacherId,
                LevelId = course.LevelId,
                StatusId = course.StatusId,
                Price = course.CoursePrice,
                Image = course.CourseImageName,
                Demo = course.DemoFileName,
                Tags = course.Tags,
                CategoryId = categories.Where(c => c.Category.ParentId == null).FirstOrDefault().CategoryId,
                SubCategoryId = categories.Where(c => c.Category.ParentId != null).FirstOrDefault().CategoryId,
            };
            return editedCourse;
        }
        public void DeleteCourse(int id)
        {
            Course course = _context.Courses.Find(id);
            course.IsDelete = true;
            _context.SaveChanges();
        }
        public List<CourseEpisode> GetListEpisodeCourse(int courseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }
        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {

            episode.EpisodeFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(episodeFile.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Episode/", episode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }
            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
            return episode.EpisodeId;
        }
        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }
        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Episode/", episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(episodeFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Episode/", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }
        public void DeleteEpisode(int id)
        {
            CourseEpisode episode = _context.CourseEpisodes.Find(id);
            if (episode != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Episode/", episode.EpisodeFileName);
                File.Delete(deleteFilePath);
                _context.CourseEpisodes.Remove(episode);
                _context.SaveChanges();
            }
        }
        public List<ShowCourseForAdminViewModel> GetDeletedCoursesForAdmin()
        {
            IQueryable<Course> result = _context.Courses.IgnoreQueryFilters().Where(u => u.IsDelete == true);
            return result.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToList();
        }
        public void RecoverCourse(int id)
        {
            Course course = _context.Courses
                .IgnoreQueryFilters()
                .Where(u => u.IsDelete == true)
                .SingleOrDefault(u => u.CourseId == id);
            course.IsDelete = false;
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

    }
}
