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
            if(!datePickerValidator.Check()) return;

            if (!string.IsNullOrWhiteSpace(NewPasswordBox.Password) || !string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                var newPasswordValidator = NewPasswordBox.Rules()
                    .MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH)
                    .WithErrorBlock(errorNewPassword);
                var confirmPasswordValidator = ConfirmPasswordBox.Rules()
                    .MinCharacters(ValidationRules.MIN_PASSWORD_LENGTH)
                    .WithErrorBlock(errorConfirmNewPassword);

                if(!newPasswordValidator.Check() || !confirmPasswordValidator.Check()) return;

                if (newPasswordValidator.GetValue() != confirmPasswordValidator.GetValue())
                {
                    MessageBox.Show(ErrorMessages.PASSWORDS_DO_NOT_MATCH);
                    return;
                }

                user.Password = PasswordHasher.Hash(newPasswordValidator.GetValue()!);
            }

            var birthDate = datePickerValidator.GetValue();

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
