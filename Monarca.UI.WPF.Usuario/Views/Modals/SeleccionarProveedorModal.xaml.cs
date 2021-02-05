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
    public partial class SeleccionarProveedorModal : Window
    {
        FactoryManager _factoryManager;
        IProveedorManager _proveedorManager;

        public SeleccionarProveedorModal(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _proveedorManager = factoryManager.CrearProveedorManager;
            InitializeComponent();
            ltbProveedores.ItemsSource = _proveedorManager.ObtenerTodo;
            if (_proveedorManager.ObtenerTodo.Count() > 1)
            {
                ltbProveedores.Visibility = Visibility.Visible;
                brdListItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                ltbProveedores.Visibility = Visibility.Collapsed;
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
            if (ltbProveedores.SelectedItem == null)
            {
                DialogResult result = CustomMessageBox.Show("Debe seleccionar un proveedor", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }
            StaticParameters.ProveedorSelected = (Proveedor)ltbProveedores.SelectedItem;
            DialogResult = true;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SelectProveedor();
        }
    }
}
