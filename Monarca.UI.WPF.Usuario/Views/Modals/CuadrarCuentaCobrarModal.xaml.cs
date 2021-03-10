using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.UI.WPF.Usuario.Helpers;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class CuadrarCuentaCobrarModal : Window
    {
        FactoryManager _factoryManager;
        CuentaPorCobrar _cuentaPorCobrar;
        ObservableCollection<MontoPagos> _montosPagos = new ObservableCollection<MontoPagos>();
        decimal _amount;
        decimal _amountPagado;
        public CuadrarCuentaCobrarModal(FactoryManager factoryManager, CuentaPorCobrar cuentaPorCobrar)
        {
            _factoryManager = factoryManager;
            _cuentaPorCobrar = cuentaPorCobrar;
            InitializeComponent();
            _montosPagos = cuentaPorCobrar.Pagos;
            dtgMontosPagos.ItemsSource = _montosPagos;
            txtNombreProveedor.Text = cuentaPorCobrar.Venta.NombreProveedor ?? "";
            txtRazonSocialProveedor.Text = cuentaPorCobrar.Venta.RazonSocialProveedor ?? "";
            txtDniProveedor.Text = cuentaPorCobrar.Venta.Dni ?? "";
            txtRucProveedor.Text = cuentaPorCobrar.Venta.Ruc ?? "";
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };
            _amountPagado = _montosPagos.Sum(x => x.Monto);

            txtTotalPagado.Text = _amountPagado.ToString("#,##0.00", nfi);
            _amount = cuentaPorCobrar.Venta.Productos.Sum(x => x.Total);
            txtTotalDeuda.Text = _amount.ToString("#,##0.00", nfi);
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
    }
}
