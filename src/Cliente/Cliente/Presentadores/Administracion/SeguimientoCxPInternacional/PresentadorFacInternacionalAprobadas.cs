using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacInternacionales;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.ViGestionAsociados;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;

using NLog;

using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorFacInternacionalAprobadas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IFacInternacionalAprobadas _ventana;
        private ISeguimientoCxPInternacionalServicios _seguimientoCxPInternacionalServicios;
        private IAsociadoServicios _asociadoServicios;
        private IFacInternacionalServicios _facInternacionalServicios;
        private IFacInternacionalConsolidadaServicios _facInternacionalConsolidadaServicios;
        private IFacAsociadoIntConsolidadoCxPIntServicios _facAsociadosIntConsolidadoCxPIntServicios;
        private IFacFacturaProformaServicios _facFacturaProformaServicios;
        private IList<FacInternacional> _proformasAprobadas;
        private IList<FacInternacional> _listaProformasAnterior;
        private FacInternacional _facturaActualizada;
        private IList<FacInternacional> _listaFacturasAprobadas = new List<FacInternacional>();

        /// <summary>
        /// Constructor predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="proformasAprobadas">Lista de proformas seleccionadas</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorFacInternacionalAprobadas(IFacInternacionalAprobadas ventana, object proformasAprobadas, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._proformasAprobadas = (IList<FacInternacional>)proformasAprobadas;

                this._seguimientoCxPInternacionalServicios =
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facInternacionalConsolidadaServicios = (IFacInternacionalConsolidadaServicios)Activator.GetObject(typeof(IFacInternacionalConsolidadaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalConsolidadaServicios"]);
                this._facFacturaProformaServicios = (IFacFacturaProformaServicios)Activator.GetObject(typeof(IFacFacturaProformaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacFacturaProformaServicios"]);
                this._facAsociadosIntConsolidadoCxPIntServicios = (IFacAsociadoIntConsolidadoCxPIntServicios)Activator.GetObject(typeof(IFacAsociadoIntConsolidadoCxPIntServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacAsociadoIntConsolidadoCxPIntServicios"]);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Metodo que carga el contenido de la ventana
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

                ActualizarTitulo();

                CalcularDiasVencimiento();

                //IList<FacInternacional> facturasInt = this._proformasAprobadas.OrderByDescending(o => o.DiasVencimiento).ToList();

                IList<FacInternacional> facturasInt = this._proformasAprobadas.OrderBy(o => o.Asociado_o.Id).ThenBy(o => o.Id).ToList();

                this._listaFacturasAprobadas = facturasInt;

                this._ventana.FacturasAutorizadas = facturasInt;
                                
                CalcularMontoTotalAprobado();

                this._ventana.TotalHits = this._proformasAprobadas.Count.ToString();
                                               
                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

        }

        
        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegCxPInternacional,
                Recursos.Ids.fac_SeguimientoCxPInternacional);
        }


        /// <summary>
        /// Metodo que calcula los dias de vencimiento de cada factura seleccionada
        /// </summary>
        private void CalcularDiasVencimiento()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacional factura in this._proformasAprobadas)
                {
                    int diferenciaDias = CalcularDiferenciaDias(factura.FechaRecepcion);
                    factura.DiasVencimiento = diferenciaDias;
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
        }

        
        /// <summary>
        /// Metodo que calcula el numero de dias entre dos fechas
        /// </summary>
        /// <returns></returns>
        private int CalcularDiferenciaDias(DateTime? FechaRecepcion)
        {
            int diferenciaDias;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion



                DateTime today = DateTime.Today;
                if (FechaRecepcion != null)
                {
                    TimeSpan ts = today - FechaRecepcion.Value;
                    diferenciaDias = ts.Days;
                }
                else
                    diferenciaDias = 0;

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
        /// Metodo para calcular el monto total de acuerdo a lo presentado en la lista de facturas aprobadas
        /// </summary>
        private void CalcularMontoTotalAprobado()
        {
            double sumatoria = 0;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacional facProformaAprobada in this._proformasAprobadas)
                {
                    if (facProformaAprobada.BIsel)
                    {
                        sumatoria += facProformaAprobada.Monto;
                    }
                }

                this._ventana.TotalMontoAprobado = sumatoria.ToString("N");

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
        /// Metodo para consolidar facturas seleccionadas en la vista
        /// <param name="nombreBoton">Nombre del boton presionado</param>
        /// </summary>
        public void IrConsolidarFacturasSeleccionadas(String nombreBoton)
        {
            IList<FacInternacional> _listaFacturasConsolidar = new List<FacInternacional>();
            IList<FacInternacionalConsolidada> _listaProformasConsolidadas = new List<FacInternacionalConsolidada>();
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                //Verifico si hay registros en la tabla FAC_CXP_INT_ISEL y borro su contenido             
                IList<FacInternacionalConsolidada> listaTablaTemporal =
                this._facInternacionalConsolidadaServicios.ConsultarTodos();

                IList<FacAsociadoIntConsolidadoCxPInt> listaDatosConsolidados = 
                    this._facAsociadosIntConsolidadoCxPIntServicios.ConsultarTodos();

                if (listaTablaTemporal.Count > 0) //Borro el contenido de la tabla ISEL
                {
                    BorrarContenidoTablaTemporal(listaTablaTemporal);
                }

                if (listaDatosConsolidados.Count > 0)
                {
                    BorrarDatosConsolidadosGuardados(listaDatosConsolidados);
                }

                //ESTAS SON TODAS LAS FACTURAS QUE SE ENCUENTRAN EN LA VENTANA
                _listaFacturasConsolidar = (IList<FacInternacional>)this._ventana.FacturasAutorizadas;

                if (_listaFacturasConsolidar.Count > 0)
                {
                    foreach (FacInternacional item in _listaFacturasConsolidar)
                    {
                        FacInternacionalConsolidada facConsolidadaAux = new FacInternacionalConsolidada();
                        facConsolidadaAux.Id = item.Id.Value;
                        facConsolidadaAux.AsociadoInt = item.Asociado_o;
                        facConsolidadaAux.Asociado = item.Asociado;

                        exitoso = 
                            this._facInternacionalConsolidadaServicios.InsertarOModificar(facConsolidadaAux, UsuarioLogeado.Hash);

                        if (exitoso)
                        {
                            exitoso = false;
                            _listaProformasConsolidadas.Add(facConsolidadaAux);
                            continue;
                        }
                        else
                        {
                            this._ventana.Mensaje("Se produjo un error al intentar guardar la proforma: " + facConsolidadaAux.Id.ToString(), 0);
                            break;
                        }
                    }

                    //SE LLAMA A LA VENTANA QUE CONSOLIDA LOS DATOS PARA REGISTRAR LOS PAGOS A CXP
                    if (nombreBoton.Equals("_btnConsolidar"))
                        this.Navegar(new FacInternacionalConsolidadas(_listaProformasConsolidadas,false,this._ventana));
                    else if (nombreBoton.Equals("_btnVerDatosConsolidar"))
                        this.Navegar(new FacInternacionalConsolidadas(_listaProformasConsolidadas,true,this._ventana));
                }
                else
                    this._ventana.Mensaje("Debe seleccionar al menos una Proforma para consolidar", 0);
                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        


        /// <summary>
        /// Metodo que borrar el contenido de la tabla FAC_CXP_INT_ISEL para actualizarla con la nueva seleccion del usuario
        /// en la ventana 
        /// </summary>
        /// <param name="listaTablaTemporal">Registros actuales que se encuentran en la tabla temporal</param>
        private void BorrarContenidoTablaTemporal(IList<FacInternacionalConsolidada> listaTablaTemporal)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacionalConsolidada registro in listaTablaTemporal)
                {
                    exitoso = this._facInternacionalConsolidadaServicios.Eliminar(registro, UsuarioLogeado.Hash);
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
        }


        /// <summary>
        /// Metodo que borra el contenido de la tabla FAC_CXP_INT_CONSOLIDA
        /// ESTE METODO SE EJECUTA CUANDO NO HAY DATOS DE CONSOLIDACION O CUANDO SE REINICIA EL PROCESO DE CONSOLIDACION
        /// </summary>
        /// <param name="listaDatosConsolidados">Registros que se encuentran guardados en la tabla FAC_CXP_INT_CONSOLIDA</param>
        private void BorrarDatosConsolidadosGuardados(IList<FacAsociadoIntConsolidadoCxPInt> listaDatosConsolidados)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacAsociadoIntConsolidadoCxPInt registro in listaDatosConsolidados)
                {
                    exitoso = this._facAsociadosIntConsolidadoCxPIntServicios.Eliminar(registro, UsuarioLogeado.Hash);
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
        }


        
        /// <summary>
        /// Metodo que despliega la ventana para Registrar el pago de una Factura Internacional especifica
        /// </summary>
        public void RegistrarPago()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.FacturasSeleccionadas != null)
                {
                    
                    IList<object> objetosSeleccionados = (IList<object>)this._ventana.FacturasSeleccionadas;

                    if (objetosSeleccionados.Count == 1)
                    {
                        FacInternacional facturaInternacional = (FacInternacional)objetosSeleccionados[0];
                        FacFacturaProforma facFacturaProforma = new FacFacturaProforma();
                        facFacturaProforma.Id = facturaInternacional.Id;
                        IList<FacFacturaProforma> listaProformas =
                            this._facFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(facFacturaProforma);
                        if (listaProformas.Count > 0)
                        {
                            this._ventana.Mensaje("Presione el boton Actualizar para refrescar el listado de Facturas seleccionadas y reiniciar los datos de Consolidación", 2);
                            this._listaProformasAnterior = (IList<FacInternacional>)this._ventana.FacturasAutorizadas;
                            this._facturaActualizada = facturaInternacional;
                            this._ventana.HabilitarBotonActualizar(true);
                            this.Navegar(new FacInternacionalPago(listaProformas[0]));
                        }

                    }
                    else
                        this._ventana.Mensaje("Solo puede seleccionar un solo registro, para registar el pago a varios registros debe Consolidar", 0);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
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
        /// Metodo que determina si hay datos guardados en la tabla FAC_CXP_INT_CONSOLIDA
        /// </summary>
        /// <returns></returns>
        public bool VerificarFacAsociadoConsolidadoGuardado()
        {
            bool hayDatos = false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<FacAsociadoIntConsolidadoCxPInt> listaDatos = this._facAsociadosIntConsolidadoCxPIntServicios.ConsultarTodos();

            if (listaDatos.Count > 0)
            {
                hayDatos = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return hayDatos;
        }

        /// <summary>
        /// Metodo que carga los datos de consolidacion que existen en la tabla FAC_CXP_INT_CONSOLIDA
        /// <param name="botonPresionado">Boton presionado en la interfaz</param>
        /// </summary>
        public void CargarDatosConsolidacion(String botonPresionado)
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<FacAsociadoIntConsolidadoCxPInt> facturacionAsociadosIntCargados = this._facAsociadosIntConsolidadoCxPIntServicios.ConsultarTodos();

                IList<FacInternacionalConsolidada> listaFacInternacionalesAprobadas = this._facInternacionalConsolidadaServicios.ConsultarTodos();

                if ((facturacionAsociadosIntCargados.Count > 0) && (listaFacInternacionalesAprobadas.Count > 0))
                    if (botonPresionado.Equals("_btnVerDatosConsolidar"))
                        this.Navegar(new FacInternacionalConsolidadas(facturacionAsociadosIntCargados, listaFacInternacionalesAprobadas, true, true, this._ventana));
                    else
                        this.Navegar(new FacInternacionalConsolidadas(facturacionAsociadosIntCargados, listaFacInternacionalesAprobadas, false, true, this._ventana));
                else
                    this._ventana.Mensaje("Hay inconsistencia de datos entre los datos guardados consolidar y las facturas seleccionadas", 0);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
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
        /// Metodo que actualiza el lista de Facturas Internacionales Aprobadas para Consolidacion
        /// </summary>
        public void ActualizarListadoFacturasAprobadas()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Hacer una lista sin la factura actualizada
                this._listaProformasAnterior.Remove(this._facturaActualizada);
                this._ventana.FacturasAutorizadas = null;
                this._ventana.FacturasAutorizadas = this._listaProformasAnterior;
                
                //Actualizar la tabla FAC_CXP_INT_ISEL
                FacInternacionalConsolidada facturaEliminar = new FacInternacionalConsolidada();
                facturaEliminar.Asociado = this._facturaActualizada.Asociado;
                facturaEliminar.AsociadoInt = this._facturaActualizada.Asociado_o;
                facturaEliminar.Id = this._facturaActualizada.Id.Value;
                bool exitoso = this._facInternacionalConsolidadaServicios.Eliminar(facturaEliminar, UsuarioLogeado.Hash);

                //Borrar el contenido de la tabla FAC_CXP_INT_CONSOLIDA para reiniciar el proceso de Consolidacion
                IList<FacAsociadoIntConsolidadoCxPInt> datosConsolidados = this._facAsociadosIntConsolidadoCxPIntServicios.ConsultarTodos();
                if (datosConsolidados.Count > 0)
                {
                    foreach (FacAsociadoIntConsolidadoCxPInt datoConsolidado in datosConsolidados)
                    {
                        bool eliminado = this._facAsociadosIntConsolidadoCxPIntServicios.Eliminar(datoConsolidado, UsuarioLogeado.Hash);
                    }
                }

                if (exitoso)
                {
                    this._ventana.Mensaje("Listado de Facturas actualizado", 2);
                    this._ventana.TotalHits = this._listaProformasAnterior.Count.ToString();
                    this._ventana.HabilitarBotonActualizar(false);
                }

                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
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
        /// Metodo que exporta el contenido del Listview que muestra las facturas seleccionadas a Excel
        /// </summary>
        public void ExportarFacturasSeleccionadasExcel()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                DataTable datosExportar = CrearDataTableExportacion();
                datosExportar = LlenarDataTable(datosExportar, this._listaFacturasAprobadas);
                String tituloReporte = "Facturas Internacionales Aprobadas para Consolidación";

                if (this._ventana.ExportarListadoFacturasAprobadas(tituloReporte, datosExportar))
                    this._ventana.Mensaje("Datos exportados con éxito", 2);
                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        
        /// <summary>
        /// Metodo que crea el DataTable con todos los campos que se van a mostrar en la hoja de Excel
        /// </summary>
        /// <returns>DataTable inicializado con las columnas ya preparadas para luego ser llenado</returns>
        private DataTable CrearDataTableExportacion()
        {

            DataTable datos = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                datos.Columns.Add("Proforma", typeof(int));
                datos.Columns.Add("Cod Asociado", typeof(int));
                datos.Columns.Add("Asociado", typeof(string));
                datos.Columns.Add("Factura", typeof(string));
                datos.Columns.Add("Fecha Emision", typeof(DateTime));
                datos.Columns.Add("Fecha Recepcion", typeof(DateTime));
                datos.Columns.Add("Monto", typeof(double));
                datos.Columns.Add("Dias Vencimiento", typeof(int));

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return datos;
        }


        private DataTable LlenarDataTable(DataTable datosExportar, IList<FacInternacional> listaFacturasAprobadas)
        {

            DataTable datos = datosExportar;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacional  facturaAprobada in listaFacturasAprobadas)
                {
                    DataRow filaNueva = datos.NewRow();
                    filaNueva["Proforma"] = facturaAprobada.Id.Value;
                    filaNueva["Cod Asociado"] = facturaAprobada.Asociado_o.Id;
                    filaNueva["Asociado"] = facturaAprobada.Asociado_o.Nombre;
                    filaNueva["Factura"] = facturaAprobada.Numerofactura;
                    filaNueva["Fecha Emision"] = facturaAprobada.Fecha;
                    if(facturaAprobada.FechaRecepcion != null)
                    {
                        filaNueva["Fecha Recepcion"] = facturaAprobada.FechaRecepcion;
                    }
                    filaNueva["Monto"] = facturaAprobada.Monto;
                    filaNueva["Dias Vencimiento"] = facturaAprobada.DiasVencimiento;
                    
                    datos.Rows.Add(filaNueva);
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

            return datos;
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

    }
}
