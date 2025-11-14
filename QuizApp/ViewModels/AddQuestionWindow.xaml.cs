using QuizApp.Common.Constants;
using QuizApp.Lib.Validator;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuizApp.Views
{
    public partial class AddQuestionWindow : Window
    {
        private readonly QuizRepository _quizRepository;
        private readonly AnswerOptionRepository _answerOptionRepository;
        private readonly Question? _existingQuestion;
        private readonly Category? _category;
        private bool _allowCategorySelection = true;

        public ObservableCollection<AnswerOption> AnswerOptions { get; set; } = new();

        public AddQuestionWindow(Category? category = null, Question? question = null)
        {
            InitializeComponent();
            _quizRepository = new QuizRepository(new DatabaseContext());
            _answerOptionRepository = new AnswerOptionRepository(new DatabaseContext());
            _existingQuestion = question;
            _category = category;

            if (_category != null)
            {
                _allowCategorySelection = false;
                categorySelector.IsEnabled = false; 
                categorySelector.SetSelectedCategory(_category);
            }

            if (question != null)
            {
                questionTextBox.Text = question.Text;
                foreach (var a in question.Options)
                {
                    AnswerOptions.Add(new AnswerOption { Text = a.Text, IsCorrect = a.IsCorrect });
                }

                if (question.Category != null)
                {
                    categorySelector.SetSelectedCategory(question.Category);
                }
            }

            answersListBox.ItemsSource = AnswerOptions;
        }

        private void AddAnswer_Click(object sender, RoutedEventArgs e)
        {
            AnswerOptions.Add(new AnswerOption { Text = "", IsCorrect = false });
        }

        private void RemoveAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (answersListBox.SelectedItem is AnswerOption answer)
            {
                AnswerOptions.Remove(answer);
            }    
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var questionTextValidator = questionTextBox.Rules()
                .MinCharacters(ValidationRules.MIN_QUESTION_TEXT_LENGTH)
                .WithErrorBlock(errorQuestionText);

            if (!questionTextValidator.Check()) return;

            string text = questionTextValidator.GetValue()!;

            if (AnswerOptions.Count == 0)
            {
                MessageBox.Show("Add at least one answer.");
                return;
            }

            Category? selectedCategory = _allowCategorySelection
                ? categorySelector.GetSelectedCategory()
                : _category;

            if (selectedCategory == null)
            {
                MessageBox.Show(ErrorMessages.PLEASE_SELECT_QUIZ_CATEGORY);
                return;
            }

            if (_existingQuestion == null)
            {
                var newQuestion = new Question
                {
                    Text = text,
                    CategoryId = selectedCategory.Id
                };
                _quizRepository.Create(newQuestion);

                foreach (var answer in AnswerOptions)
                {
                    answer.QuestionId = newQuestion.Id;
                    _answerOptionRepository.Create(answer);
                }
            }
            else
            {
                _existingQuestion.Text = text;
                _existingQuestion.CategoryId = selectedCategory.Id;
                _quizRepository.Update(_existingQuestion);

                foreach (var old in _answerOptionRepository.GetAnswersToQuestion(_existingQuestion))
                {
                    _answerOptionRepository.Delete(old.Id);
                }

                foreach (var answer in AnswerOptions)
                {
                    answer.QuestionId = _existingQuestion.Id;
                    _answerOptionRepository.Create(answer);
                }
            }

            MessageBox.Show(SuccessMessages.QUESTION_SAVED_SUCCESSFULLY);
            DialogResult = true;
            Close();
        }
    }
}
