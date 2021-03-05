using System.ComponentModel;

namespace Monarca.UI.WPF.Usuario.Models
{
    public class GastoOperativoResumenModel : INotifyPropertyChanged
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
