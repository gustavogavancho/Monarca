﻿using Monarca.BIZ;
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
    public class GastosOperativosViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        IGastoOperativoManager _gastoOperativoManager;

        private ObservableCollection<GastoOperativo> _gastosOperativos = new ObservableCollection<GastoOperativo>();
        public ObservableCollection<GastoOperativo> GastosOperativos
        {
            get => _gastosOperativos;
            set => SetProperty(ref _gastosOperativos, value);
        }

        private GastoOperativo _gastoOperativo = new GastoOperativo();
        public GastoOperativo GastoOperativo
        {
            get => _gastoOperativo;
            set
            {
                SetProperty(ref _gastoOperativo, value);
                ReadCommand.RaiseCanExecuteChanged();
                EditCommnad.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
            }
        }

        private decimal _totalGastosOperativos;
        public decimal TotalGastosOperativos
        {
            get => _totalGastosOperativos;
            set => SetProperty(ref _totalGastosOperativos, value);
        }

        private decimal _totalGastosOperativosMensual;
        public decimal TotalGastosOperativosMensual
        {
            get => _totalGastosOperativosMensual;
            set => SetProperty(ref _totalGastosOperativosMensual, value);
        }

        private decimal _totalGastosOperativosDiario;
        public decimal TotalGastosOperativosDiario
        {
            get => _totalGastosOperativosDiario;
            set => SetProperty(ref _totalGastosOperativosDiario, value);
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
        public RelayCommand ResumenCommand { get; set; }

        public GastosOperativosViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _gastoOperativoManager = factoryManager.CrearGastoOperativoManager;
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
            new ResumenGastosOperativos(_factoryManager).ShowDialog();
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                GastosOperativos = _gastoOperativoManager.SearchGastoOperativo(SearchText).OrderByDescending(x=> x.FechaHoraCreacion).ToObservableCollection();
                TotalGastosOperativos = GastosOperativos.Sum(x => x.Costo);
                TotalGastosOperativosMensual = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Costo);
                TotalGastosOperativosDiario = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Costo);

            }
            else
            {
                GastosOperativos = _gastoOperativoManager.ObtenerTodo.OrderByDescending(x => x.FechaHoraCreacion).ToObservableCollection();
                TotalGastosOperativos = GastosOperativos.Sum(x => x.Costo);
                TotalGastosOperativosMensual = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Costo);
                TotalGastosOperativosDiario = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Costo);
            }
        }

        private void OnDelete()
        {
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información del gasto operativo?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _gastoOperativoManager.Eliminar(GastoOperativo.Id);
                UpdateData();
            }
        }

        private void OnEdit()
        {
            if (new GastosOperativosModal(_factoryManager, "Edit", GastoOperativo).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnAdd()
        {
            if (new GastosOperativosModal(_factoryManager, "Add").ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private void OnRead()
        {
            if (new GastosOperativosModal(_factoryManager, "Read", GastoOperativo).ShowDialog().Value)
            {
                UpdateData();
            }
        }

        private bool CanReadEditDelete()
        {
            if (!string.IsNullOrWhiteSpace(GastoOperativo?.Id))
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
            GastosOperativos = _gastoOperativoManager.ObtenerTodo.OrderByDescending(x=> x.FechaHoraCreacion).ToObservableCollection();
            SearchText = "";
            if (GastosOperativos.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }
            TotalGastosOperativos = GastosOperativos.Sum(x => x.Costo);
            TotalGastosOperativosMensual = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Costo);
            TotalGastosOperativosDiario = GastosOperativos.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Costo);
        }
    }
}
