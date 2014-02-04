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
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IList<VencimientoPrioridadPatente> _listaPatentesPorVencer;
        private IList<VencimientoPrioridadPatente> _patentes = new List<VencimientoPrioridadPatente>();
        //private int cantDiasRecordatorio = 90;
        private int cantDiasRecordatorio;

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
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);

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
            int diasDiferencia = 0;
            int contador = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaPatentesVencPrioridad, Recursos.Ids.PatentesVencPrioridad);
                
                //this._listaPatentesPorVencer = this._patenteServicios.ObtenerPatentesPorVencerPrioridad(cantDiasRecordatorio);

                IList<ListaDatosValores> listaValores =
                        this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiDiasRecordatorioPresentacionPrioridad));

                cantDiasRecordatorio = int.Parse(listaValores[0].Valor);

                this._listaPatentesPorVencer = this._patenteServicios.ObtenerPatentesPorVencerPrioridad(cantDiasRecordatorio);

                if (this._listaPatentesPorVencer.Count > 0)
                {
                    #region CODIGO ORIGINAL COMENTADO
                    //this._ventana.TotalHits = this._listaPatentesPorVencer.Count.ToString();
                    //foreach (VencimientoPrioridadPatente item in this._listaPatentesPorVencer)
                    //{
                    //    codigoPatente = item.Id;
                    //    Patente patenteAux = this._patenteServicios.ConsultarPatenteConTodo(new Patente(codigoPatente));
                    //    item.Patente = patenteAux;
                    //} 
                    #endregion

                    foreach (VencimientoPrioridadPatente item in this._listaPatentesPorVencer)
                    {
                        codigoPatente = item.Id;
                        Patente patenteAux = this._patenteServicios.ConsultarPatenteConTodo(new Patente(codigoPatente));
                        item.Patente = patenteAux;
                        diasDiferencia = ObtenerDiasDiferencia(item.FechaVencimiento, item.FechaRecordatorio);
                        if (item.VencimientoDias <= diasDiferencia)
                        {
                            this._patentes.Add(item);
                            contador++;
                        }

                    }

                    if (this._patentes.Count > 0)
                    {
                        this._ventana.PatentesPorVencerPrioridad = this._patentes;
                        this._ventana.TotalHits = this._patentes.Count.ToString();
                    }

                    //this._ventana.PatentesPorVencerPrioridad = this._listaPatentesPorVencer;
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


        /// <summary>
        /// Metodo que obtiene la diferencia de dias entre la fecha de vencimiento de la prioridad y la fecha de recordatorio del vencimiento de
        /// la prioridad
        /// </summary>
        /// <param name="fechaVencimiento">Fecha de Vencimiento de la prioridad de la Patente</param>
        /// <param name="fechaRecordatorio">Fecha de Recordatorio del vencimiento de la prioridad de la Patente</param>
        /// <returns>Dias de diferencia entre las dos fechas</returns>
        private int ObtenerDiasDiferencia(String fechaVencimiento, String fechaRecordatorio)
        {
            int diferenciaDias = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                DateTime fechaVence = new DateTime();

                fechaVence = DateTime.Parse(fechaVencimiento);
                DateTime fechaRecuerda = DateTime.Parse(fechaRecordatorio);
                TimeSpan ts = fechaVence - fechaRecuerda;
                diferenciaDias = ts.Days;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return diferenciaDias;
        }


    }
}
