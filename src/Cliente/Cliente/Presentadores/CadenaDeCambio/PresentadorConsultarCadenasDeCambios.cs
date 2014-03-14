using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.CadenaDeCambio;
using Trascend.Bolet.Cliente.Ventanas.CadenaDeCambio;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ControlesByT.Ventanas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.CadenaDeCambio
{
    class PresentadorConsultarCadenasDeCambios : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ICadenaDeCambiosServicios _cadenaDeCambiosServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IConsultarCadenasDeCambios _ventana;
        private IList<ListaDatosValores> _tiposCadenaCambios;
        private int _filtroValido;


        public PresentadorConsultarCadenasDeCambios(IConsultarCadenasDeCambios ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                //this._ventanaPadre = ventanaPadre;
                
                this._cadenaDeCambiosServicios = (ICadenaDeCambiosServicios)Activator.GetObject(typeof(ICadenaDeCambiosServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CadenaDeCambiosServicios"]);
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCadenaDeCambios,
                       Recursos.Ids.CadenaDeCambios);
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

                ActualizarTitulo();
                
                this._tiposCadenaCambios = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiTipoCadenaCambios));
                this._ventana.TiposCadenasDeCambios = this._tiposCadenaCambios;
                this._ventana.TotalHits = "0";


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
        /// Metodo para obtener cadenas de cambios por filtro
        /// </summary>
        public void Consultar()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _filtroValido = 0;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CadenaDeCambios cadenaCambiosAux = ObtenerCadenaDeCambiosPantalla();


                if (this._filtroValido >= 2)
                {
                    IList<CadenaDeCambios> cadenasCambio = this._cadenaDeCambiosServicios.ObtenerCadenasCambioFiltro(cadenaCambiosAux);

                    if (cadenasCambio.Count > 0)
                    {
                        foreach (CadenaDeCambios item in cadenasCambio)
                        {
                            if (item.TipoCambio.Equals("M"))
                                item.TipoCambioDescripcion = "Marca";
                            else if (item.TipoCambio.Equals("P"))
                                item.TipoCambioDescripcion = "Patente";
                        }
                        this._ventana.Resultados = cadenasCambio;
                        this._ventana.TotalHits = cadenasCambio.Count.ToString();
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 2);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);

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
        /// Metodo que obtiene la CadenaDeCambios de la pantalla para realizar la consulta
        /// </summary>
        /// <returns></returns>
        private CadenaDeCambios ObtenerCadenaDeCambiosPantalla()
        {

            CadenaDeCambios cadenaCambios = new CadenaDeCambios();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!String.IsNullOrEmpty(this._ventana.IdCadenaCambios))
                {
                    this._filtroValido = 2;
                    cadenaCambios.Id = int.Parse(this._ventana.IdCadenaCambios);
                }
                else
                    cadenaCambios.Id = int.MinValue;

                if (this._ventana.TipoCadenaDeCambios != null)
                {
                    this._filtroValido = 2;
                    cadenaCambios.TipoCambio = ((ListaDatosValores)this._ventana.TipoCadenaDeCambios).Valor;
                }
                else
                    cadenaCambios.TipoCambio = null;

                if (!String.IsNullOrEmpty(this._ventana.CodigoOperacionCadenaCambios))
                {
                    this._filtroValido = 2;
                    cadenaCambios.CodigoOperacion = int.Parse(this._ventana.CodigoOperacionCadenaCambios);
                }
                else
                    cadenaCambios.CodigoOperacion = int.MinValue;

                if ((this._ventana.FechaCadenaCambios != null)&&(!this._ventana.FechaCadenaCambios.Equals("")))
                {
                    DateTime fecha = DateTime.Parse(this._ventana.FechaCadenaCambios);
                    _filtroValido = 2;
                    cadenaCambios.FechaCadenaCambio = fecha;
                }
                else
                    cadenaCambios.FechaCadenaCambio = null;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

            return cadenaCambios;
        }


        /// <summary>
        /// Metodo para limpiar los campos de la pantalla y regresarla a su condicion de inicio
        /// </summary>
        public void LimpiarCampos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.IdCadenaCambios = String.Empty;
                this._ventana.CodigoOperacionCadenaCambios = String.Empty;
                this._ventana.TipoCadenaDeCambios = null;
                this._ventana.FechaCadenaCambios = null;
                this._ventana.Resultados = null;
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
        }

        /// <summary>
        /// Metodo que carga en la pantalla la Cadena de Cambios seleccionada en la consulta
        /// </summary>
        public void IrVerCadenaDeCambios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CadenaCambioSeleccionada != null)
                {
                    CadenaDeCambios cadenaCambios = (CadenaDeCambios)this._ventana.CadenaCambioSeleccionada;
                    this.Navegar(new GestionarCadenaDeCambios(cadenaCambios, this._ventana));
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
    }
}
