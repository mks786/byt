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

namespace Trascend.Bolet.Cliente.Ventanas.Plantillas
{
    /// <summary>
    /// Lógica de interacción para GestionarMaestroPlantilla.xaml
    /// </summary>
    public partial class GestionarMaestroPlantilla : Page, IGestionarMaestroPlantilla
    {

        private bool _cargada;
        private PresentadorGestionarMaestroPlantilla _presentador;


        /// <summary>
        /// Constructor por defecto que solamente recibe un maestro de Plantilla con todos sus datos
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de plantilla seleccionado</param>
        public GestionarMaestroPlantilla(object maestroPlantilla)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarMaestroPlantilla(this, maestroPlantilla,null);
        }

        /// <summary>
        /// Constructor por defecto que recibe un maestro de plantilla y una ventana padre
        /// </summary>
        /// <param name="maestroPlantilla">Datos maestros de una plantilla seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarMaestroPlantilla(object maestroPlantilla, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarMaestroPlantilla(this, maestroPlantilla, ventanaPadre);
        }


        #region IGestionarMaestroPlantilla

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }


        public object DatosMaestrosPlantilla
        {
            get { return this._grdPlantilla.DataContext; }
            set { this._grdPlantilla.DataContext = value; }
        }


        public object Plantillas
        {
            get { return this._cbxPlantilla.DataContext; }
            set { this._cbxPlantilla.DataContext = value; }
        }

        public object Plantilla
        {
            get { return this._cbxPlantilla.SelectedItem; }
            set { this._cbxPlantilla.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdioma.DataContext; }
            set { this._cbxIdioma.DataContext = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdioma.SelectedItem; }
            set { this._cbxIdioma.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return _cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Departamento
        {
            get { return _cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return _cbxUsuario.DataContext; }
            set { this._cbxUsuario.DataContext = value; }
        }


        public object Usuario
        {
            get { return _cbxUsuario.SelectedItem; }
            set { this._cbxUsuario.SelectedItem = value; }
        }


        public object Referencias
        {
            get { return _cbxReferido.DataContext; }
            set { this._cbxReferido.DataContext = value; }
        }

        public object Referencia
        {
            get { return _cbxReferido.SelectedItem; }
            set { this._cbxReferido.SelectedItem = value; }
        }


        public object Criterios
        {
            get { return _cbxCriterio.DataContext; }
            set { this._cbxCriterio.DataContext = value; }
        }

        public object Criterio
        {
            get { return _cbxCriterio.SelectedItem; }
            set { this._cbxCriterio.SelectedItem = value; }
        }


        public object ArchivosEncabezado
        {
            get { return _cbxArchivoSQL_Encabezado.DataContext; }
            set { this._cbxArchivoSQL_Encabezado.DataContext = value; }
        }


        public object ArchivoEncabezado
        {
            get { return _cbxArchivoSQL_Encabezado.SelectedItem; }
            set { this._cbxArchivoSQL_Encabezado.SelectedItem = value; }
        }


        public object ArchivosBat
        {
            get { return _cbxArchivoBat.DataContext; }
            set { this._cbxArchivoBat.DataContext = value; }
        }

        public object ArchivoBat
        {
            get { return _cbxArchivoBat.SelectedItem; }
            set { this._cbxArchivoBat.SelectedItem = value; }
        }

        public object ArchivosDetalle
        {
            get { return _cbxArchivoSQL_Detalle.DataContext; }
            set { this._cbxArchivoSQL_Detalle.DataContext = value; }
        }

        public object ArchivoDetalle
        {
            get { return _cbxArchivoSQL_Detalle.SelectedItem; }
            set { this._cbxArchivoSQL_Detalle.SelectedItem = value; }
        }

        public object ArchivosBatDetalle
        {
            get { return this._cbxArchivoBatDetalle.DataContext; }
            set { this._cbxArchivoBatDetalle.DataContext = value; }
        }

        public object ArchivoBatDetalle
        {
            get { return this._cbxArchivoBatDetalle.SelectedItem; }
            set { this._cbxArchivoBatDetalle.SelectedItem = value; }
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

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnAnalizarEncabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ProbarEncabezado();
        }

        private void _btnAnalizarVariablesWhere_Encabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerFiltrosEncabezadoPlantilla();
        }

        private void _btnAnalizarDetalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ProbarDetalle();
        }

        private void _btnAnalizarVariablesWhere_Detalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerFiltrosDetallePlantilla();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        #endregion


        #region Metodos

        public void MensajeAlerta(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void PintarBotonVariablesEncabezado()
        {
            this._btnAnalizarVariablesWhere_Encabezado.Background = Brushes.LightGreen;
        }


        public void PintarBotonVariablesDetalle()
        {
            this._btnAnalizarVariablesWhere_Detalle.Background = Brushes.LightGreen;
        }


        public void ActivarBotonVariables(bool valor)
        {
            this._btnAnalizarVariablesWhere_Encabezado.IsEnabled = valor;
            this._btnAnalizarVariablesWhere_Detalle.IsEnabled = valor;
        }

        #endregion

        


    }
}
