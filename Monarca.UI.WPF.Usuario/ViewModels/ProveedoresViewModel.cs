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
    public class ProveedoresViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        IProveedorManager _proveedorManager;

        private ObservableCollection<Proveedor> _proveedores = new ObservableCollection<Proveedor>();
        public ObservableCollection<Proveedor> Proveedores
        {
            get => _proveedores;
            set => SetProperty(ref _proveedores, value);
        }

        private Proveedor _proveedor = new Proveedor();
        public Proveedor Proveedor
        {
            get => _proveedor;
            set
            {
                SetProperty(ref _proveedor, value);
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
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public ProveedoresViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _proveedorManager = _factoryManager.CrearProveedorManager;
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
                Proveedores = _proveedorManager.SearchProveedor(SearchText).OrderBy(x => x.Nombres).ThenBy(x => x.Apellidos).ThenBy(x => x.RazonSocial).ToObservableCollection();
            }
            else
            {
                Proveedores = _proveedorManager.ObtenerTodo.OrderBy(x => x.Nombres).ThenBy(x => x.Apellidos).ThenBy(x => x.RazonSocial).ToObservableCollection();
            }
        }

        private void OnRead()
        {
            if (new ProveedoresModal(_factoryManager, "Read", Proveedor).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnAdd()
        {
            if (new ProveedoresModal(_factoryManager, "Add").ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnEdit()
        {
            if (new ProveedoresModal(_factoryManager, "Edit", Proveedor).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnDelete()
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información del proveedor?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _proveedorManager.Eliminar(Proveedor.Id);
                UpdateData();
            }
        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(Proveedor?.Id))
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
            Proveedores = _proveedorManager.ObtenerTodo.OrderBy(x => x.Nombres).ThenBy(x => x.Apellidos).ThenBy(x => x.RazonSocial).ToObservableCollection();
            SearchText = "";
            if (Proveedores.Count >= 1)
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
