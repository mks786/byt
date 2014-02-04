using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorConsultarSeguimientoCxPInternacional : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarSeguimientoCxPInternacional _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IList<Asociado> _asociados;
        private IList<ListaDatosValores> _tiposDeuda;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ISeguimientoCxPInternacionalServicios _seguimientoCxPInternacionalServicios;
        private DataTable _datosCrudos;
        private FiltroDataCrudaCxPInternacional _filtro;

        /// <summary>
        /// Constructor predeterminado que recibe la ventana actual
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarSeguimientoCxPInternacional(IConsultarSeguimientoCxPInternacional ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;

                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._seguimientoCxPInternacionalServicios = (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " +  ex.Message, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoClientes,
                Recursos.Ids.MaestroPlantilla);
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

                double valorInicial = 0.00;


                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoCxPInternacional, "");

                this._ventana.TotalHits = "0";

                this._ventana.RangoInferior = "1";

                this._ventana.TotalUSD = valorInicial.ToString("N");

                this._ventana.TotalPorVencer = this._ventana.TotalVencido = valorInicial.ToString("N");

                CargarCombos();

                PredeterminarEjes(true);
                
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que carga los combos existentes en la ventana 
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<ListaDatosValores> tiposDeuda =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoSaldoSegClientes));
                this._tiposDeuda = tiposDeuda;
                this._ventana.TiposDeudas = tiposDeuda;
                ListaDatosValores tipoDeudaPorDefecto = new ListaDatosValores();
                tipoDeudaPorDefecto.Valor = "TODOS";
                this._ventana.TipoDeuda = this.BuscarListaDeDatosValores(tiposDeuda, tipoDeudaPorDefecto);

                IList<ListaDatosValores> ordenes =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrdenSeguimientoClientes));
                this._ventana.Ordenamientos = ordenes;

                IList<ListaDatosValores> camposVistaSegCxPInternacional =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiVistaCxPInternacional));
                this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSegCxPInternacional;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo que deja predeterminados los campos para poder realizar la tabla pivot
        /// OJO: CAMBIAR EL PARAMETRO PARA LISTADATOSVALORES DE LOS EJES
        /// </summary>
        /// <param name="iniciarVentana"></param>
        public void PredeterminarEjes(bool iniciarVentana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                IList<ListaDatosValores> camposVistaSeguimientoClientes =
                            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiVistaCxPInternacional));


                if (iniciarVentana)
                {
                    this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSeguimientoClientes;
                }


                ListaDatosValores ejeX = new ListaDatosValores();
                ejeX.Valor = "ANOFAC";
                this._ventana.EjeXSeleccionado = this.BuscarListaDeDatosValores(camposVistaSeguimientoClientes, ejeX);

                ListaDatosValores ejeY = new ListaDatosValores();
                //ejeY.Valor = "CASOCIADO_O";
                ejeY.Valor = "XASOCIADO1";
                this._ventana.EjeYSeleccionado = this.BuscarListaDeDatosValores(camposVistaSeguimientoClientes, ejeY);

                ListaDatosValores ejeZ = new ListaDatosValores();
                ejeZ.Valor = "MONTO";
                this._ventana.EjeZSeleccionado = this.BuscarListaDeDatosValores(camposVistaSeguimientoClientes, ejeZ);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }



        /// <summary>
        /// Metodo para consultar un asociado especifico
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Asociado asociadoABuscar = new Asociado();

                asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                    int.Parse(this._ventana.IdAsociadoFiltrar) : int.MinValue;

                asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                         this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

                if ((asociadoABuscar.Id != int.MinValue) || !(asociadoABuscar.Nombre.Equals("")))
                {
                    IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);

                    if (asociados.Count > 0)
                    {
                        asociados.Insert(0, new Asociado(int.MinValue));
                        this._ventana.Asociados = asociados;
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.Asociados = this._asociados;
                    }
                }

                else
                    this._ventana.Mensaje("Ingrese criterios validos para la busqueda del Asociado", 1);
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
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


        /// <summary>
        /// Cambia el usuario seleccionado despues de haber ejecutado la busqueda del mismo
        /// </summary>
        /// <returns>Asociado seleccionado de la lista de opciones de Asociado(s)</returns>
        public bool CambiarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.IdAsociado = ((Asociado)this._ventana.Asociado).Nombre;

                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        /// <summary>
        /// Metodo que llama a la ventana que muestra los datos pivot conjuntamente con su informacion de detalle
        /// La tabla pivot se usa con la vista FAC_CXP_INT_VI
        /// </summary>
        public void IrListaDatosPivot()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.EjeXSeleccionado != null)
                {
                    if (this._ventana.EjeYSeleccionado != null)
                    {
                        if (this._ventana.EjeZSeleccionado != null)
                        {

                            FiltroDataCrudaCxPInternacional filtro = ObtenerFiltroDeDataCrudaDeLaPantalla();

                            if (this._filtro.Asociados != null)
                                filtro.Asociados = this._filtro.Asociados;

                            //LLAMADA A LA VENTANA QUE MUESTRA EL PIVOT
                            this.Navegar(new ListaDatosPivotSeguimientoCxPInternacional(filtro, this._ventana.EjeXSeleccionado, this._ventana.EjeYSeleccionado, this._ventana.EjeZSeleccionado, this._ventana.TotalUSD, this._ventana));
                        }
                        else
                            this._ventana.Mensaje("Debe seleccionar un campo para el eje Z", 0);
                    }
                    else
                        this._ventana.Mensaje("Debe seleccionar un campo para el eje Y", 0);
                }
                else
                    this._ventana.Mensaje("Debe seleccionar un campo para el eje X", 0);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que obtiene los valores del filtro de la pantalla para poder obtener la data cruda para realizar la tabla pivot
        /// </summary>
        /// <returns>FiltroDataCrudaCxPInternacional de la pantalla</returns>
        private FiltroDataCrudaCxPInternacional ObtenerFiltroDeDataCrudaDeLaPantalla()
        {
            FiltroDataCrudaCxPInternacional filtro = new FiltroDataCrudaCxPInternacional();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //filtro.Moneda = this._ventana.Moneda != null ? ((Moneda)this._ventana.Moneda).Id : null;

                filtro.TipoDeuda = this._ventana.TipoDeuda != null ? ((ListaDatosValores)this._ventana.TipoDeuda).Valor : "TODOS";

                filtro.RangoSuperior = !this._ventana.RangoSuperior.Equals("") ? int.Parse(this._ventana.RangoSuperior) : 0;

                filtro.Ordenamiento = this._ventana.Ordenamiento != null ? ((ListaDatosValores)this._ventana.Ordenamiento).Valor : "ASC";

                filtro.Asociado = this._ventana.Asociado != null ? (Asociado)this._ventana.Asociado : null;

                
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

            return filtro;
        }

        /// <summary>
        /// Metodo que consulta la tabla FAC_ASO_SALDO_CXP, donde se encuentran los datos que se van a mostrar como Resumen General
        /// </summary>
        public void ObtenerResumenGeneral()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FiltroDataCrudaCxPInternacional filtroParaConsultar = new FiltroDataCrudaCxPInternacional();
                DataTable datos = new DataTable();
                String[] _asociados = null;
                String _asociadosStr = String.Empty;

                filtroParaConsultar = ObtenerFiltroDeDataCrudaDeLaPantalla();

                datos = this._seguimientoCxPInternacionalServicios.ObtenerResumenGeneralCxPInternacional(filtroParaConsultar);

                if (datos.Rows.Count > 0)
                {
                    this._ventana.Resultados = datos.DefaultView;
                    this._datosCrudos = datos;
                    if ((datos.Rows.Count != 0) && (!this._ventana.RangoSuperior.Equals("")))
                    {
                        _asociadosStr = ObtenerCodigosDeAsociados(datos);
                        _asociados = _asociadosStr.Split(',');
                        filtroParaConsultar.Asociados = _asociados;
                    }
                    this._ventana.TotalUSD = CalcularTotalColumna("MONTO", datos);
                    this._ventana.TotalPorVencer = CalcularTotalColumna("POR_VENCER", datos);
                    this._ventana.TotalVencido = CalcularTotalColumna("VENCIDO", datos);
                    this._ventana.ActivarEjesPivot();
                    this._filtro = filtroParaConsultar;
                    this._ventana.TotalHits = datos.Rows.Count.ToString();
                    this._ventana.Mensaje("Datos Origen generados, puede generar el Resumen. Elija los campos y presione Generar Resumen", 2);
                }
                else
                {
                    this._ventana.Mensaje("Consulta vacía, cambie los filtros para obtener datos", 1);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que genera el String correspondiente a los codigos de asociados si y solo si 
        /// </summary>
        /// <param name="datos">DataTable con los datos del Resumen General de Datos</param>
        /// <returns>Cadena con los numeros de asociados</returns>
        private string ObtenerCodigosDeAsociados(DataTable datos)
        {
            String _asociadoStr = String.Empty;
            String _strAux = String.Empty;
            int contadorFilas = datos.Rows.Count;
            int i = 1;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (DataRow item in datos.Rows)
                {
                    if (i < contadorFilas)
                    {
                        _strAux += item["CASOCIADO"].ToString() + ",";
                        i++; 
                    }
                    else if (i == contadorFilas)
                    {
                        _strAux += item["CASOCIADO"].ToString();
                    }

                }

                _asociadoStr = _strAux;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return _asociadoStr;
        }

        /// <summary>
        /// Metodo que calcula los totales de las columnas MONTO, POR_VENCER y VENCIDO para el resumen de totales generales al inicio del
        /// modulo.
        /// </summary>
        /// <param name="nombreColumna">Columna donde se encuentran los datos que se van a sumar</param>
        /// <param name="data">DataTable con los datos</param>
        /// <returns>Resultado de la sumatoria que se colocara en la ventana en el espacio de Totales</returns>
        private string CalcularTotalColumna(string nombreColumna, DataTable data)
        {
            String total = String.Empty;
            decimal sumaTotal = 0, cantidad = 0;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (DataRow fila in data.Rows)
                {
                    foreach (DataColumn columna in data.Columns)
                    {
                        String campo = columna.ColumnName;
                        if (campo.Equals(nombreColumna))
                        {
                            //double cantidad = Double.Parse(fila[campo].ToString());

                            if (!string.IsNullOrWhiteSpace(fila[campo].ToString()))
                            {
                                cantidad = Decimal.Parse(fila[campo].ToString());
                            }
                            else
                                cantidad = 0;
                            sumaTotal += cantidad;
                        }
                    }
                }

                total = sumaTotal.ToString("N");

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

            return total;
        }


        /// <summary>
        /// Metodo que inicializa la pantalla a sus valores por defecto
        /// </summary>
        public void LimpiarCampos()
        {
            this._ventana.TotalHits = "0";
            this._ventana.Ordenamiento = null;
            this._ventana.EjeXSeleccionado = null;
            this._ventana.EjeYSeleccionado = null;
            this._ventana.EjeZSeleccionado = null;
            this._ventana.Asociado = null;
            this._ventana.Asociados = null;
            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.NombreAsociadoFiltrar = null;
            this._ventana.IdAsociado = null;

            this._ventana.Resultados = null;
            this._ventana.RangoSuperior = null;
            double numeroInicial = 0.00;
            this._ventana.TotalUSD = numeroInicial.ToString("N");
            this._ventana.TotalPorVencer = this._ventana.TotalVencido = numeroInicial.ToString("N");
            this._ventana.TipoDeuda = null;
            
            ListaDatosValores ordenamientoPorDefecto = new ListaDatosValores();
            ordenamientoPorDefecto.Valor = "DESC";
            this._ventana.Ordenamiento = this.BuscarListaDeDatosValores((IList<ListaDatosValores>)this._ventana.Ordenamientos, ordenamientoPorDefecto);

            ListaDatosValores tipoDeudaPorDefecto = new ListaDatosValores();
            tipoDeudaPorDefecto.Valor = "TODOS";
            this._ventana.TipoDeuda = this.BuscarListaDeDatosValores(this._tiposDeuda, tipoDeudaPorDefecto);

            
            PredeterminarEjes(false);
            this._ventana.DesactivarEjesPivot();
        }
    }
}
