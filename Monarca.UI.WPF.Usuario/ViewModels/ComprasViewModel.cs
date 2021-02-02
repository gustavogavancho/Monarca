using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Helpers;
using Monarca.UI.WPF.Usuario.Views.Modals;
using System;
using System.Collections.ObjectModel;

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
            set => SetProperty(ref _compra, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public RelayCommand ReadCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public ComprasViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _compraManager = _factoryManager.CrearCompraManager;
            AddCommand = new RelayCommand(OnAdd);
        }

        private void OnAdd()
        {
            if (new ComprasModal().ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void UpdateData()
        {

        }
    }
}