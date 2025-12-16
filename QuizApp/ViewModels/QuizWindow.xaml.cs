using QuizApp.Common.Constants;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp.Views.Windows
{
    public partial class QuizWindow : Window
    {
        private readonly QuizRepository _quizRepo;
        private readonly QuizService _quizService;
        private readonly UserQuizResultRepository _userQuizResultRepository;
        private readonly AnswerOptionRepository _answerOptionRepository;
        private readonly DatabaseContext _dbContext;
        private readonly Category _category;

        private List<Question> _questions = new();
        private List<UserAnswer> _userAnswers = new();
        private int _currentQuestionIndex = 0;

        public QuizWindow(Category category)
        {
            InitializeComponent();
            _category = category;

            _dbContext = new DatabaseContext();
            _quizRepo = new QuizRepository(_dbContext);
            _userQuizResultRepository = new UserQuizResultRepository(_dbContext);
            _quizService = new QuizService(_quizRepo, _userQuizResultRepository);
            _answerOptionRepository = new AnswerOptionRepository(_dbContext);

            Loaded += QuizWindow_Loaded;
        }

        private void QuizWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _questions = _quizService.GetQuestions(_category, QuizSettings.QUESTIONS_COUNT);
            LoadQuestion(_currentQuestionIndex);
        }

        private void LoadQuestion(int index)
        {
            if (index >= _questions.Count)
            {
                FinishQuiz();
                return;
            }

            Question currentQuestion = _questions[index];
            QuestionTextBlock.Text = currentQuestion.Text;
            AnswersPanel.Children.Clear();

            List<AnswerOption> answers = _answerOptionRepository.GetAnswersToQuestion(currentQuestion);

            foreach (var answer in answers)
            {
                CheckBox checkBox = new()
                {
                    Content = answer.Text,
                    Tag = answer.Id,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                AnswersPanel.Children.Add(checkBox);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnswers = AnswersPanel.Children
                .OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => (int)cb.Tag)
                .ToList();

            var currentQuestion = _questions[_currentQuestionIndex];
            _userAnswers.Add(new UserAnswer
            {
                QuestionId = currentQuestion.Id,
                SelectedAnswerIds = selectedAnswers
            });

            _currentQuestionIndex++;

            if (_currentQuestionIndex < _questions.Count)
            {
                LoadQuestion(_currentQuestionIndex);
            }
            else
            {
                FinishQuiz();
            }
        }

        private void FinishQuiz()
        {
            QuizSession quizSession = new()
            {
                Category = _category,
                Questions = _questions,
                Answers = _userAnswers
            };

            int correctAnswers = _quizService.EvaluateQuiz(SessionManager.CurrentUser!.Id, _category, quizSession);
            MessageBox.Show($"{SuccessMessages.QUIZ_SUCCESSFULLY_ENDED} {correctAnswers} of {_questions.Count}");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
