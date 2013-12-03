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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeCobranzas;
using Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoDeCobranzas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.ContratosServicios;
using Diginsoft.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Data;

namespace Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeCobranzas
{
    class PresentadorConsultarSeguimientoDeCobranzas : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarSeguimientoDeCobranzas _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IList<Asociado> _asociados;
        private IMonedaServicios _monedaServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IMediosGestionServicios _medioGestionServicios;
        private IUsuarioServicios _usuarioServicios;
        private ISeguimientoDeCobranzasServicios _seguimientoDeCobranzasServicios;
        private DataTable _datosCrudos;
        


        public PresentadorConsultarSeguimientoDeCobranzas(IConsultarSeguimientoDeCobranzas ventana)
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
                this._medioGestionServicios = (IMediosGestionServicios)Activator.GetObject(typeof(IMediosGestionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MediosGestionServicios"]);
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
                this._seguimientoDeCobranzasServicios = (ISeguimientoDeCobranzasServicios)Activator.GetObject(typeof(ISeguimientoDeCobranzasServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["SeguimientoDeCobranzasServicios"]);
                
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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoCobranzas,
                Recursos.Ids.SeguimientoDeCobranzas);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarSeguimientoCobranzas, "");

                this._ventana.TotalHits = "0";

                this._ventana.TotalGestiones = "0";

                InicializarCombos(false);

                
                //PredeterminarEjes();

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
        /// Metodo que inicializa los combos para los filtros de Resumen General
        /// </summary>
        private void InicializarCombos(bool reiniciarVentana)
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

                IList<MediosGestion> _listaMediosGestion = this._medioGestionServicios.ConsultarTodos();
                this._ventana.Medios = _listaMediosGestion;
                //this._ventana.Medio = this.BuscarMediosGestion(_listaMediosGestion, _listaMediosGestion[1]);

                IList<Usuario> usuarios = this._usuarioServicios.ConsultarTodos();
                usuarios = this.FiltrarUsuariosRepetidos(usuarios);
                this._ventana.Usuarios = usuarios;
                Usuario usuarioPorDefecto = UsuarioLogeado;
                this._ventana.Usuario = this.BuscarUsuarioPorIniciales(usuarios, usuarioPorDefecto);

                IList<ListaDatosValores> ordenes =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrdenSeguimientoClientes));
                this._ventana.Ordenamientos = ordenes;


                if (!reiniciarVentana)
                {
                    IList<ListaDatosValores> camposVistaSeguimientoCobranzas =
                                this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCamposVistaSeguimientoCobranzas));
                    this._ventana.CamposEjeXPivot = this._ventana.CamposEjeYPivot = this._ventana.CamposEjeZPivot = camposVistaSeguimientoCobranzas; 
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
        /// Metodo que consulta un asociado filtrado desde la ventana 
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
        /// Metodo que coloca en el campo de texto el nombre del Asociado seleccionado
        /// </summary>
        /// <returns>True si la operacion se realizo con exito, False en caso contrario</returns>
        public bool CambiarAsociado()
        {
            
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.Asociado != null)
                {
                    this._ventana.IdAsociado = ((Asociado)this._ventana.Asociado).Nombre;

                    retorno = true;
                }

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
            
            return retorno;
        }



        public void GenerarResumenGeneralDeGestiones()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                FiltroDataCrudaCobranza filtroParaConsultar = new FiltroDataCrudaCobranza();
                DataTable datos = new DataTable();

                filtroParaConsultar = ObtenerFiltroDeLaPantalla();

                datos = this._seguimientoDeCobranzasServicios.GenerarDatosResumenGeneral(filtroParaConsultar);

                String totalGestiones = CalcularTotalColumna("TOTAL_GES", datos);

                if (datos.Rows.Count > 0)
                {
                    this._ventana.Resultados = datos.DefaultView;
                    this._datosCrudos = datos;
                    this._ventana.ActivarEjesPivot();
                    this._ventana.TotalHits = datos.Rows.Count.ToString();
                    this._ventana.TotalGestiones = totalGestiones;
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
        /// Metodo que recoge los filtros seleccionados en la pantalla
        /// </summary>
        /// <returns></returns>
        private FiltroDataCrudaCobranza ObtenerFiltroDeLaPantalla()
        {
            FiltroDataCrudaCobranza filtro = new FiltroDataCrudaCobranza();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                filtro.Moneda = this._ventana.Moneda != null ? ((Moneda)this._ventana.Moneda).Id : null;

                filtro.Usuario = this._ventana.Usuario != null ? ((Usuario)this._ventana.Usuario).Iniciales : null;

                filtro.MedioGestion = this._ventana.Medio != null ? ((MediosGestion)this._ventana.Medio).Descripcion : null;

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
        /// Metodo que calcula el total vertical de cualquier columna numerica del Resumen General de Gestiones
        /// </summary>
        /// <param name="nombreColumna">Nombre de columna a totalizar</param>
        /// <param name="data">Datos generados para poder hacer el calculo</param>
        /// <returns>Sumatoria de Gestiones del Resumen General de Gestiones</returns>
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

                total = sumaTotal.ToString("N0");

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
        /// Metodo para predeterminar los campos para generar la data pivot de Seguimiento de Cobranzas
        /// </summary>
        public void PredeterminarEjes()
        {
            
        }

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

                            FiltroDataCrudaCobranza filtro = ObtenerFiltroDeLaPantalla();

                            this.Navegar(new ListaDatosPivotSeguimientoCobranzas(filtro, this._ventana.EjeXSeleccionado, this._ventana.EjeYSeleccionado, this._ventana.EjeZSeleccionado, this._ventana));
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
        /// Metodo que inicializa la pantalla al principio
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
            this._ventana.DesactivarEjesPivot();
            this._ventana.Resultados = null;
            this._ventana.Moneda = null;
            this._ventana.Monedas = null;
            this._ventana.Medio = null;
            this._ventana.Medios = null;
            this._ventana.Usuario = null;
            this._ventana.Usuarios = null;
            this._ventana.TotalGestiones = "0";

            ListaDatosValores ordenamientoPorDefecto = new ListaDatosValores();
            ordenamientoPorDefecto.Valor = "DESC";
            this._ventana.Ordenamiento = this.BuscarListaDeDatosValores((IList<ListaDatosValores>)this._ventana.Ordenamientos, ordenamientoPorDefecto);

            
            InicializarCombos(true);

            this._ventana.DesactivarEjesPivot();

            
            
        }
    }
}
