using Monarca.UI.WPF.Usuario.CustomControls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace Monarca.UI.WPF.Usuario
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult result = CustomMessageBox.Show("¿Esta seguro que desear cerrar la aplicación?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
