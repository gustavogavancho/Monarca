using Monarca.COMMON.Entidades;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class AgregarPagoModal : Window
    {
        decimal _amount, _amountPagado;
        public AgregarPagoModal(decimal amount, decimal amountPagado)
        {
            _amount = amount;
            _amountPagado = amountPagado;
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AgregarCantidad();
        }

        private void AgregarCantidad()
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

            if (decimal.TryParse(txtMonto.Text, out decimal resultMonto))
            {
                MontoPagos montoPagos = new MontoPagos();
                montoPagos.Monto = resultMonto;
                montoPagos.Fecha = DateTime.Now;
                if (resultMonto <= _amount && resultMonto > 0)
                {
                    if (resultMonto <= _amount - _amountPagado)
                    {
                        StaticParameters.MontoCobrar = montoPagos;
                        DialogResult = true;
                    }
                    else
                    {
                        CustomMessageBox.Show($"Por favor ingrese un monto valido...\nDeuda total: {_amount.ToString("#,##0.00", nfi)} \nDeuda cancelada: {_amountPagado.ToString("#,##0.00", nfi)}", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                    }
                }
                else 
                {
                    CustomMessageBox.Show($"Por favor ingrese un monto valido...\nDeuda total: {_amount.ToString("#,##0.00", nfi)} \nDeuda cancelada: {_amountPagado.ToString("#,##0.00", nfi)}", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                }
            }
            else
            {
                CustomMessageBox.Show($"Por favor ingrese un monto valido...\nDeuda total: {_amount.ToString("#,##0.00", nfi)} \nDeuda cancelada: {_amountPagado.ToString("#,##0.00", nfi)}", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                AgregarCantidad();
            }
        }
    }
}
