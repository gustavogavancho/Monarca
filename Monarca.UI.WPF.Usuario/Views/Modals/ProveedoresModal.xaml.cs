using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.Tools.API;
using Monarca.Tools.API.Models;
using Monarca.UI.WPF.Usuario.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ProveedoresModal : Window
    {
        FactoryManager _factoryManager;
        IProveedorManager _proveedorManager;
        Proveedor _proveedor;
        string _operacion;

        List<TipoCliente> _tipoCliente = Enum.GetValues(typeof(TipoCliente)).Cast<TipoCliente>().ToList();

        public ProveedoresModal(FactoryManager factoryManager, string operacion, Proveedor proveedor = null)
        {
            _factoryManager = factoryManager;
            _operacion = operacion;
            _proveedor = proveedor;
            _proveedorManager = _factoryManager.CrearProveedorManager;
            InitializeComponent();
            cmbTipoCliente.ItemsSource = _tipoCliente;
            if (_operacion == "Edit" || _operacion == "Read")
            {
                cmbTipoCliente.SelectedItem = proveedor.TipoCliente;
                txtNombres.Text = proveedor.Nombres;
                txtApellidos.Text = proveedor.Apellidos;
                txtRazonSocial.Text = proveedor.RazonSocial;
                txtRepresentanteLegal.Text = proveedor.RepresentanteLegal;
                txtDireccion.Text = proveedor.Direccion;
                txtEmail.Text = proveedor.Email;
                txtRUC.Text = proveedor.Ruc.ToString();
                txtDNI.Text = proveedor.Dni.ToString();
                txtCelular.Text = proveedor.Celular.ToString();
            }
            if (_operacion == "Read")
            {
                btnSave.IsEnabled = false;
                cmbTipoCliente.IsEnabled = false;
                txtNombres.IsReadOnly = true;
                txtApellidos.IsReadOnly = true;
                txtRazonSocial.IsReadOnly = true;
                txtRepresentanteLegal.IsReadOnly = true;
                txtDireccion.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
                txtRUC.IsReadOnly = true;
                txtDNI.IsReadOnly = true;
                txtCelular.IsReadOnly = true;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            long.TryParse(txtDNI.Text, out long resultDni);
            long.TryParse(txtRUC.Text, out long resultRuc);
            long.TryParse(txtCelular.Text, out long resultCelular);

            if (cmbTipoCliente.SelectedItem == null)
            {
                DialogResult result = CustomMessageBox.Show("Debe seleccionar por lo menos un tipo de proveedor", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }

            switch (_operacion)
            {
                case "Add":
                    _proveedorManager.Insertar(new Proveedor
                    {
                        TipoCliente = (TipoCliente)cmbTipoCliente.SelectedItem,
                        Nombres = txtNombres.Text,
                        Apellidos = txtApellidos.Text,
                        RazonSocial = txtRazonSocial.Text,
                        RepresentanteLegal = txtRepresentanteLegal.Text,
                        Direccion = txtDireccion.Text,
                        Email = txtEmail.Text,
                        Ruc = resultRuc,
                        Dni = resultDni,
                        Celular = resultCelular,
                    });
                    break;
                case "Edit":
                    DialogResult result = CustomMessageBox.Show("¿Está seguro que desea editar los datos del proveedor?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        _proveedorManager.Actualizar(new Proveedor
                        {
                            Id = _proveedor.Id,
                            TipoCliente = (TipoCliente)cmbTipoCliente.SelectedItem,
                            Nombres = txtNombres.Text,
                            Apellidos = txtApellidos.Text,
                            RazonSocial = txtRazonSocial.Text,
                            RepresentanteLegal = txtRepresentanteLegal.Text,
                            Direccion = txtDireccion.Text,
                            Email = txtEmail.Text,
                            Ruc = resultRuc,
                            Dni = resultDni,
                            Celular = resultCelular,
                        });
                    }
                    break;
            }

            DialogResult = true;
            Close();
        }

        private void cmbTipoCliente_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((TipoCliente)cmbTipoCliente.SelectedItem == TipoCliente.PersonaNatural)
            {
                txtRazonSocial.IsEnabled = false;
                txtRazonSocial.Text = "";
                txtRepresentanteLegal.IsEnabled = false;
                txtRepresentanteLegal.Text = "";
                txtRUC.IsEnabled = false;
                txtRUC.Text = "";

                txtNombres.IsEnabled = true;
                txtApellidos.IsEnabled = true;
                txtDNI.IsEnabled = true;

                Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtDNI); }),
                    System.Windows.Threading.DispatcherPriority.Loaded);

            }
            else
            {
                txtNombres.IsEnabled = false;
                txtNombres.Text = "";
                txtApellidos.IsEnabled = false;
                txtApellidos.Text = "";
                txtDNI.IsEnabled = false;
                txtDNI.Text = "";

                txtRazonSocial.IsEnabled = true;
                txtRepresentanteLegal.IsEnabled = true;
                txtRUC.IsEnabled = true;

                Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtRUC); }),
                    System.Windows.Threading.DispatcherPriority.Loaded);
            }
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private async void txtDNI_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                bool canConvert = int.TryParse(txtDNI.Text, out int Dni);
                if (canConvert && txtDNI.Text.Length == 8)
                {
                    try
                    {
                        DNI dni = await ConsultaDni.GetDni(txtDNI.Text);
                        txtNombres.Text = dni.nombres;
                        txtApellidos.Text = $"{dni.apellidoPaterno} {dni.apellidoMaterno}";

                        await Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtDireccion); }),
                            System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                    catch (Exception ex)
                    {
                        DialogResult result = CustomMessageBox.Show($"{ex.Message}", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.No);
                    }
                }
            }
        }

        private async void btnConsultaDni_Click(object sender, RoutedEventArgs e)
        {
            bool canConvert = int.TryParse(txtDNI.Text, out int Dni);
            if (canConvert && txtDNI.Text.Length == 8)
            {
                try
                {
                    DNI dni = await ConsultaDni.GetDni(txtDNI.Text);
                    txtNombres.Text = dni.nombres;
                    txtApellidos.Text = $"{dni.apellidoPaterno} {dni.apellidoMaterno}";

                    await Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtDireccion); }),
                        System.Windows.Threading.DispatcherPriority.Loaded);
                }
                catch (Exception ex)
                {
                    DialogResult result = CustomMessageBox.Show($"{ex.Message}", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.No);
                }
            }
        }

        private async void txtRUC_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool canConvert = long.TryParse(txtRUC.Text, out long Ruc);
            if (canConvert && txtRUC.Text.Length == 11)
            {
                try
                {
                    RUC ruc = await ConsultaRuc.GetRuc(txtRUC.Text);
                    txtRazonSocial.Text = ruc.razonSocial;
                    txtDireccion.Text = ruc.direccion;
                    await Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtRepresentanteLegal); }),
                        System.Windows.Threading.DispatcherPriority.Loaded);

                }
                catch (Exception ex)
                {
                    DialogResult result = CustomMessageBox.Show($"{ex.Message}", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.No);
                }
            }
        }

        private async void btnConsultaRuc_Click(object sender, RoutedEventArgs e)
        {
            bool canConvert = long.TryParse(txtRUC.Text, out long Ruc);
            if (canConvert && txtRUC.Text.Length == 11)
            {
                try
                {
                    RUC ruc = await ConsultaRuc.GetRuc(txtRUC.Text);
                    txtRazonSocial.Text = ruc.razonSocial;
                    txtDireccion.Text = ruc.direccion;

                    await Dispatcher.BeginInvoke(new System.Action(() => { Keyboard.Focus(txtRepresentanteLegal); }),
                        System.Windows.Threading.DispatcherPriority.Loaded);

                }
                catch (Exception ex)
                {
                    DialogResult result = CustomMessageBox.Show($"{ex.Message}", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.No);
                }
            }
        }
    }
}
