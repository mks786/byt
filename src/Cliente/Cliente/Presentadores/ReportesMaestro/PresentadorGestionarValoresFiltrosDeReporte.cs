using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.ReportesMaestro;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.ReportesMaestro
{
    class PresentadorGestionarValoresFiltrosDeReporte: PresentadorBase
    {
        
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarValoresFiltrosDeReporte _ventana;
        private IReporteServicios _reporteServicios;
        private ICamposReporteRelacionServicios _camposReporteRelacionServicios;
        private IFiltroReporteServicios _filtroReporteServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IList<CamposReporteRelacion> _listaDeCamposDelReporte;
        private IList<ListaDatosValores> _operadoresDeReportes;

        
        
        /// <summary>
        /// Constructor predeterminado que recibe la ventana actual y la ventana que precede a esta ventana
        /// </summary>
        /// <param name="ventana">Ventana IGestionarValoresFiltrosDeReporte</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarValoresFiltrosDeReporte(IGestionarValoresFiltrosDeReporte ventana, object reporte, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                this._ventana.Reporte = (Reporte)reporte;


                this._filtroReporteServicios = (IFiltroReporteServicios)Activator.GetObject(typeof(IFiltroReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FiltroReporteServicios"]);
                this._reporteServicios = (IReporteServicios)Activator.GetObject(typeof(IReporteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ReporteServicios"]);
                this._camposReporteRelacionServicios = (ICamposReporteRelacionServicios)Activator.GetObject(typeof(ICamposReporteRelacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CamposReporteRelacionServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                
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

                this._ventana.TituloReporte = ((Reporte)this._ventana.Reporte).TituloEspanol != null ? 
                    ((Reporte)this._ventana.Reporte).TituloEspanol : ((Reporte)this._ventana.Reporte).TituloIngles;

                this._listaDeCamposDelReporte = this._camposReporteRelacionServicios.ConsultarCamposDeReporte((Reporte)this._ventana.Reporte);

                this._operadoresDeReportes = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiOperadoresDeReporte));
                    
                IList<FiltroReporte> filtrosDelReporte = this._filtroReporteServicios.ConsultarFiltrosReporte((Reporte)this._ventana.Reporte);

                this._ventana.Filtros = filtrosDelReporte;
                


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
        /// Metodo que se encarga de construir el query del reporte tomando en cuenta los campos definidos en el mismo 
        /// </summary>
        public void EjecutarReporte()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = false;
                String queryResultante = String.Empty;
                String vistaReporte = ((Reporte)this._ventana.Reporte).VistaReporte.NombreVista;

                IList<FiltroReporte> filtrosModificados = (IList<FiltroReporte>)this._ventana.Filtros;

                //Se guardan los valores de los filtros para una proxima ocasion que el usuario vuelva a solicitar ejecutar el reporte
                foreach (FiltroReporte filtro in filtrosModificados)
                {
                    exitoso = this._filtroReporteServicios.InsertarOModificar(filtro, UsuarioLogeado.Hash); 
                }

                queryResultante = ConstruirQuery(filtrosModificados, vistaReporte);

                DataSet resultado = this._reporteServicios.EjecutarQuery(queryResultante);

                if (resultado != null)
                    this.Navegar(new VisualizarReporte(this._ventana, resultado));
                else
                    this._ventana.Mensaje("El resultado de su consulta es Vacio. Revise sus filtros", 1);

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
        }


        /// <summary>
        /// Metodo que construye el query a ejecutarse en el servidor tomando en cuenta los campos, los filtros y la vista del
        /// reporte seleccionado
        /// </summary>
        /// <param name="filtrosModificados">Lista de filtros del reporte</param>
        /// <param name="vistaReporte">Vista sobre la cual se realizara la consulta</param>
        /// <returns>Query a ejecutar en el servidor</returns>
        private string ConstruirQuery(IList<FiltroReporte> filtrosModificados, string vistaReporte)
        {

            String query = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                StringBuilder resultado = new StringBuilder();
                String clausulaCabecera = String.Empty; clausulaCabecera = "Select ";
                String clausulaFrom = String.Empty; clausulaFrom = "from ";
                String clausulaWhere = String.Empty; clausulaWhere = " where ";
                String cadenaCampos = String.Empty; String cadenaFiltros = String.Empty; String nombreCampo = String.Empty;
                String simboloOperador = String.Empty;
                int posicionCampo = 1;
                int numeroFiltro = 1;

                IList<CamposReporteRelacion> camposDefinidosDelReporte = this._listaDeCamposDelReporte;
                IList<FiltroReporte> filtrosDelReporte = filtrosModificados;

                resultado.Append(clausulaCabecera);

                foreach (CamposReporteRelacion campo in camposDefinidosDelReporte)
                {
                    if (posicionCampo < camposDefinidosDelReporte.Count)
                    {
                        if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES"))
                            //cadenaCampos += campo.Campo.NombreCampo + ", ";
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + ", ";
                        else if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + ", ";
                    }
                    else if (posicionCampo == camposDefinidosDelReporte.Count)
                    {
                        if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + " ";
                        else if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + " ";
                        //cadenaCampos += campo.Campo.NombreCampo + " ";
                    }
                    posicionCampo++;
                }

                resultado.Append(cadenaCampos);
                resultado.Append(" ");
                resultado.Append(clausulaFrom);
                resultado.Append(((Reporte)this._ventana.Reporte).VistaReporte.NombreVistaBD);

                if (filtrosDelReporte.Count > 0)
                {
                    resultado.Append(clausulaWhere);

                    foreach (FiltroReporte filtro in filtrosDelReporte)
                    {
                        if (filtrosDelReporte.Count != 1)
                            cadenaFiltros += "(";

                        nombreCampo = filtro.Campo.NombreCampo;
                        cadenaFiltros += nombreCampo + " ";
                        String nombreOperador = filtro.Operador;
                        ListaDatosValores operador = this.BuscarOperador(this._operadoresDeReportes, nombreOperador);
                        simboloOperador = operador.Valor;
                        cadenaFiltros += simboloOperador;
                        if(filtro.Campo.TipoDeCampo.Equals("NUMERICO"))
                        {
                            cadenaFiltros += filtro.Valor + " ";
                        }
                        else if(filtro.Campo.TipoDeCampo.Equals("CARACTER") || filtro.Campo.TipoDeCampo.Equals("FECHA"))
                        {
                            if(operador.Valor.Equals("LIKE") && filtro.Campo.TipoDeCampo.Equals("CARACTER") )
                            {
                                cadenaFiltros += "'%" + filtro.Valor + "%' ";
                            }
                            else
                                cadenaFiltros += "'" + filtro.Valor + "' ";
                        }

                        if (filtrosDelReporte.Count != 1)
                        {   
                            if (numeroFiltro < filtrosDelReporte.Count)
                                cadenaFiltros += ") and ";
                            else if (numeroFiltro == filtrosDelReporte.Count)
                                cadenaFiltros += ") "; 
                        }

                        numeroFiltro++;

                    }
                }

                resultado.Append(cadenaFiltros);

                query = resultado.ToString();
               
                

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

            return query;
        }

        
    }
}
