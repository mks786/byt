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
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.ReportesMaestro;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesMaestro
{
    class PresentadorGestionarFiltroReporte : PresentadorBase
    {
        
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarFiltrosReporte _ventana;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ICamposReporteRelacionServicios _camposReporteDeMarcaServicios;
        private IFiltroReporteServicios _filtroReporteDeMarcaServicios;

        
        /// <summary>
        /// Constructor por defecto que recibe solamente la ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarFiltroReporte(IGestionarFiltrosReporte ventana, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._camposReporteDeMarcaServicios = (ICamposReporteRelacionServicios)Activator.GetObject(typeof(ICamposReporteRelacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CamposReporteRelacionServicios"]);
                this._filtroReporteDeMarcaServicios = (IFiltroReporteServicios)Activator.GetObject(typeof(IFiltroReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroReporteServicios"]);

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
        /// Constructor por defecto que recibe una ventana padre y un reporte de marca
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        public PresentadorGestionarFiltroReporte(IGestionarFiltrosReporte ventana, object ventanaPadre, object reporteDeMarca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventana.ReporteDeMarca = reporteDeMarca;

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._camposReporteDeMarcaServicios = (ICamposReporteRelacionServicios)Activator.GetObject(typeof(ICamposReporteRelacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CamposReporteRelacionServicios"]);
                this._filtroReporteDeMarcaServicios = (IFiltroReporteServicios)Activator.GetObject(typeof(IFiltroReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroReporteServicios"]);

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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarReporteDeMarca,
                Recursos.Ids.GeneradorReporteMarca);
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

                this._ventana.FocoPredeterminado();

                //this._ventana.TituloReporteDeMarca = ((Reporte)this._ventana.ReporteDeMarca).TituloEspanol != null ?
                //    ((Reporte)this._ventana.ReporteDeMarca).TituloEspanol : ((Reporte)this._ventana.ReporteDeMarca).TituloIngles;

                this._ventana.TituloReporteDeMarca = ((Reporte)this._ventana.ReporteDeMarca).Idioma.Id.Equals("ES") ? 
                    ((Reporte)this._ventana.ReporteDeMarca).TituloEspanol : ((Reporte)this._ventana.ReporteDeMarca).TituloIngles;

                CargarCombos();

               

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
        /// Metodo que carga los combos de la ventana
        /// </summary>
        public void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Carga del combo de operadores
                IList<ListaDatosValores> operadores = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiOperadoresDeReporte));
                this._ventana.OperadoresDeReporte = operadores;

                //Carga del combo de los campos seleccionados para mostrar al ejecutar el reporte
                IList<CamposReporteRelacion> camposSeleccionadosReporte = 
                    this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte((Reporte)this._ventana.ReporteDeMarca);
                this._ventana.CamposSeleccionadosReporteDeMarca = camposSeleccionadosReporte;

                //Carga de los campos filtro que se muestran solo si el reporte tiene filtros definidos
                IList<FiltroReporte> listaCamposFiltro = 
                    this._filtroReporteDeMarcaServicios.ConsultarFiltrosReporte((Reporte)this._ventana.ReporteDeMarca);
                if (listaCamposFiltro.Count != 0)
                    this._ventana.FiltrosReporteDeMarca = listaCamposFiltro;

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
        /// Metodo que inserta un Filtro a la lista de Filtros del Reporte de Marca
        /// </summary>
        public void AgregarFiltro()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CamposReporte campoFiltroSeleccionado;
                ListaDatosValores operadorSeleccionado;
                FiltroReporte filtroReporteDeMarca = new FiltroReporte();
                IList<FiltroReporte> listaFiltrosReporte = new List<FiltroReporte>();

                campoFiltroSeleccionado = ((CamposReporteRelacion)this._ventana.CampoSeleccionadoReporteDeMarca).Campo;
                ((CamposReporteRelacion)this._ventana.CampoSeleccionadoReporteDeMarca).CampoFiltro = "Y";

                //Actualizando el campo porque fue seleccionado como filtro
                CamposReporteRelacion campoModificar = (CamposReporteRelacion)this._ventana.CampoSeleccionadoReporteDeMarca;
                bool exito = this._camposReporteDeMarcaServicios.InsertarOModificar(campoModificar,UsuarioLogeado.Hash);


                if (this._ventana.OperadorDeReporte != null)
                {
                    operadorSeleccionado = (ListaDatosValores)this._ventana.OperadorDeReporte;
                    filtroReporteDeMarca.Reporte = (Reporte)this._ventana.ReporteDeMarca;
                    //filtroReporteDeMarca.Id = ((ReporteDeMarca)this._ventana.FiltrosReporteDeMarca).Id;
                    filtroReporteDeMarca.Campo = campoFiltroSeleccionado;
                    filtroReporteDeMarca.Operador = operadorSeleccionado.Descripcion;

                    if (this._ventana.FiltrosReporteDeMarca != null)
                    {
                        listaFiltrosReporte = (IList<FiltroReporte>)this._ventana.FiltrosReporteDeMarca;
                        listaFiltrosReporte.Add(filtroReporteDeMarca);
                        this._ventana.FiltrosReporteDeMarca = null;
                        this._ventana.FiltrosReporteDeMarca = listaFiltrosReporte;
                    }
                    else
                    {
                        listaFiltrosReporte.Add(filtroReporteDeMarca);
                        this._ventana.FiltrosReporteDeMarca = listaFiltrosReporte;
                    }
                }
                else
                    this._ventana.Mensaje("Debe seleccionar un operador para agregar el campo filtro seleccionado", 0);
                
                

                
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
        /// Metodo que elimina un Filtro de la lista de Filtros del Reporte de Marca
        /// </summary>
        public void QuitarFiltro()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FiltroReporte filtroAQuitar;
                IList<FiltroReporte> listaFiltrosReporte = new List<FiltroReporte>();
                CamposReporte campoFiltro;
                bool exitoso = false;

                filtroAQuitar = (FiltroReporte)this._ventana.FiltroReporteDeMarca;

                //Determinamos cual es el campo filtro para desmarcar en los campos relacion y poder en su momento eliminar el campo
                campoFiltro = ((FiltroReporte)this._ventana.FiltroReporteDeMarca).Campo;
                IList<CamposReporteRelacion> camposRelacion = 
                    this._camposReporteDeMarcaServicios.ConsultarCamposDeReporte((Reporte)this._ventana.ReporteDeMarca);

                //Actualizamos el bit de Filtro en la tabla MYP_REP_RELACION
                foreach (CamposReporteRelacion campoRelacion in camposRelacion)
                {
                    if (campoRelacion.Campo.Id.Equals(campoFiltro.Id))
                    {
                        campoRelacion.CampoFiltro = null;
                        exitoso = this._camposReporteDeMarcaServicios.InsertarOModificar(campoRelacion, UsuarioLogeado.Hash);
                    }
                }

                if (filtroAQuitar != null)
                {
                    listaFiltrosReporte = (IList<FiltroReporte>)this._ventana.FiltrosReporteDeMarca;
                    listaFiltrosReporte.Remove(filtroAQuitar);
                    this._ventana.FiltrosReporteDeMarca = null;
                    this._ventana.FiltrosReporteDeMarca = listaFiltrosReporte;

                    exitoso = this._filtroReporteDeMarcaServicios.Eliminar(filtroAQuitar, UsuarioLogeado.Hash);
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
        }


        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = false;

                //IList<FiltroReporte> filtrosReporteBD = this._filtroReporteDeMarcaServicios.ConsultarFiltrosReporte
                //    ((Reporte)this._ventana.ReporteDeMarca); 

                IList<FiltroReporte> filtrosReporteDeMarca = (IList<FiltroReporte>)this._ventana.FiltrosReporteDeMarca;
                if (filtrosReporteDeMarca != null)
                {
                    if (filtrosReporteDeMarca.Count > 0)
                    {
                        foreach (FiltroReporte filtro in filtrosReporteDeMarca)
                        {
                            exitoso = this._filtroReporteDeMarcaServicios.InsertarOModificar(filtro, UsuarioLogeado.Hash);
                        }
                    }
                }
                else if ((filtrosReporteDeMarca == null)||(filtrosReporteDeMarca.Count == 0))
                {
                    this._ventana.Mensaje("No hay filtros seleccionados para guardar", 1);
                    this.Navegar(new GestionarReporte(this._ventana.ReporteDeMarca));
                }

                if(exitoso)
                    this.Navegar(new GestionarReporte(this._ventana.ReporteDeMarca));

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


    }
}
