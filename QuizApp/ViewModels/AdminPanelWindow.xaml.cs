using QuizApp.Common.Constants;
using QuizApp.Lib.Validator;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp.Views
{
    public partial class AdminPanelWindow : Window
    {
        private readonly QuizCategoryRepository _quizCategoryRepository;
        private readonly QuizRepository _quizRepository;

        public AdminPanelWindow()
        {
            _quizCategoryRepository = new QuizCategoryRepository(new DatabaseContext());
            _quizRepository = new QuizRepository(new DatabaseContext());

            InitializeComponent();
        }

        private void AdminTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl) return;

            var selectedTab = AdminTabs.SelectedItem as TabItem;

            if (selectedTab == null) return;

            switch (selectedTab.Header)
            {
                case "Categories":
                    LoadCategories();
                    break;

                case "Questions":
                    LoadQuestions();
                    break;
            }
        }

        private void LoadCategories()
        {
            CategoriesDataGrid.ItemsSource = _quizCategoryRepository.GetAll().Where(c => !c.IsDeleted).ToList();
        }

        private void LoadQuestions()
        {
            QuestionsDataGrid.ItemsSource = _quizRepository.GetAllQuestions().ToList();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryNameValidator = categoryNameTextBox.Rules()
                .MinCharacters(ValidationRules.MIN_CATEGORY_NAME_LENGTH)
                .WithErrorBlock(errorCategoryName);

            if (!categoryNameValidator.Check()) return;

            string categoryName = categoryNameValidator.GetValue()!;

            Category? existingCategory = _quizCategoryRepository.GetByName(categoryName);

            if(existingCategory != null && !existingCategory.IsDeleted)
            {
                MessageBox.Show(ErrorMessages.CATEGORY_WITH_THIS_NAME_EXISTS);
                return;
            }
            else if (existingCategory != null && existingCategory.IsDeleted)
            {
                existingCategory.IsDeleted = false;
                _quizCategoryRepository.Update(existingCategory);
                LoadCategories();
                return;
            }

            Category newCategory = new Category
            {
                Name = categoryName,
            };

            _quizCategoryRepository.Create(newCategory);
            LoadCategories();
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                Category? category = _quizCategoryRepository.Get(id);

                if (category != null)
                {
                    _quizCategoryRepository.SoftDelete(category);
                    LoadCategories();
                }
            }
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionWindow dialog = new AddQuestionWindow();
            if (dialog.ShowDialog() == true)
            {
                LoadQuestions();
            }
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var question = _quizRepository.Get(id);
                if (question == null) return;

                var dialog = new AddQuestionWindow(question.Category!, question);
                if (dialog.ShowDialog() == true)
                {
                    LoadQuestions();
                }
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                _quizRepository.Delete(id);
                LoadQuestions();
            }
        }
    }
}
