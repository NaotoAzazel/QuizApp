namespace QuizApp.Models
{
    public class QuizSession
    {
        public required Category Category { get; set; }
        public List<Question> Questions { get; set; } = new();
        public List<UserAnswer> Answers { get; set; } = new();
    }
}
