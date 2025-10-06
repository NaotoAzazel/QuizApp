using System.Windows.Controls;

namespace QuizApp.Lib.Validator
{
    static class ControlValidationExtension
    {
        public static Validator Rules(this TextBox control) => new Validator(control, control.Text);

        public static Validator Rules(this PasswordBox control) => new Validator(control, control.Password);
    }
}
