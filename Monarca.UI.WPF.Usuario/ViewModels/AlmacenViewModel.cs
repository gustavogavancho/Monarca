using Monarca.BIZ;
using Monarca.COMMON.Interfaces;
using Monarca.UI.WPF.Usuario.Helpers;
using System;

namespace Monarca.UI.WPF.Usuario.ViewModels
{
    public class AlmacenViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICompraManager _compraManager;

        public AlmacenViewModel(FactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
            _compraManager = factoryManager.CrearCompraManager;
            UpdateData();
        }

        private void UpdateData()
        {
            
        }
    }
}
