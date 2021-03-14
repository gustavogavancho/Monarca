using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Globalization;
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
            if (_productoManager.ObtenerTodo.Count() >= 1)
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
            decimal.TryParse(txtCantidad.Text, out decimal resultCantidad);
            decimal.TryParse(txtPrecioUnitario.Text, out decimal resultPrecioUnitario);

            Producto producto = (Producto)ltbProductos.SelectedItem;
            if (producto == null || string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
            {
                DialogResult result = CustomMessageBox.Show("Todos los campos son obligatorios", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }
            StaticParameters.ProductoSelected = producto;
            StaticParameters.ProductoSelected.Cantidad = Convert.ToDecimal(txtCantidad.Text);
            StaticParameters.ProductoSelected.PrecioUnitario = resultPrecioUnitario;
            StaticParameters.ProductoSelected.Total = resultCantidad * resultPrecioUnitario;
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

        private void ltbProductos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ltbProductos.SelectedItem != null)
            {
                Producto producto = (Producto)ltbProductos.SelectedItem;
                txtUnidad.Text = producto.Unidad.GetDescription();

                Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtCantidad); }),
                    System.Windows.Threading.DispatcherPriority.Loaded);

            }
        }

        private void txtCantidad_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtCantidad.Text, out decimal cantidad) && decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario))
            {
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
                txtTotal.Text = (cantidad * precioUnitario).ToString("#,##0.00", nfi);
            }
            else
            {
                txtTotal.Text = "";
            }
        }

        private void SearchText()
        {
            string textSeach = txtSearch.Text;
            if (!string.IsNullOrWhiteSpace(textSeach))
            {
                ltbProductos.ItemsSource = _productoManager.SearchProducto(textSeach);
            }
            else
            {
                ltbProductos.ItemsSource = _productoManager.ObtenerTodo;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchText();
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchText();
        }
    }
}
