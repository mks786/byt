using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Justificaciones;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorListaInteresadosPatente : PresentadorBase
    {
        private IListaInteresadosPatente _ventana;
        private Patente _patente;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPatenteServicios _patenteServicios;
        private IInteresadoMultipleServicios _interesadoMultipleServicios;
        private InteresadoMultiple _interesadoMultiple;
        private object _ventanaConsultarPatentes;
        
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorListaInteresadosPatente(IListaInteresadosPatente ventana, object patente, object ventanaPadre, object ventanaConsultarPatentes)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            this._ventanaPadre = ventanaPadre;
            this._patente = (Patente)patente;
            if (ventanaConsultarPatentes != null)
                this._ventanaConsultarPatentes = ventanaConsultarPatentes;
            
            this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            this._interesadoMultipleServicios = (IInteresadoMultipleServicios)Activator.GetObject(typeof(IInteresadoMultipleServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoMultipleServicios"]);

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
            IList<Interesado> interesadosDePatente = new List<Interesado>();

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaInteresadosPatente,
                    Recursos.Ids.InteresadoPatente);

                this._patente = this._patenteServicios.ConsultarPatenteConTodo(_patente);
                
                IList<InteresadoMultiple> listaInteresados = this._interesadoMultipleServicios.ConsultarInteresadosDePatente(this._patente);
                
                if (listaInteresados.Count > 0)
                {
                    InteresadoMultiple intPatente = listaInteresados[0];
                    this._interesadoMultiple = intPatente;

                    if (intPatente.Interesado != null)
                        interesadosDePatente.Add(intPatente.Interesado);
                    if (intPatente.Interesado1 != null)
                        interesadosDePatente.Add(intPatente.Interesado1);
                    if (intPatente.Interesado2 != null)
                        interesadosDePatente.Add(intPatente.Interesado2);
                    if (intPatente.Interesado3 != null)
                        interesadosDePatente.Add(intPatente.Interesado3);

                    this._ventana.InteresadosDePatente = interesadosDePatente;
                    this._ventana.TotalHits = interesadosDePatente.Count.ToString();
                }
                else
                    this._ventana.TotalHits = "0";
               
                
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
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

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Metodo que muestra la ventana donde se encuentran todos los datos del interesado de la patente seleccionado
        /// </summary>
        public void IrVerInteresadoSeleccionado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoSeleccionado != null)
                {
                    Interesado interesadoSeleccionado = (Interesado)this._ventana.InteresadoSeleccionado;
                    this.Navegar(new ConsultarInteresado(interesadoSeleccionado, this._ventana));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que abre la ventana GestionarInteresadosDePatente
        /// </summary>
        public void IrGestionarInteresadosDePatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._interesadoMultiple != null)
                    this.Navegar(new GestionarInteresadosDePatente(this._patente, this._interesadoMultiple, this._ventana, this._ventanaPadre, this._ventanaConsultarPatentes));
                else
                    this.Navegar(new GestionarInteresadosDePatente(this._patente, this._ventana, this._ventanaPadre, this._ventanaConsultarPatentes));

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ":" + ex.Message, true);
            }
        }

        public void IrVentanaGestionarPatente()
        {
            this.Navegar(new GestionarPatente(this._patente, this._ventanaConsultarPatentes));
        }
    }
}
