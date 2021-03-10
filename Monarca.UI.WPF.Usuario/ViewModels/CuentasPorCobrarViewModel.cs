using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class CuentasPorCobrarViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICuentaCobrarManager _cuentaCobrarPagarManager;

        private ObservableCollection<CuentaPorCobrar> _cuentasPorCobrar = new ObservableCollection<CuentaPorCobrar>();
        public ObservableCollection<CuentaPorCobrar> CuentasPorCobrar
        {
            get => _cuentasPorCobrar;
            set => SetProperty(ref _cuentasPorCobrar, value);
        }

        private CuentaPorCobrar _cuentaPorCobrar = new CuentaPorCobrar();
        public CuentaPorCobrar CuentaPorCobrar
        {
            get => _cuentaPorCobrar;
            set
            {
                SetProperty(ref _cuentaPorCobrar, value);
                ReadCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
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
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }

        public CuentasPorCobrarViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _cuentaCobrarPagarManager = _factoryManager.CrearCuentaPorCobrarManager;
            ReadCommand = new RelayCommand(OnRead, CanReadEditDelete);
            AddCommand = new RelayCommand(OnAdd);
            EditCommand = new RelayCommand(OnEdit, CanReadEditDelete);
            DeleteCommand = new RelayCommand(OnDelete, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            UpdateData();
        }

        private void OnSearch()
        {

        }

        private void OnRead()
        {

        }

        private void OnAdd()
        {
            if (new CuentaCobrarModal().ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnEdit()
        {
            if (new CuadrarCuentaCobrarModal(_factoryManager, CuentaPorCobrar).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnDelete()
        {

        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(CuentaPorCobrar?.Id))
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
            CuentasPorCobrar = _cuentaCobrarPagarManager.ObtenerTodo.OrderByDescending(x => x.Estado).ThenBy(x=> x.FechaHoraCreacion).ToObservableCollection();
            SearchText = "";
            if (CuentasPorCobrar.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }

            //TODO: Totales
        }
    }
}
