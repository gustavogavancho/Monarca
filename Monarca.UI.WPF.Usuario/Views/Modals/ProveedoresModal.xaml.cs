﻿using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

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
            }
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
