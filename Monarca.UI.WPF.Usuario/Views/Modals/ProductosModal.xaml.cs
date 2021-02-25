using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ProductosModal : Window
    {
        FactoryManager _factoryManager;
        IProductoManager _productoManager;
        Producto _producto;
        string _operacion;

        List<Unidad> _unidad = Enum.GetValues(typeof(Unidad)).Cast<Unidad>().ToList();

        public ProductosModal(FactoryManager factoryManager, string operacion, Producto producto = null)
        {
            _factoryManager = factoryManager;
            _operacion = operacion;
            _producto = producto;
            _productoManager = _factoryManager.CrearProductoManager;
            InitializeComponent();
            cmbUnidad.ItemsSource = _unidad;
            cmbUnidad.SelectedItem = Unidad.NIU;
            txtCodigoInterno.Text = GenerarCodigo();
            if (_operacion == "Edit" || _operacion == "Read")
            {
                txtCodigoInterno.Text = producto.CodigoInterno;
                txtCodigoInterno.IsEnabled = false;
                txtNombre.Text = producto.Nombre;
                txtDescripción.Text = producto.Descripción;
                txtMarca.Text = producto.Marca;
                cmbUnidad.SelectedItem = producto.Unidad;
            }
            if (_operacion == "Read")
            {
                btnSave.IsEnabled = false;
                cmbUnidad.IsEnabled = false;
                txtNombre.IsReadOnly = true;
                txtDescripción.IsReadOnly = true;
                txtMarca.IsReadOnly = true;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                DialogResult result = CustomMessageBox.Show("Debe escribir por lo menos el nombre de producto", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                return;
            }

            switch (_operacion)
            {
                case "Add":
                    _productoManager.Insertar(new Producto
                    {
                        Nombre = txtNombre.Text,
                        CodigoInterno = txtCodigoInterno.Text,
                        Descripción = txtDescripción.Text,
                        Marca = txtMarca.Text,
                        Unidad = (Unidad)cmbUnidad.SelectedItem,
                    });
                    break;
                case "Edit":
                    DialogResult result = CustomMessageBox.Show("¿Está seguro que desea editar los datos del producto?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        _productoManager.Actualizar(new Producto
                        {
                            Id = _producto.Id,
                            CodigoInterno = txtCodigoInterno.Text,
                            Nombre = txtNombre.Text,
                            Descripción = txtDescripción.Text,
                            Marca = txtMarca.Text,
                            Unidad = (Unidad)cmbUnidad.SelectedItem,
                        });
                    }
                    break;
            }

            DialogResult = true;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string GenerarCodigo()
        {
            var cantidadProductos = _productoManager.ObtenerTodo;

            if (cantidadProductos.Count() <= 0)
            {
                int codigo = 1;
                return codigo.ToString("D3");
            }
            else
            {
                List<int> listInt = cantidadProductos.Select(x => x.CodigoInterno).ToList().ConvertAll(int.Parse);
                int numero = listInt.Max() + 1;
                return numero.ToString("D3");
            }
        }
    }
}
