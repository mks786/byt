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
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        //private int _diasVencimientoPrioridad = 90;
        private int _diasVencimientoPrioridad;



        public PresentadorPaginaPrincipal(IPaginaPrincipal ventana)
        {
            this._ventana = ventana;

            this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
            this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
        /// ESTE METODO CONTROLA EL MENSAJE QUE APARECE 3 SEGUNDOS DESPUES DE INICIAR EL SISTEMA PARA AVISAR DE LAS PATENTES POR VENCER
        /// </summary>
        public void MostarPatentesPorVencerFechaPresentacion()
        {

            String titulo = "Patentes por Vencerse";
            int cantDiasRecordatorio, diasDiferencia;
            int cantidadPatentes = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<ListaDatosValores> listaValores =
                        this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiDiasRecordatorioPresentacionPrioridad));

                cantDiasRecordatorio = int.Parse(listaValores[0].Valor);
                
                //IList<VencimientoPrioridadPatente> listaPatentesPorVencerPrioridad = 
                //    this._patenteServicios.ObtenerPatentesPorVencerPrioridad(this._diasVencimientoPrioridad);

                IList<VencimientoPrioridadPatente> listaPatentesPorVencerPrioridad = 
                    this._patenteServicios.ObtenerPatentesPorVencerPrioridad(cantDiasRecordatorio);

                if (listaPatentesPorVencerPrioridad.Count > 0)
                {
                    #region CODIGO ORIGINAL COMENTADO
                    //if (this._ventana.ConfirmarAccion(titulo, Recursos.MensajesConElUsuario.AlertaPatentesAVencer))
                    //{
                    //    ListaPatentesPrioridadVencidaStartUp patentes = new ListaPatentesPrioridadVencidaStartUp();
                    //    patentes.ShowDialog();
                    //}
                    //else
                    //    this._ventana.Mensaje(Recursos.MensajesConElUsuario.AlertaPatenteAVencerNo, 2); 
                    #endregion
                    foreach (VencimientoPrioridadPatente item in listaPatentesPorVencerPrioridad)
                    {
                        diasDiferencia = ObtenerDiasDiferencia(item.FechaVencimiento, item.FechaRecordatorio);
                        if (item.VencimientoDias <= diasDiferencia)
                            cantidadPatentes++;
                    }

                    if (cantidadPatentes > 0)
                    {
                        ListaPatentesPrioridadVencidaStartUp patentes = new ListaPatentesPrioridadVencidaStartUp();
                        patentes.ShowDialog();
                    }
                    
                }
                

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

        private int ObtenerDiasDiferencia(string fechaVencimiento, string fechaRecordatorio)
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


        /// <summary>
        /// Metodo que determina si el usuario logueado cumple con el rol de PATENTES para que el mensaje y el listado de patentes por 
        /// vencer aparezca
        /// </summary>
        /// <returns></returns>
        public bool EsUSuarioPatente()
        {
            bool existe = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (UsuarioLogeado.Rol.Id.Equals("OPR_PATENTE"))
                {
                    existe = true;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message;
                logger.Error(ex.Message);
            }

            return existe;
        }
    }
}
