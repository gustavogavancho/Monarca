using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views.Modals
{
    public partial class ResumenVentas : Window
    {
        ObservableCollection<VentaResumenModel> _ventasResumen;
        IVentaManager _ventaManager;

        public ResumenVentas(FactoryManager factoryManager)
        {
            InitializeComponent();
            _ventaManager = factoryManager.CrearVentaManager;
            _ventasResumen = new ObservableCollection<VentaResumenModel>
            {
                new VentaResumenModel
                {
                    Mes = "Enero"
                },
                new VentaResumenModel
                {
                    Mes = "Febero"
                },
                new VentaResumenModel
                {
                    Mes = "Marzo"
                },
                new VentaResumenModel
                {
                    Mes = "Abril"
                },
                new VentaResumenModel
                {
                    Mes = "Mayo"
                },
                new VentaResumenModel
                {
                    Mes = "Junio"
                },
                new VentaResumenModel
                {
                    Mes = "Julio"
                },
                new VentaResumenModel
                {
                    Mes = "Agosto"
                },
                new VentaResumenModel
                {
                    Mes = "Septiembre"
                },
                new VentaResumenModel
                {
                    Mes = "Octubre"
                },
                new VentaResumenModel
                {
                    Mes = "Noviembre"
                },
                new VentaResumenModel
                {
                    Mes = "Diciembre"
                }
            };
            SeleccionPestania(0);
        }

        private void ItemTabCustom_ClickItem(object sender, int e) => SeleccionPestania(e);

        private void SeleccionPestania(int index)
        {
            if (stpPestañas != null)
            {
                foreach (UIElement item in stpPestañas.Children)
                {
                    if (item is CustomControls.ItemTabCustom tab)
                    {
                        tab.SetSelection(index);

                        IEnumerable<Venta> ventas = _ventaManager.ObtenerTodo;
                        var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

                        switch (index)
                        {
                            case 0:
                                GetData(ventas, nfi, 2021);
                                break;
                            case 1:
                                GetData(ventas, nfi, 2022);
                                break;
                            case 2:
                                GetData(ventas, nfi, 2023);
                                break;
                            case 3:
                                GetData(ventas, nfi, 2024);
                                break;
                            case 4:
                                GetData(ventas, nfi, 2025);
                                break;
                        }
                    }
                }
            }
        }

        private void GetData(IEnumerable<Venta> ventas, NumberFormatInfo nfi, int año)
        {
            for (int i = 0; i < 12; i++)
            {
                int value = i + 1;
                _ventasResumen[i].Monto = ventas.Where(z => z.Baja == false && z.FechaHoraCreacion.Year == año && z.FechaHoraCreacion.Month == value).Sum(x => x.Productos.Sum(y => y.Total));
                if (i <= 0)
                {
                    _ventasResumen[i].Crecimiento = _ventasResumen[i].Monto;
                    SelectColor(i);
                }
                else
                {
                    int sus = i - 1;
                    _ventasResumen[i].Crecimiento = _ventasResumen[i].Monto - _ventasResumen[sus].Monto;
                    SelectColor(i);
                }
                if (_ventasResumen[i].Monto > 0)
                {
                    _ventasResumen[i].Porcentaje = _ventasResumen[i].Crecimiento / _ventasResumen[i].Monto;
                }
            }
            ltbResumenVentas.ItemsSource = _ventasResumen;
            txtTotalResumenVenta.Text = _ventasResumen.Sum(x => x.Monto).ToString("#,##0.00", nfi);
        }

        private void SelectColor(int i)
        {
            int value = i - 1;
            if (_ventasResumen[i].Crecimiento == 0 || _ventasResumen[i].Monto == 0)
            {
                _ventasResumen[i].Color = "Gray";
            }
            else if (_ventasResumen[i].Crecimiento < _ventasResumen[value].Crecimiento)
            {
                _ventasResumen[i].Color = "Red";
            }
            else
            {
                _ventasResumen[i].Color = "Green";
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
