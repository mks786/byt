using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorListaPatentesPrioridadVencidaStartUp : PresentadorBase
    {
        private IListaPatentesPrioridadVencidaStartUp _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPatenteServicios _patenteServicios;
        private IList<VencimientoPrioridadPatente> _listaPatentesPorVencer;
        private int cantDiasRecordatorio = 90;

        public PresentadorListaPatentesPrioridadVencidaStartUp(IListaPatentesPrioridadVencidaStartUp ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;

                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);

                CargarPagina();

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
        }

        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            int codigoPatente;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaPatentesVencPrioridad, Recursos.Ids.PatentesVencPrioridad);
                
                this._listaPatentesPorVencer = this._patenteServicios.ObtenerPatentesPorVencerPrioridad(cantDiasRecordatorio);


                if (this._listaPatentesPorVencer.Count > 0)
                {
                    this._ventana.TotalHits = this._listaPatentesPorVencer.Count.ToString();
                    foreach (VencimientoPrioridadPatente item in this._listaPatentesPorVencer)
                    {
                        codigoPatente = item.Id;
                        Patente patenteAux = this._patenteServicios.ConsultarPatenteConTodo(new Patente(codigoPatente));
                        item.Patente = patenteAux;
                    }

                    this._ventana.PatentesPorVencerPrioridad = this._listaPatentesPorVencer;
                }

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
    }
}
