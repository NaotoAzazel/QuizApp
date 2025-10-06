using System.Windows.Controls;

namespace QuizApp.Lib.Validator
{
    internal class Validator
    {
        private readonly Control _control;
        private readonly string _content;
        private bool _valid = true;

        public bool IsValid => _valid;

        public Validator(Control control, string content)
            => (_control, _content) = (control, content);

        public Validator MinCharacters(int count)
        {
            if (_content.Length < count)
            {
                _valid = false;
                _control.ToolTip = $"Minimum {count} characters required";
            }
            return this;
        }

        public void Validate()
        {
            if (_valid == false)
            {
                _control.BorderBrush = System.Windows.Media.Brushes.Red;
                _control.ToolTip ??= "This field is incorrect";
            }
            else
            {
                _control.BorderBrush = System.Windows.Media.Brushes.Gray;
                _control.ToolTip = null;
            }
        }
    }
}
