using QuizApp.Common.Constants;
using QuizApp.Models;
using System.Windows;

namespace QuizApp.Views.Windows
{
    public partial class StartQuizWindow : Window
    {
        public Category? SelectedCategory { get; private set; }

        public StartQuizWindow()
        {
            InitializeComponent();
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            SelectedCategory = CategorySelectorControl.GetSelectedCategory();

            if (SelectedCategory == null)
            {
                MessageBox.Show(ErrorMessages.PLEASE_SELECT_QUIZ_CATEGORY);
                return;
            }

            DialogResult = true;
        }
    }
}
