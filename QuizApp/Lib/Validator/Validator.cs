using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizApp.Lib.Validator
{
    internal class Validator<T>
    {
        private readonly Control _control;
        private readonly T? _content;
        private bool _isValid = true;
        private TextBlock? _errorBlock;
        private string? _errorMessage;

        public Validator(Control control, T? content)
            => (_control, _content) = (control, content);

        private void SetInvalid(string message)
        {
            _isValid = false;
            _errorMessage = message;
        }

        public Validator<T> WithErrorBlock(TextBlock errorBlock)
        {
            _errorBlock = errorBlock;
            return this;
        }

        public Validator<T> MinCharacters(int count)
        {
            if (_content is string s && s.Length < count)
            {
                SetInvalid($"Minimum {count} characters required");
            }
            return this;
        }

        public Validator<T> Required()
        {
            if (_content == null)
            {
                SetInvalid("This field is required");
                return this;
            }

            if (_content is string s && string.IsNullOrWhiteSpace(s))
            {
                SetInvalid("This field is required");
            }
            return this;
        }

        public bool Check()
        {
            if (!_isValid)
            {
                _control.BorderBrush = Brushes.Red;
                _errorBlock?.SetValue(TextBlock.TextProperty, _errorMessage);
                _errorBlock?.SetValue(TextBlock.ForegroundProperty, Brushes.Red);
                _errorBlock?.SetValue(UIElement.VisibilityProperty, Visibility.Visible);
            }
            else
            {
                _control.BorderBrush = Brushes.Gray;
                _errorBlock?.SetValue(TextBlock.TextProperty, string.Empty);
                _errorBlock?.SetValue(UIElement.VisibilityProperty, Visibility.Collapsed);
            }

            return _isValid;
        }

        public static implicit operator bool(Validator<T> validator) => validator.Check();

        public T? GetValue() => _content;
    }
}
