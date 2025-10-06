using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using QuizApp.Lib.Validator;

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
            var usernameValidator = textBoxUsername.Rules().MinCharacters(5);
            var passwordValidator = textBoxPassword.Rules().MinCharacters(8);

            usernameValidator.Validate();
            passwordValidator.Validate();

            if (!usernameValidator.IsValid || !passwordValidator.IsValid)
            {
                MessageBox.Show("Please correct the highlighted fields.");
                return;
            }

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