using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.Tools.API;
using Monarca.Tools.API.Models;
using Monarca.Tools.API.Models.APIFacturadorSunat.Model;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class VentasModal : Window
    {
        FactoryManager _factoryManager;
        ObservableCollection<Producto> Productos = new ObservableCollection<Producto>();
        IVentaManager _ventaManager;
        IClienteManager _clienteManager;
        Venta _venta;
        string _operacion;

        List<TipoVenta> _tipoVenta = Enum.GetValues(typeof(TipoVenta)).Cast<TipoVenta>().ToList();

        public VentasModal(FactoryManager factoryManager, string operacion, Venta venta = null)
        {
            _operacion = operacion;
            _venta = venta;
            _factoryManager = factoryManager;
            _ventaManager = factoryManager.CrearVentaManager;
            _clienteManager = factoryManager.CrearClienteManager;
            InitializeComponent();
            cmbTipo.ItemsSource = _tipoVenta;
            dtpFechaEmision.SelectedDate = DateTime.Now;
            if (_operacion == "Read")
            {
                txtSerieDocumento.Text = venta.SerieDocumento;
                txtNumeroDocumento.Text = venta.NumeroDocumento;
                txtNombreApellidoProveedor.Text = venta.NombreProveedor;
                txtRazonSocialProveedor.Text = venta.RazonSocialProveedor;
                txtDniProveedor.Text = venta.Dni;
                txtRucProveedor.Text = venta.Ruc;
                txtNumeroDocumento.Text = venta.NumeroDocumento;
                Productos = venta.Productos;
                cmbTipo.SelectedItem = venta.TipoVenta;
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
                txbSubTotal.Text = Productos.Sum(x => x.Total).ToString("#,##0.00", nfi);
                txbTotal.Text = txbSubTotal.Text;
                txtExternalId.Text = venta.ExternalId;

                btnSave.IsEnabled = false;
                btnSelectProdcuto.IsEnabled = false;
                btnSelectProveedor.IsEnabled = false;
                btnDeleteProducto.IsEnabled = false;
                txtNombreApellidoProveedor.IsReadOnly = true;
                txtRazonSocialProveedor.IsReadOnly = true;
                txtDniProveedor.IsReadOnly = true;
                txtRucProveedor.IsReadOnly = true;
                txtNumeroDocumento.IsReadOnly = true;
                cmbTipo.IsReadOnly = true;
            }
            else if (_operacion == "NotaVenta")
            {
                cmbTipo.SelectedItem = TipoVenta.NotaDeVenta;
            }

            dtgProductos.ItemsSource = Productos;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
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

            Venta venta = new Venta
            {
                IdCliente = StaticParameters.ClienteSelected.Id,
                TipoCliente = StaticParameters.ClienteSelected.TipoCliente,
                NombreProveedor = txtNombreApellidoProveedor.Text,
                RazonSocialProveedor = txtRazonSocialProveedor.Text,
                Dni = txtDniProveedor.Text,
                Ruc = txtRucProveedor.Text,
                SerieDocumento = txtSerieDocumento.Text,
                NumeroDocumento = txtNumeroDocumento.Text,
                Productos = Productos,
                TipoVenta = (TipoVenta)cmbTipo.SelectedItem,
            };
            if(venta.TipoVenta == TipoVenta.NotaDeVenta)
            {
                _ventaManager.Insertar(venta);
            }

            else if (venta.TipoVenta == TipoVenta.Boleta)
            {
                BoletaResponse response = await ConsultaEmitirRecibo.Envio_seguro_boleta(venta);

                if (response.success)
                {
                    venta.ExternalId = response.data.external_id;
                    venta.Hash = response.data.hash;
                    venta.Qr = response.data.qr;
                    venta.linkPdf = response.links.pdf;
                    venta.linkXml = response.links.xml;
                    venta.linkCdr = response.links.cdr;
                    _ventaManager.Insertar(venta);
                }
                else
                {
                    DialogResult result = CustomMessageBox.Show(response.message, CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                    btnSave.IsEnabled = true;
                    btnClose.IsEnabled = true;
                    return;
                }
            }
            else if (venta.TipoVenta == TipoVenta.Factura)
            {
                FacturaReponse response = await ConsultaEmitirRecibo.Envio_seguro_factura(venta);
                if (response.success)
                {
                    venta.ExternalId = response.data.external_id;
                    venta.Hash = response.data.hash;
                    venta.Qr = response.data.qr;
                    venta.linkPdf = response.links.pdf;
                    venta.linkXml = response.links.xml;
                    venta.linkCdr = response.links.cdr;
                    _ventaManager.Insertar(venta);
                }
                else
                {
                    DialogResult result = CustomMessageBox.Show(response.message, CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                    btnSave.IsEnabled = true;
                    btnClose.IsEnabled = true;
                    return;
                }
            }

            DialogResult = true;
            Close();
            if (venta.TipoVenta != TipoVenta.NotaDeVenta)
            {
                Process.Start(venta?.linkPdf);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSelectProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (new SeleccionarClienteModal(_factoryManager).ShowDialog().Value)
            {
                txtNombreApellidoProveedor.Text = $"{StaticParameters.ClienteSelected.Nombres} {StaticParameters.ClienteSelected.Apellidos}";
                txtRazonSocialProveedor.Text = StaticParameters.ClienteSelected.RazonSocial;
                txtDniProveedor.Text = StaticParameters.ClienteSelected.Dni;
                txtRucProveedor.Text = StaticParameters.ClienteSelected.Ruc;

                if (_operacion != "NotaVenta")
                {
                    if (StaticParameters.ClienteSelected.TipoCliente == TipoCliente.PersonaNatural)
                    {
                        cmbTipo.SelectedItem = TipoVenta.Boleta;
                    }
                    else if (StaticParameters.ClienteSelected.TipoCliente == TipoCliente.PersonaJuridica)
                    {
                        cmbTipo.SelectedItem = TipoVenta.Factura;
                    }
                }
            }
        }

        private void btnSelectProdcuto_Click(object sender, RoutedEventArgs e)
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

        private string GenerarCodigoFactura(TipoVenta tipoVenta)
        {
            var cantidadProductos = _ventaManager.ObtenerTodo.Where(x=> x.TipoVenta == tipoVenta);

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

        private void cmbTipo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TipoVenta tipoVenta = (TipoVenta)cmbTipo.SelectedItem;
            if (tipoVenta == TipoVenta.Boleta)
            {
                txtSerieDocumento.Text = "B001";
                txtNumeroDocumento.Text = GenerarCodigoFactura(TipoVenta.Boleta);
            }
            else if (tipoVenta == TipoVenta.Factura)
            {
                txtSerieDocumento.Text = "F001";
                txtNumeroDocumento.Text = GenerarCodigoFactura(TipoVenta.Factura);
            }
            else if (tipoVenta == TipoVenta.NotaDeVenta)
            {
                txtSerieDocumento.Text = "NV001";
                txtNumeroDocumento.Text = GenerarCodigoFactura(TipoVenta.NotaDeVenta);
            }
        }
    }
}
