using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.TicketModels
{
   public class Question
    {
        public int QuestionId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Relations
        public User User { get; set; }
        public Course Course { get; set; }
        public List<Answer> Answers { get; set; }
        #endregion
    }
}
