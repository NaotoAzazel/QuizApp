using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows.Controls;
using System.Windows;

namespace QuizApp.Pages
{
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();

            Loaded += HistoryPage_Loaded;
        }

        private void HistoryPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DatabaseContext databaseContext = new DatabaseContext(); 
            UserQuizResultRepository userQuizResultRepository = new UserQuizResultRepository(databaseContext);

            var results = userQuizResultRepository.GetAllWithCategory()
                .Where(r => r.UserId == SessionManager.CurrentUser!.Id)
                .OrderByDescending(r => r.CompletedAt)
                .Take(20)
                .Select(r => new
                {
                    CategoryName = r.Category != null ? r.Category.Name : "Mixed",
                    CorrectAnswers = r.CorrectAnswers,
                    TotalQuestions = databaseContext.Questions
                        .Count(q => q.CategoryId == r.CategoryId),
                    CompletedAt = r.CompletedAt
                })
                .ToList();

            QuizHistoryList.ItemsSource = results;

            if (results.Any())
            {
                QuizHistoryList.ItemsSource = results;
                QuizHistoryList.Visibility = Visibility.Visible;
                EmptyHistoryText.Visibility = Visibility.Collapsed;
            }
            else
            {
                QuizHistoryList.Visibility = Visibility.Collapsed;
                EmptyHistoryText.Visibility = Visibility.Visible;
            }
        }
    }
}
