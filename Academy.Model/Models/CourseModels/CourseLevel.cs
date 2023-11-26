
namespace Academy.Model.Models.CourseModels
{
    public class CourseLevel
    {
        public int LevelId { get; set; }
        public string LevelTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
