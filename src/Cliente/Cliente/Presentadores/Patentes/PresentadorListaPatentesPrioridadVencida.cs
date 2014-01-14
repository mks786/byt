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
using System.Data;
using System.Reflection;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorListaPatentesPrioridadVencida : PresentadorBase
    {
        private IListaPatentesPrioridadVencida _ventana;
        private Patente _patente;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPatenteServicios _patenteServicios;
        private IList<VencimientoPrioridadPatente> _listaPatentesPorVencer;
        private int cantDiasRecordatorio = 90;
        private DataTable _datos;
        

        public PresentadorListaPatentesPrioridadVencida(IListaPatentesPrioridadVencida ventana)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana = ventana;
            //this._ventanaPadre = ventanaPadre;
            
            this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            
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
            int codigoPatente;

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaPatentesVencPrioridad,
                    Recursos.Ids.PatentesVencPrioridad);

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
                    _datos = GenerarDataTable();
                }
                else
                {
                    this._ventana.Mensaje("No hay Patentes donde su Prioridad venza en los prósimos días", 0);
                    this._ventana.TotalHits = "0";
                }


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



        private DataTable GenerarDataTable()
        {

            DataTable data = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                data.Columns.Add("Patente", typeof(int));
                data.Columns.Add("Interesado", typeof(int));
                data.Columns.Add("Fecha Solicitud", typeof(DateTime));
                data.Columns.Add("Fecha Vencimiento", typeof(string));
                data.Columns.Add("Dias Restantes", typeof(int));

                foreach (VencimientoPrioridadPatente item in this._listaPatentesPorVencer)
                {
                    DataRow nuevaFila = data.NewRow();

                    nuevaFila["Patente"] = item.Id;
                    nuevaFila["Interesado"] = item.Patente.Interesado.Id;
                    nuevaFila["Fecha Solicitud"] = item.FechaSolicitud;
                    nuevaFila["Fecha Vencimiento"] = item.FechaVencimiento;
                    nuevaFila["Dias Restantes"] = item.VencimientoDias;
                    data.Rows.Add(nuevaFila);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return data;
        }

        
        public void ConsultarPatenteSeleccionada()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.PatenteSeleccionada != null)
                {
                    VencimientoPrioridadPatente patentePorVencer = new VencimientoPrioridadPatente();
                    patentePorVencer = (VencimientoPrioridadPatente)this._ventana.PatenteSeleccionada;
                    Patente patente = this._patenteServicios.ConsultarPatenteConTodo(new Patente(patentePorVencer.Id));
                    if (patente != null)
                        this.Navegar(new GestionarPatente(patente, this._ventana));
                    else
                        this._ventana.Mensaje("Patente no válida, Revise los datos", 0);

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

        
        
        public void ExportarAExcel()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (_datos != null)
                    this._ventana.ExportarListadoDePatentesPorVencer(_datos);
                else
                    this._ventana.Mensaje("No existe informacion a generar", 2);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorEjecucionReporte + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        public String ObtenerTituloReporte()
        {
            String tituloReporte = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tituloReporte = Recursos.Etiquetas.titleTituloDelReportePatentesVencidas;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorEjecucionReporte + ex.Message, true);
            }

            return tituloReporte;

        }
    }
}
