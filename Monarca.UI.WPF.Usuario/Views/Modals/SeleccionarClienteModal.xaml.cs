using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class SeleccionarClienteModal : Window
    {
        IClienteManager _clienteManager;

        public SeleccionarClienteModal(FactoryManager factoryManager)
        {
            _clienteManager = factoryManager.CrearClienteManager;
            InitializeComponent();
            ltbClientes.ItemsSource = _clienteManager.ObtenerTodo;
            if (_clienteManager.ObtenerTodo.Count() > 0)
            {
                ltbClientes.Visibility = Visibility.Visible;
                brdListItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                ltbClientes.Visibility = Visibility.Collapsed;
                brdListItem.Visibility = Visibility.Visible;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectCliente();
        }

        private void SelectCliente()
        {
            if (ltbClientes.SelectedItem == null)
            {
                DialogResult result = CustomMessageBox.Show("Debe seleccionar un cliente", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }
            StaticParameters.ClienteSelected = (Cliente)ltbClientes.SelectedItem;
            DialogResult = true;
        }

        private void Window_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                SelectCliente();
            }
        }

        private void SearchText()
        {
            string textSearch = txtSearch.Text;
            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                ltbClientes.ItemsSource = _clienteManager.SearchCliente(txtSearch.Text);
            }
            else
            {
                ltbClientes.ItemsSource = _clienteManager.ObtenerTodo;
            }
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchText();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchText();
        }
    }
}
