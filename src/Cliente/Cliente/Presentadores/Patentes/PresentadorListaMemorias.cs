using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Memorias;

namespace Trascend.Bolet.Cliente.Presentadores.Memorias
{
    class PresentadorListaMemorias : PresentadorBase
    {

        private IListaMemorias _ventana;
        private Patente _patente;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IPatenteServicios _patenteServicios;
        private IMemoriaServicios _memoriaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;


        private IList<Memoria> _memorias;
        private IList<ListaDatosValores> _tiposMensaje;
        private ListaDatosValores _formatoDocumento;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaMemorias(IListaMemorias ventana, object patente)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._patente = (Patente)patente;

            this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            this._memoriaServicios = (IMemoriaServicios)Activator.GetObject(typeof(IMemoriaServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MemoriaServicios"]);
            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaMemorias,
                    "");
                _memorias = this._memoriaServicios.ConsultarMemoriasPorPatente(this._patente);
                this._ventana.Memorias = _memorias;
                this._ventana.TotalHits = ((IList<Memoria>)this._ventana.Memorias).Count.ToString();

                IList<ListaDatosValores> formatosDocs = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaFormatoDoc));
                ListaDatosValores primerFormato = new ListaDatosValores();
                primerFormato.Id = "NGN";
                formatosDocs.Insert(0, primerFormato);
                this._ventana.FormatosDocumentos = formatosDocs;

                _tiposMensaje = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoMensaje));
                ListaDatosValores primerMensaje = new ListaDatosValores();
                primerMensaje.Id = "NGN";
                _tiposMensaje.Insert(0, primerMensaje);
                this._ventana.TiposMensajes = _tiposMensaje;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que invoca una nueva página "Consultar Justificación" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMemoria()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarMemoria(this._ventana.MemoriaSeleccionada, this._patente));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que invoca una nueva página "AgregarMemoria" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrAgregarMemoria()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new AgregarMemoria(this._patente));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //String field = column.Tag as String;

            //if (this._ventana.CurSortCol != null)
            //{
            //    AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
            //    this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            //}

            //ListSortDirection newDir = ListSortDirection.Ascending;
            //if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
            //    newDir = ListSortDirection.Descending;

            //this._ventana.CurSortCol = column;
            //this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            //AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            //this._ventana.ListaResultados.Items.SortDescriptions.Add(
            //    new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void ActualizarTitulo()
        {

        }


        public void Consultar()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Memoria memoria = new Memoria();

            memoria.Id = !this._ventana.IdMemoria.Equals("") ? int.Parse(this._ventana.IdMemoria) : 0;

            memoria.TipoDocumento = !((ListaDatosValores)this._ventana.FormatoDocumento).Id.Equals("NGN") ?
                        ((ListaDatosValores)this._ventana.FormatoDocumento).Valor[0] : (char?)null;
            memoria.TipoMensaje = !((ListaDatosValores)this._ventana.TipoMensaje).Id.Equals("NGN") ?
                int.Parse(((ListaDatosValores)this._ventana.TipoMensaje).Valor) : 0;

            IEnumerable<Memoria> memoriasFiltradas = this._memorias;

            if (memoria.Id != 0)
            {
                memoriasFiltradas = from m in memoriasFiltradas
                                    where m.Id == int.Parse(this._ventana.IdMemoria)
                                    select m;
            }

            if (memoria.TipoMensaje != 0)
            {
                memoriasFiltradas = from m in memoriasFiltradas
                                    where m.TipoMensaje != null &&
                                    m.TipoMensaje == memoria.TipoMensaje
                                    select m;
            }

            if (!memoria.TipoDocumento.Equals(null))
            {
                memoriasFiltradas = from m in memoriasFiltradas
                                    where m.TipoDocumento != null &&
                                    m.TipoDocumento == memoria.TipoDocumento
                                    select m;
            }


            this._ventana.ListaResultados = memoriasFiltradas.ToList<Memoria>();
            this._ventana.TotalHits = memoriasFiltradas.ToList<Memoria>().Count.ToString();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

    }
}
