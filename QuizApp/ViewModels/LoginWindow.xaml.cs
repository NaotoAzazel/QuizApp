using QuizApp.Common.Constants;
using QuizApp.Lib.Validator;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using System.Windows.Input;

namespace QuizApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterWindowOpen(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registrationWindow = new RegisterWindow();
            registrationWindow.Show();
            Close();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var usernameValidator = textBoxUsername.Rules()
                .MinCharacters(ValidationRules.MIN_USERNAME_LENGTH)
                .WithErrorBlock(errorUsername);

            var passwordValidator = textBoxPassword.Rules()
                .MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH)
                .WithErrorBlock(errorPassword);

            if (!usernameValidator.Check() || !passwordValidator.Check()) return;

            string username = usernameValidator.GetValue()!;
            string password = passwordValidator.GetValue()!;

            var dbContext = new DatabaseContext();
            var userRepository = new UserRepository(dbContext);

            var validatedUser = userRepository.validateUser(username, password);
            if (validatedUser == null)
            {
                MessageBox.Show(ErrorMessages.USERNAME_OR_PASSWORD_INCORRECT);
                return;
            }

            SessionManager.SetCurrentUser(validatedUser);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}