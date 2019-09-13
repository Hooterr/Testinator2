using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Testinator.Server
{
    /// <summary>
    /// The WPF TextBox that has extra functionality of accepting only characters that meet special regex requirement
    /// </summary>
    public class RegexTextBox : TextBox
    {
        #region Dependency Properties

        /// <summary>
        /// The regex property as string
        /// It will be converted to regular expression and will be used to check any text entered in this textbox
        /// </summary>
        public string Regex
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegexProperty =
            DependencyProperty.Register(nameof(Regex), typeof(string), typeof(RegexTextBox), new PropertyMetadata(string.Empty));

        #endregion

        #region Override Methods

        /// <summary>
        /// Fired whenever any new character is entered to textbox
        /// </summary>
        /// <param name="e">e.Text is the new Text that is checked by Regex</param>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            // Create regex object out of provided property string
            var regexObject = new Regex(Regex);

            // If new text does not match the regex
            if (!regexObject.IsMatch((e.Source as RegexTextBox).Text + e.Text))
            {
                // Make this event as handled, so no text will be applied
                e.Handled = true;
                return;
            }

            // Otherwise, simply allow this text to be entered
            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
