using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.CourseModels
{
  public class CourseVote
    {
        public int VoteId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;


        public User User { get; set; }
        public Course Course { get; set; }
    }
}
