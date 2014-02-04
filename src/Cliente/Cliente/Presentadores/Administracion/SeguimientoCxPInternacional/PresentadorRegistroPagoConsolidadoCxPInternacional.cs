using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorRegistroPagoConsolidadoCxPInternacional : PresentadorBase
    {
        private IRegistroPagoConsolidadoCxPInternacional _ventana;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FacAsociadoIntConsolidadoCxPInt _facConsolidadaAsociado;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IFacBancoServicios _facBancosServicios;
        private IFacInternacionalServicios _facInternacionalServicios;
        private IFacAsociadoIntConsolidadoCxPIntServicios _facAsociadoIntConsolidadoCxPIntServicios;


        /// <summary>
        /// Constructor por defecto que recibe la ventana actual
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorRegistroPagoConsolidadoCxPInternacional(IRegistroPagoConsolidadoCxPInternacional ventana,
                                                                  object facConsolidadaAsociado)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._facConsolidadaAsociado = (FacAsociadoIntConsolidadoCxPInt)facConsolidadaAsociado;

                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._facBancosServicios = (IFacBancoServicios)Activator.GetObject(typeof(IFacBancoServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacBancoServicios"]);

                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);

                this._facAsociadoIntConsolidadoCxPIntServicios = (IFacAsociadoIntConsolidadoCxPIntServicios)Activator.GetObject(typeof(IFacAsociadoIntConsolidadoCxPIntServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacAsociadoIntConsolidadoCxPIntServicios"]);

                CargarPagina();

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
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CargarCombos();

                this._ventana.FechaPago = DateTime.Today.ToString();
                
                this._ventana.FocoPredeterminado();

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
        /// Metodo que carga el combo de los bancos 
        /// </summary>
        private void CargarCombos()
        {
            String formaPago = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                formaPago = this._facConsolidadaAsociado.FormaPago;
                ListaDatosValores formaPagoConsolidado = new ListaDatosValores();
                formaPagoConsolidado.Valor = formaPago;

                //Llenando combo de Bancos
                IList<FacBanco> bancos = this._facBancosServicios.ObtenerFacBancosFiltro(null);
                this._ventana.Bancos = bancos;

                //Llenando combo de Formas de Pago y seleccionando el que ya trae la estructura de datos
                IList<ListaDatosValores> formasDePago = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiFacFormasDePagoCxPInt));
                this._ventana.TiposPago = formasDePago;
                this._ventana.TipoPago = this.BuscarListaDeDatosValores(formasDePago, formaPagoConsolidado);

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
        /// Metodo que registra los pagos en la tabla FAC_CXP_INT de las facturas consolidadas
        /// </summary>
        public void Aceptar()
        {
            bool exitoso = false;
            bool datoConsolidadoEliminado = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FacAsociadoIntConsolidadoCxPInt facturasConsolidadosAsociado = this._facConsolidadaAsociado;

                IList<FacInternacional> facturasAPagar = facturasConsolidadosAsociado.FacturasIntConsolidadas;

                foreach (FacInternacional factura in facturasAPagar)
                {
                    factura.FechaPago = DateTime.Parse(this._ventana.FechaPago);
                    factura.TipoPago = GetTipoPago();
                    factura.Banco = (FacBanco)this._ventana.Banco;
                    factura.DescripcionPago = this._ventana.DescripcionPago;
                    exitoso = this._facInternacionalServicios.InsertarOModificar(factura, UsuarioLogeado.Hash);
                    if (exitoso)
                    {
                        exitoso = false;
                        continue;
                    }
                    else
                    {
                        this._ventana.Mensaje
                            ("Hubo un problema insertando el pago de la proforma: " + factura.Id.Value.ToString(), 0);
                        break;
                    }
                }

                datoConsolidadoEliminado = this._facAsociadoIntConsolidadoCxPIntServicios.Eliminar(facturasConsolidadosAsociado, UsuarioLogeado.Hash);
                if (datoConsolidadoEliminado)
                {
                    this._ventana.Mensaje("Proceso de Registro de Pagos consolidados terminados", 0);
                }
                

                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                this._ventana.CerrarVentanaException();
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        /// <summary>
        /// Metodo que obtiene el caracter que describe el tipo de pago
        /// </summary>
        /// <returns></returns>
        private char GetTipoPago()
        {
            char tipoPagoCaracter = ' ';
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ListaDatosValores tipoPagoSeleccionado = (ListaDatosValores)this._ventana.TipoPago;
                if (tipoPagoSeleccionado.Valor.Equals("Cheque"))
                    tipoPagoCaracter = 'C';
                else if (tipoPagoSeleccionado.Valor.Equals("Transferencia"))
                    tipoPagoCaracter = 'T';
                else if (tipoPagoSeleccionado.Valor.Equals("Deposito"))
                    tipoPagoCaracter = 'D';

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return tipoPagoCaracter;
        }
    }
}
