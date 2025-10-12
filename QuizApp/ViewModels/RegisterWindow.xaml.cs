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
            var usernameValidator = textBoxUsername.Rules().MinCharacters(5);
            var passwordValidator = textBoxPassword.Rules().MinCharacters(8);
            var datePickerValidator = birthdayPicker.Rules().Required();

            usernameValidator.Validate();
            passwordValidator.Validate();
            datePickerValidator.Validate();

            if (!usernameValidator.IsValid || !passwordValidator.IsValid || !datePickerValidator.IsValid)
            {
                MessageBox.Show("Please correct the highlighted fields.");
                return;
            }

            var dbContext = new DatabaseContext();
            var userRepository = new UserRepository(dbContext);

            string username = textBoxUsername.Text;

            User user = userRepository.GetByUsername(username);
            if(user != null)
            {
                MessageBox.Show("User with this username already exists");
                return;
            }

            string password = textBoxPassword.Password;
            DateTime birthday = birthdayPicker.DisplayDate;

            User newUser = new User{ Username = username, Password = password, Birthday = birthday.ToUniversalTime() };
            
            userRepository.Create(newUser);

            MessageBox.Show("Your Account created succesfully, redirecting to login window...");

            Thread.Sleep(2_000);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }

}
