using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.OrderModels;
using Academy.Model.Models.TicketModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Model.Models.CourseModels
{
    public class Course
    {
        public int CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int StatusId { get; set; }
        public int LevelId { get; set; }
        public bool IsDelete { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public int CoursePrice { get; set; }
        public string Tags { get; set; }
        public string CourseImageName { get; set; }
        public string DemoFileName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ShortKey { get; set; }

        #region Relations
        public CourseStatus CourseStatus { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public List<CourseCategory> CourseCategories { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public User User { get; set; }
        public List<CourseEpisode> CourseEpisodes { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Question> Questions { get; set; }
        #endregion
    }
}
