using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ControlesByT.Ventanas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Pirateria.Casos;

namespace Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos
{
    class PresentadorGestionarCaso : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarCaso _ventana;
        private bool _agregar;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<TipoCaso> _tiposCasos;
        private IList<Accion> _acciones;
        private Marca _marca;
        private Patente _patente;
        private IList<Auditoria> _auditorias;
        
        private ICasoServicios _casoServicios;
        private ICasoBaseServicios _casoBaseServicios;
        private ITipoCasoServicios _tipoCasoServicios;
        private IAccionServicios _accionServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IServicioServicios _servicioServicios;
        private IMarcaServicios _marcaServicios;
        private IPatenteServicios _patenteServicios;
        private IInternacionalServicios _internacionalServicios;
        private INacionalServicios _nacionalServicios;
        

        /// <summary>
        /// Constructor predeterminado que obtiene un caso y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana Actual</param>
        /// <param name="caso">Caso a visualizar</param>
        /// <param name="ventanaPadre">Ventana que antecede a esta ventana</param>
        public PresentadorGestionarCaso(IGestionarCaso ventana, object caso, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                if (ventanaPadre != null)
                    this._ventanaPadre = ventanaPadre;

                if (caso != null)
                {
                    this._ventana.Caso = (Caso)caso;
                }
                else
                {
                    //Caso nuevoCaso = new Caso();
                    //this._ventana.Caso = nuevoCaso;
                    this._agregar = true;
                }

                
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
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._internacionalServicios = (IInternacionalServicios)Activator.GetObject(typeof(IInternacionalServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InternacionalServicios"]);
                this._nacionalServicios = (INacionalServicios)Activator.GetObject(typeof(INacionalServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["NacionalServicios"]);

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
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCasoPirateria,
                Recursos.Ids.CasosPirateria);
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
                LlenarCombosVentana();

                if (!this._agregar)
                {

                    Caso caso = (Caso)this._ventana.Caso;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                    
                    IList<Asociado> listaAsociado = new List<Asociado>();
                    Asociado primerAsociado = new Asociado(int.MinValue);
                    listaAsociado.Add(primerAsociado);
                    listaAsociado.Add(caso.Asociado);
                    this._ventana.AsociadosConsultados = listaAsociado;
                    this._ventana.AsociadoSeleccionado = this.BuscarAsociado((IList<Asociado>)this._ventana.AsociadosConsultados, ((Caso)this._ventana.Caso).Asociado);

                    this._ventana.AsociadoCaso = caso.Asociado != null ? caso.Asociado.Nombre : "";
                    this._ventana.IdAsociadoCaso = caso.Asociado != null ? caso.Asociado.Id.ToString() : "";

                    if ((null != caso.Asociado) && (caso.Asociado.TipoCliente != null))
                        this._ventana.PintarAsociado(caso.Asociado.TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");


                    IList<Interesado> listaInteresados = new List<Interesado>();
                    Interesado primerInteresado = new Interesado(int.MinValue);
                    listaInteresados.Add(primerInteresado);
                    listaInteresados.Add(caso.Interesado);
                    this._ventana.InteresadosConsultados = listaInteresados;
                    this._ventana.InteresadoSeleccionado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosConsultados, ((Caso)this._ventana.Caso).Interesado);
                    this._ventana.InteresadoCaso = caso.Interesado != null ? caso.Interesado.Nombre : "";
                    this._ventana.IdInteresadoCaso = caso.Interesado != null ? caso.Interesado.Id.ToString() : "";
                    this._ventana.InteresadoCiudad = caso.Interesado != null ? caso.Interesado.Ciudad : "";

                    if (caso.TiposCaso != null)
                    {
                        ActualizarTiposDeCaso(caso);
                        this._ventana.ListaTiposCaso = caso.TiposCaso;
                    }

                    if (caso.Acciones != null)
                    {
                        ActualizarAccionesDeCaso(caso);
                        this._ventana.ListaAccionesCaso = caso.Acciones;
                    }

                    CargarCasosBase(ref caso);

                    if (caso.CasosBase != null)
                    {
                        if (caso.CasosBase.Count > 0)
                        {
                            this._ventana.CasosBases = caso.CasosBase;
                            this._ventana.mostrarListaCasosBase();
                        }
                    }
                    else
                        this._ventana.CasosBases = null;

                    Auditoria auditoria = new Auditoria();
                    auditoria.Fk = caso.Id;
                    auditoria.Tabla = "PRT_CASOS";
                    this._auditorias = this._casoServicios.AuditoriaPorFkyTabla(auditoria);

                }
                else
                {
                    this._ventana.HabilitarCampos = true;
                }

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
        /// Metodo que carga los Casos Base de un Caso en pantalla
        /// </summary>
        /// <param name="caso">Caso de la pantalla</param>
        private void CargarCasosBase(ref Caso caso)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CasoBase casoBaseAuxiliar = new CasoBase();
                casoBaseAuxiliar.Caso = caso;

                IList<CasoBase> casosBase = this._casoBaseServicios.ObtenerCasosBasePorCaso(casoBaseAuxiliar);
                if (casosBase.Count > 0)
                    caso.CasosBase = casosBase;
                else
                    caso.CasosBase = null;

                this._ventana.Caso = caso;

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
        /// Metodo que actualiza la lista de Acciones en el combobox de las Acciones de un Caso
        /// </summary>
        /// <param name="caso">Caso de la pantalla</param>
        private void ActualizarAccionesDeCaso(Caso caso)
        {
            IList<Accion> accionesActualizadas = new List<Accion>();
            bool encontrado = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<Accion> accionesAsociadas = caso.Acciones;

                if (caso.Acciones.Count > 0)
                {
                    foreach (Accion item in this._acciones)
                    {
                        foreach (Accion item1 in caso.Acciones)
                        {
                            if (item1.Id.Equals(item.Id))
                            {
                                encontrado = true;
                                break;
                            }

                        }

                        if (!encontrado)
                            accionesActualizadas.Add(item);
                        else
                            encontrado = false;
                    }

                    this._acciones = null;
                    this._acciones = accionesActualizadas;
                    this._ventana.AccionesCaso = this._acciones;
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
        /// Metodo que actualiza la lista de tipos de Caso en el Combo de Tipos de Caso
        /// </summary>
        /// <param name="caso">Caso de la pantalla</param>
        private void ActualizarTiposDeCaso(Caso caso)
        {
            IList<TipoCaso> tiposCasosActualizados = new List<TipoCaso>();
            bool encontrado = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<TipoCaso> tiposCasoAsociados = caso.TiposCaso;

                if (tiposCasoAsociados.Count > 0)
                {
                    foreach (TipoCaso item in this._tiposCasos)
                    {
                        foreach (TipoCaso item1 in tiposCasoAsociados)
                        {
                            if (item1.Id.Equals(item.Id))
                            {
                                encontrado = true;
                                break;
                            }

                        }

                        if (!encontrado)
                            tiposCasosActualizados.Add(item);
                        else
                            encontrado = false;
                    }

                    this._tiposCasos = null;
                    this._tiposCasos = tiposCasosActualizados;
                    this._ventana.TiposDeCaso = this._tiposCasos;
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
        /// Metodo que llena los combos de la Ventana
        /// </summary>
        private void LlenarCombosVentana()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IList<ListaDatosValores> origenes = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrigenClienteAsociado));
                origenes.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.OrigenesCaso = origenes;
                if(!this._agregar)
                {
                    ListaDatosValores origenBuscar = new ListaDatosValores();
                    origenBuscar.Valor = ((Caso)this._ventana.Caso).Origen;
                    this._ventana.OrigenCaso = this.BuscarListaDeDatosValores(origenes, origenBuscar);
                }

                this._tiposCasos = this._tipoCasoServicios.ConsultarTodos();
                this._tiposCasos.Insert(0, new TipoCaso("NGN"));
                this._ventana.TiposDeCaso = this._tiposCasos;
                
                this._acciones = this._accionServicios.ConsultarTodos();
                this._acciones.Insert(0, new Accion("NGN"));
                this._ventana.AccionesCaso = this._acciones;
                
                IList<Servicio> situaciones = this._servicioServicios.ConsultarTodos();
                situaciones.Insert(0, new Servicio("NGN"));
                this._ventana.SituacionesCaso = situaciones;
                if (!this._agregar)
                {
                    Servicio situacionBuscar = ((Caso)this._ventana.Caso).Servicio;
                    if (situacionBuscar != null)
                    {
                        this._ventana.SituacionCaso = this.BuscarServicio(situaciones, situacionBuscar);
                    }
                }

                IList<ListaDatosValores> tiposBase = 
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoBasePirateria));
                tiposBase.Insert(0, new ListaDatosValores("NGN"));
                this._ventana.TiposBase = tiposBase;


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
        /// Metodo que inserta o actualiza un Caso de Pirateria
        /// </summary>
        public void Aceptar()
        {
            int? exitoso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                    this._ventana.ActivarBotones(true);
                }
                else
                {
                    Caso caso = ObtenerCasoPantalla();

                    if (caso.Fecha != null)
                    {
                        if ((caso.Descripcion != null) && (!caso.Descripcion.Equals(String.Empty)))
                        {
                            if (this._agregar)
                            {
                                exitoso = this._casoServicios.InsertarOModificarCaso(caso, UsuarioLogeado.Hash);
                                if (exitoso != null)
                                {
                                    caso.Id = exitoso.Value;
                                }
                            }
                            else
                            {
                                bool actualizado = this._casoServicios.InsertarOModificar(caso, UsuarioLogeado.Hash);

                            }

                            if (this._ventana.CasosBases != null)
                                GuardarCasosBase(caso);

                            if (this._agregar)
                            {
                                this._ventana.Mensaje(string.Format("El caso {0} fue ingresado con éxito", caso.Id.ToString()), 2);
                                this._ventana.IdCaso = caso.Id.ToString();
                                this._agregar = false;
                                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                                this._ventana.HabilitarCampos = false;
                            }
                            else
                            {
                                this._ventana.Mensaje(string.Format("El caso {0} fue modificado con éxito", caso.Id.ToString()), 2);
                                this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                                this._ventana.HabilitarCampos = false;
                                this._ventana.ActivarBotones(false);
                            }


                        }
                        else
                            this._ventana.Mensaje("El Caso debe tener una Descripción", 0);
                    }
                    else
                        this._ventana.Mensaje("El Caso debe tener una Fecha de Apertura", 0);
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
        /// Metodo que guarda los Casos Base de un Caso de Pirateria
        /// </summary>
        /// <param name="caso">Caso del Caso Base</param>
        private void GuardarCasosBase(Caso caso)
        {
            IList<CasoBase> casosBase;
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                casosBase = (IList<CasoBase>)this._ventana.CasosBases;
                if (casosBase.Count != 0)
                {
                    foreach (CasoBase casoBase in casosBase)
                    {
                        casoBase.Caso = caso;
                        exitoso = this._casoBaseServicios.InsertarOModificar(casoBase, UsuarioLogeado.Hash);
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
        /// Metodo que carga la entidad Caso que se va a guardar o a actualizar
        /// </summary>
        /// <returns>Entidad Caso a insertar o actualizar</returns>
        private Caso ObtenerCasoPantalla()
        {
            Caso caso = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._agregar)
                    caso = (Caso)this._ventana.Caso;
                else
                    caso = new Caso();

                if (null != this._ventana.FechaCaso)
                    caso.Fecha = DateTime.Parse(this._ventana.FechaCaso);
                else
                    caso.Fecha = null;

                if (!String.IsNullOrEmpty(this._ventana.DescripcionCaso))
                    caso.Descripcion = this._ventana.DescripcionCaso;

                if(null != this._ventana.OrigenCaso)
                    caso.Origen = !((ListaDatosValores)this._ventana.OrigenCaso).Id.Equals("NGN") ? ((ListaDatosValores)this._ventana.OrigenCaso).Valor : null;

                if (null != this._ventana.AsociadoSeleccionado)
                    caso.Asociado = ((Asociado)this._ventana.AsociadoSeleccionado).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSeleccionado : null;

                if (null != this._ventana.InteresadoSeleccionado)
                    caso.Interesado = ((Interesado)this._ventana.InteresadoSeleccionado).Id != int.MinValue ? (Interesado)this._ventana.InteresadoSeleccionado : null;

                if (null != this._ventana.SituacionCaso)
                    caso.Servicio = !((Servicio)this._ventana.SituacionCaso).Id.Equals("NGN") ? (Servicio)this._ventana.SituacionCaso : null;

                if (null != this._ventana.ListaTiposCaso)
                    caso.TiposCaso = (IList<TipoCaso>)this._ventana.ListaTiposCaso;

                if (null != this._ventana.ListaAccionesCaso)
                    caso.Acciones = (IList<Accion>)this._ventana.ListaAccionesCaso;

                if (!String.IsNullOrEmpty(this._ventana.PrimeraReferencia))
                    caso.PrimeraReferencia = this._ventana.PrimeraReferencia;

                if (!String.IsNullOrEmpty(this._ventana.ComentariosCaso))
                    caso.Observacion = this._ventana.ComentariosCaso;

                if ((IList<CasoBase>)this._ventana.CasosBases != null)
                {
                    if (((IList<CasoBase>)this._ventana.CasosBases).Count > 0)
                        caso.CasosBase = (IList<CasoBase>)this._ventana.CasosBases;
                    else
                        caso.CasosBase = null;
                }
                
                if (this._agregar)
                    caso.Operacion = "CREATE";
                else
                    caso.Operacion = "MODIFY";

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
        /// Metodo que visualizar los datos del Asociado del Caso 
        /// </summary>
        public void VerAsociadoCaso()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.IdAsociadoCaso != null) && (!this._ventana.IdAsociadoCaso.Equals(String.Empty)))
                {
                    Asociado asociadoConsultar = new Asociado();
                    asociadoConsultar.Id = int.Parse(this._ventana.IdAsociadoCaso);
                    IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoConsultar);
                    if (asociados.Count > 0)
                    {
                        this.Navegar(new ConsultarAsociado(asociados[0],this._ventana,false));
                    }
                    else
                        this._ventana.Mensaje("El Asociado no existe", 0);
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
        /// Metodo que despliega la informacion del Interesado del Caso
        /// </summary>
        public void VerInteresadoCaso()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.IdInteresadoCaso != null) && (!this._ventana.IdInteresadoCaso.Equals(String.Empty)))
                {
                    Interesado interesadoConsultar = new Interesado();
                    interesadoConsultar.Id = int.Parse(this._ventana.IdInteresadoCaso);
                    IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoConsultar);
                    if (interesados.Count > 0)
                    {
                        this.Navegar(new ConsultarInteresado(interesados[0], this._ventana));
                    }
                    else
                        this._ventana.Mensaje("El Interesado no existe", 0);
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
        /// Metodo para Consultar Asociados
        /// </summary>
        public void ConsultarAsociados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IEnumerable<Asociado> asociadosFiltrados = this._asociados;
                bool bandera = false;
                Asociado asociado = new Asociado();

                if (this._ventana.IdAsociadoConsultar != "")
                {
                    asociado.Id = int.Parse(this._ventana.IdAsociadoConsultar);
                    bandera = true;
                }

                if (this._ventana.AsociadoConsultar != "")
                {
                    asociado.Nombre = this._ventana.AsociadoConsultar.ToUpper();
                    bandera = true;
                }

                if (bandera)
                {
                    IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociado);
                    if (asociados.Count != 0)
                    {
                        Asociado primerAsociado = new Asociado(int.MinValue);
                        asociados.Insert(0, primerAsociado);
                        asociadosFiltrados = asociados;
                        if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                        {
                            this._ventana.AsociadosConsultados = asociadosFiltrados.ToList<Asociado>();
                        }
                        else
                            this._ventana.AsociadosConsultados = this._asociados;
                    }
                    else
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
        /// Metodo que carga el Asociado en la ventana
        /// </summary>
        public void CargarAsociados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._agregar)
                {
                    if (((Caso)this._ventana.Caso).Asociado != null)
                    {
                        this._ventana.IdAsociadoCaso = ((Caso)this._ventana.Caso).Asociado.Id.ToString();
                        this._ventana.AsociadoCaso = ((Caso)this._ventana.Caso).Asociado.Nombre;
                    }
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
        /// Metodo que cambia el Asociado seleccionado en la lista de Consulta de Asociados
        /// </summary>
        public void CambiarAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Asociado)this._ventana.AsociadoSeleccionado != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSeleccionado);
                    this._ventana.AsociadoCaso = ((Asociado)this._ventana.AsociadoSeleccionado).Nombre;
                    this._ventana.IdAsociadoCaso = ((Asociado)this._ventana.AsociadoSeleccionado).Id.ToString();
                    if (asociado != null)
                        if (asociado.TipoCliente != null)
                            this._ventana.PintarAsociado(asociado.TipoCliente.Id);
                        else
                            this._ventana.PintarAsociado("1");
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();
                }
                else
                    this._ventana.PintarAsociado("5");
                
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
        /// Metodo que carga el Interesado que tiene el caso
        /// </summary>
        public void CargarInteresados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._agregar)
                {
                    if (((Caso)this._ventana.Caso).Interesado != null)
                    {
                        this._ventana.IdInteresadoCaso = ((Caso)this._ventana.Caso).Interesado.Id.ToString();
                        this._ventana.InteresadoCaso = ((Caso)this._ventana.Caso).Interesado.Nombre;
                        this._ventana.InteresadoCiudad = ((Caso)this._ventana.Caso).Interesado.Ciudad;
                    }
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
        /// Metodo que consulta Interesados para asignarselos al Caso
        /// </summary>
        public void ConsultarInteresados()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IEnumerable<Interesado> interesadosFiltrados = this._interesados;
                Interesado interesado = new Interesado();
                bool bandera = false;

                if (this._ventana.IdInteresadoConsultar != "")
                {
                    interesado.Id = int.Parse(this._ventana.IdInteresadoConsultar);
                    bandera = true;
                }
                if (this._ventana.InteresadoConsultar != "")
                {
                    interesado.Nombre = this._ventana.InteresadoConsultar.ToUpper();
                    bandera = true;
                }
                if (bandera)
                {
                    IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                    if (interesados.Count != 0)
                    {
                        Interesado primerInteresado = new Interesado(int.MinValue);
                        interesados.Insert(0, primerInteresado);
                        interesadosFiltrados = interesados;
                        if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                            this._ventana.InteresadosConsultados = interesadosFiltrados.ToList<Interesado>();
                        else
                            this._ventana.InteresadosConsultados = this._interesados;
                    }
                    else
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
        /// Metodo que selecciona el Interesado que elija el usuario
        /// </summary>
        public void CambiarInteresado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Interesado)this._ventana.InteresadoSeleccionado != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSeleccionado);
                    this._ventana.InteresadoCaso = ((Interesado)this._ventana.InteresadoSeleccionado).Nombre;
                    this._ventana.IdInteresadoCaso = ((Interesado)this._ventana.InteresadoSeleccionado).Id.ToString();
                    this._ventana.InteresadoCiudad = ((Interesado)this._ventana.InteresadoSeleccionado).Ciudad;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();
                
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
        /// Metodo que pinta en el campo de texto el nombre del Servicio seleccionado en el combo
        /// </summary>
        public void VisualizarNombreServicio()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.SituacionCaso != null)
                {
                    if (!((Servicio)this._ventana.SituacionCaso).Id.Equals("NGN"))
                    {
                        this._ventana.SituacionDescripcion = ((Servicio)this._ventana.SituacionCaso).Descripcion;
                    }
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
        /// Metodo que agrega un Tipo de Caso a la lista de tipos de casos del Caso de Pirateria
        /// </summary>
        public void AgregarTipoCaso()
        {
            IList<TipoCaso> tiposDeCasos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                
                if(!((TipoCaso)this._ventana.TipoDeCaso).Id.Equals("NGN"))
                {
                    
                    if (null == this._ventana.ListaTiposCaso)
                        tiposDeCasos = new List<TipoCaso>();
                    else
                        tiposDeCasos = (IList<TipoCaso>)this._ventana.ListaTiposCaso;

                    tiposDeCasos.Insert(0, (TipoCaso)this._ventana.TipoDeCaso);
                    if(this._ventana.ListaTiposCaso != null)
                        this._ventana.ListaTiposCaso = null;
                    this._ventana.ListaTiposCaso = tiposDeCasos;

                    this._tiposCasos.Remove((TipoCaso)this._ventana.TipoDeCaso);
                    this._ventana.TiposDeCaso = null;
                    this._ventana.TiposDeCaso = this._tiposCasos;

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
        /// Metodo que quita un Tipo de Caso de la lista de tipos de casos de pirateria
        /// </summary>
        public void QuitarTipoCaso()
        {
            IList<TipoCaso> tiposDeCasos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                if(this._ventana.ListaTipoCaso != null)
                {
                    if (null == this._ventana.ListaTiposCaso)
                        tiposDeCasos = new List<TipoCaso>();
                    else
                        tiposDeCasos = (IList<TipoCaso>)this._ventana.ListaTiposCaso;

                    this._tiposCasos.Add((TipoCaso)this._ventana.ListaTipoCaso);
                    this._ventana.TiposDeCaso = null;
                    this._ventana.TiposDeCaso = this._tiposCasos;

                    tiposDeCasos.Remove((TipoCaso)this._ventana.ListaTipoCaso);
                    this._ventana.ListaTiposCaso = null;
                    this._ventana.ListaTiposCaso = tiposDeCasos;
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
        /// Metodo que agrega una accion en la lista de Acciones del Caso
        /// </summary>
        public void AgregarAccionCaso()
        {
            IList<Accion> accionesDeCasos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Accion)this._ventana.AccionCaso).Id.Equals("NGN"))
                {

                    if (null == this._ventana.ListaAccionesCaso)
                        accionesDeCasos = new List<Accion>();
                    else
                        accionesDeCasos = (IList<Accion>)this._ventana.ListaAccionesCaso;

                    accionesDeCasos.Insert(0, (Accion)this._ventana.AccionCaso);
                    if (this._ventana.ListaAccionesCaso != null)
                        this._ventana.ListaAccionesCaso = null;
                    this._ventana.ListaAccionesCaso = accionesDeCasos;

                    this._acciones.Remove((Accion)this._ventana.AccionCaso);
                    this._ventana.AccionesCaso = null;
                    this._ventana.AccionesCaso = this._acciones;

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
        /// Metodo que elimina una Accion de las Acciones del Caso
        /// </summary>
        public void QuitarAccionCaso()
        {
            IList<Accion> accionesDeCasos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.ListaAccionCaso != null)
                {
                    if (null == this._ventana.ListaAccionesCaso)
                        accionesDeCasos = new List<Accion>();
                    else
                        accionesDeCasos = (IList<Accion>)this._ventana.ListaAccionesCaso;

                    this._acciones.Add((Accion)this._ventana.ListaAccionCaso);
                    this._ventana.AccionesCaso = null;
                    this._ventana.AccionesCaso = this._acciones;

                    accionesDeCasos.Remove((Accion)this._ventana.ListaAccionCaso);
                    this._ventana.ListaAccionesCaso = null;
                    this._ventana.ListaAccionesCaso = accionesDeCasos;
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
        /// Metodo que agrega un Caso Base a la lista de Casos Bases 
        /// </summary>
        public void AgregarCasoBase()
        {
            IList<CasoBase> casosBase;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (null == this._ventana.CasosBases)
                {
                    casosBase = new List<CasoBase>();
                    this._ventana.CasosBases = casosBase;
                }
                else
                    casosBase = (IList<CasoBase>)this._ventana.CasosBases;

                //Creando el nuevo CasoBase
                if (!this._ventana.IdCasoBase.Equals(String.Empty))
                {
                    CasoBase casoBase = new CasoBase();
                    casoBase.Id = this._ventana.IdCasoBase;

                    if (!this._agregar)
                        casoBase.Caso = (Caso)this._ventana.Caso;
                    else
                        casoBase.Caso = new Caso();

                    casoBase.TipoBase = ((ListaDatosValores)this._ventana.TipoBase).Valor;
                    if (this._ventana.ByT != null)
                        casoBase.BInterno = this._ventana.ByT.Value;
                    else
                        casoBase.BInterno = false;
                    if (((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("MAR"))
                    {
                        casoBase.Descripcion = this._marca.Descripcion;
                    }
                    else if (((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("PAT"))
                    {
                        casoBase.Descripcion = this._patente.Descripcion;
                    }
                    else if (((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("DAU"))
                    {
                        casoBase.Descripcion = ((ListaDatosValores)this._ventana.TipoBase).Descripcion + " Caso No. " + casoBase.Id;
                    }
                    else if (((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("OTR"))
                    {
                        casoBase.Descripcion = ((ListaDatosValores)this._ventana.TipoBase).Descripcion + " Caso No. " + casoBase.Id;
                    }

                    casoBase.TipoCodigoBase = this._ventana.ITipoCasoBase;

                    if (!this._ventana.ClaseInternacionalCasoBase.Equals(String.Empty))
                    {
                        Internacional internacionalAux = new Internacional();
                        internacionalAux.Id = int.Parse(this._ventana.ClaseInternacionalCasoBase);
                        internacionalAux = this._internacionalServicios.ConsultarPorId(internacionalAux);
                        casoBase.Internacional = internacionalAux;
                    }
                    else
                        casoBase.Internacional = null;

                    if (!this._ventana.ClaseNacionalCasoBase.Equals(String.Empty))
                    {
                        Nacional nacionalAux = new Nacional();
                        nacionalAux.Id = int.Parse(this._ventana.ClaseNacionalCasoBase);
                        casoBase.Nacional = nacionalAux;
                    }
                    else
                        casoBase.Nacional = null;

                    //Agregando el nuevo caso base a la lista
                    casosBase.Add(casoBase);
                    if (this._ventana.CasosBases != null)
                        this._ventana.CasosBases = null;
                    this._ventana.CasosBases = casosBase; 
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
        /// Metodo que quita un Caso Base de la lista de Casos Bases 
        /// </summary>
        public void QuitarCasoBase()
        {
            IList<CasoBase> casosBase;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.CasoBaseSeleccionado != null)
                {
                    if (null == this._ventana.CasosBases)
                    {
                        casosBase = new List<CasoBase>();
                        this._ventana.CasosBases = casosBase;
                    }
                    else
                    {
                        casosBase = new List<CasoBase>();
                        casosBase = (IList<CasoBase>)this._ventana.CasosBases;
                    }

                    CasoBase casoBaseQuitar = (CasoBase)this._ventana.CasoBaseSeleccionado;

                    if (!this._agregar)
                    {
                        bool eliminado = this._casoBaseServicios.Eliminar(casoBaseQuitar, UsuarioLogeado.Hash);
                    }

                    //Se remueve el CasoBase de la lista de la pantalla
                    casosBase.Remove(casoBaseQuitar);
                    this._ventana.CasosBases = null;
                    this._ventana.CasosBases = casosBase;

                    if (casosBase.Count == 0)
                    {
                        this._ventana.ocultarListaCasosBase();
                    }
                    
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
        /// Metodo que obtiene el elemento Caso Base, ya sea una Marca o una Patente
        /// </summary>
        public void ObtenerCasoBaseMarcaOPatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.TipoBase != null)
                {
                    String tipoCasoBase = ((ListaDatosValores)this._ventana.TipoBase).Valor;

                    if (tipoCasoBase.Equals("MAR"))
                    {
                        ConsultarMarca();
                    }
                    else if (tipoCasoBase.Equals("PAT"))
                    {
                        ConsultarPatente();
                    }
                    else
                        this._ventana.Mensaje("Debe colocar los datos del Caso Base manualmente", 2);
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
        /// Metodo que consulta un Caso Base de tipo MARCA
        /// </summary>
        private void ConsultarMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool bandera = false;
                Marca marca = new Marca();

                if (this._ventana.IdMarcaPatenteFiltrar != "")
                {
                    marca.Id = int.Parse(this._ventana.IdMarcaPatenteFiltrar);
                    bandera = true;
                }

                if (this._ventana.NombreMarcaPatenteFiltrar != "")
                {
                    marca.Descripcion = this._ventana.NombreMarcaPatenteFiltrar.ToUpper();
                    bandera = true;
                }

                if (bandera)
                {
                    IList<Marca> marcas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                    if (marcas.Count != 0)
                    {
                        this._ventana.ListaMarcasPatentes = marcas;
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
        /// Metodo que consulta un Caso Base del tipo PATENTE
        /// </summary>
        private void ConsultarPatente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool bandera = false;
                Patente patente = new Patente();

                if (this._ventana.IdMarcaPatenteFiltrar != "")
                {
                    patente.Id = int.Parse(this._ventana.IdMarcaPatenteFiltrar);
                    bandera = true;
                }

                if (this._ventana.NombreMarcaPatenteFiltrar != "")
                {
                    patente.Descripcion = this._ventana.NombreMarcaPatenteFiltrar.ToUpper();
                    bandera = true;
                }

                if (bandera)
                {
                    IList<Patente> patentes = this._patenteServicios.ObtenerPatentesFiltro(patente);
                    if (patentes.Count != 0)
                    {
                        this._ventana.ListaMarcasPatentes = patentes;
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
        /// Metodo que valida la opcion seleccionada en los tipos de base
        /// </summary>
        /// <returns>True si es una opcion de Marca o Patente; False, en caso contrario</returns>
        public bool ValidarCasoBaseMarcaPatente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((this._ventana.TipoBase != null) && (!((ListaDatosValores)this._ventana.TipoBase).Id.Equals("NGN")))
                {
                    if ((((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("MAR")) || 
                        (((ListaDatosValores)this._ventana.TipoBase).Valor.Equals("PAT")))
                    {
                        retorno = true;
                    }
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
        /// Metodo que captura los datos del Caso Base seleccionado y lo presenta en los campos para luego ser incluidos o no en la lista
        /// de Casos Bases del Caso de Pirateria
        /// </summary>
        public void CapturarDatosCasoBaseSeleccionado()
        {
            String tipoCasoBase = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.ListaMarcaOPatente != null)
                {
                    if ((this._ventana.TipoBase != null) && (!((ListaDatosValores)this._ventana.TipoBase).Id.Equals("NGN")))
                    {
                        tipoCasoBase = ((ListaDatosValores)this._ventana.TipoBase).Valor;

                        switch (tipoCasoBase)
                        {
                            case "MAR":
                                Marca marca = (Marca)this._ventana.ListaMarcaOPatente;
                                this._ventana.IdCasoBase = marca.Id.ToString();
                                this._ventana.ITipoCasoBase = marca.Tipo;
                                this._ventana.ClaseInternacionalCasoBase = marca.Internacional.Id.ToString();
                                this._ventana.ClaseNacionalCasoBase = marca.Nacional.Id.ToString();
                                this._marca = marca;
                                break;

                            case "PAT":
                                Patente patente = (Patente)this._ventana.ListaMarcaOPatente;
                                this._ventana.IdCasoBase = patente.Id.ToString();
                                this._ventana.ITipoCasoBase = patente.Tipo;
                                this._patente = patente;
                                break;
                        }

                        this._ventana.IdMarcaPatenteFiltrar = String.Empty;
                        this._ventana.NombreMarcaPatenteFiltrar = String.Empty;
                        this._ventana.ListaMarcasPatentes = null;
                    }
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



        public void VerInfoAdicionalTerceros()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Caso caso = (Caso)this._ventana.Caso;

                if (caso != null)
                {
                    this.Navegar(new GestionarInfoTerceros(caso, this._ventana));
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
        /// Metodo que despliega la ventana de Auditoria
        /// </summary>
        public void VerAuditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ListaAuditorias(_auditorias));

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
