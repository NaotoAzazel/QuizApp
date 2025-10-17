using System.Windows.Controls;

namespace QuizApp.Lib.Validator
{
    static class ControlValidationExtension
    {
        public static Validator<string> Rules(this TextBox control) => new Validator<string>(control, control.Text);

        public static Validator<string> Rules(this PasswordBox control) => new Validator<string>(control, control.Password);

        public static Validator<DateTime?> Rules(this DatePicker control)
            => new Validator<DateTime?>(control, control.SelectedDate);
    }
}
