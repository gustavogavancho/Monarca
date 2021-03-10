using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class CuadrarCuentaCobrarModal : Window
    {
        FactoryManager _factoryManager;
        ICuentaCobrarManager _cuentaCobrarManager;
        CuentaPorCobrar _cuentaPorCobrar;
        ObservableCollection<MontoPagos> _montosPagos = new ObservableCollection<MontoPagos>();
        decimal _amount;
        decimal _amountPagado;
        public CuadrarCuentaCobrarModal(FactoryManager factoryManager, CuentaPorCobrar cuentaPorCobrar)
        {
            InitializeComponent();
            _factoryManager = factoryManager;
            _cuentaCobrarManager = factoryManager.CrearCuentaPorCobrarManager;
            _cuentaPorCobrar = cuentaPorCobrar;

            _montosPagos = cuentaPorCobrar.Pagos;
            dtgMontosPagos.ItemsSource = _montosPagos;

            txtNombreProveedor.Text = cuentaPorCobrar.Venta.NombreProveedor ?? "";
            txtRazonSocialProveedor.Text = cuentaPorCobrar.Venta.RazonSocialProveedor ?? "";
            txtDniProveedor.Text = cuentaPorCobrar.Venta.Dni ?? "";
            txtRucProveedor.Text = cuentaPorCobrar.Venta.Ruc ?? "";

            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
            _amount = cuentaPorCobrar.Venta.Productos.Sum(x => x.Total);
            txtTotalDeuda.Text = _amount.ToString("#,##0.00", nfi);
            UpdateTotal();
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddPago_Click(object sender, RoutedEventArgs e)
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
            if (new AgregarPagoModal(_amount, _amountPagado).ShowDialog().Value)
            {
                _montosPagos.Add(StaticParameters.MontoCobrar);
                _amountPagado = _montosPagos.Sum(x => x.Monto);
                txtTotalPagado.Text = _amountPagado.ToString("#,##0.00", nfi);
            }
        }

        private void btnDeletePago_Click(object sender, RoutedEventArgs e)
        {
            MontoPagos montoSelected = (MontoPagos)dtgMontosPagos.SelectedItem;
            if (montoSelected != null)
            {
                _montosPagos.Remove(montoSelected);
                UpdateTotal();
                btnDeletePago.IsEnabled = false;
            }
        }

        private void UpdateTotal()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
            _amountPagado = _montosPagos.Sum(x => x.Monto);
            txtTotalPagado.Text = _amountPagado.ToString("#,##0.00", nfi);
        }

        private void dtgMontosPagos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dtgMontosPagos.SelectedItem != null)
            {
                btnDeletePago.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _cuentaPorCobrar.Balance = _amount - _amountPagado;
            if (_cuentaPorCobrar.Balance == 0)
            {
                _cuentaPorCobrar.Estado = true;
            }
            else
            {
                _cuentaPorCobrar.Estado = false;
            }
            _cuentaCobrarManager.Actualizar(_cuentaPorCobrar);
            DialogResult = true;
        }
    }
}
