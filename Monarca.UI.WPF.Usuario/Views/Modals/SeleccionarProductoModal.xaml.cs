using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class SeleccionarProductoModal : Window
    {
        FactoryManager _factoryManager;
        IProductoManager _productoManager;

        public event Action<Producto> Producto = delegate { };

        public SeleccionarProductoModal(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _productoManager = factoryManager.CrearProductoManager;
            InitializeComponent();
            ltbProductos.ItemsSource = _productoManager.ObtenerTodo;
            if (_productoManager.ObtenerTodo.Count() > 1)
            {
                ltbProductos.Visibility = Visibility.Visible;
                brdListItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                ltbProductos.Visibility = Visibility.Collapsed;
                brdListItem.Visibility = Visibility.Visible;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectProduct();
        }

        private void SelectProduct()
        {
            if (ltbProductos.SelectedItem == null)
            {
                DialogResult result = CustomMessageBox.Show("Debe seleccionar un producto", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }
            StaticParameters.ProductoSelected = (Producto)ltbProductos.SelectedItem;
            DialogResult = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                SelectProduct();
            }
        }
    }
}
