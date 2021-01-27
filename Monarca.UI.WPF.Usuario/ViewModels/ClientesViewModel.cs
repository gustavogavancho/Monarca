using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        IClienteManager _clienteManager;

        private ObservableCollection<Cliente> _clientes = new ObservableCollection<Cliente>();
        public ObservableCollection<Cliente> Clientes
        {
            get => _clientes;
            set => SetProperty(ref _clientes, value);
        }

        private Cliente _cliente = new Cliente();
        public Cliente Cliente
        {
            get => _cliente;
            set
            {
                SetProperty(ref _cliente, value);
                ReadClienteCommand.RaiseCanExecuteChanged();
                EditClienteCommnad.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public RelayCommand ReadClienteCommand { get; private set; }
        public RelayCommand AddClienteCommand { get; private set; }
        public RelayCommand EditClienteCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public ClientesViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _clienteManager = _factoryManager.CrearClienteManager;
            ReadClienteCommand = new RelayCommand(OnReadCliente, CanReadEditDelete);
            AddClienteCommand = new RelayCommand(OnAddCliente);
            EditClienteCommnad = new RelayCommand(OnEditCliente, CanReadEditDelete);
            DeleteCommnad = new RelayCommand(OnDelete, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            UpdateData();
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Clientes = _clienteManager.SearchCliente(SearchText).ToObservableCollection();
            }
            else
            {
                Clientes = _clienteManager.ObtenerTodo.ToObservableCollection();
            }
        }

        private void OnReadCliente()
        {
            if (new ClientesModal(_factoryManager, "Read", Cliente).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnAddCliente()
        {
            if (new ClientesModal(_factoryManager, "Add").ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnEditCliente()
        {
            if (new ClientesModal(_factoryManager, "Edit", Cliente).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnDelete()
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información del cliente?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _clienteManager.Eliminar(Cliente.Id);
                UpdateData();
            }
        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(Cliente?.Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateData()
        {
            Clientes = _clienteManager.ObtenerTodo.ToObservableCollection();
        }
    }
}