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
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional
{
    class PresentadorFacInternacionalPagadasConsolidadas: PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IFacInternacionalPagadasConsolidadas _ventana;
        private FacAsociadoIntConsolidadoCxPInt _facAsociadoConsolidadas;
        private IAsociadoServicios _asociadoServicios;
        private IFacInternacionalServicios _facInternacionalServicios;
        private IFacFacturaProformaServicios _facFacturaProformaServicios;

        /// <summary>
        /// Constructor por defecto que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="ventanaPadre">Ventana que antecede a esta ventana</param>
        public PresentadorFacInternacionalPagadasConsolidadas(IFacInternacionalPagadasConsolidadas ventana, 
                                                              object facAsociadoConsolidadas,
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
                this._facAsociadoConsolidadas = (FacAsociadoIntConsolidadoCxPInt)facAsociadoConsolidadas;

                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._facInternacionalServicios = (IFacInternacionalServicios)Activator.GetObject(typeof(IFacInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacInternacionalServicios"]);
                this._facFacturaProformaServicios = (IFacFacturaProformaServicios)Activator.GetObject(typeof(IFacFacturaProformaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FacFacturaProformaServicios"]);
                
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

                this._ventana.FacturasConsolidadas = this._facAsociadoConsolidadas.FacturasIntConsolidadas;

                IList<FacInternacional> listaFacturasConsolidadas = this._facAsociadoConsolidadas.FacturasIntConsolidadas;

                this._ventana.TotalMontoConsolidado = CalcularMontoTotalConsolidar(listaFacturasConsolidadas).ToString("N");

                this._ventana.TotalHits = listaFacturasConsolidadas.Count.ToString();

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


        /// <summary>
        /// Metodo que devuelve el resultado de la sumatoria de los montos de una serie de facturas internacionales consolidadas
        /// </summary>
        /// <param name="listaFacturasConsolidadas">Lista de facturas internacionales consolidadas</param>
        /// <returns>Monto total a consolidar de ese asociado en particular</returns>
        private Double CalcularMontoTotalConsolidar(IList<FacInternacional> listaFacturasConsolidadas)
        {

            double montoTotal = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                foreach (FacInternacional factura in listaFacturasConsolidadas)
                {
                    montoTotal += factura.Monto;                    
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



        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleListaResumenDatosSegCxPInternacional,
                Recursos.Ids.fac_SeguimientoCxPInternacional);
        }

        /// <summary>
        /// Metodo que consulta la factura proforma internacional seleccionada y que ademas esta consolidada para el 
        /// registro del pago
        /// </summary>
        public void IrConsultarFacturaProforma()
        {

            Mouse.OverrideCursor = Cursors.Wait;
            FacFacturaProforma proformaAux = new FacFacturaProforma();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.FacturaConsolidada != null)
                {
                    FacInternacional facInternacionalAux = (FacInternacional)this._ventana.FacturaConsolidada;
                    proformaAux.Id = facInternacionalAux.Id.Value;
                    proformaAux.Local = 'I';
                    IList<FacFacturaProforma> proformasInternacionales = 
                        this._facFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(proformaAux);
                    if (proformasInternacionales.Count > 0)
                    {
                        this.Navegar(new ConsultarFacFacturaProforma(proformasInternacionales[0], this._ventana));
                    }
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
    }
}
