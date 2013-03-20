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
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Memorias;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.ControlesByT.Ventanas;
using System.Text;
using System.IO;
using Trascend.Bolet.Cliente.Ventanas.Poderes;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Asociados;

using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorGestionarPatente : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarPatente _ventana;


        private IPatenteServicios _patenteServicios;
        private IAsociadoServicios _asociadoServicios;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private IInfoAdicionalServicios _infoAdicionalServicios;
        private IInfoBolPatenteServicios _infoBolServicios;
        private IOperacionServicios _operacionServicios;
        private IBusquedaServicios _busquedaServicios;
        private IPlanillaServicios _planillaServicios;
        private IInternacionalServicios _internacionalServicios;
        private IStatusWebServicios _statusWebServicios;
        private IBoletinServicios _boletinServicios;
        private IInventorServicios _inventorServicios;
        private IMemoriaServicios _memoriaServicios;


        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Inventor> _inventores;
        private IList<Auditoria> _auditorias;
        private IList<Operacion> _abandonos;
        private IList<Anualidad> _anualidades;
        private IList<Poder> _poderesInterseccion;


        private Patente _patente;
        private bool _cargarPoderInicial = false;

        private bool _agregar;


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarPatente(IGestionarPatente ventana, object patente, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                if (patente != null)
                {
                    this._ventana.Patente = patente;
                    this._agregar = false;
                }
                else
                {

                    this.CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;
                    this._agregar = true;

                    this._ventana.Patente = new Patente();
                    this._ventana.ActivarControlesAlAgregar();
                }



                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);
                this._infoBolServicios = (IInfoBolPatenteServicios)Activator.GetObject(typeof(IInfoBolPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolPatenteServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                this._planillaServicios = (IPlanillaServicios)Activator.GetObject(typeof(IPlanillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlanillaServicios"]);
                this._internacionalServicios = (IInternacionalServicios)Activator.GetObject(typeof(IInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InternacionalServicios"]);
                this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._inventorServicios = (IInventorServicios)Activator.GetObject(typeof(IInventorServicios),
                ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InventorServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._memoriaServicios = (IMemoriaServicios)Activator.GetObject(typeof(IMemoriaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MemoriaServicios"]);

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
        /// Método que actualiza el título de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._agregar == false)
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarPatente,
                    Recursos.Ids.GestionarPatentes);
            }
            else
            {
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarPatente,
                    Recursos.Ids.AgregarPatentes);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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



                #region Internacional

                IList<Pais> paisesInternacionales = this._paisServicios.ConsultarTodos();
                Pais primerPaisInt = new Pais();
                primerPaisInt.Id = int.MinValue;
                paisesInternacionales.Insert(0, primerPaisInt);
                this._ventana.PaisesInternacionales = paisesInternacionales;
                this._ventana.PaisesInternacionalesDatos = paisesInternacionales;

                #endregion

                if (this._agregar == false)
                {


                    if (!PoseePermisologia(UsuarioLogeado.Rol.Objetos, Recursos.Ids.SolicitudPatente))
                    {
                        this._ventana.OcultarTabSolicitud();
                    }

                    _patente = this._patenteServicios.ConsultarPatenteConTodo((Patente)this._ventana.Patente);
                    this._ventana.Patente = _patente;

                    if (_patente.LocalidadPatente != null)
                        this._ventana.MarcarRadioPatenteNacional(_patente.LocalidadPatente.Equals("N"));

                    DateTime fechaTerminoAux = null != ((Patente)this._ventana.Patente).FechaTermino ? (DateTime)((Patente)this._ventana.Patente).FechaTermino
                        : DateTime.MinValue;

                    if (fechaTerminoAux != DateTime.MinValue)
                        this._ventana.FechaTermino = fechaTerminoAux.ToShortDateString();
                    else
                        this._ventana.FechaTermino = "";

                    InfoAdicional infoAdicional = new InfoAdicional("P." + _patente.Id);

                    _patente.InfoAdicional = this._infoAdicionalServicios.ConsultarPorId(infoAdicional);
                    _patente.Operaciones = this._operacionServicios.ConsultarOperacionesPorPatente(_patente);

                    IList<ListaDatosDominio> tipoPatente = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoPatente));
                    ListaDatosDominio primerTipoPatente = new ListaDatosDominio();
                    primerTipoPatente.Id = "NGN";
                    tipoPatente.Insert(0, primerTipoPatente);
                    this._ventana.TiposPatenteSolicitud = tipoPatente;
                    this._ventana.TipoPatenteSolicitud = this.BuscarTipoPatente(_patente.Tipo, tipoPatente);

                    this._ventana.TiposPatenteDatos = tipoPatente;
                    this._ventana.TipoPatenteDatos = this.BuscarTipoPatente(_patente.Tipo, tipoPatente);

                    IList<ListaDatosDominio> presentacionPatente = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiPresentacionPatente));
                    ListaDatosDominio primeraPresentacionPatente = new ListaDatosDominio();
                    primeraPresentacionPatente.Id = "NGN";
                    presentacionPatente.Insert(0, primeraPresentacionPatente);
                    this._ventana.PresentacionesPatenteSolicitud = presentacionPatente;
                    this._ventana.PresentacionPatenteSolicitud = this.BuscarPresentacionPatente(_patente.Presentacion, presentacionPatente);

                    this._ventana.PresentacionesPatenteDatos = presentacionPatente;
                    this._ventana.PresentacionPatenteDatos = this.BuscarPresentacionPatente(_patente.Presentacion, presentacionPatente);

                    IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
                    Agente primerAgente = new Agente();
                    primerAgente.Id = "NGN";
                    agentes.Insert(0, primerAgente);
                    this._ventana.AgentesSolicitudFiltrar = agentes;
                    this._ventana.AgenteSolicitudFiltrar = this.BuscarAgente(agentes, (Agente)_patente.Agente);

                    //IList<Interesado> Interesados = this._interesadoServicios.ConsultarTodos();
                    IList<Interesado> Interesados = new List<Interesado>();
                    Interesado primerInteresado = new Interesado();
                    primerInteresado.Id = int.MinValue;
                    Interesados.Add(((Patente)this._ventana.Patente).Interesado);
                    Interesados.Insert(0, primerInteresado);
                    this._ventana.InteresadosSolicitud = Interesados;
                    this._ventana.InteresadoSolicitud = this.BuscarInteresado(Interesados, ((Patente)this._ventana.Patente).Interesado);

                    IList<Pais> paises = this._paisServicios.ConsultarTodos();
                    Pais primerPais = new Pais();
                    primerPais.Id = int.MinValue;
                    paises.Insert(0, primerPais);
                    this._ventana.PaisesSolicitud = paises;
                    this._ventana.PaisSolicitud = this.BuscarPais(paises, _patente.Pais);

                    this._ventana.PaisesDatos = paises;
                    this._ventana.PaisDatos = this.BuscarPais(paises, _patente.Pais);

                    IList<Poder> _poderPatente = new List<Poder>();
                    _poderPatente.Add(_patente.Poder);

                    this._ventana.PoderesDatosFiltrar = _poderPatente;
                    this._ventana.PoderesSolicitudFiltrar = _poderPatente;

                    this._ventana.PoderDatosFiltrar = _patente.Poder;
                    this._ventana.PoderSolicitudFiltrar = _patente.Poder;

                    this._ventana.NumPoderSolicitud = _patente.Poder != null ? _patente.Poder.NumPoder : "";

                    IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                    StatusWeb primerStatus = new StatusWeb();
                    primerStatus.Id = "NGN";
                    statusWebs.Insert(0, primerStatus);
                    this._ventana.StatusesWebDatos = statusWebs;
                    this._ventana.StatusWebDatos = this.BuscarStatusWeb(statusWebs, _patente.StatusWeb);

                    IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                    TipoEstado primerDetalle = new TipoEstado();
                    primerDetalle.Id = "NGN";
                    tipoEstados.Insert(0, primerDetalle);
                    this._ventana.DetallesDatos = tipoEstados;
                    this._ventana.DetalleDatos = BuscarDetalle((IList<TipoEstado>)this._ventana.DetallesDatos, _patente.TipoEstado);


                    IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                    Servicio primerServicio = new Servicio();
                    primerServicio.Id = "";
                    servicios.Insert(0, primerServicio);
                    this._ventana.SituacionesDatos = servicios;

                    if (_patente.Servicio != null)
                        this._ventana.SituacionDatos = this.BuscarServicio((IList<Servicio>)this._ventana.SituacionesDatos, _patente.Servicio);
                    else
                        this._ventana.SituacionDatos = this.BuscarServicio((IList<Servicio>)this._ventana.SituacionesDatos, primerServicio);


                    IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                    Boletin primerBoletin = new Boletin();
                    primerBoletin.Id = int.MinValue;
                    boletines.Insert(0, primerBoletin);
                    this._ventana.BoletinesOrdenPublicacionDatos = boletines;
                    this._ventana.BoletinesPublicacionDatos = boletines;
                    this._ventana.BoletinesConcesionDatos = boletines;
                    this._ventana.BoletinConcesionDatos = this.BuscarBoletin(boletines, _patente.BoletinConcesion);
                    this._ventana.BoletinPublicacionDatos = this.BuscarBoletin(boletines, _patente.BoletinPublicacion);
                    this._ventana.BoletinOrdenPublicacionDatos = this.BuscarBoletin(boletines, _patente.BoletinOrdenPublicacion);                    

                    if (File.Exists(ConfigurationManager.AppSettings["RutaImagenesDePatentes"] + _patente.Id + ".jpg"))
                    {
                        ((Patente)this._ventana.Patente).BDibujo = true;
                        this._ventana.PintarDisenoDatos();
                        this._ventana.PintarDisenoSolicitud();

                    }

                    this._abandonos = this._operacionServicios.ConsultarOperacionesPorPatente(_patente);
                    this._ventana.AbandonoDatos = this._abandonos.Count > 0 ? this._abandonos[0].Descripcion + " - "
                                                    + ((DateTime)this._abandonos[0].Fecha).ToShortDateString() : "";

                    this._anualidades = this._patenteServicios.ConsultarPatenteConTodo(_patente).Anualidades;

                    string anualidadAux = null;

                    if (this._anualidades.Count > 0)
                    {
                        anualidadAux = (this._anualidades[this._anualidades.Count - 1].QAnualidad + 1).ToString();

                        if (null != this._anualidades[this._anualidades.Count - 1].FechaAnualidad)
                            anualidadAux += " - " + ((DateTime)this._anualidades[this._anualidades.Count - 1].FechaAnualidad).AddYears(1).ToShortDateString();
                    }

                    //this._ventana.AnualidadDatos = this._anualidades[this._anualidades.Count - 1].QAnualidad + 1 + " - "
                    //                                + ((DateTime)this._anualidades[this._anualidades.Count - 1].FechaAnualidad).AddYears(1).ToShortDateString();

                    this._ventana.AnualidadDatos = anualidadAux;


                    _inventores = this._inventorServicios.ConsultarInventoresPorPatente(_patente);
                    _patente.InfoBoles = this._infoBolServicios.ConsultarInfoBolesPorPatente(_patente);

                    Auditoria auditoria = new Auditoria();
                    auditoria.Fk = ((Patente)this._ventana.Patente).Id;
                    auditoria.Tabla = "MYP_PATENTES";
                    this._auditorias = this._patenteServicios.AuditoriaPorFkyTabla(auditoria);

                    ((Patente)this._ventana.Patente).Memorias = this._memoriaServicios.ConsultarMemoriasPorPatente(((Patente)this._ventana.Patente));

                    _patente.Fechas = this._patenteServicios.ConsultarFechasPorPatente(((Patente)this._ventana.Patente));

                    CalcularDuracion();

                    if (null != _patente.Fechas && _patente.Fechas.Count != 0)
                        this._ventana.PintarFechasDatos();

                    if (null != _patente.Memorias && _patente.Memorias.Count != 0)
                        this._ventana.PintarMemoriaDatos();

                    if (null != _patente.InfoAdicional && !string.IsNullOrEmpty(_patente.InfoAdicional.Id))
                        this._ventana.PintarInfoAdicionalSolicitud();

                    if (null != _patente.InfoBoles && _patente.InfoBoles.Count > 0)
                        this._ventana.PintarInfoBolDatos();

                    if ((null != this._inventores) && (this._inventores.Count > 0))
                    {
                        this._ventana.PintarInventoresDatos();
                        this._ventana.PintarInventoresSolicitud();
                    }

                    if ((null != this._auditorias) && (this._auditorias.Count > 0))
                        this._ventana.PintarAuditoriaDatos();

                    if (_patente.Operaciones.Count > 0)
                        this._ventana.PintarOperacionesDatos();


                    this._ventana.NombreAsociadoDatos = _patente.Asociado != null ? _patente.Asociado.Nombre : "";
                    this._ventana.NombreAsociadoSolicitud = _patente.Asociado != null ? _patente.Asociado.Nombre : "";


                    if (_patente.Asociado != null)
                    {
                        IList<Asociado> asociados = new List<Asociado>();
                        asociados.Add(_patente.Asociado);
                        this._ventana.AsociadosDatos = asociados;
                        this._ventana.AsociadosSolicitud = asociados;
                        this._ventana.AsociadoDatos = _patente.Asociado != null ? _patente.Asociado : null;
                        this._ventana.AsociadoSolicitud = _patente.Asociado != null ? _patente.Asociado : null;
                    }
                    this._ventana.AsociadoDatos = _patente.Asociado;
                    if (null != _patente.PaisInternacional)
                    {
                        Pais paisABuscar = new Pais(_patente.PaisInternacional.Id);
                        this._ventana.PaisInternacional = this.BuscarPais(paisesInternacionales, paisABuscar);
                        this._ventana.PaisInternacionalDatos = this.BuscarPais(paisesInternacionales, paisABuscar);
                    }

                    if (null != _patente.AsociadoInternacional)
                    {
                        IList<Asociado> asociados = new List<Asociado>();
                        asociados.Add(_patente.AsociadoInternacional);

                        this._ventana.AsociadosInternacionales = asociados;
                        this._ventana.AsociadoInternacional = _patente.AsociadoInternacional;

                        this._ventana.AsociadosInternacionalesDatos = asociados;
                        this._ventana.AsociadoInternacionalDatos = _patente.AsociadoInternacional;

                        this._ventana.TextoAsociadoInternacional = _patente.AsociadoInternacional.Nombre;
                    }
                    else
                        CargarAsociadoInternacionalVacio();


                    if (null != _patente.Asociado)
                        this._ventana.PintarAsociado(_patente.Asociado.TipoCliente.Id);


                    if ((null != _patente.Interesado) && (null != _patente.Agente))
                        _cargarPoderInicial = true;

                }
                else
                {

                    //this._ventana.CambiarLabelsPorBotones();
                    this._ventana.SeleccionarTabSolicitud();
                    this.CargarTipos();
                    this.CargarPresentaciones();
                    this.CargarPaises();
                    this.CargarSituaciones();
                    this.CargarDetalles();
                    this.CargarStatusWeb();
                    this.CargarBoletines();
                    CargarAsociadoInternacionalVacio();
                }

                this._ventana.BorrarCeros();
                this._ventana.FocoPredeterminado();

                CalcularSaldos();

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


        private void CargarBoletines()
        {
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            Boletin primerBoletin = new Boletin();
            primerBoletin.Id = int.MinValue;
            boletines.Insert(0, primerBoletin);
            this._ventana.BoletinesOrdenPublicacionDatos = boletines;
            this._ventana.BoletinesPublicacionDatos = boletines;
            this._ventana.BoletinesConcesionDatos = boletines;
        }


        private void CargarStatusWeb()
        {
            IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
            StatusWeb primerStatus = new StatusWeb();
            primerStatus.Id = "NGN";
            statusWebs.Insert(0, primerStatus);
            this._ventana.StatusesWebDatos = statusWebs;
        }


        private void CargarDetalles()
        {
            IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
            TipoEstado primerDetalle = new TipoEstado();
            primerDetalle.Id = "NGN";
            tipoEstados.Insert(0, primerDetalle);
            this._ventana.DetallesDatos = tipoEstados;
        }


        private void CargarSituaciones()
        {
            IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
            Servicio primerServicio = new Servicio();
            this._ventana.SituacionesDatos = servicios;

            Servicio servicioAux = new Servicio("PS");
            this._ventana.SituacionDatos = this.BuscarServicio((IList<Servicio>)this._ventana.SituacionesDatos, servicioAux);
        }


        private void CargarPresentaciones()
        {
            IList<ListaDatosDominio> presentacionPatente = this._listaDatosDominioServicios.
               ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiPresentacionPatente));
            ListaDatosDominio primeraPresentacionPatente = new ListaDatosDominio();
            primeraPresentacionPatente.Id = "NGN";
            presentacionPatente.Insert(0, primeraPresentacionPatente);
            this._ventana.PresentacionesPatenteSolicitud = presentacionPatente;
            this._ventana.PresentacionesPatenteDatos = presentacionPatente;

        }


        private void CargarTipos()
        {
            IList<ListaDatosDominio> tipoPatente = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoPatente));
            ListaDatosDominio primerTipoPatente = new ListaDatosDominio();
            primerTipoPatente.Id = "NGN";
            tipoPatente.Insert(0, primerTipoPatente);
            this._ventana.TiposPatenteSolicitud = tipoPatente;
            this._ventana.TiposPatenteDatos = tipoPatente;
        }


        /// <summary>
        /// Método que carga la ventana de consulta marcas
        /// </summary>
        public void IrConsultarPatentes()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //this.Navegar(new ConsultarPatentes());
            this.RegresarVentanaPadre();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que guardar los datos de la ventana y los almacena en las variables
        /// </summary>
        /// <returns></returns>
        public Patente CargarPatenteDeLaPantalla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Patente patente = (Patente)this._ventana.Patente;

            if (_agregar) { patente.Operacion = "CREATE"; }
            else { patente.Operacion = "MODIFY"; }

            if (null != this._ventana.AgenteSolicitudFiltrar)
                patente.Agente = !((Agente)this._ventana.AgenteSolicitudFiltrar).Id.Equals("NGN") ? (Agente)this._ventana.AgenteSolicitudFiltrar : null;

            if (null != this._ventana.AsociadoSolicitud)
                patente.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            if (null != this._ventana.InteresadoSolicitud)
                patente.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            if (null != this._ventana.PoderSolicitudFiltrar)
                patente.Poder = ((Poder)this._ventana.PoderSolicitudFiltrar).Id != int.MinValue ? ((Poder)this._ventana.PoderSolicitudFiltrar) : null;

            if (null != this._ventana.PaisSolicitud)
                patente.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

            if (null != this._ventana.BoletinConcesionDatos)
                patente.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesionDatos).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesionDatos : null;

            if (null != this._ventana.BoletinPublicacionDatos)
                patente.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacionDatos).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacionDatos : null;

            if (null != this._ventana.BoletinOrdenPublicacionDatos)
                patente.BoletinOrdenPublicacion = ((Boletin)this._ventana.BoletinOrdenPublicacionDatos).Id != int.MinValue ? (Boletin)this._ventana.BoletinOrdenPublicacionDatos : null;

            if (null != this._ventana.SituacionDatos)
                patente.Servicio = !((Servicio)this._ventana.SituacionDatos).Id.Equals("NGN") ? ((Servicio)this._ventana.SituacionDatos) : null;

            if (null != this._ventana.StatusWebDatos)
                patente.StatusWeb = !((StatusWeb)this._ventana.StatusWebDatos).Id.Equals("NGN") ? ((StatusWeb)this._ventana.StatusWebDatos) : null;

            if (null != this._ventana.DetalleDatos)
                patente.TipoEstado = !((TipoEstado)this._ventana.DetalleDatos).Id.Equals("NGN") ? ((TipoEstado)this._ventana.DetalleDatos) : null;

            if (null != this._ventana.PresentacionPatenteDatos)
                patente.Presentacion = !((ListaDatosDominio)this._ventana.PresentacionPatenteDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.PresentacionPatenteDatos).Id[0] : (char?)null;

            if (null != this._ventana.TipoPatenteDatos)
                patente.Tipo = !((ListaDatosDominio)this._ventana.TipoPatenteDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoPatenteDatos).Id : null;

            return patente;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de cambiar el boton y habilitar los campos de la ventana para
        /// su modificación
        /// </summary>
        public void CambiarAModificar()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.HabilitarCampos = true;
            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos de la Patente
        /// </summary>
        public void Modificar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                int? exitosoEntero = null;
                bool exitoso = false;

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Patente
                else
                {
                    string mensaje = "";
                    string titulo = "";

                    if (((Patente)this._ventana.Patente).Id == 0)
                    {
                        titulo = "Agregar Patente";
                        mensaje = Recursos.MensajesConElUsuario.InsertarPatente;
                    }
                    else
                    {
                        titulo = "Modificar Patente";
                        mensaje = string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarPatente, ((Patente)this._ventana.Patente).Id);
                    }
                    if (this._ventana.ConfirmarAccion(titulo, mensaje))
                        if (ValidarPoder())
                        {

                            Patente patente = CargarPatenteDeLaPantalla();


                            if (!this._ventana.EsPatenteNacional)
                                patente = AgregarDatosInternacionales(patente);
                            else
                                patente.LocalidadPatente = "N";
                            if (ValidarPatenteInternacional())
                            {
                                if (_agregar)
                                {
                                    exitosoEntero = this._patenteServicios.InsertarOModificarPatente(patente, UsuarioLogeado.Hash);
                                    patente.Id = (int)exitosoEntero;
                                }
                                else
                                {
                                    exitoso = this._patenteServicios.InsertarOModificar(patente, UsuarioLogeado.Hash);
                                }

                                if ((!exitosoEntero.Equals(null)) || (exitoso))
                                {
                                    if (_agregar)
                                        this.Navegar(new GestionarPatente(patente, null));
                                    else
                                    {
                                        this._ventana.HabilitarCampos = false;
                                        this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;

                                    }
                                }
                            }
                            else
                            {
                                this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorPatenteInternacional, 0);
                            }
                        }
                        else
                            this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderesConAgente, 0);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de validar la marca internacional antes de modificarla
        /// </summary>
        /// <returns></returns>
        private bool ValidarPatenteInternacional()
        {
            bool retorno = true;

            if (!this._ventana.EsPatenteNacional)
                if (((Pais)this._ventana.PaisInternacional).Id == int.MinValue)
                    retorno = false;

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de agregarle los datos pertenecientes a las marcas internacionales
        /// </summary>
        /// <param name="patente"></param>
        /// <returns></returns>
        private Patente AgregarDatosInternacionales(Patente patente)
        {
            try
            {
                patente.LocalidadPatente = "I";
                patente.PaisInternacional = (Pais)this._ventana.PaisInternacional;
                patente.AsociadoInternacional = (Asociado)this._ventana.AsociadoInternacional;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }

            return patente;
        }


        /// <summary>
        /// Metodo que se encarga de eliminar una Patente
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if (this._patenteServicios.Eliminar((Patente)this._ventana.Patente, UsuarioLogeado.Hash))
                //{
                //    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PaisEliminado;
                //    this.Navegar(_paginaPrincipal);
                //}

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de información adicional
        /// </summary>
        /// <param name="tab"></param>
        public void IrInfoAdicional(string tab)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new GestionarInfoAdicional(CargarPatenteDeLaPantalla(), tab));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de las operaciones de la Patente
        /// </summary>
        public void IrOperaciones()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaOperaciones(CargarPatenteDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de InfoBoles
        /// </summary>
        public void IrInfoBoles()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaInfoBoles(CargarPatenteDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se ecarga la descripcion de la situacion
        /// </summary>
        public void DescripcionSituacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.SituacionDescripcion = ((Servicio)this._ventana.SituacionDatos).Descripcion;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana con la lista de Auditorías
        /// </summary>
        public void Auditoria()
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
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de las fechas de la Patente
        /// </summary>
        public void VerFechas()
        {

            this.Navegar(new ListaFechas(CargarPatenteDeLaPantalla()));
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de los inventores de la Patente
        /// </summary>
        public void Inventores()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaInventores(CargarPatenteDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se encarga de llamar a las memoria en el servidor
        /// </summary>
        public void VerMemoriaRuta()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string rutaArchivo = ConfigurationManager.AppSettings["RutaMemoriaPatente"];

                string nombreArchivo = ConfigurationManager.AppSettings["NombreMemoriaPatente"] + ((Patente)this._ventana.Patente).Id;

                string[] archivos = Directory.GetFiles(rutaArchivo, nombreArchivo + ".*");

                if (archivos.Length > 0)
                {
                    this.AbrirArchivoPorConsola(archivos[0], "Abriendo Archivo de Memoria de la Patente: " + ((Patente)this._ventana.Patente).Id);
                }
                else { this._ventana.ArchivoNoEncontrado(); }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(String.Format(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado, "Memoria"), true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de las memorias de la Patente
        /// </summary>
        public void VerMemoria()
        {
            //this.Navegar(new ConsultarMemoria(this._memoriaServicios.ConsultarMemoriasPorPatente((Patente)this._ventana.Patente)));
            this.Navegar(new ListaMemorias(this._ventana.Patente));
        }


        /// <summary>
        /// Método que se encarga de llamar a las memoria en el servidor
        /// </summary>
        public void VerExpediente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                string rutaArchivo = ConfigurationManager.AppSettings["RutaExpedientePatente"];

                string nombreArchivo = ConfigurationManager.AppSettings["NombreExpedientePatente"] + ((Patente)this._ventana.Patente).Id;

                string[] archivos = Directory.GetFiles(rutaArchivo, nombreArchivo + ".*");

                if (archivos.Length > 0)
                {
                    this.AbrirArchivoPorConsola(archivos[0], "Abriendo Archivo de Expediente de la Patente: " + ((Patente)this._ventana.Patente).Id);
                }
                else { this._ventana.ArchivoNoEncontrado(); }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(String.Format(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado, "Memoria"), true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        /// <summary>
        /// Método que se encarga de llamar a los titulos en el servidor
        /// </summary>
        public void VerTitulo()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                string rutaArchivo = ConfigurationManager.AppSettings["RutaTituloPatente"];

                string nombreArchivo = ConfigurationManager.AppSettings["NombreTituloPatente"] + ((Patente)this._ventana.Patente).Id;

                string[] archivos = Directory.GetFiles(rutaArchivo, nombreArchivo + ".*");

                if (archivos.Length > 0)
                {
                    this.AbrirArchivoPorConsola(archivos[0], "Abriendo Archivo de Memoria de la Patente: " + ((Patente)this._ventana.Patente).Id);
                }
                else { this._ventana.ArchivoNoEncontrado(); }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(String.Format(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado, "Memoria"), true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }

        }


        /// <summary>
        /// Método que se encarga de llamar a las solicitudes en el servidor
        /// </summary>
        public void VerSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                string rutaArchivo = ConfigurationManager.AppSettings["RutaSolicitudPatente"];

                string nombreArchivo = ConfigurationManager.AppSettings["NombreSolicitudPatente"] + ((Patente)this._ventana.Patente).Id;

                string[] archivos = Directory.GetFiles(rutaArchivo, nombreArchivo + ".*");

                if (archivos.Length > 0)
                {
                    this.AbrirArchivoPorConsola(archivos[0], "Abriendo Archivo de Memoria de la Patente: " + ((Patente)this._ventana.Patente).Id);
                }
                else { this._ventana.ArchivoNoEncontrado(); }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(ex.Message, true);
            }
            catch (RemotingException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
            }
            catch (SocketException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(String.Format(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado, "Memoria"), true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void CargarPaises()
        {
            IList<Pais> paises = this._paisServicios.ConsultarTodos();
            Pais primerPais = new Pais();
            primerPais.Id = int.MinValue;
            paises.Insert(0, primerPais);
            this._ventana.PaisesSolicitud = paises;
            this._ventana.PaisesDatos = paises;
        }


        /// <summary>
        /// Método que se encarga de mostrar la ventana de la lista de búsquedas de la patente
        /// </summary>
        /// <param name="tab"></param>
        public void IrBusquedas(string tab)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //this.Navegar(new ListaBusquedas(CargarMarcaDeLaPantalla(), tab));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column, ListView ListaResultados)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        #region Metodos de los filtros de asociados


        /// <summary>
        /// Método que cambia asociado solicitud
        /// </summary>
        public void CambiarAsociadoSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Asociado)this._ventana.AsociadoSolicitud != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSolicitud);

                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    this._ventana.IdAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Id.ToString();

                    this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
                    this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    this._ventana.IdAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Id.ToString();

                    if (asociado != null)
                        this._ventana.PintarAsociado(asociado.TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();
                    CalcularSaldos();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                //this._ventana.NombreAsociadoDatos = "";
            }
        }


        /// <summary>
        /// Método que cambia asociado datos
        /// </summary>
        public void CambiarAsociadoDatos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Asociado)this._ventana.AsociadoDatos != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoDatos);
                    this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoDatos).Nombre;
                    this._ventana.IdAsociadoDatos = ((Asociado)this._ventana.AsociadoDatos).Id.ToString();
                    this._ventana.AsociadoSolicitud = (Asociado)this._ventana.AsociadoDatos;
                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoDatos).Nombre;
                    this._ventana.IdAsociadoSolicitud = ((Asociado)this._ventana.AsociadoDatos).Id.ToString();

                    if (asociado != null)
                        this._ventana.PintarAsociado(asociado.TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                //this._ventana.NombreAsociadoDatos = "";
            }
        }


        /// <summary>
        /// Método que filtra un asociado
        /// </summary>
        /// <param name="filtrarEn"></param>
        public void BuscarAsociado(int filtrarEn)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //IEnumerable<Asociado> asociadosFiltrados = this._asociados;
            Asociado asociadoABuscar = new Asociado();

            if (filtrarEn == 0)
            {

                asociadoABuscar.Id = this._ventana.IdAsociadoSolicitudFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAsociadoSolicitudFiltrar);
                asociadoABuscar.Nombre = this._ventana.NombreAsociadoSolicitudFiltrar.Equals("") ? "" : this._ventana.NombreAsociadoSolicitudFiltrar.ToUpper();

                IList<Asociado> asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);

                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociadosFiltrados.Insert(0, primerAsociado);

                if (asociadosFiltrados.Count == 0)
                {
                    this._ventana.AsociadosDatos = _asociados;
                    this._ventana.AsociadosSolicitud = _asociados;

                }
                else
                {
                    this._ventana.AsociadosDatos = asociadosFiltrados;
                    this._ventana.AsociadosSolicitud = asociadosFiltrados;
                }
                //if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
                //{
                //    asociadosFiltrados = from p in asociadosFiltrados
                //                         where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
                //                         select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitudFiltrar))
                //{
                //    asociadosFiltrados = from p in asociadosFiltrados
                //                         where p.Nombre != null &&
                //                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitudFiltrar.ToLower())
                //                         select p;
                //}
            }
            else
            {


                asociadoABuscar.Id = this._ventana.IdAsociadoDatosFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAsociadoDatosFiltrar);
                asociadoABuscar.Nombre = this._ventana.NombreAsociadoDatosFiltrar.Equals("") ? "" : this._ventana.NombreAsociadoDatosFiltrar.ToUpper();

                IList<Asociado> asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);

                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociadosFiltrados.Insert(0, primerAsociado);
                if (asociadosFiltrados.Count == 0)
                {
                    this._ventana.AsociadosDatos = _asociados;
                    this._ventana.AsociadosSolicitud = _asociados;

                }
                else
                {
                    this._ventana.AsociadosDatos = asociadosFiltrados;
                    this._ventana.AsociadosSolicitud = asociadosFiltrados;
                }
                //if (!string.IsNullOrEmpty(this._ventana.IdAsociadoDatosFiltrar))
                //{
                //    asociadosFiltrados = from p in asociadosFiltrados
                //                         where p.Id == int.Parse(this._ventana.IdAsociadoDatosFiltrar)
                //                         select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoDatosFiltrar))
                //{
                //    asociadosFiltrados = from p in asociadosFiltrados
                //                         where p.Nombre != null &&
                //                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoDatosFiltrar.ToLower())
                //                         select p;
                //}
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                //if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                //    this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
                //else
                //    this._ventana.AsociadosSolicitud = this._asociados;
            }
            else
            {
                //if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                //    this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
                //else
                //    this._ventana.AsociadosDatos = this._asociados;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que carga los asociados
        /// </summary>
        public void CargarAsociados()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Patente patente = null != this._ventana.Patente ? (Patente)this._ventana.Patente : new Patente();
            //IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            IList<Asociado> asociados = new List<Asociado>();
            Asociado primerAsociado = new Asociado();
            primerAsociado.Id = int.MinValue;
            asociados.Insert(0, primerAsociado);

            //this._ventana.AsociadosSolicitud = asociados;
            //this._ventana.AsociadosDatos = asociados;
            //this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, patente.Asociado);
            //this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, patente.Asociado);
            this._ventana.NombreAsociadoDatos = null != ((Patente)this._ventana.Patente).Asociado ? ((Patente)this._ventana.Patente).Asociado.Nombre : "";
            this._ventana.NombreAsociadoSolicitud = null != ((Patente)this._ventana.Patente).Asociado ? ((Patente)this._ventana.Patente).Asociado.Nombre : "";
            this._asociados = asociados;
            if (null != ((Patente)this._ventana.Patente).Asociado)
            {
                asociados.Add(((Patente)this._ventana.Patente).Asociado);
            }
            this._ventana.AsociadosDatos = asociados;
            this._ventana.AsociadosSolicitud = asociados;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        #endregion


        #region Metodos de los filtros de interesados


        /// <summary>
        /// Método que cambia interesado solicitud
        /// </summary>
        public void CambiarInteresadoSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Interesado)this._ventana.InteresadoSolicitud != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    this._ventana.IdInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Id.ToString();
                    this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    this._ventana.IdInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Id.ToString();

                    if (interesadoAux != null)
                    {
                        this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                        this._ventana.InteresadoEstadoSolicitud = interesadoAux.Estado != null ? interesadoAux.Estado : "";
                        this._ventana.InteresadoPaisDatos = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                        this._ventana.InteresadoEstadoDatos = interesadoAux.Estado != null ? interesadoAux.Estado : "";

                        IList<Poder> poderes = this._poderServicios.ConsultarPoderesPorInteresado(interesadoAux);

                        this._ventana.PoderesSolicitudFiltrar = poderes;
                        this._ventana.PoderesDatosFiltrar = poderes;
                    }

                    this._ventana.ConvertirEnteroMinimoABlanco();
                    //CargarPoderesEntreInteresadoAgente();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                //this._ventana.NombreInteresadoDatos = "";
                this._ventana.InteresadoPaisSolicitud = "";
                this._ventana.InteresadoEstadoSolicitud = "";
            }
        }


        /// <summary>
        /// Método que cambia interesado datos
        /// </summary>
        public void CambiarInteresadoDatos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.InteresadoSolicitud != null) || ((Interesado)this._ventana.InteresadoDatos != null))
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);

                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.IdInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();
                    this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoDatos;
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.IdInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();

                    if (interesadoAux != null)
                    {
                        this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                        this._ventana.InteresadoEstadoSolicitud = interesadoAux.Estado != null ? interesadoAux.Estado : "";
                        this._ventana.InteresadoPaisDatos = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                        this._ventana.InteresadoEstadoDatos = interesadoAux.Estado != null ? interesadoAux.Estado : "";
                    }

                    this._ventana.ConvertirEnteroMinimoABlanco();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                //this._ventana.NombreInteresadoDatos = "";
                this._ventana.InteresadoPaisSolicitud = "";
                this._ventana.InteresadoEstadoSolicitud = "";
            }
        }


        /// <summary>
        /// Método que filtra un interesado
        /// </summary>
        /// <param name="filtrarEn"></param>
        public void BuscarInteresado(int filtrarEn)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (filtrarEn == 0)
            {
                Interesado interesadoABuscar = new Interesado();
                interesadoABuscar.Id = this._ventana.IdInteresadoSolicitudFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoSolicitudFiltrar);
                interesadoABuscar.Nombre = this._ventana.NombreInteresadoSolicitudFiltrar.Equals("") ? "" : this._ventana.NombreInteresadoSolicitudFiltrar.ToUpper();

                IList<Interesado> interesadosFiltrados = new List<Interesado>();
                if ((interesadoABuscar.Id != 0) || (!interesadoABuscar.Nombre.Equals("")))
                {
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                }
                else
                {
                    interesadosFiltrados = _interesados;
                }
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesadosFiltrados.Insert(0, primerInteresado);

                if (interesadosFiltrados.Count == 0)
                {
                    this._ventana.InteresadosSolicitud = _interesados;
                    this._ventana.InteresadosDatos = _interesados;

                }
                else
                {
                    this._ventana.InteresadosSolicitud = interesadosFiltrados;
                    this._ventana.InteresadosDatos = interesadosFiltrados;
                }

                //if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
                //{
                //    interesadosFiltrados = from p in interesadosFiltrados
                //                           where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
                //                           select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitudFiltrar))
                //{
                //    interesadosFiltrados = from p in interesadosFiltrados
                //                           where p.Nombre != null &&
                //                           p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitudFiltrar.ToLower())
                //                           select p;
                //}
            }
            else
            {
                Interesado interesadoABuscar = new Interesado();
                interesadoABuscar.Id = this._ventana.IdInteresadoDatosFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoDatosFiltrar);
                interesadoABuscar.Nombre = this._ventana.NombreInteresadoDatosFiltrar.Equals("") ? "" : this._ventana.NombreInteresadoDatosFiltrar.ToUpper();

                IList<Interesado> interesadosFiltrados = new List<Interesado>();
                if ((interesadoABuscar.Id != 0) || (!interesadoABuscar.Nombre.Equals("")))
                {
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                }
                else
                {
                    interesadosFiltrados = _interesados;
                }

                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesadosFiltrados.Insert(0, primerInteresado);

                if (interesadosFiltrados.Count == 0)
                {
                    this._ventana.InteresadosSolicitud = _interesados;
                    this._ventana.InteresadosDatos = _interesados;

                }
                else
                {
                    this._ventana.InteresadosSolicitud = interesadosFiltrados;
                    this._ventana.InteresadosDatos = interesadosFiltrados;
                }
                //if (!string.IsNullOrEmpty(this._ventana.IdInteresadoDatosFiltrar))
                //{
                //    interesadosFiltrados = from p in interesadosFiltrados
                //                           where p.Id == int.Parse(this._ventana.IdInteresadoDatosFiltrar)
                //                           select p;
                //}

                //if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoDatosFiltrar))
                //{
                //    interesadosFiltrados = from p in interesadosFiltrados
                //                           where p.Nombre != null &&
                //                           p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoDatosFiltrar.ToLower())
                //                           select p;
                //}
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                //        this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                //    else
                //        this._ventana.InteresadosSolicitud = this._interesados;
            }
            else
            {
                //if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                //    this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
                //else
                //    this._ventana.InteresadosDatos = this._interesados;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que carga los interesados
        /// </summary>
        public void CargarInteresados()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Patente patente = null != this._ventana.Patente ? (Patente)this._ventana.Patente : new Patente();
            //IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            IList<Interesado> interesados = new List<Interesado>();
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);
            this._interesados = interesados;

            if (this._agregar == false)
            {
                ((Patente)this._ventana.Patente).Interesado = this._interesadoServicios.ConsultarInteresadoConTodo(patente.Interesado);
                if (null != ((Patente)this._ventana.Patente).Interesado)
                {
                    Interesado interesado = this.BuscarInteresado(interesados, patente.Interesado);
                    this._interesados.Add(((Patente)this._ventana.Patente).Interesado);
                    this._ventana.InteresadoSolicitud = ((Patente)this._ventana.Patente).Interesado;
                    this._ventana.InteresadoDatos = ((Patente)this._ventana.Patente).Interesado;
                    this._ventana.InteresadoPaisSolicitud = ((Patente)this._ventana.Patente).Interesado.Pais.NombreEspanol;
                    this._ventana.InteresadoEstadoSolicitud = ((Patente)this._ventana.Patente).Interesado.Ciudad;

                }
                this._ventana.NombreInteresadoDatos = null != ((Patente)this._ventana.Patente).Interesado ? ((Patente)this._ventana.Patente).Interesado.Nombre : "";
                this._ventana.NombreInteresadoSolicitud = null != ((Patente)this._ventana.Patente).Interesado ? ((Patente)this._ventana.Patente).Interesado.Nombre : "";
            }

            this._ventana.InteresadosDatos = _interesados;
            this._ventana.InteresadosSolicitud = _interesados;
            //this._ventana.InteresadosEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        #endregion


        #region Metodos de la lista de poderes


        /// <summary>
        /// Método que cambia poder solicitud
        /// </summary>
        public void CambiarPoderSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderSolicitudFiltrar != null) || ((Poder)this._ventana.PoderDatosFiltrar != null))
                {
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderSolicitudFiltrar).NumPoder;
                    this._ventana.PoderSolicitud = ((Poder)this._ventana.PoderSolicitudFiltrar).Id.ToString();
                    this._ventana.PoderDatos = ((Poder)this._ventana.PoderSolicitudFiltrar).Id.ToString();

                    IList<Agente> agentes = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderSolicitudFiltrar);
                    this._ventana.AgentesSolicitudFiltrar = null;
                    this._ventana.AgentesSolicitudFiltrar = agentes;
                    //this._ventana.AgenteDatosFiltrar = agentes;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
            }
        }


        /// <summary>
        /// Método que cambia Agente solicitud
        /// </summary>
        public void CambiarAgenteSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Agente)this._ventana.AgenteSolicitudFiltrar != null)
                {
                    this._ventana.IdAgenteSolicitud = ((Agente)this._ventana.AgenteSolicitudFiltrar).Id;
                    this._ventana.AgenteSolicitud = ((Agente)this._ventana.AgenteSolicitudFiltrar).Nombre;
                    //this._ventana.IdAgenteDatos = ((Agente)this._ventana.AgenteDatosFiltrar).Id;
                    //this._ventana.AgenteDatos = ((Agente)this._ventana.AgenteDatosFiltrar).Nombre;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
                //this._ventana.NumPoderDatos = "";
            }
        }


        /// <summary>
        /// Método que cambia Agente Datos
        /// </summary>
        public void CambiarAgenteDatos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Agente)this._ventana.AgenteSolicitudFiltrar != null)
                {
                    this._ventana.IdAgenteSolicitud = ((Agente)this._ventana.AgenteSolicitudFiltrar).Id;
                    this._ventana.AgenteSolicitud = ((Agente)this._ventana.AgenteSolicitudFiltrar).Nombre;
                    //this._ventana.IdAgenteDatos = ((Agente)this._ventana.AgenteDatosFiltrar).Id;
                    //this._ventana.AgenteDatos = ((Agente)this._ventana.AgenteDatosFiltrar).Nombre;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
                //this._ventana.NumPoderDatos = "";
            }
        }


        /// <summary>
        /// Método que cambia poder datos
        /// </summary>
        public void CambiarPoderDatos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderSolicitudFiltrar != null) || ((Poder)this._ventana.PoderDatosFiltrar != null))
                {
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderDatosFiltrar).NumPoder;
                    this._ventana.PoderSolicitud = ((Poder)this._ventana.PoderDatosFiltrar).Id.ToString();
                    this._ventana.PoderDatos = ((Poder)this._ventana.PoderDatosFiltrar).Id.ToString();
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
                //this._ventana.NumPoderDatos = "";
            }
        }


        /// <summary>
        /// Método que carga los agentes
        /// </summary>
        public void CargarAgentes()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;


            Patente patente = null != this._ventana.Patente ? (Patente)this._ventana.Patente : new Patente();
            IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
            Agente primerAgente = new Agente();
            primerAgente.Id = string.Empty;
            agentes.Insert(0, primerAgente);
            this._ventana.AgentesSolicitudFiltrar = agentes;
            this._ventana.AgenteSolicitudFiltrar = this.BuscarAgente(agentes, patente.Agente);

            //this._ventana.AgentesDatos = agentes;
            //this._ventana.AgenteDatos = this.BuscarAgente(agentes, patente.Agente);

            this._ventana.PoderesEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que carga los poderes
        /// </summary>
        public void CargarPoderes()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;




            //CargarPoderesEntreInteresadoAgente();

            //Patente patente = null != this._ventana.Patente ? (Patente)this._ventana.Patente : new Patente();
            ////IList<Poder> poderes = this._poderServicios.ConsultarTodos();
            ////Poder poder = new Poder();
            ////poder.Id = int.MinValue;
            ////poderes.Insert(0, poder);
            ////this._ventana.PoderesDatosFiltrar = poderes;
            ////this._ventana.PoderesSolicitudFiltrar = poderes;

            //if (this._agregar == false)
            //{
            //    Poder poder = _patente.Poder;

            //    this._ventana.PoderSolicitud = poder != null ? poder.Id.ToString() : "";
            //    //this._ventana.PoderSolicitud = poder.Id.ToString();
            //    this._ventana.NumPoderSolicitud = poder != null ? poder.NumPoder : "";

            //    this._ventana.PoderDatos = poder != null ? poder.Id.ToString() : "";

            //    //this._poderesInterseccion.Insert(0, poder);
            //    this._ventana.PoderesSolicitudFiltrar = _poderesInterseccion;
            //    this._ventana.PoderesDatosFiltrar = _poderesInterseccion;

            //    this._ventana.PoderSolicitudFiltrar = poder;
            //    this._ventana.PoderDatosFiltrar = poder;


            //}
            //else
            //{
            //    this._ventana.PoderesSolicitudFiltrar = _poderesInterseccion;
            //    this._ventana.PoderesDatosFiltrar = _poderesInterseccion;
            //}




            //this._ventana.PoderesEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        private void CargarPoderesEntreInteresadoAgente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            _poderesInterseccion = new List<Poder>();

            if ((this._ventana.InteresadoSolicitud != null) && (this._ventana.AgenteSolicitudFiltrar != null))
            {
                _poderesInterseccion = this._poderServicios
                    .ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.AgenteSolicitudFiltrar, (Interesado)this._ventana.InteresadoSolicitud);

                if (_poderesInterseccion.Count() == 0)
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderesConAgente, 0);
                else
                {
                    this._ventana.MostrarLstPoderSolicitud();
                }

                //_poderesInterseccion.Insert(0, new Poder(int.MinValue));

            }
            else if (_cargarPoderInicial)
            {
                _poderesInterseccion = this._poderServicios
                    .ObtenerPoderesEntreAgenteEInteresado(_patente.Agente, _patente.Interesado);
            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderesConAgente, 0);
            }

            _poderesInterseccion.Insert(0, new Poder(int.MinValue));

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que se usa para validar que cuando se modifique el poder sea actualizado
        /// </summary>
        /// <returns>true si el poder es válido, false en caso contrario</returns>
        private bool ValidarPoder()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                if (!this._ventana.PoderSolicitud.Equals(""))
                {
                    IList<Poder> poderesAux = new List<Poder>();

                    Interesado interesadoAux = new Interesado(int.Parse(this._ventana.IdInteresadoSolicitud));

                    poderesAux = this._poderServicios
                            .ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.AgenteSolicitudFiltrar, interesadoAux);


                    foreach (Poder poder in poderesAux)
                    {
                        if (poder.Id == int.Parse(this._ventana.PoderSolicitud))
                        {
                            retorno = true;
                            break;
                        }
                    }
                }
                else
                    retorno = true;

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

            return retorno;
        }

        #endregion


        /// <summary>
        /// Metodo que abre el explorador de internet predeterminado del sistema a una página determinada
        /// </summary>
        public void IrSAPI()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.IrURL(ConfigurationManager.AppSettings["UrlSAPI"].ToString());

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
        ///Método que realiza el llamado al explorador para abrir el cartel de la patente
        /// </summary>
        public void GenerarCartel()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.IrURL(ConfigurationManager.AppSettings["UrlGenerarCartel"] + ((Patente)this._ventana.Patente).Id);

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
        /// Método que se encarga de abrir el certificado de la patente en formato .pdf
        /// </summary>
        public void Certificado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["rutaCertificados"].ToString() + ((Patente)this._ventana.Patente).Id + ".pdf");

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Win32Exception ex)
            {
                logger.Error(ex.Message);
                this._ventana.ArchivoNoEncontrado();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void MostrarDiseno()
        {
            Patente patenteAux = ((Patente)this._ventana.Patente);
            if (((Patente)this._ventana.Patente).BDibujo)
            {
                EtiquetaMarca detalleEtiqueta = new EtiquetaMarca(ConfigurationManager.AppSettings["RutaImagenesDePatentes"] + patenteAux.Id + ".jpg", patenteAux.Descripcion);
                detalleEtiqueta.ShowDialog();

            }
        }


        public void CalcularDuracion()
        {
            int duracionAux = 0;

            if (((Patente)this._ventana.Patente).FechaTermino != null)
            {
                if (((Patente)this._ventana.Patente).FechaRegistro != null)
                {
                    duracionAux = TimeSpan.Parse((((Patente)this._ventana.Patente).FechaTermino -
                        ((Patente)this._ventana.Patente).FechaRegistro).ToString()).Days / 365;
                    this._ventana.Duracion = duracionAux.ToString();
                }
                else
                {

                    duracionAux = TimeSpan.Parse((((Patente)this._ventana.Patente).FechaTermino -
                        ((Patente)this._ventana.Patente).FechaInscripcion).ToString()).Days / 365;
                    this._ventana.Duracion = duracionAux.ToString();
                }

            }
        }


        public string ObtenerIdPatente()
        {
            return ((Patente)this._ventana.Patente).Id.ToString();
        }


        /// <summary>
        /// Método que consulta al asociado
        /// </summary>
        /// <returns></returns>
        public bool ConsultarAsociado()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if ((!this._ventana.IdAsociadoInternacionalFiltrar.Equals(string.Empty)) || (!this._ventana.NombreAsociadoInternacionalFiltrar.Equals(string.Empty)))
                {
                    Asociado asociadoAux = new Asociado();
                    asociadoAux.Id = !this._ventana.IdAsociadoInternacionalFiltrar.Equals(string.Empty) ? int.Parse(this._ventana.IdAsociadoInternacionalFiltrar) : 0;
                    asociadoAux.Nombre = !this._ventana.NombreAsociadoInternacionalFiltrar.Equals(string.Empty) ? this._ventana.NombreAsociadoInternacionalFiltrar : string.Empty;

                    IList<Asociado> resultados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoAux);

                    resultados.Insert(0, new Asociado());

                    this._ventana.AsociadosInternacionalesDatos = resultados;
                    this._ventana.AsociadosInternacionales = resultados;

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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }

            return retorno;
        }


        /// <summary>
        /// Método que consulta el asociado desde la pestana de datos
        /// </summary>
        /// <returns></returns>
        public bool ConsultarAsociadoDatos()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if ((!this._ventana.IdAsociadoInternacionalFiltrarDatos.Equals(string.Empty)) || (!this._ventana.NombreAsociadoInternacionalFiltrarDatos.Equals(string.Empty)))
                {
                    Asociado asociadoAux = new Asociado();
                    asociadoAux.Id = !this._ventana.IdAsociadoInternacionalFiltrarDatos.Equals(string.Empty) ? int.Parse(this._ventana.IdAsociadoInternacionalFiltrarDatos) : 0;
                    asociadoAux.Nombre = !this._ventana.NombreAsociadoInternacionalFiltrarDatos.Equals(string.Empty) ? this._ventana.NombreAsociadoInternacionalFiltrarDatos : string.Empty;

                    IList<Asociado> resultados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoAux);

                    resultados.Insert(0, new Asociado());

                    this._ventana.AsociadosInternacionalesDatos = resultados;
                    this._ventana.AsociadosInternacionales = resultados;

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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }

            return retorno;
        }


        /// <summary>
        /// Método que consulta el asociado desde la pestana de solicitud
        /// </summary>
        /// <returns></returns>
        public bool CambiarAsociadoInternacionalSolicitud()
        {
            bool retorno = false;

            try
            {
                if (this._ventana.AsociadoInternacional != null)
                {
                    if (((Asociado)this._ventana.AsociadoInternacional).Id != 0)
                    {
                        this._ventana.TextoAsociadoInternacional = ((Asociado)this._ventana.AsociadoInternacional).Nombre;
                        this._ventana.AsociadoInternacionalDatos = this._ventana.AsociadoInternacional;
                        this._ventana.AsociadoInternacional = this._ventana.AsociadoInternacional;


                    }
                    else
                    {
                        this._ventana.AsociadoInternacional = null;
                        this._ventana.AsociadoInternacionalDatos = null;
                    }
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de cambiar el asociado internacional
        /// </summary>
        /// <returns></returns>
        public bool CambiarAsociadoInternacionalDatos()
        {
            bool retorno = false;

            try
            {
                if (this._ventana.AsociadoInternacionalDatos != null)
                {
                    if (((Asociado)this._ventana.AsociadoInternacionalDatos).Id != 0)
                    {
                        this._ventana.TextoAsociadoInternacional = ((Asociado)this._ventana.AsociadoInternacionalDatos).Nombre;
                        this._ventana.AsociadoInternacional = this._ventana.AsociadoInternacional;
                        this._ventana.AsociadoInternacionalDatos = this._ventana.AsociadoInternacional;
                    }
                    else
                    {
                        this._ventana.AsociadoInternacional = null;
                        this._ventana.AsociadoInternacionalDatos = null;
                    }
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return retorno;
        }


        public void IrVentanaAsociado()
        {
            if (this._ventana.AsociadoSolicitud != null)
            {
                Asociado asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;
                Navegar(new ConsultarAsociado(asociado, this._ventana,false));
            }
        }


        public void IrVentanaInteresado()
        {
            if (this._ventana.InteresadoSolicitud != null)
            {
                Interesado interesado = ((Interesado)this._ventana.InteresadoSolicitud).Id != int.MinValue ? (Interesado)this._ventana.InteresadoSolicitud : null;
                Navegar(new ConsultarInteresado(interesado, this._ventana));
            }
        }


        public void IrVentanaPoder()
        {
            if (this._ventana.PoderSolicitud != null)
            {
                Poder poder = ((Patente)this._ventana.Patente).Poder.Id != int.MinValue ? ((Patente)this._ventana.Patente).Poder : null;
                Navegar(new ConsultarPoder(poder, this._ventana));
            }
        }

        public void CargarAsociadoInternacionalVacio()
        {
            IList<Asociado> asociados = new List<Asociado>();
            asociados.Add(new Asociado());
            this._ventana.AsociadosInternacionalesDatos = asociados;
            this._ventana.AsociadosInternacionales = asociados;
        }
        
        public void IrVentanaImprimirEdoCuenta()
        {
            if ((Asociado)this._ventana.AsociadoSolicitud != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;
                Navegar(new EstadoCuentas("2", Asociado));

            }
        }

        public void IrVentanaFacturacionDatos()
        {
            if ((Patente)this._ventana.Patente != null)
            {
                Patente Patente = ((Patente)this._ventana.Patente).Id != int.MinValue ? (Patente)this._ventana.Patente : null;
                string Id = System.Convert.ToString(Patente.Id);
                Navegar(new ConsultarFacVistaFacturaServicios(Id, "P"));

            }
        }

        public void CalcularSaldos()
        {
                        if ((Asociado)this._ventana.AsociadoSolicitud != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;
                           
                            double?  w_1,w_2,w_3,w_4,w_5,w_6,msaldope;
                            w_1 = 0;
                            w_2 = 0;
                            w_3 = 0;
                            w_4 = 0;
                            w_5 = 0;
                            w_6 = 0;
                            msaldope = 0;
                            string moneda="";
                            int casociado = Asociado.Id;
                            int? dias = 30;
                            CalcularSaldosAsociado(casociado, dias, ref w_1, ref w_2, ref w_3, ref w_4, ref w_5, ref w_6, ref msaldope, ref  moneda);

                            if (moneda=="US")
                            {
                                
                            this._ventana.SaldoVencidoSolicitud=System.Convert.ToString(w_2);
                                this._ventana.SaldoPorVencerSolicitud=System.Convert.ToString(w_4);
                                this._ventana.TotalSolicitud=System.Convert.ToString(w_2+w_4);

                            }else{
                            this._ventana.SaldoVencidoSolicitud=System.Convert.ToString(w_1);
                                this._ventana.SaldoPorVencerSolicitud=    System.Convert.ToString(w_3);
                                this._ventana.TotalSolicitud=                     System.Convert.ToString(w_1+w_3);
                            }                                
            }         

        }


    }
}