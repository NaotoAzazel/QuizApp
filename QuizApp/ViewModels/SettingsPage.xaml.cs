using QuizApp.Common.Constants;
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

            var datePickerValidator = BirthDatePicker.Rules().Required();
            datePickerValidator.Validate();

            if (!datePickerValidator.IsValid)
            {
                MessageBox.Show(ErrorMessages.CORRECT_HIGHLIGHTED_FIELDS);
                return;
            }

            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (!string.IsNullOrWhiteSpace(newPassword) || !string.IsNullOrWhiteSpace(confirmPassword))
            {
                var passwordValidator = NewPasswordBox.Rules().MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH);
                var confirmPasswordValidator = ConfirmPasswordBox.Rules().MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH);

                passwordValidator.Validate();
                confirmPasswordValidator.Validate();

                if (!passwordValidator.IsValid || !confirmPasswordValidator.IsValid)
                {
                    MessageBox.Show(ErrorMessages.CORRECT_HIGHLIGHTED_FIELDS);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show(ErrorMessages.PASSWORDS_DO_NOT_MATCH);
                    return;
                }

                user.Password = PasswordHasher.Hash(newPassword);
            }

            var birthDate = BirthDatePicker.SelectedDate;

            if(birthDate.HasValue)
            {
                user.Birthday = birthDate.Value.ToUniversalTime();
            }

            var userRepository = new UserRepository(new DatabaseContext());
            userRepository.Update(user);
            SessionManager.SetCurrentUser(user);

            MessageBox.Show(SuccessMessages.CHANGES_SAVED_SUCCESSFULLY);
        }
    }
}
