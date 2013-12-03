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
        private IList<ListaDatosValores> _tiposSaldos;
        private IList<ListaDatosValores> _departamentos;
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

                double valorInicial = 0.00;


                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoClientes, "");

                this._ventana.TotalHits = "0";

                this._ventana.RangoInferior = "1";

                this._ventana.TotalUSD = valorInicial.ToString("N");

                this._ventana.TotalBSF = this._ventana.TotalPorVencer = this._ventana.TotalVencido = this._ventana.TotalReportes = this._ventana.TotalOtrosDptos = valorInicial.ToString("N");

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
                Moneda monedaPorDefecto = new Moneda();
                monedaPorDefecto.Id = "US";
                this._ventana.Moneda = this.BuscarMoneda(monedas, monedaPorDefecto);

                IList<ListaDatosValores> tiposSaldos = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoSaldoSegClientes));
                this._tiposSaldos = tiposSaldos;
                this._ventana.TiposSaldos = tiposSaldos;
                ListaDatosValores tipoSaldoPorDefecto = new ListaDatosValores();
                tipoSaldoPorDefecto.Valor = "TODOS";
                this._ventana.TipoSaldo = this.BuscarListaDeDatosValores(tiposSaldos, tipoSaldoPorDefecto);

                IList<ListaDatosValores> departamentos =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiDepartamentoSegClientes));
                this._departamentos = departamentos;
                this._ventana.Departamentos = departamentos;
                ListaDatosValores departamentoPorDefecto = new ListaDatosValores();
                departamentoPorDefecto.Valor = "TODOS";
                this._ventana.Departamento = this.BuscarListaDeDatosValores(departamentos, departamentoPorDefecto);

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
        /// Metodo que realiza la consulta sobre la base de datos para obtener la Data de los saldos de la vista FAC_ASO_SALDO 
        /// </summary>
        public void ObtenerResumenGeneralSaldos()
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
                    this._ventana.TotalUSD = CalcularTotalColumna("BSALDO",datos);
                    this._ventana.TotalBSF = CalcularTotalColumna("BSALDO_BF", datos);
                    this._ventana.TotalPorVencer = CalcularTotalColumna("PORVENCER", datos);
                    this._ventana.TotalVencido = CalcularTotalColumna("VENCIDO", datos);
                    this._ventana.TotalReportes = CalcularTotalColumna("REPORTES", datos);
                    this._ventana.TotalOtrosDptos = CalcularTotalColumna("OTROS_DPTOS", datos);
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
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Cayo aqui:", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

        }

        
        /// <summary>
        /// Metodo que calcula los totales de las columnas BSALDO y BSALDO_BF para el resumen de totales generales al inicio del modulo
        /// ESTE METODO NO PERTENECE AL PIVOT
        /// </summary>
        /// <param name="nombreColumna">Columna donde se encuentran los datos que se van a sumar</param>
        /// <param name="data">DataTable con los datos</param>
        /// <returns>Resultado de la sumatoria que se colocara en la ventana en el espacio de Totales</returns>
        private string CalcularTotalColumna(string nombreColumna, DataTable data)
        {
            String total = String.Empty;
            decimal sumaTotal =0, cantidad =0;
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

                filtro.TipoSaldo = this._ventana.TipoSaldo != null ? ((ListaDatosValores)this._ventana.TipoSaldo).Valor : "TODOS";

                filtro.Departamento = this._ventana.Departamento != null ? ((ListaDatosValores)this._ventana.Departamento).Valor : "TODOS";

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

                            this.Navegar(new ListaDatosPivotSeguimientoClientes(filtro, this._ventana.EjeXSeleccionado, this._ventana.EjeYSeleccionado, this._ventana.EjeZSeleccionado, this._ventana.TotalUSD, this._ventana.TotalBSF, this._ventana));
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
            this._ventana.TotalBSF = numeroInicial.ToString("N");
            this._ventana.TotalUSD = numeroInicial.ToString("N");
            this._ventana.TotalPorVencer = this._ventana.TotalVencido = this._ventana.TotalReportes = this._ventana.TotalOtrosDptos = numeroInicial.ToString("N");
            this._ventana.TipoSaldo = null;
            this._ventana.Departamento = null;

            IList<Moneda> monedas = this._monedaServicios.ConsultarTodos();
            this._ventana.Monedas = monedas;
            Moneda monedaPorDefecto = new Moneda();
            monedaPorDefecto.Id = "US";
            this._ventana.Moneda = this.BuscarMoneda(monedas, monedaPorDefecto);
            
            ListaDatosValores ordenamientoPorDefecto = new ListaDatosValores();
            ordenamientoPorDefecto.Valor = "DESC";
            this._ventana.Ordenamiento = this.BuscarListaDeDatosValores((IList<ListaDatosValores>)this._ventana.Ordenamientos, ordenamientoPorDefecto);

            ListaDatosValores tipoSaldoPorDefecto = new ListaDatosValores();
            tipoSaldoPorDefecto.Valor = "TODOS";
            this._ventana.TipoSaldo = this.BuscarListaDeDatosValores(this._tiposSaldos, tipoSaldoPorDefecto);

            ListaDatosValores departamentoPorDefecto = new ListaDatosValores();
            departamentoPorDefecto.Valor = "TODOS";
            this._ventana.Departamento = this.BuscarListaDeDatosValores(this._departamentos, departamentoPorDefecto);

            PredeterminarEjes(false);
            this._ventana.DesactivarEjesPivot();
            

        }

        public void PredeterminarEjes(bool iniciarVentana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                IList<ListaDatosValores> camposVistaSeguimientoClientes =
                            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCamposVistaSeguimientoClientes));


                if (iniciarVentana)
                {
                    this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSeguimientoClientes; 
                } 
                

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
