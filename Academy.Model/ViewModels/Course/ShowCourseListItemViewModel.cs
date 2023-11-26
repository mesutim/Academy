using Academy.Model.Models.CourseModels;

namespace Academy.Model.ViewModels
{
    public class ShowCourseListItemViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        //public TimeSpan TotalTime { get; set; }
        public List<CourseEpisode> CourseEpisodes { get; set; }

    }
}
