using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Contratos.TipoFechas;
using Trascend.Bolet.Cliente.Presentadores.TipoFechas;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.TipoFechas
{
    /// <summary>
    /// Interaction logic for ConsultarTipoFecha.xaml
    /// </summary>
    public partial class ConsultarTipoFecha : Page, IConsultarTipoFecha
    {
        private PresentadorConsultarTipoFecha _presentador;
        private bool _cargada;

        #region IConsultarTipoFecha

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object TipoFecha
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set { this._txtDescripcion.IsEnabled = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion


        public ConsultarTipoFecha(object fecha)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarTipoFecha(this, (TipoFecha)fecha);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarTipoFecha, "Eliminar TipoFecha", MessageBoxButton.YesNo, MessageBoxImage.Question))
                this._presentador.Eliminar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }
    }
}