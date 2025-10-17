using QuizApp.Lib.Validator;
using QuizApp.Services;
using QuizApp.Services.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp.Pages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            
            Loaded += SettingsPage_Loaded;
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            BirthDatePicker.SelectedDate = SessionManager.CurrentUser!.Birthday;
        }

        private void SaveChangesClick(object sender, EventArgs e)
        {
            var user = SessionManager.CurrentUser!;

            var passwordValidator = NewPasswordBox.Rules().MinCharacters(8);
            var confirmPasswordValidator = ConfirmPasswordBox.Rules().MinCharacters(8);
            var datePickerValidator = BirthDatePicker.Rules().Required();

            passwordValidator.Validate();
            confirmPasswordValidator.Validate();
            datePickerValidator.Validate();

            if (!passwordValidator.IsValid || !confirmPasswordValidator.IsValid || !datePickerValidator.IsValid)
            {
                MessageBox.Show("Please correct the highlighted fields.");
                return;
            }

            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match");
            }

            DateTime birthDate = BirthDatePicker.DisplayDate;

            user.Birthday = birthDate.ToUniversalTime();
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                user.Password = PasswordHasher.Hash(newPassword);
            }

            var userRepository = new UserRepository(new DatabaseContext());
            userRepository.Update(user);

            MessageBox.Show("Changes successfully saved");
        }
    }
}
