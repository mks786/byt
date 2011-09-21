using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class ConsultarPoder : Page, IConsultarPoder
    {
        private PresentadorConsultarPoder _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private bool _cargada;
        private SortAdorner _CurAdorner = null;

        #region IConsultarPoder

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Poder
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtNumPoder.IsEnabled = value;
                this._txtFacultad.IsEnabled = value;
                this._cbxBoletin.IsEnabled = value;
                this._lstInteresados.IsEnabled = value;
                this._txtAnexo.IsEnabled = value;
                this._txtObservaciones.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }


        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value;}
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set 
            { 
                this._lstInteresados.SelectedItem = value; 
                this._lstInteresados.ScrollIntoView(value); 
            }
        }

        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorPoderNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            get { return this._lstInteresados; }
            set { this._lstInteresados = value; }
        }

        #endregion

        public ConsultarPoder(object poder,object Boletines,object Interesados)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPoder(this, poder,Boletines,Interesados);
        }

        public ConsultarPoder(object poder)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPoder(this, poder);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPoder, "Eliminar Poder", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnVerPoder_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AbrirPoder();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

    }
}
