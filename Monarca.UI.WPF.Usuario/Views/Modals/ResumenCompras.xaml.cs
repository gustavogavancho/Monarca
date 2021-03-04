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
    public partial class ResumenCompras : Window
    {
        ObservableCollection<CompraResumenModel> _comprasResumen;
        ICompraManager _compraManager;

        public ResumenCompras(FactoryManager factoryManager)
        {
            InitializeComponent();
            _compraManager = factoryManager.CrearCompraManager;
            _comprasResumen = new ObservableCollection<CompraResumenModel>
            {
                new CompraResumenModel
                {
                    Mes = "Enero"
                },
                new CompraResumenModel
                {
                    Mes = "Febero"
                },
                new CompraResumenModel
                {
                    Mes = "Marzo"
                },
                new CompraResumenModel
                {
                    Mes = "Abril"
                },
                new CompraResumenModel
                {
                    Mes = "Mayo"
                },
                new CompraResumenModel
                {
                    Mes = "Junio"
                },
                new CompraResumenModel
                {
                    Mes = "Julio"
                },
                new CompraResumenModel
                {
                    Mes = "Agosto"
                },
                new CompraResumenModel
                {
                    Mes = "Septiembre"
                },
                new CompraResumenModel
                {
                    Mes = "Octubre"
                },
                new CompraResumenModel
                {
                    Mes = "Noviembre"
                },
                new CompraResumenModel
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

                        IEnumerable<Compra> compras = _compraManager.ObtenerTodo;
                        var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

                        switch (index)
                        {
                            case 0:
                                GetData(compras, nfi, 2021);
                                break;
                            case 1:
                                GetData(compras, nfi, 2022);
                                break;
                            case 2:
                                GetData(compras, nfi, 2023);
                                break;
                            case 3:
                                GetData(compras, nfi, 2024);
                                break;
                            case 4:
                                GetData(compras, nfi, 2025);
                                break;
                        }
                    }
                }
            }
        }
        
        private void GetData(IEnumerable<Compra> compras, NumberFormatInfo nfi, int año)
        {
            for (int i = 0; i < 12; i++)
            {
                int value = i + 1;
                _comprasResumen[i].Monto = compras.Where(z => z.FechaHoraCreacion.Year == año && z.FechaHoraCreacion.Month == value).Sum(x => x.Productos.Sum(y => y.Total));
                if (i <= 0)
                {
                    _comprasResumen[i].Crecimiento = _comprasResumen[i].Monto;
                    SelectColor(i);
                }
                else
                {
                    int sus = i - 1;
                    _comprasResumen[i].Crecimiento = _comprasResumen[i].Monto - _comprasResumen[sus].Monto;
                    SelectColor(i);
                }
                if (_comprasResumen[i].Monto > 0)
                {
                    _comprasResumen[i].Porcentaje = _comprasResumen[i].Crecimiento / _comprasResumen[i].Monto;
                }
            }
            ltbResumenVentas.ItemsSource = _comprasResumen;
            txtTotalResumenVenta.Text = _comprasResumen.Sum(x => x.Monto).ToString("#,##0.00", nfi);
        }

        private void SelectColor(int i)
        {
            int value = i - 1;
            if (_comprasResumen[i].Crecimiento == 0 || _comprasResumen[i].Monto == 0)
            {
                _comprasResumen[i].Color = "Gray";
            }
            else if (_comprasResumen[i].Crecimiento < _comprasResumen[value].Crecimiento)
            {
                _comprasResumen[i].Color = "Red";
            }
            else
            {
                _comprasResumen[i].Color = "Green";
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
