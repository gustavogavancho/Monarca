using System.Windows;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ComprasModal : Window
    {
        public ComprasModal()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
