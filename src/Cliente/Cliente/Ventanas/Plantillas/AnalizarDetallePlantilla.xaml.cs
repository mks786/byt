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
    /// Lógica de interacción para AnalizarDetallePlantilla.xaml
    /// </summary>
    public partial class AnalizarDetallePlantilla : Page, IAnalizarDetallePlantilla
    {

        private bool _cargada;
        private PresentadorAnalizarDetallePlantilla _presentador;

        
        
        public AnalizarDetallePlantilla(object detalle, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAnalizarDetallePlantilla(this, detalle, ventanaPadre);
        }


        #region IAnalizarDetallePlantilla

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object DetallePlantilla
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string SQLDetalle
        {
            get { return this._txtNombreArchivoDetalle.Text; }
            set { this._txtNombreArchivoDetalle.Text = value; }
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

        private void _btnProbarDetalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EjecutarDetallePlantilla();
        }

        private void _btnProbarBatDetalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EjecutarBatDetallePlantilla();
        }

        private void _btnVerScriptDetalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerScript();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }


        #endregion


        #region Metodos

        public void DesactivarListaVariables()
        {
            this._lstVariables.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void GestionarVisibilidadBat()
        {
            this._lblMensajeAnalisisBAT.Visibility = System.Windows.Visibility.Collapsed;
            this._txtComandoBatConsola.Visibility = System.Windows.Visibility.Collapsed;
            this._btnProbarBatDetalle.Visibility = System.Windows.Visibility.Collapsed;
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


        #endregion


    }
}
