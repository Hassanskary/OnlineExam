using Online_Exam.Models;

namespace Online_Exam.Models
{
    public class UserAnswer
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
public class UserAnswerRepository
{
    public void SaveAnswer(UserAnswer userAnswer)
    {
        // Code to insert the answer into the database
    }
}