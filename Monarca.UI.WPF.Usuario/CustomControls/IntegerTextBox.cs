using System.Windows.Controls;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.CustomControls
{
    public class IntegerTextBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            e.Handled = !int.TryParse(e.Text, out int result);
        }
    }
}
