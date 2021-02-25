using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Monarca.UI.WPF.Usuario.CustomControls
{
    public partial class ItemTabCustom : UserControl
    {
        private static TextBlock _texto;
        private static ICommand _command;
        /// <summary>
        /// Nueva pestaña
        /// </summary>
        public ItemTabCustom()
        {
            InitializeComponent();
            _texto = txbTitulo;
        }

        #region Eventos_Region
        /// <summary>
        /// Se produce cuando se presiona el elemento
        /// </summary>
        public event EventHandler<int> ClickItem;
        #endregion

        #region Propiedades_De_Dependencia_Region
        /// <summary>
        /// Propiedad de dependencia ligada a TextTab
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("TextTab", typeof(string), typeof(ItemTabCustom), new FrameworkPropertyMetadata()
        {
            DefaultValue = "",
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            PropertyChangedCallback = new PropertyChangedCallback(CambioValorPropiedadText),
        });

        /// <summary>
        /// Propiedad de dependencia ligada a IndexItem
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("IndexItem", typeof(int), typeof(ItemTabCustom), new FrameworkPropertyMetadata()
        {
            DefaultValue = 0,
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });

        /// <summary>
        /// Propiedad de dependencia ligada a TabCommand
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("TabCommand", typeof(ICommand), typeof(ItemTabCustom), new FrameworkPropertyMetadata()
        {
            DefaultValue = default,
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            PropertyChangedCallback = new PropertyChangedCallback(CambioValorPropiedadCommand),
        });
        #endregion

        #region Callbacks_Region
        /// <summary>
        /// Metodo que recibe la llamada de PropertyChanged de TextTab
        /// </summary>
        /// <param name="d">Objeto de dependencia de cambio de propiedad</param>
        /// <param name="e">Argumentos de cambio (valor nuevo)</param>
        private static void CambioValorPropiedadText(DependencyObject d, DependencyPropertyChangedEventArgs e) => _texto.Text = (string)e.NewValue;

        /// <summary>
        /// Metodo que recibe la llamada de PropertyChanged de TabCommand
        /// </summary>
        /// <param name="d">Objeto de dependencia de cambio de propiedad</param>
        /// <param name="e">Argumentos de cambio (valor nuevo)</param>
        private static void CambioValorPropiedadCommand(DependencyObject d, DependencyPropertyChangedEventArgs e) => _command = (ICommand)e.NewValue;
        #endregion

        #region Commands_Region
        /// <summary>
        /// Commando que se invoca cuando se da click al elemento
        /// </summary>
        public ICommand TabCommand
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region Propiedades_Full_Region
        /// <summary>
        /// Texto del elemento
        /// </summary>
        public string TextTab
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Parametro del comando
        /// </summary>
        public int IndexItem
        {
            get { return (int)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Metodos_Publicos_Region
        /// <summary>
        /// Establece la seleccion del item
        /// </summary>
        /// <param name="commandParameterIndex">Inidice de parametro</param>
        public void SetSelection(int commandParameterIndex)
        {
            txbTitulo.Foreground = commandParameterIndex == IndexItem ? (SolidColorBrush)Application.Current.FindResource("SecondaryBrush") : (SolidColorBrush)Application.Current.FindResource("PrimaryBrush");
            this.Background = commandParameterIndex == IndexItem ? (SolidColorBrush)Application.Current.FindResource("PrimaryBrush") : (SolidColorBrush)Application.Current.FindResource("SecondaryBrush");
        }
        #endregion

        #region Controladores_De_Eventos_Region
        /// <summary>
        /// Controlador de evento click del item
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_command != null)
            {
                if (_command.CanExecute(IndexItem))
                {
                    _command.Execute(IndexItem);
                }
            }
            ClickItem?.Invoke(this, IndexItem);
        }
        #endregion
    }
}
