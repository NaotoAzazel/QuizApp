using System.Windows.Controls;

namespace QuizApp.Lib.Validator
{
    static class ControlValidationExtension
    {
        public static Validator Rules(this TextBox control) => new Validator(control, control.Text);

        public static Validator Rules(this PasswordBox control) => new Validator(control, control.Password);

        public static Validator Rules(this DatePicker control)
        {
            var content = control.SelectedDate?.ToString() ?? string.Empty;
            return new Validator(control, content);
        }
    }
}
