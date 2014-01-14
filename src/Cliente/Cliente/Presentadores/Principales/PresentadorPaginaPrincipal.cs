using System;
using System.Configuration;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.Principales
{
    class PresentadorPaginaPrincipal : PresentadorBase
    {
        private IPaginaPrincipal _ventana;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPatenteServicios _patenteServicios;
        private int _diasVencimientoPrioridad = 90;

        public PresentadorPaginaPrincipal(IPaginaPrincipal ventana)
        {
            this._ventana = ventana;

            this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titlePaginaPrincipal,
                    string.Empty);


                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                this._ventana.MensajeError = ex.Message;
                logger.Error(ex.Message);
                //this.Navegar(this._ventana);
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
                //this.Navegar(this._paginaPrincipal);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que verifica si existen patentes con fecha de presentacion por vencer
        /// </summary>
        public void MostarPatentesPorVencerFechaPresentacion()
        {

            String titulo = "Patentes por Vencerse";

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<VencimientoPrioridadPatente> listaPatentesPorVencerPrioridad = 
                    this._patenteServicios.ObtenerPatentesPorVencerPrioridad(this._diasVencimientoPrioridad);

                if (listaPatentesPorVencerPrioridad.Count > 0)
                {
                    if (this._ventana.ConfirmarAccion(titulo, Recursos.MensajesConElUsuario.AlertaPatentesAVencer))
                    {
                        ListaPatentesPrioridadVencidaStartUp patentes = new ListaPatentesPrioridadVencidaStartUp();
                        patentes.ShowDialog();
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.AlertaPatenteAVencerNo, 2);
                }
                else
                    this._ventana.Mensaje("No hay patentes por vencer", 2);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message ;
                logger.Error(ex.Message);
            }
        }
    }
}
