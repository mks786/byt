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
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Presentadores.Plantillas;
using System.Threading;
using System.ComponentModel;


namespace Trascend.Bolet.Cliente.Ventanas.Plantillas
{
    /// <summary>
    /// Lógica de interacción para AnalizarEncabezadoPlantilla.xaml
    /// </summary>
    public partial class AnalizarEncabezadoPlantilla : Page, IAnalizarEncabezadoPlantilla
    {
        private bool _cargada;
        private PresentadorAnalizarEncabezadoPlantilla _presentador;
        
        

        public AnalizarEncabezadoPlantilla(object encabezado, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAnalizarEncabezadoPlantilla(this, encabezado, ventanaPadre);
        }


        #region IAnalizarEncabezadoPlantilla

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object EncabezadoPlantilla
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string SQLEncabezado
        {
            get {return this._txtNombreArchivoEncabezado.Text; }
            set { this._txtNombreArchivoEncabezado.Text = value; }
        }

        public object Variables
        {
            get { return this._lstVariables.DataContext; }
            set { this._lstVariables.DataContext = value; }

        }

        public string ComandoSQLPlus
        {
            get { return this._txtComandoConsola.Text; }
            set { this._txtComandoConsola.Text = value; }
        }

        public string ComandoBat
        {
            get { return this._txtComandoBatConsola.Text; }
            set { this._txtComandoBatConsola.Text = value; }
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
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnProbarEncabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EjecutarEncabezadoPlantilla();
        }

        private void _btnProbarBatEncabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EjecutarBatEncabezadoPlantilla();
        }

        private void _btnVerScriptEncabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerScript();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _chkProbarBATEncabezado_Checked(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Metodos

        public void DesactivarListaVariables()
        {
            this._lstVariables.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void MensajeAlerta(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Proceso Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void GestionarVisibilidadBat()
        {
            this._lblMensajeAnalisisBAT.Visibility = System.Windows.Visibility.Collapsed;
            this._txtComandoBatConsola.Visibility = System.Windows.Visibility.Collapsed;
            this._btnProbarBatEncabezado.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        

        
    }
}
