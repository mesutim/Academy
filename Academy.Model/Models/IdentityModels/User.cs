using Academy.Model.Models.CourseModels;
using Academy.Model.Models.OrderModels;
using Academy.Model.Models.TicketModels;
using Academy.Model.Models.TransactionModels;

namespace Academy.Model.Models.IdentityModels
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public string UserAvatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDelete { get; set; }

        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<Course> Courses { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Question> Questions { get; set; }
        public List<Order> Orders { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
        public List<Transaction> Transactions { get; set; }
        #endregion


    }
}
