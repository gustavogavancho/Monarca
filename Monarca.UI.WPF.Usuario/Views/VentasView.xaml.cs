using Monarca.UI.WPF.Usuario.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Monarca.UI.WPF.Usuario.Views
{
    public partial class VentasView : UserControl
    {
        public VentasView()
        {
            InitializeComponent();
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
                        VentasViewModel dataConext = this.DataContext as VentasViewModel;
                        if (dataConext != null)
                        {
                            if (index == 0)
                            {
                                dataConext.State = false;
                                dataConext.UpdateData(dataConext.State);
                            }

                            else if (index == 1)
                            {
                                dataConext.State = true;
                                dataConext.UpdateData(dataConext.State);
                            }
                        }
                    }
                }
            }
        }

        protected override void OnTouchLeave(TouchEventArgs e)
        {
            base.OnTouchLeave(e);
            SeleccionPestania(0);
        }
    }
}
