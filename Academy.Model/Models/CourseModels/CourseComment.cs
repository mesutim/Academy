using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.CourseModels
{
    public class CourseComment
    {
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }
        public bool IsConfirmed { get; set; }


        public Course Course { get; set; }
        public User User { get; set; }
    }
}
