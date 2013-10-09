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
        private IList<ListaDatosValores> _filtrosDeOrdenReporte;

        
        
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

                //this._ventana.TituloReporte = ((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") ?
                //    ((Reporte)this._ventana.Reporte).TituloEspanol : ((Reporte)this._ventana.Reporte).TituloIngles;

                this._ventana.TituloReporte = ((Reporte)this._ventana.Reporte).Descripcion;

                this._listaDeCamposDelReporte = this._camposReporteRelacionServicios.ConsultarCamposDeReporte((Reporte)this._ventana.Reporte);

                this._operadoresDeReportes = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiOperadoresDeReporte));

                //Se recuperan los tipos de ordenamiento existentes para los reportes
                ListaDatosValores ordenamientoVacio = new ListaDatosValores();
                ordenamientoVacio.Id = Recursos.Etiquetas.cbiOrdenamientoReporte;
                ordenamientoVacio.Valor = "NGN";

                this._filtrosDeOrdenReporte = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiOrdenamientoReporte));
                this._filtrosDeOrdenReporte.Insert(0, ordenamientoVacio);
                this._ventana.TiposDeOrdenamiento = this._filtrosDeOrdenReporte;

                //Se recuperan los campos del Reporte para hacer el ordenamiento
                this._ventana.CamposDelReporte = this._listaDeCamposDelReporte;
                    
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
        /// Metodo que agrega un tipo de ordenamiento al Reporte para ejecutarlo
        /// </summary>
        public void AgregarOrdenamientoAReporte()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CamposReporte campoFiltroSeleccionado;
                ListaDatosValores operadorSeleccionado = new ListaDatosValores();
                OrdenReporte ordenReporte = new OrdenReporte();
                IList<OrdenReporte> listaOrdenamientosReporte = new List<OrdenReporte>();

                //campoFiltroSeleccionado = ((CamposReporteRelacion)this._ventana.CampoDelReporte).Campo;

                if (this._ventana.CampoDelReporte != null)
                {
                    campoFiltroSeleccionado = ((CamposReporteRelacion)this._ventana.CampoDelReporte).Campo;
                }
                else
                    campoFiltroSeleccionado = null;


                if (this._ventana.TipoDeOrdenamiento != null)
                {
                    operadorSeleccionado = (ListaDatosValores)this._ventana.TipoDeOrdenamiento;
                    ordenReporte.Reporte = (Reporte)this._ventana.Reporte;
                    ordenReporte.Campo = campoFiltroSeleccionado;
                    ordenReporte.TipoOrdenamiento = operadorSeleccionado.Valor;
                }
                else
                    ordenReporte = null;

                if ((campoFiltroSeleccionado != null) && (ordenReporte != null))
                {
                    if (!ordenReporte.TipoOrdenamiento.Equals("NGN"))
                    {
                        if (this._ventana.OrdenamientosReporte != null)
                        {
                            listaOrdenamientosReporte = (IList<OrdenReporte>)this._ventana.OrdenamientosReporte;
                            listaOrdenamientosReporte.Add(ordenReporte);
                            this._ventana.OrdenamientosReporte = null;
                            this._ventana.OrdenamientosReporte = listaOrdenamientosReporte;
                        }
                        else
                        {
                            listaOrdenamientosReporte.Add(ordenReporte);
                            this._ventana.OrdenamientosReporte = listaOrdenamientosReporte;
                        }
                        this._ventana.CampoDelReporte = null;
                        this._ventana.TipoDeOrdenamiento = null;
                    }
                    else
                    {
                        this._ventana.Mensaje("Seleccione un tipo de ordenamiento válido para generar el Reporte", 0);
                        this._ventana.CampoDelReporte = null;
                        this._ventana.TipoDeOrdenamiento = null;
                    }
                }
                else
                    this._ventana.Mensaje("Debe seleccionar un Campo y un Tipo de Ordenamiento para agregarlo a la lista de Ordenamientos", 0);



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
        /// Metodo que quita un ordenamiento seleccionado de la lista de ordenamientos del Reporte
        /// </summary>
        public void QuitarOrdenamientoAReporte()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                OrdenReporte ordenAQuitar;
                IList<OrdenReporte> listaOrdenamientosDeReporte;
               
                ordenAQuitar = (OrdenReporte)this._ventana.OrdenamientoReporte; 

                if (ordenAQuitar != null)
                {
                    listaOrdenamientosDeReporte = (IList<OrdenReporte>)this._ventana.OrdenamientosReporte;
                    listaOrdenamientosDeReporte.Remove(ordenAQuitar);
                    this._ventana.OrdenamientosReporte = null;
                    this._ventana.OrdenamientosReporte = listaOrdenamientosDeReporte;
                    this._ventana.OrdenamientoReporte = null;
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


        /// <summary>
        /// Metodo que quita un elemento de la lista de ordenamientos a pie (OPCIONAL)
        /// </summary>
        /// <param name="listaOrdenamientos">Lista de ordenamientos de la ventana</param>
        /// <param name="ordenBuscada">Ordenamiento a eliminar</param>
        /// <returns>Nueva Lista de ordenamientos del reporte</returns>
        public IList<OrdenReporte> BuscarYQuitarOrden(IList<OrdenReporte> listaOrdenamientos, OrdenReporte ordenBuscada)
        {
            IList<OrdenReporte> retorno = new List<OrdenReporte>();

            if (listaOrdenamientos != null)
                foreach (OrdenReporte item in listaOrdenamientos)
                {
                    if ((!item.Campo.EncabezadoEspanol.Equals(ordenBuscada.Campo.EncabezadoEspanol)) && (!item.TipoOrdenamiento.Equals(ordenBuscada.TipoOrdenamiento)))
                    {
                        retorno.Add(item) ;
                        
                    }
                }

            return retorno;
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

                //Se verifica que existan ordenamiento para ejecutar el reporte
                if (this._ventana.OrdenamientosReporte != null)
                {
                    queryResultante = ConstruirQuery(filtrosModificados, vistaReporte);

                    DataSet resultado = this._reporteServicios.EjecutarQuery(queryResultante);

                    if (resultado != null)
                        this.Navegar(new VisualizarReporte(this._ventana, this._ventana.Reporte, resultado));
                    else
                        this._ventana.Mensaje("El resultado de su consulta es Vacio. Revise sus filtros", 1);
                }
                else
                    this._ventana.Mensaje("Debe elegir al menos un criterio de Ordenamiento", 0);

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
                String clausulaCabecera = String.Empty; clausulaCabecera = "SELECT ";
                String cabeceraOrdenamiento = "SELECT ROWNUM " + '\u0022' + "No" + '\u0022' + ", T.* FROM ( ";
                String clausulaFrom = String.Empty; clausulaFrom = "FROM ";
                String clausulaWhere = String.Empty; clausulaWhere = " WHERE ";
                String clausulaOrderBy = String.Empty; clausulaOrderBy = " ORDER BY ";
                String cadenaCampos = String.Empty; String cadenaFiltros = String.Empty; String nombreCampo = String.Empty;
                String simboloOperador = String.Empty;
                int posicionCampo = 1;
                int numeroFiltro = 1;
                int numeroOrdenamientos = 1;
                String[] parametros = null;
                

                IList<CamposReporteRelacion> camposDefinidosDelReporte = this._listaDeCamposDelReporte;
                IList<FiltroReporte> filtrosDelReporte = filtrosModificados;

                resultado.Append(cabeceraOrdenamiento);
                resultado.Append(clausulaCabecera);

                //Definiendo la cadena de los campos a consultar SELECT campo1, campo2,....,campoN
                foreach (CamposReporteRelacion campo in camposDefinidosDelReporte)
                {
                    if (posicionCampo < camposDefinidosDelReporte.Count)
                    {
                        if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") && campo.Campo.Clase.Equals("NORMAL"))
                            //cadenaCampos += campo.Campo.NombreCampo + ", ";
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + ", ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN") && campo.Campo.Clase.Equals("NORMAL"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + ", ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") && campo.Campo.Clase.Equals("ESPECIAL"))
                            cadenaCampos += campo.Campo.AdicionalEspanol + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + ", ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN") && campo.Campo.Clase.Equals("ESPECIAL"))
                            cadenaCampos += campo.Campo.AdicionalIngles + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + ", ";

                    }
                    else if (posicionCampo == camposDefinidosDelReporte.Count)
                    {
                        if(((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") && campo.Campo.Clase.Equals("NORMAL"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + " ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN") && campo.Campo.Clase.Equals("NORMAL"))
                            cadenaCampos += campo.Campo.NombreCampo + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + " ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("ES") && campo.Campo.Clase.Equals("ESPECIAL"))
                            cadenaCampos += campo.Campo.AdicionalEspanol + " " + '\u0022' + campo.Campo.EncabezadoEspanol + '\u0022' + " ";
                        else if (((Reporte)this._ventana.Reporte).Idioma.Id.Equals("IN") && campo.Campo.Clase.Equals("ESPECIAL"))
                            cadenaCampos += campo.Campo.AdicionalIngles + " " + '\u0022' + campo.Campo.EncabezadoIngles + '\u0022' + " ";
                        //cadenaCampos += campo.Campo.NombreCampo + " ";
                    }
                    posicionCampo++;
                }

                resultado.Append(cadenaCampos);
                resultado.Append(" ");
                resultado.Append(clausulaFrom);
                resultado.Append(((Reporte)this._ventana.Reporte).VistaReporte.NombreVistaBD);


                //Definiendo el WHERE de la consulta
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
                            if (operador.Valor.Equals("IN"))
                            {
                                cadenaFiltros += "(";
                                parametros = filtro.Valor.Split(',');
                                for (int i = 0; i < parametros.Length; i++)
                                {
                                    if (i < parametros.Length - 1)
                                        cadenaFiltros += parametros[i] + ",";
                                    else if (i == parametros.Length - 1)
                                        cadenaFiltros += parametros[i];
                                }
                                cadenaFiltros += ")";
                                parametros = null;
                            }
                            else if (operador.Valor.Equals("BETWEEN"))
                            {
                                parametros = filtro.Valor.Split(',');
                                cadenaFiltros +=  " " + parametros[0] + " AND " + parametros[1] ;
                                parametros = null;
                            }
                            else
                                cadenaFiltros += filtro.Valor + " ";
                        }
                        else if(filtro.Campo.TipoDeCampo.Equals("CARACTER") || filtro.Campo.TipoDeCampo.Equals("FECHA"))
                        {
                            if(operador.Valor.Equals("LIKE") && filtro.Campo.TipoDeCampo.Equals("CARACTER") )
                            {
                                cadenaFiltros += "'%" + filtro.Valor + "%' ";
                            }
                            else if (operador.Valor.Equals("IN"))
                            {
                                cadenaFiltros += "(";
                                parametros = filtro.Valor.Split(',');
                                for (int i = 0; i < parametros.Length; i++)
                                {
                                    if (i < parametros.Length - 1)
                                        cadenaFiltros += "'" + parametros[i] + "'" + ",";
                                    else if (i == parametros.Length - 1)
                                        cadenaFiltros += "'" + parametros[i] + "'";
                                }
                                cadenaFiltros += ")";
                                parametros = null;
                            }
                            else if (operador.Valor.Equals("BETWEEN") && filtro.Campo.TipoDeCampo.Equals("FECHA"))
                            {
                                
                                parametros = filtro.Valor.Split(',');
                                cadenaFiltros += "'" + parametros[0] + "' AND '" + parametros[1] + "' ";
                                parametros = null;
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

                //resultado.Append(cadenaFiltros);

                if (this._ventana.OrdenamientosReporte != null)
                {
                    IList<OrdenReporte> ordersBy = (IList<OrdenReporte>)this._ventana.OrdenamientosReporte;
                    if (ordersBy.Count > 0)
                    {
                        cadenaFiltros += clausulaOrderBy;

                        foreach (OrdenReporte item in ordersBy)
                        {
                            if(numeroOrdenamientos < ordersBy.Count)
                                cadenaFiltros += item.Campo.NombreCampo + " " + item.TipoOrdenamiento + ", ";
                            else if(numeroOrdenamientos == ordersBy.Count)
                                cadenaFiltros += item.Campo.NombreCampo + " " + item.TipoOrdenamiento;

                            numeroOrdenamientos++;
                        }
                    }
                }


                resultado.Append(cadenaFiltros);
                resultado.Append(")T");

                
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
