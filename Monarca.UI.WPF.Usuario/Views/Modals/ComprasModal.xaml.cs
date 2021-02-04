using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ComprasModal : Window
    {
        FactoryManager _factoryManager;
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
            if (_operacion == "Edit" || _operacion == "Read")
            {
                txtNombreProducto.Text = compra.NombreProducto;
                txtDescripcionProducto.Text = compra.DescripcionProducto;
                txtMarcaProducto.Text = compra.MarcaProducto;
                txtUnidadProducto.Text = compra.UnidadProducto;
                txtNombreApellidoProveedor.Text = compra.NombreProveedor;
                txtRazonSocialProveedor.Text = compra.RazonSocialProveedor;
                txtDniProveedor.Text = compra.Dni.ToString();
                txtRucProveedor.Text = compra.Ruc.ToString();
                txtCantidad.Text = compra.Cantidad.ToString();
                txtPrecioUnitario.Text = compra.Cantidad.ToString();
                txtTotal.Text = compra.Total.ToString();
            }
            if (_operacion == "Read")
            {
                btnSave.IsEnabled = false;
                btnSelectProduct.IsEnabled = false;
                btnSelectProveedor.IsEnabled = false;
                txtNombreProducto.IsReadOnly = false;
                txtDescripcionProducto.IsReadOnly = false;
                txtMarcaProducto.IsReadOnly = false;
                txtUnidadProducto.IsReadOnly = false;
                txtNombreApellidoProveedor.IsReadOnly = false;
                txtRazonSocialProveedor.IsReadOnly = false;
                txtDniProveedor.IsReadOnly = false;
                txtRucProveedor.IsReadOnly = false;
                txtCantidad.IsReadOnly = false;
                txtPrecioUnitario.IsReadOnly = false;
                txtTotal.IsReadOnly = false;
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

        private void btnSelectProduct_Click(object sender, RoutedEventArgs e)
        {
            if (new SeleccionarProductoModal(_factoryManager).ShowDialog().Value)
            {
                txtNombreProducto.Text = StaticParameters.ProductoSelected.Nombre;
                txtDescripcionProducto.Text = StaticParameters.ProductoSelected.Descripción;
                txtMarcaProducto.Text = StaticParameters.ProductoSelected.Marca;
                txtUnidadProducto.Text = StaticParameters.ProductoSelected.Unidad.ToString();
            }
        }

        private void btnSelectProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (new SeleccionarProveedorModal(_factoryManager).ShowDialog().Value)
            {
                txtNombreApellidoProveedor.Text = $"{StaticParameters.ProveedorSelected.Nombres} {StaticParameters.ProveedorSelected.Apellidos}";
                txtRazonSocialProveedor.Text = StaticParameters.ProveedorSelected.RazonSocial;
                txtDniProveedor.Text = StaticParameters.ProveedorSelected.Dni.ToString();
                txtRucProveedor.Text = StaticParameters.ProveedorSelected.Ruc.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            long.TryParse(txtDniProveedor.Text, out long resultDni);
            long.TryParse(txtRucProveedor.Text, out long resultRuc);
            decimal.TryParse(txtCantidad.Text, out decimal resultCantidad);
            decimal.TryParse(txtPrecioUnitario.Text, out decimal resultPrecioUnitario);

            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) || string.IsNullOrWhiteSpace(txtRucProveedor.Text) || string.IsNullOrWhiteSpace(txtDniProveedor.Text) || string.IsNullOrWhiteSpace(txtPrecioUnitario.Text) || string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                DialogResult result = CustomMessageBox.Show("Todos los campos son obligatorios", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }

            switch (_operacion)
            {
                case "Add":
                    _compraManager.Insertar(new Compra
                    {
                        IdProducto = StaticParameters.ProductoSelected.Id,
                        IdProveedor = StaticParameters.ProveedorSelected.Id,
                        TipoCliente = StaticParameters.ProveedorSelected.TipoCliente,
                        NombreProducto = txtNombreProducto.Text,
                        DescripcionProducto = txtDescripcionProducto.Text,
                        MarcaProducto = txtMarcaProducto.Text,
                        UnidadProducto = txtUnidadProducto.Text,
                        NombreProveedor = txtNombreApellidoProveedor.Text,
                        RazonSocialProveedor = txtRazonSocialProveedor.Text,
                        Dni = resultDni,
                        Ruc = resultRuc,
                        Cantidad = resultCantidad,
                        PrecioCosto = resultPrecioUnitario,
                        Total = resultCantidad * resultPrecioUnitario,
                    });
                    break;
                case "Edit":
                    StaticParameters.ProductoSelected = _productoManager.SearchById(_compra.IdProducto);
                    StaticParameters.ProveedorSelected = _proveedorManager.SearchById(_compra.IdProveedor);
                    DialogResult result = CustomMessageBox.Show("¿Está seguro que desea editar los datos de la compra?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        _compraManager.Actualizar(new Compra
                        {
                            Id = _compra.Id,
                            FechaHoraCreacion = _compra.FechaHoraCreacion,
                            IdProducto = StaticParameters.ProductoSelected.Id,
                            IdProveedor = StaticParameters.ProveedorSelected.Id,
                            TipoCliente = StaticParameters.ProveedorSelected.TipoCliente,
                            NombreProducto = txtNombreProducto.Text,
                            DescripcionProducto = txtDescripcionProducto.Text,
                            MarcaProducto = txtMarcaProducto.Text,
                            UnidadProducto = txtUnidadProducto.Text,
                            NombreProveedor = txtNombreApellidoProveedor.Text,
                            RazonSocialProveedor = txtRazonSocialProveedor.Text,
                            Dni = resultDni,
                            Ruc = resultRuc,
                            Cantidad = resultCantidad,
                            PrecioCosto = resultPrecioUnitario,
                            Total = resultCantidad * resultPrecioUnitario,
                        });
                    }

                    break;
            }

            DialogResult = true;
            Close();
        }

        private void txtCantidad_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtCantidad.Text, out decimal cantidad) && decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario))
            {
                txtTotal.Text = (cantidad * precioUnitario).ToString("n");
            }
            else
            {
                txtTotal.Text = "";
            }
        }
    }
}
