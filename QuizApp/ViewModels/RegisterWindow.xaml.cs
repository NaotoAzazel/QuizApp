using QuizApp.Common.Constants;
using QuizApp.Lib.Validator;
using QuizApp.Models;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;

namespace QuizApp.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void LoginWindowOpen(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e) 
        {
            var usernameValidator = textBoxUsername.Rules()
                .MinCharacters(ValidationRules.MIN_USERNAME_LENGTH)
                .WithErrorBlock(errorUsername);
            var passwordValidator = textBoxPassword.Rules()
                .MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH)
                .WithErrorBlock(errorPassword);
            var datePickerValidator = birthdayPicker.Rules().Required();

            if(!usernameValidator.Check() || !passwordValidator.Check() || !datePickerValidator.Check()) return;

            var dbContext = new DatabaseContext();
            var userRepository = new UserRepository(dbContext);

            string username = usernameValidator.GetValue()!;

            User user = userRepository.GetByUsername(username);
            if(user != null)
            {
                MessageBox.Show(ErrorMessages.USER_WITH_THIS_USERNAME_EXISTS);
                return;
            }

            string password = passwordValidator.GetValue()!;
            DateTime birthday = datePickerValidator.GetValue()!.Value;

            User newUser = new User
            { 
                Username = username, 
                Password = password, 
                Birthday = birthday.ToUniversalTime() 
            };
            
            userRepository.Create(newUser);

            MessageBox.Show(SuccessMessages.ACCOUNT_CREATED_SUCCESSFULLY);

            Thread.Sleep(2_000);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }

}
