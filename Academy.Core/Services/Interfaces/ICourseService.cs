using Academy.Model.Models.CourseModels;
using Academy.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Core.Services.Interfaces 
{ 
    public interface ICourseService
    {
        List<Category> GetAllCategories();
        void AddCategory(Category cat);
        Category GetCategoryById(int catId);
        void UpdateCategory(Category cat);
        void DeleteCategoryById(int id);
        List<ShowCourseForAdminViewModel> GetCoursesForAdmin();
        public List<SelectListItem> GetCategoriesForManageCourse();
        public List<SelectListItem> GetSubCategoriesForManageCourse(int groupId);
        public List<SelectListItem> GetTeachers();
        public List<SelectListItem> GetLevels();
        public List<SelectListItem> GetStatues();
        int AddCourse(CreateCourseAdminViewModel newCourse, IFormFile imgCourse, IFormFile courseDemo);
        Course GetCourseById(int courseId);
        CreateCourseViewModel GetCourseForEditByAdmin(int courseId);
        void DeleteCourse(int id);
        List<CourseEpisode> GetListEpisodeCourse(int courseId);
        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        void DeleteEpisode(int id);
        List<ShowCourseForAdminViewModel> GetDeletedCoursesForAdmin();
        void RecoverCourse(int id);

    }
}
