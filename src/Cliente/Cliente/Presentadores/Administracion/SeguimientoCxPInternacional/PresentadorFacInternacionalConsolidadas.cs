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
using Trascend.Bolet.Cliente.Ventanas.Asociados;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorFacInternacionalConsolidadas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IFacInternacionalConsolidadas _ventana;
        private ISeguimientoCxPInternacionalServicios _seguimientoCxPInternacionalServicios;
        private IAsociadoServicios _asociadoServicios;
        private IFacInternacionalServicios _facInternacionalServicios;
        private IFacInternacionalConsolidadaServicios _facInternacionalConsolidadaServicios;
        private IDatosTransferenciaServicios _datosTransferenciaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IFacAsociadoIntConsolidadoCxPIntServicios _facAsociadosIntConsolidadoCxPIntServicios;

        private IList<FacInternacionalConsolidada> _facCxPAprobadas;
        private IList<FacAsociadoIntConsolidadoCxPInt> _asociadosConsolidados;
        private bool _soloVer;
        private bool _datosConsolidados;
        private bool _datosModificados;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="listaFacInternacionalesAprobadas">Lista de facturas seleccionadas para consolidar</param>
        /// <param name="soloVer">Bit para diferenciar si la vista sera solo ver o para realizar el proceso de consolidacion</param>
        public PresentadorFacInternacionalConsolidadas( IFacInternacionalConsolidadas ventana, 
                                                        object listaFacInternacionalesAprobadas,
                                                        bool soloVer)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._facCxPAprobadas = (IList <FacInternacionalConsolidada>) listaFacInternacionalesAprobadas;
                this._soloVer = soloVer;

                //this._ventanaPadre = ventanaPadre;
                //this._proformasAprobadas = (IList<FacInternacional>)proformasAprobadas;

                this._seguimientoCxPInternacionalServicios =
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facInternacionalConsolidadaServicios = (IFacInternacionalConsolidadaServicios)Activator.GetObject(typeof(IFacInternacionalConsolidadaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalConsolidadaServicios"]);
                this._datosTransferenciaServicios = (IDatosTransferenciaServicios)Activator.GetObject(typeof(IDatosTransferenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DatosTransferenciaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
        /// Constructor predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="listaFacInternacionalesAprobadas">Lista de facturas seleccionadas para consolidar</param>
        /// <param name="soloVer">Bit para diferenciar si la vista sera solo ver o para realizar el proceso de consolidacion</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorFacInternacionalConsolidadas(IFacInternacionalConsolidadas ventana,
                                                        object listaFacInternacionalesAprobadas,
                                                        bool soloVer,
                                                        object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._facCxPAprobadas = (IList<FacInternacionalConsolidada>)listaFacInternacionalesAprobadas;
                this._soloVer = soloVer;
                this._ventanaPadre = ventanaPadre;
                //this._proformasAprobadas = (IList<FacInternacional>)proformasAprobadas;

                this._seguimientoCxPInternacionalServicios =
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facInternacionalConsolidadaServicios = (IFacInternacionalConsolidadaServicios)Activator.GetObject(typeof(IFacInternacionalConsolidadaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalConsolidadaServicios"]);
                this._datosTransferenciaServicios = (IDatosTransferenciaServicios)Activator.GetObject(typeof(IDatosTransferenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DatosTransferenciaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
        /// Constructor predeterminado que recibe unos datos consolidados previamente guardados, un bit para saber que tipo de ventana es y, una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="listaFacAsociadoIntCxPInternacional">Lista de datos consolidados</param>
        /// <param name="soloVer">Bit para diferenciar si es solo ver o no</param>
        /// <param name="datosConsolidados">Bit para indicar si son datos consolidados</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorFacInternacionalConsolidadas(IFacInternacionalConsolidadas ventana,
                                                        object listaFacAsociadoIntCxPInternacional,
                                                        object listaFacInternacionalesAprobadas,
                                                        bool soloVer,
                                                        bool datosConsolidados,
                                                        object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._facCxPAprobadas = (IList<FacInternacionalConsolidada>)listaFacInternacionalesAprobadas;
                this._asociadosConsolidados = (IList<FacAsociadoIntConsolidadoCxPInt>)listaFacAsociadoIntCxPInternacional;
                this._soloVer = soloVer;
                this._datosConsolidados = datosConsolidados;
                this._ventanaPadre = ventanaPadre;
                

                this._seguimientoCxPInternacionalServicios =
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facInternacionalConsolidadaServicios = (IFacInternacionalConsolidadaServicios)Activator.GetObject(typeof(IFacInternacionalConsolidadaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalConsolidadaServicios"]);
                this._datosTransferenciaServicios = (IDatosTransferenciaServicios)Activator.GetObject(typeof(IDatosTransferenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DatosTransferenciaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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
        /// Constructor por defecto que recibe la lista modificada de los datos de consolidacion y banderas de confirmacion
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="listaFacAsociadoIntCxPInternacional">Lista de datos consolidados</param>
        /// <param name="soloVer">Modo SOLO VER que identifica si se carga la ventana de ver los datos o la ventana que muestra los datos consolidados definitivos</param>
        /// <param name="datosModificados">Bandera que indica si hubo modificaciones en los datos</param>
        /// <param name="refrescaVentana">Bandera de referencia para indicar si se refresco la ventana o no</param>
        /// <param name="ventanaPadre">Ventana de Facturas Internacionales seleccionadas para consolidar</param>
        public PresentadorFacInternacionalConsolidadas(IFacInternacionalConsolidadas ventana,
                                                        object listaFacAsociadoIntCxPInternacional,
                                                        bool soloVer,
                                                        bool datosModificados,
                                                        bool refrescaVentana, 
                                                        object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._asociadosConsolidados = (IList<FacAsociadoIntConsolidadoCxPInt>)listaFacAsociadoIntCxPInternacional;
                this._soloVer = soloVer;
                this._datosModificados = datosModificados;
                
                //this._proformasAprobadas = (IList<FacInternacional>)proformasAprobadas;

                this._seguimientoCxPInternacionalServicios =
                    (ISeguimientoCxPInternacionalServicios)Activator.GetObject(typeof(ISeguimientoCxPInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoCxPInternacionalServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facInternacionalConsolidadaServicios = (IFacInternacionalConsolidadaServicios)Activator.GetObject(typeof(IFacInternacionalConsolidadaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalConsolidadaServicios"]);
                this._datosTransferenciaServicios = (IDatosTransferenciaServicios)Activator.GetObject(typeof(IDatosTransferenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DatosTransferenciaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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

                if (!this._datosModificados)
                {
                    IList<FacInternacionalConsolidada> facturasIntAprobadas = this._facCxPAprobadas.OrderBy(o => o.AsociadoInt.Id).ToList();

                    //ConsolidarFacturasInternacionales(this._facCxPAprobadas);
                    ConsolidarFacturasInternacionales(facturasIntAprobadas);

                    this._ventana.TotalMontoConsolidado = CalcularMontoTotalConsolidacion(this._asociadosConsolidados).ToString("N");

                    this._ventana.FocoPredeterminado();
                }
                else
                {
                    if (this._soloVer)
                    {
                        this._ventana.HabilitarListaSoloVer();
                        this._ventana.HabilitarBotonModificar();
                        this._ventana.FacturasAprobadasSoloVer = this._asociadosConsolidados;
                        this._ventana.TotalMontoConsolidado = CalcularMontoTotalConsolidacion(this._asociadosConsolidados).ToString("N");
                    }
                    else
                    {
                    }
                }

                #region CODIGO ORIGINAL COMENTADO NO BORRAR
                //IList<FacInternacionalConsolidada> facturasIntAprobadas = this._facCxPAprobadas.OrderBy(o => o.AsociadoInt.Id).ToList();

                ////ConsolidarFacturasInternacionales(this._facCxPAprobadas);
                //ConsolidarFacturasInternacionales(facturasIntAprobadas);

                //this._ventana.TotalMontoConsolidado = CalcularMontoTotalConsolidacion(this._asociadosConsolidados).ToString("N");

                //this._ventana.FocoPredeterminado(); 
                #endregion

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
        /// Metodo que calcula el total general a consolidar 
        /// </summary>
        /// <param name="asociadosConsolidar">Estructura donde se guarda la data completa de cada uno de los Asociados a consolidar</param>
        private double CalcularMontoTotalConsolidacion(IList<FacAsociadoIntConsolidadoCxPInt> asociadosConsolidar)
        {
            double montoTotal = 0;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacAsociadoIntConsolidadoCxPInt item in asociadosConsolidar)
                {
                    montoTotal += item.MontoConsolidado;
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

            return montoTotal;
        }

        
        /// <summary>
        /// Metodo que totaliza las facturas por Asociado Internacional para despues proceder a registras los pagos ylas 
        /// presenta en la pantalla 
        /// </summary>
        /// <param name="listaFacIntAprobadas">Lista de Facturas Internacionales aprobadas y a consolidar</param>
        private void ConsolidarFacturasInternacionales(IList<FacInternacionalConsolidada> listaFacIntAprobadas)
        {
            IList<FacInternacional> facturasInternacionales = new List<FacInternacional>(); //Facturas Internacionales Consolidadas
            IList<FacAsociadoIntConsolidadoCxPInt> asociadosIntConsolidado = new List<FacAsociadoIntConsolidadoCxPInt>();
            IList<FacInternacional> facturasDelAsociado;
            IList<ListaDatosValores> formasDePago;

            String _strAsociadosIntCodigos = String.Empty;
            int codigoAsociadoInt, codigoAsocAnt = 0;
            double sumatoria = 0;
            int contador = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Cargo los tipos de pagos para asignarselos al ComboBox de cada una de las lineas del ListView
                formasDePago = 
                    this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiFacFormasDePagoCxPInt));



                //Hago una lista con todas las facturas internacionales que estan aprobadas para asignarselas a cada objeto 
                //FacAsociadoIntConsolidadoCxPInt que guarda todos los datos de consolidacion. 
                foreach (FacInternacionalConsolidada facturaIntConsolidada in listaFacIntAprobadas)
                {
                    FacInternacional facIntAux = new FacInternacional();
                    facIntAux.Id = facturaIntConsolidada.Id;
                    IList<FacInternacional> resultado = 
                        this._facInternacionalServicios.ObtenerFacInternacionalesFiltro(facIntAux);
                    facturasInternacionales.Add(resultado[0]);
                }

                //PROCESO DE CONSOLIDACION: LA BANDERA _datosConsolidados ES PARA VERIFICAR SI EXISTEN DATOS EN LA TABLA FAC_CXP_INT_CONSOLIDA
                //SI NO LOS HAY HACE LA PRIMERA PARTE DEL IF SINO HACE EL ELSE
                //Lleno cada uno de los objetos Asociado Internacional Consolidado con sus datos restantes
                if (!this._datosConsolidados)
                {
                    foreach (FacInternacionalConsolidada item in listaFacIntAprobadas)
                    {
                        //Se asignan las facturas a cada Asociado Consolidado
                        if (codigoAsocAnt != item.AsociadoInt.Id)
                        {
                            FacAsociadoIntConsolidadoCxPInt facAsociadoFacInt = new FacAsociadoIntConsolidadoCxPInt();
                            facAsociadoFacInt.AsociadoInt = item.AsociadoInt;
                            asociadosIntConsolidado.Add(facAsociadoFacInt);

                            codigoAsocAnt = item.AsociadoInt.Id;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    //Luego, se calcula el monto total de las facturas por asociado
                    //y se asignan las facturas por cada asociado para consolidar y mostrar los totales
                    foreach (FacAsociadoIntConsolidadoCxPInt item1 in asociadosIntConsolidado)
                    {
                        facturasDelAsociado = new List<FacInternacional>();
                        codigoAsociadoInt = item1.AsociadoInt.Id;
                        foreach (FacInternacional item2 in facturasInternacionales)
                        {

                            if (codigoAsociadoInt == item2.Asociado_o.Id)
                            {
                                sumatoria += item2.Monto;
                                facturasDelAsociado.Add(item2);
                            }
                        }
                        item1.MontoConsolidado = sumatoria;
                        item1.FacturasIntConsolidadas = facturasDelAsociado;
                        item1.FormasDePago = formasDePago;
                        IList<DatosTransferencia> datosTransferenciaAsocInt =
                            this._datosTransferenciaServicios.ConsultarDatosTransferenciaPorAsociado(item1.AsociadoInt);
                        if (datosTransferenciaAsocInt.Count > 0)
                        {
                            item1.DatosTransferencia = datosTransferenciaAsocInt[0];

                            if (item1.DatosTransferencia != null)
                            {
                                if (item1.DatosTransferencia.Beneficiario != null)
                                {
                                    item1.Beneficiario = item1.DatosTransferencia.Beneficiario;
                                }

                                ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                                operacionTransferenciaBuscar.Valor = "Transferencia";
                                ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                                item1.FormaPago = valorEncontrado.Valor;
                                item1.DatosBancariosStr = datosTransferenciaAsocInt[0].BancoBenef + Environment.NewLine + datosTransferenciaAsocInt[0].Direccion +
                                    Environment.NewLine + datosTransferenciaAsocInt[0].Cuenta + Environment.NewLine + datosTransferenciaAsocInt[0].Aba +
                                    Environment.NewLine + datosTransferenciaAsocInt[0].Swif;
                                item1.NumeroSecuenciaTransferencia = item1.DatosTransferencia.Id;
                            }
                        }
                        else
                        {
                            item1.Beneficiario = item1.AsociadoInt.Nombre;
                            ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                            operacionTransferenciaBuscar.Valor = "Cheque";
                            ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                            item1.FormaPago = valorEncontrado.Valor;
                        }   

                        #region CODIGO ORIGINAL COMENTADO
                        /*if (item1.DatosTransferencia != null)
                        {
                            if (item1.DatosTransferencia.Beneficiario != null)
                            {
                                item1.Beneficiario = item1.DatosTransferencia.Beneficiario;
                            }

                            ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                            operacionTransferenciaBuscar.Valor = "Transferencia";
                            ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                            item1.FormaPago = valorEncontrado.Valor;
                            item1.DatosBancariosStr = datosTransferenciaAsocInt[0].BancoBenef + Environment.NewLine + datosTransferenciaAsocInt[0].Direccion +
                                Environment.NewLine + datosTransferenciaAsocInt[0].Cuenta + Environment.NewLine + datosTransferenciaAsocInt[0].Aba +
                                Environment.NewLine + datosTransferenciaAsocInt[0].Swif;
                        }*/
                        
                        #endregion

                        sumatoria = 0;
                    }

                    this._asociadosConsolidados = asociadosIntConsolidado;
                }
                else //CUANDO EXISTEN DATOS CONSOLIDADOS GUARDADOS
                {
                    foreach (FacAsociadoIntConsolidadoCxPInt datoConsolidado in this._asociadosConsolidados)
                    {
                        facturasDelAsociado = new List<FacInternacional>();
                        codigoAsociadoInt = datoConsolidado.AsociadoInt.Id;
                        foreach (FacInternacional item2 in facturasInternacionales)
                        {

                            if (codigoAsociadoInt == item2.Asociado_o.Id)
                            {
                                //sumatoria += item2.Monto;
                                facturasDelAsociado.Add(item2);
                            }
                        }

                        datoConsolidado.FacturasIntConsolidadas = facturasDelAsociado;
                        datoConsolidado.FormasDePago = formasDePago;
                        IList<DatosTransferencia> datosTransferenciaAsocInt =
                            this._datosTransferenciaServicios.ConsultarDatosTransferenciaPorAsociado(datoConsolidado.AsociadoInt);
                        if (datosTransferenciaAsocInt.Count > 0)
                        {
                            if (datoConsolidado.NumeroSecuenciaTransferencia != 0)
                            {
                                foreach (DatosTransferencia  transferencia in datosTransferenciaAsocInt)
                                {
                                    if (transferencia.Id == datoConsolidado.NumeroSecuenciaTransferencia)
                                    {
                                        datoConsolidado.DatosTransferencia = transferencia;
                                        break;
                                    }
                                    contador++;
                                }
                            }
                            //datoConsolidado.DatosTransferencia = datosTransferenciaAsocInt[0];
                            if (datoConsolidado.DatosTransferencia != null)
                            {
                                if (datoConsolidado.DatosTransferencia.Beneficiario != null)
                                {
                                    if(datoConsolidado.Beneficiario.Equals(datoConsolidado.DatosTransferencia.Beneficiario))
                                        datoConsolidado.Beneficiario = datoConsolidado.DatosTransferencia.Beneficiario;
                                }

                                ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                                operacionTransferenciaBuscar.Valor = datoConsolidado.FormaPago;
                                ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                                datoConsolidado.FormaPago = valorEncontrado.Valor;
                                if (datoConsolidado.DatosBancariosStr.Equals(String.Empty))
                                {
                                    datoConsolidado.DatosBancariosStr = datosTransferenciaAsocInt[contador].BancoBenef + Environment.NewLine + datosTransferenciaAsocInt[contador].Direccion +
                                    Environment.NewLine + datosTransferenciaAsocInt[contador].Cuenta + Environment.NewLine + datosTransferenciaAsocInt[contador].Aba +
                                    Environment.NewLine + datosTransferenciaAsocInt[contador].Swif;
                                }
                                
                                //datoConsolidado.DatosBancariosStr = datosTransferenciaAsocInt[0].BancoBenef + Environment.NewLine + datosTransferenciaAsocInt[0].Direccion +
                                //    Environment.NewLine + datosTransferenciaAsocInt[0].Cuenta + Environment.NewLine + datosTransferenciaAsocInt[0].Aba +
                                //    Environment.NewLine + datosTransferenciaAsocInt[0].Swif;
                            }

                            contador = 0;
                        }
                        else
                        {
                            if(datoConsolidado.Beneficiario.Equals(datoConsolidado.AsociadoInt.Nombre))
                                datoConsolidado.Beneficiario = datoConsolidado.AsociadoInt.Nombre;

                            ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                            operacionTransferenciaBuscar.Valor = datoConsolidado.FormaPago;
                            ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                            datoConsolidado.FormaPago = valorEncontrado.Valor;
                        }

                        #region CODIGO ORIGINAL COMENTADO
                        /*if (datoConsolidado.DatosTransferencia != null)
                        {
                            if (datoConsolidado.DatosTransferencia.Beneficiario != null)
                            {
                                datoConsolidado.Beneficiario = datoConsolidado.DatosTransferencia.Beneficiario;
                            }

                            ListaDatosValores operacionTransferenciaBuscar = new ListaDatosValores();
                            operacionTransferenciaBuscar.Valor = datoConsolidado.FormaPago;
                            ListaDatosValores valorEncontrado = this.BuscarListaDeDatosValores(formasDePago, operacionTransferenciaBuscar);
                            datoConsolidado.FormaPago = valorEncontrado.Valor;
                            datoConsolidado.DatosBancariosStr = datosTransferenciaAsocInt[0].BancoBenef + Environment.NewLine + datosTransferenciaAsocInt[0].Direccion +
                                Environment.NewLine + datosTransferenciaAsocInt[0].Cuenta + Environment.NewLine + datosTransferenciaAsocInt[0].Aba +
                                Environment.NewLine + datosTransferenciaAsocInt[0].Swif;
                        }*/
                        
                        #endregion

                    }

                    asociadosIntConsolidado = this._asociadosConsolidados;
                }


                //this._asociadosConsolidados = asociadosIntConsolidado;

                if (!this._soloVer)
                {
                    this._ventana.FacturasAprobadas = asociadosIntConsolidado;
                    this._ventana.TotalHits = asociadosIntConsolidado.Count.ToString();
                    
                }
                else
                {
                    this._ventana.HabilitarListaSoloVer();
                    this._ventana.HabilitarBotonModificar();
                    this._ventana.FacturasAprobadasSoloVer = asociadosIntConsolidado;
                    this._ventana.TotalHits = asociadosIntConsolidado.Count.ToString();
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
        /// Metodo que verifica si el Objeto FacAsociadoIntConsolidadoCxPInternacional existe ya
        /// </summary>
        /// <param name="asociadosIntConsolidado">Lista de objetos FacAsociadoIntConsolidadoCxPInt</param>
        /// <returns>True si existe; False en caso contrario</returns>
        private bool BuscarEnListaFacAsociadoIntConsolidado(IList<FacAsociadoIntConsolidadoCxPInt> asociadosIntConsolidado, int codigoAsocidadoInt)
        {
            bool existe = false;

            foreach (FacAsociadoIntConsolidadoCxPInt item in asociadosIntConsolidado)
            {
                if (item.AsociadoInt.Id == codigoAsocidadoInt)
                    existe = true;
                else
                    existe = false;
            }

            return existe;
        }



        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegCxPInternacional,
                Recursos.Ids.fac_SeguimientoCxPInternacional);
        }


        /// <summary>
        /// Metodo que registra el pago de facturas consolidadas en la base de datos 
        /// </summary>
        /// <param name="objeto">Facturas consolidadas</param>
        public void RegistrarPagoConsolidado(object facturaConsolidadaAsoc)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FacAsociadoIntConsolidadoCxPInt facConsolidadoAsociado = (FacAsociadoIntConsolidadoCxPInt)facturaConsolidadaAsoc;

                RegistroPagoConsolidadoCxPInternacional ventanaRegistroPago = 
                    new RegistroPagoConsolidadoCxPInternacional(facConsolidadoAsociado);
                ventanaRegistroPago.ShowDialog();

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
        /// Metodo que muestra en una ventana las facturas utilizadas en el consolidado de pago CxP Internacional
        /// </summary>
        /// <param name="facturaConsolidadaAsoc">Estructura de datos que trae todos los datos para hacer el registro de pago internacional</param>
        public void VerDetalleDeConsolidado(object facturaConsolidadaAsoc)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FacAsociadoIntConsolidadoCxPInt facConsolidadoAsociado = (FacAsociadoIntConsolidadoCxPInt)facturaConsolidadaAsoc;

                this.Navegar(new FacInternacionalPagadasConsolidadas(facConsolidadoAsociado, this._ventana));
                

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
        /// Metodo para guardar los datos de consolidacion que se encuentran en pantalla en la tabla FAC_CXP_INT_CONSOLIDA
        /// </summary>
        public void Modificar()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            bool exitoso = false;
            int contadorId = 1;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._asociadosConsolidados.Count > 0)
                {
                    IList<FacAsociadoIntConsolidadoCxPInt> datosConsolidadosExistentes = 
                        this._facAsociadosIntConsolidadoCxPIntServicios.ConsultarTodos();

                    if (datosConsolidadosExistentes.Count > 0)
                    {
                        BorrarRegistrosExistentes(datosConsolidadosExistentes);
                    }

                    foreach (FacAsociadoIntConsolidadoCxPInt asociadoConsolidado in this._asociadosConsolidados)
                    {
                        FacAsociadoIntConsolidadoCxPInt facAsocAux = new FacAsociadoIntConsolidadoCxPInt();
                        facAsocAux = asociadoConsolidado;
                        facAsocAux.Id = contadorId;
                        exitoso = 
                            this._facAsociadosIntConsolidadoCxPIntServicios.InsertarOModificar(asociadoConsolidado, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            contadorId++;
                            continue;
                        }
                    }

                    this._ventana.Mensaje("Se han guardado los datos de Consolidación exitosamente", 2);
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
        /// Metodo que borrar todo el contenido de la tabla que se usa para guardar los datos de consolidacion
        /// </summary>
        private void BorrarRegistrosExistentes(IList<FacAsociadoIntConsolidadoCxPInt> datosExistentes)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacAsociadoIntConsolidadoCxPInt item in datosExistentes)
                {
                    exitoso = this._facAsociadosIntConsolidadoCxPIntServicios.Eliminar(item, UsuarioLogeado.Hash);
                    if (exitoso)
                    {
                        exitoso = false;
                        continue;
                    }
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
        /// Metodo que toma el codigo del Asociado Internacional seleccionado y muestra sus datos de transferencia
        /// </summary>
        /// <param name="objeto">Objeto que contiene todos los datos de la transaccion de consolidacion de un Asociado Internacional</param>
        public void VerDatosTransferenciaAsociado(object datosConsolidacionAsociado)
        {

            IList<FacAsociadoIntConsolidadoCxPInt> consolida;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FacAsociadoIntConsolidadoCxPInt datosConsolidacionSeleccionado = (FacAsociadoIntConsolidadoCxPInt)datosConsolidacionAsociado;
                Asociado asociadoInternacional = datosConsolidacionSeleccionado.AsociadoInt;

                IList<DatosTransferencia> transferencias = this._datosTransferenciaServicios.ConsultarDatosTransferenciaPorAsociado(asociadoInternacional);
                asociadoInternacional.DatosTransferencias = transferencias;

                if (_soloVer)
                {
                    consolida = (IList<FacAsociadoIntConsolidadoCxPInt>)this._ventana.FacturasAprobadasSoloVer;
                }
                else
                {
                    consolida = (IList<FacAsociadoIntConsolidadoCxPInt>)this._ventana.FacturasAprobadas;
                }

                             
                
                this.Navegar(new ListaDatosTransferencias(asociadoInternacional,consolida, this._ventana,true,this._soloVer,this._ventanaPadre));


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
        /// Metodo que exporta los datos de consolidacion a una hoja Excel
        /// </summary>
        public void ExportarExcelDatosConsolidados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                IList<FacAsociadoIntConsolidadoCxPInt> datosConsolidadosPresentados;
                DataTable datosExportar = CrearDataTableExportacion();
                String titulo = String.Empty;

                if (this._soloVer)
                {
                    datosConsolidadosPresentados = (IList<FacAsociadoIntConsolidadoCxPInt>)this._ventana.FacturasAprobadasSoloVer;
                    titulo = "Preview";
                }
                else
                {
                    datosConsolidadosPresentados = (IList<FacAsociadoIntConsolidadoCxPInt>)this._ventana.FacturasAprobadas;
                    titulo = "Definitivo";
                }

                datosExportar = LlenarDataTableExportacion(datosConsolidadosPresentados, datosExportar);

                this._ventana.ExportarDatosConsolidadosExcel(titulo, datosExportar);

                this._ventana.Mensaje("Datos de Consolidación exportados con éxito", 2);


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

        private DataTable LlenarDataTableExportacion(IList<FacAsociadoIntConsolidadoCxPInt> datosConsolidadosPresentados, 
                                                     DataTable datosExportar)
        {

            DataTable datos = datosExportar;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacAsociadoIntConsolidadoCxPInt item in datosConsolidadosPresentados)
                {
                    DataRow filaNueva = datos.NewRow();
                    filaNueva["Asociado No"] = item.AsociadoInt.Id;
                    filaNueva["Asociado"] = item.AsociadoInt.Nombre;
                    filaNueva["Monto"] = item.MontoConsolidado;
                    filaNueva["Forma Pago"] = item.FormaPago;

                    if (!String.IsNullOrEmpty(item.DatosBancariosStr))
                        filaNueva["Datos Bancarios"] = item.DatosBancariosStr;

                    if(item.DatosTransferencia != null)
                        filaNueva["Beneficiario"] = item.DatosTransferencia.Beneficiario;
                    else
                        filaNueva["Beneficiario"] = item.AsociadoInt.Nombre;

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
        /// Metodo que crea el DataTable necesario para mostrar los datos de Consolidacion en una hoja Excel
        /// </summary>
        /// <returns></returns>
        private DataTable CrearDataTableExportacion()
        {

            DataTable datos = new DataTable();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                datos.Columns.Add("Asociado No", typeof(int));
                datos.Columns.Add("Asociado", typeof(string));
                datos.Columns.Add("Monto", typeof(double));
                datos.Columns.Add("Forma Pago", typeof(string));
                datos.Columns.Add("Datos Bancarios", typeof(string));
                datos.Columns.Add("Beneficiario", typeof(string));
                

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


        public String ObtenerTituloReporte(string tipo)
        {
            String titulo = String.Empty;

            if (tipo.Equals("Preview"))
            {
                titulo = "Datos Consolidados - Vista Previa";
            }
            else
                titulo = "Datos Consolidados Definitivos";

            return titulo;
        }
    }
}
