
namespace Academy.Model.Models.CourseModels
{
    public class CourseCategory
    {
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        
        #region Relations
        public Category Category { get; set; }
        public Course Course { get; set; }
        #endregion

    }
}
