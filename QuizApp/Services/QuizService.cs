using Microsoft.EntityFrameworkCore;
using QuizApp.Common.Constants;
using QuizApp.Models;
using QuizApp.Services.Repositories;
using System.Net.NetworkInformation;

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
            return category.Id == QuizSettings.MIXED_CATEGORY_ID 
                ? _quizRepo.GetRandomQuestions(questionsCount) 
                : _quizRepo.GetRandomQuestionsByCategory(category, questionsCount);
        }

        public int EvaluateQuiz(int userId, Category category, QuizSession quizSession)
        {
            int correctAnswers = 0;

            foreach (var question in quizSession.Questions)
            {
                UserAnswer userAnswer = quizSession.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (userAnswer == null) continue;

                List<int> correctIds = question.Options
                    .Where(a => a.IsCorrect)
                    .Select(a => a.Id)
                    .OrderBy(id => id)
                    .ToList();

                List<int> selectedIds = userAnswer.SelectedAnswerIds
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
                CategoryId = category.Id == QuizSettings.MIXED_CATEGORY_ID ? null : category.Id,
                CorrectAnswers = correctAnswers,
            };

            _resultRepo.Create(result);
            return correctAnswers;
        }

        public List<UserQuizResult> GetLeaderboard(Category? category)
        {
            using var context = new DatabaseContext();

            var query = context.UserQuizResult
                .Include(r => r.User)
                .AsNoTracking()
                .AsQueryable();

            if (category != null && category.Id != QuizSettings.MIXED_CATEGORY_ID)
            {
                query = query.Where(r => r.CategoryId == category.Id);
            }

            List<UserQuizResult> results = query.ToList();

            return results
                .GroupBy(r => r.UserId)
                .Select(g => g
                    .OrderByDescending(r => r.CorrectAnswers)
                    .ThenBy(r => r.Id)
                    .First())
                .OrderByDescending(r => r.CorrectAnswers)
                .Take(20)
                .ToList();
        }
    }
}
