namespace QuizApp.Models
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public List<int> SelectedAnswerIds { get; set; } = new();
    }
}
