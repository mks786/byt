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
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;
using Trascend.Bolet.Cliente.Contratos.Patentes;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Lógica de interacción para ListaPatentesPrioridadVencidaStartUp.xaml
    /// </summary>
    public partial class ListaPatentesPrioridadVencidaStartUp : Window, IListaPatentesPrioridadVencidaStartUp
    {

        private PresentadorListaPatentesPrioridadVencidaStartUp _presentador;
        private bool _cargada;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ListaPatentesPrioridadVencidaStartUp()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaPatentesPrioridadVencidaStartUp(this);
        }

        #region IListaPatentesPrioridadVencidaStartUp

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }
        
        public void FocoPredeterminado()
        {
        }

        public object PatentesPorVencerPrioridad
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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

        #endregion

        #region Metodos

        #endregion
    }
}
