using QuizApp.Common.Constants;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows.Controls;

namespace QuizApp.Views.Components
{
    public partial class CategorySelector : UserControl
    {
        public event Action<Category?>? CategorySelected;

        public CategorySelector()
        {
            InitializeComponent();
            Loaded += (_, __) => LoadCategories();
        }

        private void LoadCategories()
        {
            QuizCategoryRepository quizCategoryRepository = new QuizCategoryRepository(new DatabaseContext());
            List<Category> categories = quizCategoryRepository.GetAll().ToList();

            if (categories.Any())
            {
                categories.Insert(0, new Category
                {
                    Id = QuizSettings.MIXED_CATEGORY_ID,
                    Name = QuizSettings.MIXED_CATEGORY_NAME
                });

                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.SelectedIndex = 0;
                CategoryComboBox.IsEnabled = true;
            }
            else
            {
                CategoryComboBox.ItemsSource = null;
                CategoryComboBox.IsEnabled = false;
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = CategoryComboBox.SelectedItem as Category;
            CategorySelected?.Invoke(selectedCategory);
        }

        public Category? GetSelectedCategory()
        {
            return CategoryComboBox.SelectedItem as Category;
        }

        public void SetSelectedCategory(Category? category)
        {
            CategoryComboBox.SelectedItem = category;
        }
    }
}