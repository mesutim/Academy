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
            course.UpdateDate = DateTime.Now;
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
        public void UpdateCourse(CreateCourseAdminViewModel course, IFormFile imgCourse, IFormFile courseDemo)
        {
            Course editedCourse = _context.Courses.Find(course.Course.Id);
            editedCourse.UpdateDate = DateTime.Now;
            editedCourse.CourseTitle = course.Course.Title;
            editedCourse.TeacherId = course.Course.TeacherId;
            editedCourse.CourseDescription = course.Course.Description;
            editedCourse.CoursePrice = course.Course.Price;
            editedCourse.Tags = course.Course.Tags;
            editedCourse.LevelId = course.Course.LevelId;
            editedCourse.StatusId = course.Course.StatusId;
            if (imgCourse != null && imgCourse.IsImage())
            {
                if (editedCourse.CourseImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Image/", editedCourse.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Thumb/", editedCourse.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                editedCourse.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Image/", editedCourse.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Thumb/", editedCourse.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            if (courseDemo != null)
            {
                if (editedCourse.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Demo/", editedCourse.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }
                editedCourse.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFiles/Demo/", editedCourse.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Courses.Update(editedCourse);

            foreach (var item in _context.courseCategoryMap.Where(c=>c.CourseId==editedCourse.CourseId))
            {
                _context.courseCategoryMap.Remove(item);
            };

            _context.courseCategoryMap.Add(new CourseCategory()
            {
                CourseId = editedCourse.CourseId,
                CategoryId = course.Course.CategoryId
            });

            if (course.Course.SubCategoryId != null)
            {
                _context.courseCategoryMap.Add(new CourseCategory()
                {
                    CourseId = editedCourse.CourseId,
                    CategoryId = course.Course.SubCategoryId
                });
            }

            _context.SaveChanges();
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
            List<CourseCategory> categories = _context.courseCategoryMap.Where(c => c.CourseId == courseId).Include(c => c.Category).ToList();
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
        public List<Category> GetAllGroup()
        {
            return _context.Categories.ToList();
        }
        public List<ShowCourseListItemViewModel> GetLatestCourseList()
        {
            return _context.Courses
                .Include(c => c.CourseEpisodes)
                .OrderBy(c => c.CreateDate)
                .Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    Title = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    CourseEpisodes = c.CourseEpisodes
                }).ToList();
        }
        public List<ShowCourseListItemViewModel> GetPopularCourseList()
        {
            return _context.Courses.Include(c => c.OrderDetails)
               .Where(c => c.OrderDetails.Any())
               .OrderByDescending(d => d.OrderDetails.Count)
               .Take(8)
               .Select(c => new ShowCourseListItemViewModel()
               {
                   CourseId = c.CourseId,
                   ImageName = c.CourseImageName,
                   Price = c.CoursePrice,
                   Title = c.CourseTitle,
                   CourseEpisodes = c.CourseEpisodes
               })
               .ToList();
        }
        public Tuple<List<ShowCourseListItemViewModel>, int, int>
            GetCourse(
                int pageId = 1,
                string filter = "",
                string getType = "all",
                string orderByType = "date",
                int startPrice = 0,
                int endPrice = 0,
                int? selectedCategory = null,
                int take = 8)
        {
            IQueryable<Course> result = _context.Courses;
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    result = result.Where(c => c.CoursePrice != 0);
                    break;
                case "free":
                    result = result.Where(c => c.CoursePrice == 0);
                    break;
            }

            switch (orderByType)
            {
                case "date":
                    result = result.OrderByDescending(c => c.CreateDate);
                    break;
                case "updatedate":
                    result = result.OrderByDescending(c => c.UpdateDate);
                    break;
                case "price":
                    result = result.OrderByDescending(c => c.CoursePrice);
                    break;
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }
            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < endPrice);
            }

            if (selectedCategory != null)
            {
                    result = result.Where(c => c.CourseCategories.Any(c => c.CategoryId == selectedCategory));
            }

            int count = result.Count();

            int pageCount = count / take;

            int skip = (pageId - 1) * take;

            var query = result.Include(c => c.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                CourseEpisodes = c.CourseEpisodes


            }).Skip(skip).Take(take).ToList();
            return Tuple.Create(query, pageCount, count);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses
                .Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.UserCourses)
                .Include(c => c.User)
                .FirstOrDefault(c => c.CourseId == courseId);

        }
        public Course GetCourseByShortKey(string shortKey)
        {
            return _context.Courses.FirstOrDefault(c => c.ShortKey == shortKey);
        }
        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }
        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1)
        {
            int take = 2;
            int skip = (pageId - 1) * take;
            int pageCount = _context.CourseComments.Where(c => !c.IsDelete && c.CourseId == courseId).Count() / take;
            //if ((pageCount % 2) != 0)
            //{
            //    pageCount += 1;
            //}
            return Tuple.Create(
                _context.CourseComments.Include(c => c.User).Where(c => !c.IsDelete && c.CourseId == courseId).Skip(skip).Take(take)
                    .OrderByDescending(c => c.CreateDate).ToList(), pageCount);
        }
        public bool IsFree(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Select(c => c.CoursePrice).First() == 0;
        }
        public Tuple<int, int> GetCourseVotes(int courseId)
        {
            var votes = _context.CourseVotes.Where(v => v.CourseId == courseId).Select(v => v.Vote).ToList();

            return Tuple.Create(votes.Count(c => c), votes.Count(c => !c));

        }
        public void AddsVote(int userId, int courseId, bool vote)
        {
            var UserVote = _context.CourseVotes.FirstOrDefault(c => c.UserId == userId && c.CourseId == courseId);
            if (UserVote != null)
            {
                UserVote.Vote = vote;
            }
            else
            {
                UserVote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userId,
                    Vote = vote
                };
                _context.Add(UserVote);
            }

            _context.SaveChanges();
        }
        public List<Course> GetAllMasterCourses(string userName)
        {
            var userId = _context.Users.FirstOrDefault(s => s.UserName == userName).UserId;

            var courses = _context.Courses
                .Include(s => s.CourseStatus)
                .Include(s => s.CourseEpisodes)
                .Where(s => s.TeacherId == userId).ToList();

            return courses;
        }
        public List<Course> GetAllCashCoursesForUser(int userId)
        {
            var courses = _context.UserCourseMap
                .Include(c => c.Course)
                .ThenInclude(c => c.CourseStatus)
                .Include(c => c.Course.CourseEpisodes)
                .Where(c => c.UserId == userId && c.Course.CoursePrice > 0).Select(c => c.Course).ToList();

            return courses;
        }
        public List<Course> GetAllFreeCoursesForUser(string userName)
        {
            var userId = _context.Users.FirstOrDefault(s => s.UserName == userName).UserId;

            var courses = _context.UserCourseMap
                .Include(c => c.Course)
                .ThenInclude(c => c.CourseStatus)
                .Include(c => c.Course.CourseEpisodes)
                .Where(c => c.UserId == userId && c.Course.CoursePrice == 0).Select(c => c.Course).ToList();
            return courses;
        }
        public List<CourseEpisode> GetCourseEpisodesByCourseId(int courseId)
        {
            var episodes = _context.CourseEpisodes
                .Include(s => s.Course)
                .Where(s => s.CourseId == courseId).ToList();

            return episodes;
        }
        public bool AddEpisode(AddEpisodeViewModel episodeViewModel, string userName)
        {
            var course = GetCourseById(episodeViewModel.CourseId);

            var userId = _context.Users.FirstOrDefault(s => s.UserName == userName).UserId;

            if (course == null || course.TeacherId != userId)
            {
                return false;
            }

            var episode = new CourseEpisode()
            {
                CourseId = course.CourseId,
                IsFree = episodeViewModel.IsFree,
                EpisodeTitle = episodeViewModel.EpisodeTitle,
                EpisodeTime = episodeViewModel.EpisodeTime,
                EpisodeFileName = episodeViewModel.EpisodeFileName
            };

            _context.Add(episode);
            _context.SaveChanges();

            return true;
        }
    }
}
