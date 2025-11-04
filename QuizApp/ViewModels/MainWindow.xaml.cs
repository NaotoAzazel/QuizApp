using QuizApp.Models;
using QuizApp.Pages;
using QuizApp.Views.Windows;
using System.Windows;

namespace QuizApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartNewQuizButton_Click(object sender, RoutedEventArgs e)
        {
            StartQuizWindow startQuizWindow = new StartQuizWindow()
            {
                Owner = this
            };

            bool? result = startQuizWindow.ShowDialog();
            if (result == false) return;

            Category selectedCategory = startQuizWindow.SelectedCategory!;
            QuizWindow quizWindow = new QuizWindow(selectedCategory);
            quizWindow.Show();
            Close();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HistoryPage());
        }

        private void Top20Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LeaderboardPage());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}
