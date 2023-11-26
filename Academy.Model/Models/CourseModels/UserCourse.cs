using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.CourseModels
{
    public class UserCourse
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User User { get; set; }
        #endregion
    }
}
