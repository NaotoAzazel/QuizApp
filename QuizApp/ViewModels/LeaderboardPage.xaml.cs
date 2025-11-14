using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp.Pages
{
    public partial class LeaderboardPage : Page
    {
        private readonly QuizService _quizService;

        public LeaderboardPage()
        {
            InitializeComponent();

            QuizRepository quizRepo = new QuizRepository(new DatabaseContext());
            UserQuizResultRepository resultRepo = new UserQuizResultRepository(new DatabaseContext());
            _quizService = new QuizService(quizRepo, resultRepo);

            CategorySelectorControl.CategorySelected += OnCategorySelected;
        }

        private void OnCategorySelected(Category? category)
        {
            List<UserQuizResult> leaderboard = _quizService.GetLeaderboard(category);

            LeaderboardGrid.ItemsSource = leaderboard
                .Select((r, index) => new
                {
                    Rank = index + 1,
                    UserName = r.User?.Username ?? "Unknown",
                    CorrectAnswers = r.CorrectAnswers
                })
                .ToList();
        }
    }
}
