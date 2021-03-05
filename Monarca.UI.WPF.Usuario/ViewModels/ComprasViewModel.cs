using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.CustomControls;
using Monarca.UI.WPF.Usuario.Extensions;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class ComprasViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICompraManager _compraManager;

        private ObservableCollection<Compra> _compras = new ObservableCollection<Compra>();
        public ObservableCollection<Compra> Compras
        {
            get => _compras;
            set => SetProperty(ref _compras, value);
        }

        private Compra _compra = new Compra();
        public Compra Compra
        {
            get => _compra;
            set
            {
                SetProperty(ref _compra, value);
                EditCommnad.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
                ReadCommand.RaiseCanExecuteChanged();
            }
        }

        private decimal _totalCompras;
        public decimal TotalCompras
        {
            get => _totalCompras;
            set => SetProperty(ref _totalCompras, value);
        }

        private decimal _totalComprasMensual;
        public decimal TotalComprasMensual
        {
            get => _totalComprasMensual;
            set => SetProperty(ref _totalComprasMensual, value);
        }

        private decimal _totalComprasDiario;
        public decimal TotalComprasDiario
        {
            get => _totalComprasDiario;
            set => SetProperty(ref _totalComprasDiario, value);
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

        public event Action AlmacenUpdate = delegate { };

        public RelayCommand ReadCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResumenCommand { get; set; }

        public ComprasViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _compraManager = _factoryManager.CrearCompraManager;
            ReadCommand = new RelayCommand(OnRead, CanReadEditDelete);
            AddCommand = new RelayCommand(OnAdd);
            EditCommnad = new RelayCommand(OnEdit, CanReadEditDelete);
            DeleteCommnad = new RelayCommand(OnDelete, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            ResumenCommand = new RelayCommand(OnResumen);
            UpdateData();
        }

        private void OnResumen()
        {
            new ResumenCompras(_factoryManager).ShowDialog();
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Compras = _compraManager.SearchCompra(SearchText).OrderByDescending(x => x.FechaHoraCreacion).ToObservableCollection();
                TotalCompras = Compras.Sum(x => x.Productos.Sum(y => y.Total));
                TotalComprasMensual = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));
                TotalComprasDiario = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z => z.Total));
            }
            else
            {
                Compras = _compraManager.ObtenerTodo.OrderByDescending(x=> x.FechaHoraCreacion).ToObservableCollection();
                TotalCompras = Compras.Sum(x => x.Productos.Sum(y => y.Total));
                TotalComprasMensual = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));
                TotalComprasDiario = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z => z.Total));
            }
        }

        private void OnRead()
        {
            if (new ComprasModal(_factoryManager, "Read", Compra).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnEdit()
        {
            if (new ComprasModal(_factoryManager, "Edit", Compra).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnDelete()
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información de la compra?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _compraManager.Eliminar(Compra.Id);
                UpdateData();
            }
        }

        private void OnAdd()
        {
            if (new ComprasModal(_factoryManager, "Add").ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(Compra?.Id))
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
            Compras = _compraManager.ObtenerTodo.OrderByDescending(x=> x.FechaHoraCreacion).ToObservableCollection();
            SearchText = "";
            if (Compras.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }

            TotalCompras = Compras.Sum(x => x.Productos.Sum(y => y.Total));
            TotalComprasMensual = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));
            TotalComprasDiario = Compras.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z => z.Total));

            AlmacenUpdate();
        }
    }
}