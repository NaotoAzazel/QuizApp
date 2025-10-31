using QuizApp.Common.Constants;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;

namespace QuizApp.Views.Windows
{
    public partial class StartQuizWindow : Window
    {
        public Category? SelectedCategory { get; private set; }

        public StartQuizWindow()
        {
            InitializeComponent();

            Loaded += StartQuizWindow_Loaded;
        }

        private void StartQuizWindow_Loaded(object sender, RoutedEventArgs e)
        {
            QuizCategoryRepository quizCategoryRepository = new QuizCategoryRepository(new DatabaseContext());
            List<Category> categories = quizCategoryRepository.GetAll().ToList();
            categories.Insert(0, new Category
            {
                Id = QuizSettings.MIXED_CATEGORY_ID,
                Name = QuizSettings.MIXED_CATEGORY_NAME
            });

            if (categories.Any())
            {
                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.SelectedIndex = 0;
                CategoryComboBox.IsEnabled = true;
                EmptyMessageTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                CategoryComboBox.ItemsSource = null;
                CategoryComboBox.IsEnabled = false;
                EmptyMessageTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            SelectedCategory = CategoryComboBox.SelectedItem as Category;

            if (SelectedCategory == null)
            {
                MessageBox.Show(ErrorMessages.PLEASE_SELECT_QUIZ_CATEGORY);
                return;
            }

            DialogResult = true;
        }
    }
}
