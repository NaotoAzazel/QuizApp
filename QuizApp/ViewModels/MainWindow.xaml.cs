using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            // todo: add fields validating
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Password;

            var dbContext = new DatabaseContext();
            var userRepository = new UserRepository(dbContext);

            var validatedUser = userRepository.validateUser(username, password);
            if (validatedUser == null)
            {
                MessageBox.Show("Username or password incorrect");
            }

            // TODO: do when login
            MessageBox.Show("Logged successfully");
        }
    }
}