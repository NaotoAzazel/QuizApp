using QuizApp.Models;
using QuizApp.Services.Repositories;

namespace QuizApp.Services
{
    public class QuizService
    {
        private readonly QuizRepository _quizRepo;
        private readonly UserQuizResultRepository _resultRepo;

        public QuizService(QuizRepository quizRepo, UserQuizResultRepository resultRepo)
        {
            _quizRepo = quizRepo;
            _resultRepo = resultRepo;
        }

        public List<Question> GetQuestions(Category? category, int questionsCount)
        {
            return _quizRepo.GetRandomQuestions(category, questionsCount);
        }

        public int EvaluateQuiz(int userId, Category category, QuizSession quizSession)
        {
            int correctAnswers = 0;

            foreach (var question in quizSession.Questions)
            {
                var userAnswer = quizSession.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (userAnswer == null) continue;

                var correctIds = question.Options
                    .Where(a => a.IsCorrect)
                    .Select(a => a.Id)
                    .OrderBy(id => id)
                    .ToList();

                var selectedIds = userAnswer.SelectedAnswerIds
                    .OrderBy(id => id)
                    .ToList();

                if (correctIds.SequenceEqual(selectedIds))
                {
                    correctAnswers++;
                }
            }

            UserQuizResult result = new UserQuizResult
            {
                UserId = userId,
                CategoryId = category.Id,
                CorrectAnswers = correctAnswers,
            };

            _resultRepo.Create(result);
            return correctAnswers;
        }

        public List<UserQuizResult> GetLeaderboard(Category? category)
        {
            // TODO: implement
            return [];
        }
    }
}
