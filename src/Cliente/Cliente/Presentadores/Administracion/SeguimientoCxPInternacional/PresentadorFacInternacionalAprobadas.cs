using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.ViGestionAsociados;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacInternacionales;

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

                IList<FacInternacional> facturasInt = this._proformasAprobadas.OrderByDescending(o => o.DiasVencimiento).ToList();

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

                

                if (this._ventana.FacturasSeleccionadas != null)
                {
                    IList<FacInternacionalConsolidada> listaTablaTemporal =
                    this._facInternacionalConsolidadaServicios.ConsultarTodos();

                    if (listaTablaTemporal.Count > 0)
                    {
                        BorrarContenidoTablaTemporal(listaTablaTemporal);
                    }

                    System.Collections.IList items = (System.Collections.IList)this._ventana.FacturasSeleccionadas;
                    var collection = items.Cast<FacInternacional>();
                    _listaFacturasConsolidar = collection.ToList<FacInternacional>();

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
                            this.Navegar(new FacInternacionalPago(listaProformas[0]));

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
        /// </summary>
        public void CargarDatosConsolidacion()
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
    }
}
