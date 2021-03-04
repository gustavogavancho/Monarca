using System.ComponentModel;

namespace Monarca.UI.WPF.Usuario.Models
{
    public class CompraResumenModel : INotifyPropertyChanged
    {
        private string _mes;
        public string Mes
        {
            get => _mes;
            set
            {
                _mes = value;
                RaisePropertyChanged(nameof(Mes));
            }
        }

        private decimal _monto;
        public decimal Monto
        {
            get => _monto;
            set
            {
                _monto = value;
                RaisePropertyChanged(nameof(Monto));
            }
        }

        private decimal _crecimiento;
        public decimal Crecimiento
        {
            get => _crecimiento;
            set
            {
                _crecimiento = value;
                RaisePropertyChanged(nameof(Crecimiento));
            }
        }

        private decimal _porcentaje;
        public decimal Porcentaje
        {
            get => _porcentaje;
            set
            {
                _porcentaje = value;
                RaisePropertyChanged(nameof(Porcentaje));
            }
        }

        private string _color;
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
