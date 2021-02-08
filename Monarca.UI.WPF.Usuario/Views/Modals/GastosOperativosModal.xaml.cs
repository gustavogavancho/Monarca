using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class GastosOperativosModal : Window
    {
        FactoryManager _factoryManager;
        IGastoOperativoManager _gastoOperativoManager;
        GastoOperativo _gastoOperativo;
        string _operacion;

        public GastosOperativosModal(FactoryManager factoryManager, string operacion, GastoOperativo gastoOperativo = null)
        {
            _factoryManager = factoryManager;
            _operacion = operacion;
            _gastoOperativo = gastoOperativo;
            _gastoOperativoManager = factoryManager.CrearGastoOperativoManager;
            InitializeComponent();
            dtpFecha.SelectedDate = DateTime.Now;
            if (_operacion == "Edit" || _operacion == "Read")
            {
                txtNombre.Text = gastoOperativo.Nombre;
                txtDescripcion.Text = gastoOperativo.Descripcion;
                dtpFecha.SelectedDate = gastoOperativo.Fecha;
                txtCosto.Text = gastoOperativo.Costo.ToString();
            }
            if (_operacion == "Read")
            {
                btnSave.IsEnabled = false;
                txtNombre.IsReadOnly = false;
                txtDescripcion.IsReadOnly = false;
                dtpFecha.IsEnabled = false;
                txtCosto.IsReadOnly = true;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal.TryParse(txtCosto.Text, out decimal resultCosto);

            if (string.IsNullOrWhiteSpace(txtNombre.Text) && string.IsNullOrWhiteSpace(txtDescripcion.Text) && string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                DialogResult result = CustomMessageBox.Show("Todos los campos son obligatorios", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }

            switch (_operacion)
            {
                case "Add":
                    _gastoOperativoManager.Insertar(new GastoOperativo
                    {
                        Nombre = txtNombre.Text,
                        Descripcion = txtDescripcion.Text,
                        Fecha = dtpFecha.SelectedDate,
                        Costo = resultCosto,
                    });
                    break;
                case "Edit":
                    DialogResult result = CustomMessageBox.Show("¿Está seguro que desea editar los datos del gasto operativo?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        _gastoOperativoManager.Actualizar(new GastoOperativo
                        {
                            Id = _gastoOperativo.Id,
                            Nombre = txtNombre.Text,
                            Descripcion = txtDescripcion.Text,
                            Fecha = dtpFecha.SelectedDate,
                            Costo = resultCosto,
                        });
                    }
                    break;
            }

            DialogResult = true;
            Close();
        }
    }
}
