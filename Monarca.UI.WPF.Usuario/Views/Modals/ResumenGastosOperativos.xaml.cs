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
    public partial class ResumenGastosOperativos : Window
    {
        ObservableCollection<GastoOperativoResumenModel> _gastoOperativoResumen;
        IGastoOperativoManager _gastoOperativoManager;
        public ResumenGastosOperativos(FactoryManager factoryManager)
        {
            InitializeComponent();
            _gastoOperativoManager = factoryManager.CrearGastoOperativoManager;
            _gastoOperativoResumen = new ObservableCollection<GastoOperativoResumenModel>
            {
                new GastoOperativoResumenModel
                {
                    Mes = "Enero"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Febero"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Marzo"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Abril"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Mayo"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Junio"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Julio"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Agosto"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Septiembre"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Octubre"
                },
                new GastoOperativoResumenModel
                {
                    Mes = "Noviembre"
                },
                new GastoOperativoResumenModel
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

                        IEnumerable<GastoOperativo> gastoOperativo = _gastoOperativoManager.ObtenerTodo;
                        var nfi = new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

                        switch (index)
                        {
                            case 0:
                                GetData(gastoOperativo, nfi, 2021);
                                break;
                            case 1:
                                GetData(gastoOperativo, nfi, 2022);
                                break;
                            case 2:
                                GetData(gastoOperativo, nfi, 2023);
                                break;
                            case 3:
                                GetData(gastoOperativo, nfi, 2024);
                                break;
                            case 4:
                                GetData(gastoOperativo, nfi, 2025);
                                break;
                        }
                    }
                }
            }
        }

        private void GetData(IEnumerable<GastoOperativo> compras, NumberFormatInfo nfi, int año)
        {
            for (int i = 0; i < 12; i++)
            {
                int value = i + 1;
                _gastoOperativoResumen[i].Monto = compras.Where(z => z.FechaHoraCreacion.Year == año && z.FechaHoraCreacion.Month == value).Sum(x => x.Costo);
                SelectColor(i);
            }

            ltbResumenVentas.ItemsSource = _gastoOperativoResumen;
            txtTotalResumenVenta.Text = _gastoOperativoResumen.Sum(x => x.Monto).ToString("#,##0.00", nfi);
        }

        private void SelectColor(int i)
        {
            int value = i - 1;
            if ( _gastoOperativoResumen[i].Monto == 0)
            {
                _gastoOperativoResumen[i].Color = "Gray";
            }
            else
            {
                _gastoOperativoResumen[i].Color = "Green";
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
