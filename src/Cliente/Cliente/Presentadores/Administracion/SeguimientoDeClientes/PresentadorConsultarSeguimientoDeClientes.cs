using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Data;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeClientes
{
    class PresentadorConsultarSeguimientoDeClientes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarSeguimientoDeClientes _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IList<Asociado> _asociados;
        private IMonedaServicios _monedaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private ISeguimientoClientesServicios _seguimientoClientesServicios;
        private DataTable _datosCrudos;


        /// <summary>
        /// Constructor por defecto que recibe una ventana
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarSeguimientoDeClientes(IConsultarSeguimientoDeClientes ventana)
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
                this._monedaServicios = (IMonedaServicios)Activator.GetObject(typeof(IMonedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MonedaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._seguimientoClientesServicios = (ISeguimientoClientesServicios)Activator.GetObject(typeof(ISeguimientoClientesServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoClientesServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoClientes, "");

                this._ventana.TotalHits = "0";

                this._ventana.RangoInferior = "1";

                CargarCombos();

                PredeterminarEjes();
                
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
        /// Metodo que carga los combos al iniciar la ventana
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Moneda> monedas = this._monedaServicios.ConsultarTodos();
                this._ventana.Monedas = monedas;

                //IList<ListaDatosValores> meses = 
                //    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiMesSeguimientoClientes));
                //this._ventana.Meses = meses;

                IList<ListaDatosValores> ordenes =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrdenSeguimientoClientes));
                this._ventana.Ordenamientos = ordenes;

                IList<ListaDatosValores> camposVistaSeguimientoClientes = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCamposVistaSeguimientoClientes));
                this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSeguimientoClientes;


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
        /// Metodo que consulta un Asociado por su codigo o por su nombre
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
        /// Metodo que cambia el asociado seleccionado en la consulta 
        /// </summary>
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
        /// Metodo que realiza la consulta sobre la base de datos para obtener la Data Cruda segun el filtro dado 
        /// </summary>
        public void ConsultarDataCruda()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FiltroDataCruda filtroParaConsultar = new FiltroDataCruda();
                DataTable datos = new DataTable();

                filtroParaConsultar = ObtenerFiltroDeDataCrudaDeLaPantalla();

                datos = this._seguimientoClientesServicios.ObtenerDatosSaldos(filtroParaConsultar);

                if (datos.Rows.Count > 0)
                {
                    this._ventana.Resultados = datos.DefaultView;
                    this._datosCrudos = datos;
                    this._ventana.ActivarEjesPivot();
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
            finally
            {
                Mouse.OverrideCursor = null;
            }

        }


        /// <summary>
        /// Metodo que obtiene los filtros necesarios para la consulta en base de datos
        /// </summary>
        /// <returns></returns>
        private FiltroDataCruda ObtenerFiltroDeDataCrudaDeLaPantalla()
        {

            FiltroDataCruda filtro = new FiltroDataCruda();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                filtro.Moneda = this._ventana.Moneda != null ? ((Moneda)this._ventana.Moneda).Id : null;

                //filtro.Anio = !this._ventana.Annio.Equals("") ? int.Parse(this._ventana.Annio) : 0;

                //filtro.Mes = this._ventana.Mes != null ? int.Parse(((ListaDatosValores)this._ventana.Mes).Valor) : 0;

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
        /// Metodo que muestra el resultado de los datos al generar la tabla pivot con los campos seleccionados
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
                            
                            FiltroDataCruda filtro = ObtenerFiltroDeDataCrudaDeLaPantalla();

                            this.Navegar(new ListaDatosPivotSeguimientoClientes(filtro, this._ventana.EjeXSeleccionado, this._ventana.EjeYSeleccionado, this._ventana.EjeZSeleccionado, this._ventana));
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

        public void LimpiarCampos()
        {
            this._ventana.TotalHits = "0";
            this._ventana.EjeXSeleccionado = null;
            this._ventana.EjeYSeleccionado = null;
            this._ventana.EjeZSeleccionado = null;
            this._ventana.Asociado = null;
            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.DesactivarEjesPivot();
            this._ventana.Resultados = null;
            this._ventana.RangoSuperior = null;
            
        }

        public void PredeterminarEjes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<ListaDatosValores> camposVistaSeguimientoClientes =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCamposVistaSeguimientoClientes));
                this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSeguimientoClientes;

                ListaDatosValores ejeX = new ListaDatosValores();
                ejeX.Valor = "AÑO";
                this._ventana.EjeXSeleccionado = this.BuscarListaDeDatosValores(camposVistaSeguimientoClientes, ejeX);

                ListaDatosValores ejeY = new ListaDatosValores();
                ejeY.Valor = "CASOCIADO";
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
    }
}
