using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.Tools.API;
using Monarca.Tools.API.Models;
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
    public class VentasViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        IVentaManager _ventaManager;
        ICuentaCobrarManager _cuentaCobrarManager;

        private ObservableCollection<Venta> _ventas = new ObservableCollection<Venta>();
        public ObservableCollection<Venta> Ventas
        {
            get => _ventas;
            set => SetProperty(ref _ventas, value);
        }

        private Venta _venta = new Venta();
        public Venta Venta
        {
            get => _venta;
            set
            {
                SetProperty(ref _venta, value);
                ReadCommand.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
                CreditoCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isBusy = true;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
                AddCommand.RaiseCanExecuteChanged();
                DeleteCommnad.RaiseCanExecuteChanged();
                CreditoCommand.RaiseCanExecuteChanged();
            }
        }

        private decimal _totalVentas;
        public decimal TotalVentas
        {
            get => _totalVentas;
            set => SetProperty(ref _totalVentas, value);
        }

        private decimal _totalVentasMensual;
        public decimal TotalVentasMensual
        {
            get => _totalVentasMensual;
            set => SetProperty(ref _totalVentasMensual, value);
        }

        private decimal _totalVentasDiario;
        public decimal TotalVentasDiario
        {
            get => _totalVentasDiario;
            set => SetProperty(ref _totalVentasDiario, value);
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

        private bool _state;
        public bool State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public event Action CuentasCobrarUpdate = delegate { };

        public RelayCommand ReadCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommnad { get; private set; }
        public RelayCommand DeleteCommnad { get; set; }
        public RelayCommand CreditoCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResumenCommand { get; set; }

        public VentasViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _ventaManager = factoryManager.CrearVentaManager;
            _cuentaCobrarManager = factoryManager.CrearCuentaPorCobrarManager;
            ReadCommand = new RelayCommand(OnRead, CanReadEditDelete);
            AddCommand = new RelayCommand(OnAdd,CanAdd);
            DeleteCommnad = new RelayCommand(OnDelete, CanReadEditDelete);
            CreditoCommand = new RelayCommand(OnCredito, CanReadEditDelete);
            SearchCommand = new RelayCommand(OnSearch);
            ResumenCommand = new RelayCommand(OnResumen);
            UpdateData(State);
        }

        private void OnCredito()
        {
            if (Venta.Credito == false)
            {
                DialogResult result = CustomMessageBox.Show("¿Está seguro que desea marcar la venta como crédito?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Venta.Credito = true;
                    _ventaManager.Actualizar(Venta);
                    CuentaPorCobrar cuentaPorCobrar = new CuentaPorCobrar
                    {
                        Venta = Venta,
                        FechaHoraCreacion = Venta.FechaHoraCreacion,
                        TipoCliente = Venta.TipoCliente,
                        TotalCobrar = Venta.Productos.Sum(x => x.Total),
                        Balance = Venta.Productos.Sum(x => x.Total),
                    };
                    _cuentaCobrarManager.Insertar(cuentaPorCobrar);
                    UpdateData(State);
                }
            }
            else
            {
                DialogResult result = CustomMessageBox.Show("¿Está seguro que desea desmarcar la venta como crédito?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Venta.Credito = false;
                    _ventaManager.Actualizar(Venta);
                    CuentaPorCobrar cuentaPagarCobrar = _cuentaCobrarManager.ObtenerTodo.Where(x => x.Venta.Id == Venta.Id).FirstOrDefault();
                    _cuentaCobrarManager.Eliminar(cuentaPagarCobrar.Id);
                    UpdateData(State);
                }
            }
        }

        private void OnResumen()
        {
            new ResumenVentas(_factoryManager).ShowDialog();
        }

        private void OnRead()
        {
            if (new VentasModal(_factoryManager, "Read", Venta).ShowDialog().Value)
            {
                UpdateData(State);
            }
        }

        private void OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Ventas = _ventaManager.SearchVenta(SearchText).OrderByDescending(x => x.FechaHoraCreacion).Where(x => x.Baja == State).ToObservableCollection();
                TotalVentas = Ventas.Sum(x => x.Productos.Sum(y => y.Total));
                TotalVentasMensual = Ventas.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));
                TotalVentasDiario = Ventas.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z => z.Total));
            }
            else
            {
                Ventas = _ventaManager.ObtenerTodo.OrderByDescending(x => x.FechaHoraCreacion).Where(x => x.Baja == State).ToObservableCollection();
                TotalVentas = Ventas.Sum(x => x.Productos.Sum(y => y.Total));
                TotalVentasMensual = Ventas.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));
                TotalVentasDiario = Ventas.Where(x => x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z => z.Total));
            }
        }

        private async void OnDelete()
        {
            IsBusy = false;
            DialogResult result = CustomMessageBox.Show("¿Está seguro que desea borrar la información de la venta?", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.Si, CustomMessageBox.CMessageBoxButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (Venta != null && Venta.TipoVenta == TipoVenta.Factura && !Venta.Baja)
                {
                    BajaResponse response = await ConsultaBaja.Baja_factura(Venta);
                    if (response.success)
                    {
                        Venta.Baja = true;
                        Venta.ExternalIdBaja = response.data.external_id;
                        Venta.Ticket = response.data.ticket;
                        _ventaManager.Actualizar(Venta);
                        DeleteCuentaCobrar();
                        UpdateData(State);
                        CustomMessageBox.Show("Se dio de baja la boleta/factura/nota de venta correctamente", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                        IsBusy = true;
                    }
                    else
                    {
                        CustomMessageBox.Show("No se pudo dar de baja a la factura/boleta/nota de venta seleccionada, por favor intentelo de nuevo o contacte con su proveedor", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                    }
                }
                else if (Venta != null && Venta.TipoVenta == TipoVenta.Boleta && !Venta.Baja)
                {
                    BajaResponse response = await ConsultaBaja.Baja_boleta(Venta);
                    if (response.success)
                    {
                        Venta.Baja = true;
                        Venta.ExternalIdBaja = response.data.external_id;
                        Venta.Ticket = response.data.ticket;
                        _ventaManager.Actualizar(Venta);
                        DeleteCuentaCobrar();
                        UpdateData(State);
                        CustomMessageBox.Show("Se dio de baja la boleta/factura/nota de venta correctamente", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                        IsBusy = true;
                    }
                    else
                    {
                        CustomMessageBox.Show("No se pudo dar de baja a la factura/boleta/ nota de venta seleccionada, por favor intentelo de nuevo o contacte con su proveedor", CustomMessageBox.CMessageBoxTitle.Advertencia, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                    }
                }
                else if (Venta != null && Venta.TipoVenta == TipoVenta.NotaDeVenta && !Venta.Baja)
                {
                    Venta.Baja = true;
                    _ventaManager.Actualizar(Venta);
                    DeleteCuentaCobrar();
                    UpdateData(State);
                    CustomMessageBox.Show("Se dio de baja la boleta/factura/nota de venta correctamente", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                }
                else
                {
                    CustomMessageBox.Show("La venta ya se encuentra dada de baja", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                }
            }
            else
            {
                IsBusy = true;
            }
        }

        private void DeleteCuentaCobrar()
        {
            if (Venta.Credito)
            {
                CuentaPorCobrar cuentaPagarCobrar = _cuentaCobrarManager.ObtenerTodo.Where(x => x.Venta.Id == Venta.Id).FirstOrDefault();
                _cuentaCobrarManager.Eliminar(cuentaPagarCobrar.Id);
            }
        }

        private void OnAdd()
        {
            DialogResult result = CustomMessageBox.Show("Seleccione el tipo de recibo", CustomMessageBox.CMessageBoxTitle.Confirmación, CustomMessageBox.CMessageBoxButton.NotaDeVenta, CustomMessageBox.CMessageBoxButton.BoletaFactura);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (new VentasModal(_factoryManager, "NotaVenta").ShowDialog().Value)
                {
                    UpdateData(State);
                    CustomMessageBox.Show("Se guardó la boleta/factura/nota de venta correctamente", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                }
            }
            else if(result == System.Windows.Forms.DialogResult.No)
            {
                if (new VentasModal(_factoryManager, "Add").ShowDialog().Value)
                {
                    UpdateData(State);
                    CustomMessageBox.Show("Se guardó la boleta/factura/nota de venta correctamente", CustomMessageBox.CMessageBoxTitle.Información, CustomMessageBox.CMessageBoxButton.Aceptar, CustomMessageBox.CMessageBoxButton.Cancelar);
                }
            }
        }

        public bool CanAdd()
        {
            if (IsBusy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanReadEditDelete()
        {
            if (IsBusy && !string.IsNullOrWhiteSpace(Venta?.Id) && !State)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateData(bool baja)
        {
            State = baja;
            Ventas = _ventaManager.ObtenerTodo.OrderByDescending(x=> x.FechaHoraCreacion).Where(x=> x.Baja == baja).ToObservableCollection();
            SearchText = "";
            if (Ventas.Count >= 1)
            {
                VisibilityListBox = true;
                VisibilityBorder = false;
            }
            else
            {
                VisibilityBorder = true;
                VisibilityListBox = false;
            }

            
            TotalVentas = Ventas.Sum(x => x.Productos.Sum(y => y.Total));

            TotalVentasMensual = Ventas.Where(x=> x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month).Sum(x => x.Productos.Sum(z => z.Total));

            TotalVentasDiario = Ventas.Where(x=> x.FechaHoraCreacion.Year == DateTime.Now.Year && x.FechaHoraCreacion.Month == DateTime.Now.Month && x.FechaHoraCreacion.Day == DateTime.Now.Day).Sum(x => x.Productos.Sum(z=> z.Total));

            CuentasCobrarUpdate();
        }
    }
}
