using QuizApp.Pages;
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
