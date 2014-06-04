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
using Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Pirateria.Casos
{
    /// <summary>
    /// Lógica de interacción para ConsultarCasos.xaml
    /// </summary>
    public partial class ConsultarCasos : Page, IConsultarCasos
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private bool _cargada;
        private PresentadorConsultarCasos _presentador;

        #region IConsultarCasos

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnConsultarCasos.Focus();
        }

        public GridViewColumnHeader CurSortCol
        {
            get { return _CurSortCol; }
            set { _CurSortCol = value; }
        }

        public SortAdorner CurAdorner
        {
            get { return _CurAdorner; }
            set { _CurAdorner = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }

        public object Caso
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public string IdCaso
        {
            get { return this._txtIdCaso.Text; }
            set { this._txtIdCaso.Text = value; }
        }


        public string DescripcionCaso
        {
            get { return this._txtDescripcionCaso.Text; }
            set { this._txtDescripcionCaso.Text = value; }
        }

        public string FechaCaso
        {
            get { return this._dpkFechaCaso.Text; }
            set { this._dpkFechaCaso.Text = value; }
        }

        public object OrigenesCaso
        {
            get { return this._cbxOrigenCaso.DataContext; }
            set { this._cbxOrigenCaso.DataContext = value; }
        }

        public object OrigenCaso
        {
            get { return this._cbxOrigenCaso.SelectedItem; }
            set { this._cbxOrigenCaso.SelectedItem = value; }
        }

        public object SituacionesCaso
        {
            get { return this._cbxSituacionCaso.DataContext; }
            set { this._cbxSituacionCaso.DataContext = value; }
        }

        public object SituacionCaso
        {
            get { return this._cbxSituacionCaso.SelectedItem; }
            set { this._cbxSituacionCaso.SelectedItem = value; }
        }

        public object TiposDeCaso
        {
            get { return this._cbxTipoCaso.DataContext; }
            set { this._cbxTipoCaso.DataContext = value; }
        }

        public object TipoDeCaso
        {
            get { return this._cbxTipoCaso.SelectedItem; }
            set { this._cbxTipoCaso.SelectedItem = value; }
        }

        public object AccionesDeCaso
        {
            get { return this._cbxAccionCaso.DataContext; }
            set { this._cbxAccionCaso.DataContext = value; }
        }

        public object AccionDeCaso
        {
            get { return this._cbxAccionCaso.SelectedItem; }
            set { this._cbxAccionCaso.SelectedItem = value; }
        }

        //Filtro Asociado del Caso

        public string AsociadoFiltro
        {
            set { this._txtAsociado.Text = value; }
        }

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        //Filtro Interesado del Caso

        public string InteresadoFiltro
        {
            set { this._txtInteresado.Text = value; }
        }

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresado.Text; }
            set { this._txtNombreInteresado.Text = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object CasoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        #region Constructores

        public ConsultarCasos()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarCasos(this);
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

        private void _btnLimpiarTodo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarTodo();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _txtAsociado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
            GestionarVisibilidadFiltroInteresado(false);
        }

        private void _btnConsultarAsociadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarCasos.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = true;
            this._btnConsultarInteresado.IsDefault = false;
        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }

        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
                GestionarVisibilidadFiltroAsociado(false);
            this._btnConsultarCasos.IsDefault = true;
        }

        private void _txtInteresado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(false);
            GestionarVisibilidadFiltroInteresado(true);
        }

        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarCasos.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
        }

        private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
                GestionarVisibilidadFiltroInteresado(false);
            this._btnConsultarCasos.IsDefault = true;
        }

        private void _btnConsultarCasos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.VerCasoSeleccionado();
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

        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._txtIdCaso.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdCaso.Focus();
            }

            if (this._dpkFechaCaso.Text != null)
            {
                if (!this._dpkFechaCaso.Text.Equals(""))
                {
                    todosCamposVacios = false;
                    this._dpkFechaCaso.Focus();
                }
            }

            if ((this._cbxOrigenCaso.SelectedIndex != 0) && (this._cbxOrigenCaso.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrigenCaso.Focus();
            }

            if (!this._txtDescripcionCaso.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcionCaso.Focus();
            }

            if (!this._txtAsociado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtAsociado.Focus();
            }

            if (!this._txtInteresado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtInteresado.Focus();
            }

            if ((this._cbxSituacionCaso.SelectedIndex != 0) && (this._cbxSituacionCaso.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxSituacionCaso.Focus();
            }

            if ((this._cbxTipoCaso.SelectedIndex != 0) && (this._cbxTipoCaso.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoCaso.Focus();
            }

            if ((this._cbxAccionCaso.SelectedIndex != 0) && (this._cbxAccionCaso.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxAccionCaso.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtIdCaso.Focus();
            }

        }


        private void GestionarVisibilidadFiltroAsociado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtAsociado.Visibility = Visibility.Collapsed;

                this._txtIdAsociado.Visibility = Visibility.Visible;
                this._txtNombreAsociado.Visibility = Visibility.Visible;
                this._lblIdAsociado.Visibility = Visibility.Visible;
                this._lblNombreAsociado.Visibility = Visibility.Visible;
                this._lstAsociados.Visibility = Visibility.Visible;
                this._btnConsultarAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociado.Visibility = Visibility.Visible;

                this._txtIdAsociado.Visibility = Visibility.Collapsed;
                this._txtNombreAsociado.Visibility = Visibility.Collapsed;
                this._lblIdAsociado.Visibility = Visibility.Collapsed;
                this._lblNombreAsociado.Visibility = Visibility.Collapsed;
                this._lstAsociados.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociado.Visibility = Visibility.Collapsed;
            }
        }


        private void GestionarVisibilidadFiltroInteresado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtInteresado.Visibility = Visibility.Collapsed;

                this._txtIdInteresado.Visibility = Visibility.Visible;
                this._txtNombreInteresado.Visibility = Visibility.Visible;
                this._lblIdInteresado.Visibility = Visibility.Visible;
                this._lblNombreInteresado.Visibility = Visibility.Visible;
                this._lstInteresados.Visibility = Visibility.Visible;
                this._btnConsultarInteresado.Visibility = Visibility.Visible;

            }
            else
            {
                this._txtInteresado.Visibility = Visibility.Visible;

                this._txtIdInteresado.Visibility = Visibility.Collapsed;
                this._txtNombreInteresado.Visibility = Visibility.Collapsed;
                this._lblIdInteresado.Visibility = Visibility.Collapsed;
                this._lblNombreInteresado.Visibility = Visibility.Collapsed;
                this._lstInteresados.Visibility = Visibility.Collapsed;
                this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        
    }
}
