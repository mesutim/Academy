using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Academy.Model.Models.CourseModels;
using Academy.Model.ViewModels;

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
        void UpdateCourse(CreateCourseAdminViewModel course, IFormFile imgCourse, IFormFile courseDemo);
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
        List<Category> GetAllGroup();
        List<ShowCourseListItemViewModel> GetLatestCourseList();
        List<ShowCourseListItemViewModel> GetPopularCourseList();
        Tuple<List<ShowCourseListItemViewModel>, int, int> GetCourse(
            int pageId = 1,
            string filter = "",
            string getType = "all",
            string orderByType = "date",
            int startPrice = 0,
            int endPrice = 0,
            int? selectedGroups = null,
            int take = 8);
        Course GetCourseForShow(int courseId);
        Course GetCourseByShortKey(string shortKey);
        public void AddComment(CourseComment comment);
        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1);
        bool IsFree(int courseId);
        Tuple<int, int> GetCourseVotes(int courseId);
        void AddsVote(int userId, int courseId, bool vote);
    }
}
