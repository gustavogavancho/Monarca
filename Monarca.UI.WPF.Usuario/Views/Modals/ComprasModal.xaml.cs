using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ComprasModal : Window
    {
        FactoryManager _factoryManager;
        ObservableCollection<Producto> Productos = new ObservableCollection<Producto>();
        ICompraManager _compraManager;
        IProductoManager _productoManager;
        IProveedorManager _proveedorManager;
        Compra _compra;
        string _operacion;

        public ComprasModal(FactoryManager factoryManager, string operacion, Compra compra = null)
        {
            _factoryManager = factoryManager;
            _compraManager = factoryManager.CrearCompraManager;
            _productoManager = factoryManager.CrearProductoManager;
            _proveedorManager = factoryManager.CrearProveedorManager;
            _compra = compra;
            _operacion = operacion;
            InitializeComponent();
            if (_operacion == "Edit")
            {
                txtNumeroDocumento.Text = compra.NumeroDocumento;
                txtNombreApellidoProveedor.Text = compra.NombreProveedor;
                txtRazonSocialProveedor.Text = compra.RazonSocialProveedor;
                txtDniProveedor.Text = compra.Dni;
                txtRucProveedor.Text = compra.Ruc;
                Productos = compra.Productos;
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
                txbSubTotal.Text = Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
                txbTotal.Text = txbSubTotal.Text;
            }
            else if (_operacion == "Read")
            {
                txtNumeroDocumento.Text = compra.NumeroDocumento;
                txtNombreApellidoProveedor.Text = compra.NombreProveedor;
                txtRazonSocialProveedor.Text = compra.RazonSocialProveedor;
                txtDniProveedor.Text = compra.Dni;
                txtRucProveedor.Text = compra.Ruc;
                Productos = compra.Productos;
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
                txbSubTotal.Text = Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
                txbTotal.Text = txbSubTotal.Text;

                btnSave.IsEnabled = false;
                btnSelectProdcuto.IsEnabled = false;
                btnSelectProveedor.IsEnabled = false;
                btnDeleteProducto.IsEnabled = false;
                txtNombreApellidoProveedor.IsReadOnly = true;
                txtRazonSocialProveedor.IsReadOnly = true;
                txtDniProveedor.IsReadOnly = true;
                txtRucProveedor.IsReadOnly = true;
            }
            else if (_operacion == "Add")
            {
                txtNumeroDocumento.Text = GenerarCodigo();
            }
            dtgProductos.ItemsSource = Productos;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = false;
            btnClose.IsEnabled = false;

            if (Productos.Count < 1 || string.IsNullOrWhiteSpace(txtNombreApellidoProveedor.Text) && string.IsNullOrWhiteSpace(txtRazonSocialProveedor.Text))
            {
                DialogResult result = CustomMessageBox.Show("Todos los cambios son obligatorios", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                btnSave.IsEnabled = true;
                btnClose.IsEnabled = true;
                return;
            }

            switch (_operacion)
            {
                case "Add":
                    _compraManager.Insertar(new Compra
                    {
                        IdProveedor = StaticParameters.ProveedorSelected.Id,
                        TipoCliente = StaticParameters.ProveedorSelected.TipoCliente,
                        NombreProveedor = txtNombreApellidoProveedor.Text,
                        RazonSocialProveedor = txtRazonSocialProveedor.Text,
                        Dni = txtDniProveedor.Text,
                        Ruc = txtRucProveedor.Text,
                        NumeroDocumento = txtNumeroDocumento.Text,
                        Productos = Productos,
                    });
                    break;
                case "Edit":
                    StaticParameters.ProveedorSelected = _proveedorManager.SearchById(_compra.IdProveedor);
                    DialogResult result = CustomMessageBox.Show("¿Está seguro que desea editar los datos de la compra?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        _compraManager.Actualizar(new Compra
                        {
                            Id = _compra.Id,
                            FechaHoraCreacion = _compra.FechaHoraCreacion,
                            IdProveedor = StaticParameters.ProveedorSelected.Id,
                            TipoCliente = StaticParameters.ProveedorSelected.TipoCliente,
                            NombreProveedor = txtNombreApellidoProveedor.Text,
                            RazonSocialProveedor = txtRazonSocialProveedor.Text,
                            Dni = txtDniProveedor.Text,
                            Ruc = txtRucProveedor.Text,
                            NumeroDocumento = txtNumeroDocumento.Text,
                            Productos = Productos,
                        });
                    }

                    break;
            }

            DialogResult = true;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSelectProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (new SeleccionarProveedorModal(_factoryManager).ShowDialog().Value)
            {
                txtNombreApellidoProveedor.Text = $"{StaticParameters.ProveedorSelected.Nombres} {StaticParameters.ProveedorSelected.Apellidos}";
                txtRazonSocialProveedor.Text = StaticParameters.ProveedorSelected.RazonSocial;
                txtDniProveedor.Text = StaticParameters.ProveedorSelected.Dni;
                txtRucProveedor.Text = StaticParameters.ProveedorSelected.Ruc;
            }
        }

        private void btnSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            if (new SeleccionarProductoModal(_factoryManager).ShowDialog().Value)
            {
                Productos.Add(StaticParameters.ProductoSelected);
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
            txbSubTotal.Text = Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
            txbTotal.Text = txbSubTotal.Text;
        }

        private void btnDeleteProducto_Click(object sender, RoutedEventArgs e)
        {
            Producto productoSelected = (Producto)dtgProductos.SelectedItem;
            if (productoSelected != null)
            {
                Productos.Remove(productoSelected);
                UpdateTotal();
            }
            else
            {

            }
        }
        private string GenerarCodigo()
        {
            var cantidadProductos = _compraManager.ObtenerTodo;

            if (cantidadProductos.Count() <= 0)
            {
                int codigo = 1;
                return codigo.ToString();
            }
            else
            {
                List<int> listInt = cantidadProductos.Select(x => x.NumeroDocumento).ToList().ConvertAll(int.Parse);
                int numero = listInt.Max() + 1;
                return numero.ToString();
            }
        }
    }
}
