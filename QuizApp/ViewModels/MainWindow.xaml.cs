using QuizApp.Common.Constants;
using QuizApp.Models;
using QuizApp.Pages;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;

namespace QuizApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // base logic how to create new quiz step by step
            // be sure to delete this later

            var dbContext = new DatabaseContext();
            QuizRepository quizRepo = new QuizRepository(dbContext);
            UserQuizResultRepository userQuizResultRepository = new UserQuizResultRepository(dbContext);
            AnswerOptionRepository answerOptionRepository = new AnswerOptionRepository(dbContext);

            QuizService quizService = new QuizService(quizRepo, userQuizResultRepository);

            // TODO: get category from popup
            Category category = new Category
            {
                Id = 1,
                Name = "History"
            };

            // generate questions
            List<Question> questions = quizService.GetQuestions(category, QuizSettings.QUESTIONS_COUNT);
            List<UserAnswer> userAnswers = new();

            foreach (Question question in questions)
            {
                Console.WriteLine(question.Id);
                Console.WriteLine(question.Text);
                Console.WriteLine(question.Category!.Name);

                // when press next button
                List<AnswerOption> answers = answerOptionRepository.GetAnswersToQuestion(question);
                userAnswers.Add(new UserAnswer
                {
                    QuestionId = question.Id,
                    SelectedAnswerIds = new List<int> { 101, 103 } // ID of selected options
                });
                
                Console.WriteLine("Answers to question:");
                foreach (var answer in answers)
                {
                    Console.WriteLine($" - {answer.Text} (Correct: {answer.IsCorrect})");
                }

                Console.WriteLine("\n");
                Console.WriteLine("-------------------------");
            }

            QuizSession quizSession = new QuizSession
            {
                Category = category,
                Answers = userAnswers,
                Questions = questions
            };

            // get result from game
            int correctAnswers = quizService.EvaluateQuiz(SessionManager.CurrentUser!.Id, category, quizSession);
            Console.WriteLine(correctAnswers);
        }

        private void StartNewQuizButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Top20Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}
