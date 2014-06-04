using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Ventanas.Pirateria.Casos;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos
{
    class PresentadorConsultarCasos : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IConsultarCasos _ventana;
        private int _filtroValido;
        private IList<Caso> _casos;

        private ICasoServicios _casoServicios;
        private ICasoBaseServicios _casoBaseServicios;
        private ITipoCasoServicios _tipoCasoServicios;
        private IAccionServicios _accionServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IServicioServicios _servicioServicios;
        
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        public PresentadorConsultarCasos(IConsultarCasos ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                Caso casoFiltro = new Caso();
                this._ventana.Caso = casoFiltro;

                this._casoServicios = (ICasoServicios)Activator.GetObject(typeof(ICasoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CasoServicios"]);
                this._casoBaseServicios = (ICasoBaseServicios)Activator.GetObject(typeof(ICasoBaseServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CasoBaseServicios"]);
                this._tipoCasoServicios = (ITipoCasoServicios)Activator.GetObject(typeof(ITipoCasoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoCasoServicios"]);
                this._accionServicios = (IAccionServicios)Activator.GetObject(typeof(IAccionServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AccionServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);

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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarCasosPirateria,
                Recursos.Ids.ConsultarCasosPirateria);
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

                this.ActualizarTitulo();

                LlenarCombos();

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

        }


        /// <summary>
        /// Metodo que llena los combos de la pantalla
        /// </summary>
        private void LlenarCombos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Combo Origen de Caso
                IList<ListaDatosValores> origenesCaso = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrigenClienteAsociado));
                origenesCaso.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.OrigenesCaso = origenesCaso;

                //Combo Situaciones de Caso
                IList<Servicio> situacionesCaso = this._servicioServicios.ConsultarTodos();
                situacionesCaso.Insert(0, new Servicio("NGN"));
                this._ventana.SituacionesCaso = situacionesCaso;

                //Combo de Tipos de Caso
                IList<TipoCaso> tiposDeCaso = this._tipoCasoServicios.ConsultarTodos();
                tiposDeCaso.Insert(0, new TipoCaso("NGN"));
                this._ventana.TiposDeCaso = tiposDeCaso;

                //Combo de Acciones de Caso
                IList<Accion> accionesCaso = this._accionServicios.ConsultarTodos();
                accionesCaso.Insert(0, new Accion("NGN"));
                this._ventana.AccionesDeCaso = accionesCaso;


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
        /// Metodo que inicializa la ventana de Consultar Casos
        /// </summary>
        public void LimpiarTodo()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarCasos());

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
        /// Metodo que Consulta un Asociado
        /// </summary>
        public void BuscarAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                Asociado asociadoABuscar = new Asociado();

                asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                     int.Parse(this._ventana.IdAsociadoFiltrar) : int.MinValue;

                asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                         this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

                if ((asociadoABuscar.Id != int.MinValue) || !(asociadoABuscar.Nombre.Equals("")))
                {
                    IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);
                    asociados.Insert(0, new Asociado(int.MinValue));
                    this._ventana.Asociados = asociados;

                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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
        /// Metodo que cambia el texto del Asociado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Asociado haya sido valido, false en caso contrario</returns>
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
                    this._ventana.AsociadoFiltro = ((Asociado)this._ventana.Asociado).Nombre;
                    retorno = true;
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

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de buscar el interesado definido en el filtro
        /// </summary>
        public void BuscarInteresado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado interesadoABuscar = new Interesado();

                interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                                       int.Parse(this._ventana.IdInteresadoFiltrar) : 0;

                interesadoABuscar.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                                           this._ventana.NombreInteresadoFiltrar.ToUpper() : "";

                if ((interesadoABuscar.Id != 0) || !(interesadoABuscar.Nombre.Equals("")))
                {
                    IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                    interesados.Insert(0, new Interesado(int.MinValue));
                    this._ventana.Interesados = interesados;
                }
                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
        /// Metodo que cambia el texto del interesado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el interesado haya sido valido, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.Interesado != null)
                {
                    this._ventana.InteresadoFiltro = ((Interesado)this._ventana.Interesado).Nombre;
                    retorno = true;
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

            return retorno;

        }


        /// <summary>
        /// Metodo que obtiene una Lista de Casos de acuerdo a un Filtro determinado 
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

                Caso casoAuxiliar = ObtenerCasoFiltro();

                if (this._filtroValido >= 2)
                {
                    this._casos = this._casoServicios.ObtenerCasosFiltro(casoAuxiliar);
                    if (this._casos.Count > 0)
                    {
                        this._ventana.Resultados = this._casos;
                        this._ventana.TotalHits = this._casos.Count.ToString();
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 0);

                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);
                

                /*MarcaTercero MarcaTerceroAuxiliar = ObtenerMarcaTerceroFiltro();

                if (_filtroValido >= 2)
                {
                    this._marcas = this._marcaServicios.ObtenerMarcaTerceroFiltro(MarcaTerceroAuxiliar);

                    IList<MarcaTercero> marcasDesinfladas = new List<MarcaTercero>();

                    foreach (var marca in this._marcas)
                    {
                        NullFK = false;
                        MarcaTerceroAuxiliar = new MarcaTercero(marca.Id.ToUpper());
                        MarcaTerceroAuxiliar.Anexo = marca.Anexo;
                        Asociado asociadoAuxiliar = new Asociado();
                        Interesado interesadoAuxiliar = new Interesado();

                        MarcaTerceroAuxiliar.Descripcion = marca.Descripcion != null ? marca.Descripcion : "";

                        try
                        {


                            if ((marca.Asociado != null) && (!string.IsNullOrEmpty(marca.Asociado.Nombre)))
                            {

                                asociadoAuxiliar.Nombre = marca.Asociado.Nombre;
                                MarcaTerceroAuxiliar.Asociado = asociadoAuxiliar;

                            }
                        }
                        catch (NHibernate.LazyInitializationException ex)
                        {

                            marca.Asociado = null;
                            NullFK = true;
                        }

                        try
                        {



                            if ((marca.Interesado != null) && (!string.IsNullOrEmpty(marca.Interesado.Nombre)))
                            {

                                interesadoAuxiliar.Nombre = marca.Interesado.Nombre;
                                MarcaTerceroAuxiliar.Interesado = interesadoAuxiliar;



                            }
                        }
                        catch (NHibernate.LazyInitializationException ex)
                        {
                            marca.Interesado = null;
                            NullFK = true;
                        }

                        MarcaTerceroAuxiliar.Nacional = marca.Nacional;
                        MarcaTerceroAuxiliar.Internacional = marca.Internacional;
                        MarcaTerceroAuxiliar.CodigoInscripcion = marca.CodigoInscripcion;

                        MarcaTerceroAuxiliar.FechaPublicacion = marca.FechaPublicacion != null ? marca.FechaPublicacion : null;
                        if (!NullFK)
                            marcasDesinfladas.Add(MarcaTerceroAuxiliar);

                    }
                    // marcasDesinfladas = _marcas;
                    this._ventana.Resultados = marcasDesinfladas;
                    // this._ventana.Resultados = _marcas;
                    this._ventana.TotalHits = marcasDesinfladas.Count.ToString();
                    if (marcasDesinfladas.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);*/

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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que obtiene los filtros de la pantalla para hacer la consulta
        /// </summary>
        /// <returns>Caso que sirve como filtro para la consulta</returns>
        private Caso ObtenerCasoFiltro()
        {
            Caso caso = (Caso)this._ventana.Caso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.IdCaso.Equals(String.Empty))
                {
                    caso.Id = int.Parse(this._ventana.IdCaso);
                    this._filtroValido = 2;
                }

                if(!string.IsNullOrEmpty(this._ventana.DescripcionCaso))
                {
                    caso.Descripcion = this._ventana.DescripcionCaso;
                    this._filtroValido = 2;
                }

                if(!string.IsNullOrEmpty(this._ventana.FechaCaso))
                {
                    caso.Fecha = DateTime.Parse(this._ventana.FechaCaso);
                    this._filtroValido = 2;
                }

                if ((this._ventana.OrigenCaso != null) && (!((ListaDatosValores)this._ventana.OrigenCaso).Valor.Equals("NGN")))
                {
                    caso.Origen = ((ListaDatosValores)this._ventana.OrigenCaso).Valor;
                    this._filtroValido = 2;
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    caso.Asociado = (Asociado)this._ventana.Asociado;
                    _filtroValido = 2;
                }

                if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                {
                    caso.Interesado = (Interesado)this._ventana.Interesado;
                    _filtroValido = 2;
                }

                if ((null != this._ventana.SituacionCaso) && (!((Servicio)this._ventana.SituacionCaso).Id.Equals("NGN")))
                {
                    caso.Servicio = (Servicio)this._ventana.SituacionCaso;
                    _filtroValido = 2;
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

            return caso;
        }


        /// <summary>
        /// Metodo que muestra en pantalla el Caso seleccionado en la lista de resultados
        /// </summary>
        public void VerCasoSeleccionado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CasoSeleccionado != null)
                {
                    Caso casoSeleccionado = (Caso)this._ventana.CasoSeleccionado;
                    this.Navegar(new GestionarCaso(casoSeleccionado, this._ventana));
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
