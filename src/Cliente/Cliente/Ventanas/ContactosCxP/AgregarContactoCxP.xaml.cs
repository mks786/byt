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
using Trascend.Bolet.Cliente.Contratos.ContactosCxP;
using Trascend.Bolet.Cliente.Presentadores.ContactosCxP;

namespace Trascend.Bolet.Cliente.Ventanas.ContactosCxP
{
    /// <summary>
    /// Lógica de interacción para AgregarContactoCxP.xaml
    /// </summary>
    public partial class AgregarContactoCxP : Page, IAgregarContactoCxP
    {

        private PresentadorAgregarContactoCxP _presentador;
        private bool _cargada;


        #region IAgregarContactoCxP

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
        }

        public object ContactoCxP
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object FormasDePago
        {
            get { return this._cbxFormaPago.DataContext; }
            set { this._cbxFormaPago.DataContext = value; }
        }

        public object FormaDePago
        {
            get { return this._cbxFormaPago.SelectedItem; }
            set { this._cbxFormaPago.SelectedItem = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor predeterminado que solo carga la ventana padre
        /// <param name="ventanaPadre">Ventana Padre de esta ventana</param>
        /// </summary>
        public AgregarContactoCxP(object contactoCxP, object ventanaPadre,bool nuevoContacto)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarContactoCxP(this, contactoCxP, nuevoContacto, ventanaPadre);
        } 

        

        #endregion



        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.AlertaAgregarOModificarContactoCxP),
                    "Registro de Contacto CxP", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }
            
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        #endregion

        

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        


    }
}
