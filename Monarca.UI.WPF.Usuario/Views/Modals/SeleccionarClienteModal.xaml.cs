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
            SelectProveedor();
        }

        private void SelectProveedor()
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
            SelectProveedor();
        }
    }
}
