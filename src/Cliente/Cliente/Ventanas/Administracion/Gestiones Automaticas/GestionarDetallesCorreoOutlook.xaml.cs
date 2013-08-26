using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.Gestiones_Automaticas;
using Trascend.Bolet.Cliente.Presentadores.Administracion.Gestiones_Automaticas;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.Gestiones_Automaticas
{
    /// <summary>
    /// Lógica de interacción para GestionarDetallesCorreoOutlook.xaml
    /// </summary>
    public partial class GestionarDetallesCorreoOutlook : Page, IGestionarDetallesCorreoOutlook
    {
        private bool _cargada;
        private PresentadorGestionarDetallesCorreoOutlook _presentador;

        #region IGestionarDetallesCorreoOutlook

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public String Remitente
        {
            get { return this._txtCorreoFrom.Text; }
            set { this._txtCorreoFrom.Text = value; }
        }

        public String Destinatario
        {
            get { return this._txtCorreoTo.Text; }
            set { this._txtCorreoTo.Text = value; }
        }


        public String ConCopiaA
        {
            get { return this._txtCorreoCC.Text; }
            set { this._txtCorreoCC.Text = value; }
        }

        public String Subject
        {
            get { return this._txtCorreoSubject.Text; }
            set { this._txtCorreoSubject.Text = value; }
        }

        public String CuerpoDelCorreo
        {
            get { return this._txtCorreoBody.Text; }
            set { this._txtCorreoBody.Text = value; }
        }


        #endregion

        /// <summary>
        /// Constructor por defecto que recibe un correo a mostrar y una ventana padre
        /// </summary>
        /// <param name="correo">Correo que se va a mostrar en la ventana</param>
        /// <param name="ventanaPadre">Ventana que llama a esta forma</param>
        public GestionarDetallesCorreoOutlook(object correo, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarDetallesCorreoOutlook(this, correo, ventanaPadre);

        }




        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        } 

        #endregion


        #region Metodos

        #endregion
    }
}
