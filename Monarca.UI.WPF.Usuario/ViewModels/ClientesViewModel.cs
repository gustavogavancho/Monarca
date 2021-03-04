using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
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
                ReadCommand.RaiseCanExecuteChanged();
                EditCommnad.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private bool _visibilityListBox;
        public bool VisibilityListBox
        {
            get => _visibilityListBox;
            set => SetProperty(ref _visibilityListBox, value);
        }

        private bool _visibilityBorder;
        public bool VisibilityBorder
        {
            get => _visibilityBorder;
            set => SetProperty(ref _visibilityBorder, value);
        }

        public RelayCommand ReadCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; private set; }
        public RelayCommand SearchCommand { get; private set; }

        public ClientesViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _clienteManager = _factoryManager.CrearClienteManager;
            ReadCommand = new RelayCommand(OnRead, CanReadEditDelete);
            AddCommand = new RelayCommand(OnAdd);
            EditCommnad = new RelayCommand(OnEdit, CanReadEditDelete);
            DeleteCommnad = new RelayCommand(OnDelete, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            UpdateData();
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Clientes = _clienteManager.SearchCliente(SearchText).OrderBy(x => x.Nombres).ThenBy(x => x.Apellidos).ThenBy(x => x.RazonSocial).ToObservableCollection();
            }
            else
            {
                Clientes = _clienteManager.ObtenerTodo.OrderBy(x => x.Nombres).ThenBy(x => x.Apellidos).ThenBy(x => x.RazonSocial).ToObservableCollection();
            }
        }

        private void OnRead()
        {
            if (new ClientesModal(_factoryManager, "Read", Cliente).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnAdd()
        {
            if (new ClientesModal(_factoryManager, "Add").ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnEdit()
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
            Clientes = _clienteManager.ObtenerTodo.OrderBy(x=> x.Nombres).ThenBy(x=> x.Apellidos).ThenBy(x=> x.RazonSocial).ToObservableCollection();
            SearchText = "";
            if (Clientes.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }
        }
    }
}