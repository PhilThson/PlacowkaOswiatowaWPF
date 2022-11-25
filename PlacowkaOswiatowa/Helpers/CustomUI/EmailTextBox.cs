using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlacowkaOswiatowa.Helpers.CustomUI
{
    public class EmailTextBox : TextBox
    {
        private static readonly Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
    }
}
