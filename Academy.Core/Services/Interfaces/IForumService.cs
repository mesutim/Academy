using Academy.Model.ViewModels;
using Academy.Model.Models.TicketModels;

namespace Academy.Core.Services.Interfaces
{
   public interface IForumService
    {
        #region Question
        int AddQuestion(Question question);

        ShowQuestionVM ShowQuestion(int questionId);

        IEnumerable<Question> GetQuestions(int? courseId, string filter = "");

        public IEnumerable<Question> GetQuestionsForMaster(int userId, string filter = "");
        #endregion

        #region Answer
        void AddAnswer(Answer answer);

        void ChangeIsTrueAnswer(int questionId, int answerId);

        #endregion
    }
}
