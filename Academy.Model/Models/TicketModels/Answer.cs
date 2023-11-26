using Academy.Model.Models.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Model.Models.TicketModels
{
   public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int? UserId { get; set; }
        public string BodyAnswer { get; set; }
        public bool IsTrue { get; set; } = false;
        public DateTime CreateDate { get; set; }

        #region Relations
        public Question Question { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        #endregion
    }
}
