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
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.LicenciasPatentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.LicenciasPatentes;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Patentes;

namespace Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.LicenciasPatentes
{
    class PresentadorGestionarLicenciaPatentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        private IGestionarLicenciaPatentes _ventana;

        private IPatenteServicios _patenteServicios;
        private IAnaquaServicios _anaquaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IBoletinServicios _boletinServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IInteresadoServicios _interesadoServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private ICorresponsalServicios _corresponsalServicios;
        private ICondicionServicios _condicionServicios;
        private IInfoAdicionalServicios _infoAdicionalServicios;
        private IInfoBolServicios _infoBolServicios;
        private IOperacionServicios _operacionServicios;
        private IBusquedaServicios _busquedaServicios;
        private IStatusWebServicios _statusWebServicios;
        private ILicenciaPatenteServicios _licenciaServicios;


        private IList<Interesado> _interesadosLicenciante;
        private IList<Interesado> _interesadosLicenciatario;
        private IList<Agente> _agentesLicenciatario;
        private IList<Agente> _agentesLicenciante;
        private IList<Patente> _patentes;

        private IList<Poder> _poderesLicenciante;
        private IList<Poder> _poderesLicenciatario;

        private IList<Poder> _poderesApoderadosLicenciante;
        private IList<Poder> _poderesApoderadosLicenciatario;

        private IList<Poder> _poderesInterseccionLicenciante;
        private IList<Poder> _poderesInterseccionLicenciatario;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarLicenciaPatentes(IGestionarLicenciaPatentes ventana, object licencia)
        {
            try
            {

                this._ventana = ventana;

                if (licencia != null)
                {
                    this._ventana.LicenciaPatente = licencia;
                    _agregar = false;
                    CambiarAModificar();
                }
                else
                {
                    LicenciaPatente licenciaAgregar = new LicenciaPatente();
                    this._ventana.LicenciaPatente = licenciaAgregar;

                    ((LicenciaPatente)this._ventana.LicenciaPatente).Fecha = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.PoderLicenciante = null;
                    this._ventana.PoderLicenciatario = null;
                    this._ventana.InteresadoLicenciante = null;
                    this._ventana.InteresadoLicenciatario = null;
                    this._ventana.ApoderadoLicenciante = null;
                    this._ventana.ApoderadoLicenciatario = null;
                    this._ventana.Boletin = null;

                    CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

                    this._ventana.ActivarControlesAlAgregar();
                }

                #region Servicios

                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
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
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._condicionServicios = (ICondicionServicios)Activator.GetObject(typeof(ICondicionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CondicionServicios"]);
                this._anaquaServicios = (IAnaquaServicios)Activator.GetObject(typeof(IAnaquaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnaquaServicios"]);
                this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);
                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);
                this._licenciaServicios = (ILicenciaPatenteServicios)Activator.GetObject(typeof(ILicenciaPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaPatenteServicios"]);


                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }



        /// <summary>
        /// Constructor Predeterminado que recibe una ventana Padre
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarLicenciaPatentes(IGestionarLicenciaPatentes ventana, object licencia, object ventanaPadre)
        {
            try
            {

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                if (licencia != null)
                {
                    this._ventana.LicenciaPatente = licencia;
                    _agregar = false;
                    CambiarAModificar();
                }
                else
                {
                    LicenciaPatente licenciaAgregar = new LicenciaPatente();
                    this._ventana.LicenciaPatente = licenciaAgregar;

                    ((LicenciaPatente)this._ventana.LicenciaPatente).Fecha = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.PoderLicenciante = null;
                    this._ventana.PoderLicenciatario = null;
                    this._ventana.InteresadoLicenciante = null;
                    this._ventana.InteresadoLicenciatario = null;
                    this._ventana.ApoderadoLicenciante = null;
                    this._ventana.ApoderadoLicenciatario = null;
                    this._ventana.Boletin = null;

                    CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

                    this._ventana.ActivarControlesAlAgregar();
                }

                #region Servicios

                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._agenteServicios = (IAgenteServicios)Activator.GetObject(typeof(IAgenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AgenteServicios"]);
                this._poderServicios = (IPoderServicios)Activator.GetObject(typeof(IPoderServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PoderServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
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
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._condicionServicios = (ICondicionServicios)Activator.GetObject(typeof(ICondicionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CondicionServicios"]);
                this._anaquaServicios = (IAnaquaServicios)Activator.GetObject(typeof(IAnaquaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnaquaServicios"]);
                this._infoAdicionalServicios = (IInfoAdicionalServicios)Activator.GetObject(typeof(IInfoAdicionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoAdicionalServicios"]);
                this._infoBolServicios = (IInfoBolServicios)Activator.GetObject(typeof(IInfoBolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InfoBolServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);
                this._busquedaServicios = (IBusquedaServicios)Activator.GetObject(typeof(IBusquedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BusquedaServicios"]);
                this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);
                this._licenciaServicios = (ILicenciaPatenteServicios)Activator.GetObject(typeof(ILicenciaPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaPatenteServicios"]);


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
            if (_agregar == true)
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarLicenciaPatente,
                Recursos.Ids.GestionarLicenciaPatente);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarLicenciaPatente,
                Recursos.Ids.GestionarLicenciaPatente);
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

                if (_agregar == false)
                {
                    LicenciaPatente licencia = (LicenciaPatente)this._ventana.LicenciaPatente;

                    this._ventana.Patente = this._patenteServicios.ConsultarPatenteConTodo(licencia.Patente);

                    this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;

                    this._ventana.ApoderadoLicenciante = licencia.AgenteLicenciante;
                    this._ventana.ApoderadoLicenciatario = licencia.AgenteLicenciatario;
                    this._ventana.PoderLicenciante = licencia.PoderLicenciante;
                    this._ventana.PoderLicenciatario = licencia.PoderLicenciatario;

                    if (((Patente)this._ventana.Patente).LocalidadPatente != null)
                    {
                        if (((Patente)this._ventana.Patente).LocalidadPatente.Equals("I"))
                        {
                            this._ventana.EsPatenteNacional(false);
                        }
                        else
                        {
                            this._ventana.BorrarCerosInternacional();
                            this._ventana.EsPatenteNacional(true);
                        }
                    }
                    else
                    {
                        this._ventana.BorrarCerosInternacional();
                        this._ventana.EsPatenteNacional(true);
                    }



                    CargaBoletines();

                    this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, licencia.Boletin);

                    CargarPatente();

                    CargarInteresado("Licenciante");

                    CargarApoderado("Licenciante");

                    CargarPoder("Licenciante");

                    CargarInteresado("Licenciatario");

                    CargarApoderado("Licenciatario");

                    CargarPoder("Licenciatario");

                    LlenarListasPoderes((LicenciaPatente)this._ventana.LicenciaPatente);

                    ValidarLicenciante();

                    ValidarLicenciatario();

                    this._ventana.Boletin = licencia.Boletin;

                }
                else
                {
                    CargarPatente();

                    CargarInteresado("Licenciante");

                    CargarApoderado("Licenciante");

                    CargarPoder("Licenciante");

                    CargarInteresado("Licenciatario");

                    CargarApoderado("Licenciatario");

                    CargarPoder("Licenciatario");

                    CargaBoletines();
                }

                this._ventana.ConvertirEnteroMinimoABlanco();
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Método que carga los interesados implicados
        /// </summary>
        private void CargarInteresado(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Licenciante"))
            {
                this._interesadosLicenciante = new List<Interesado>();

                this._interesadosLicenciante.Add(primerInteresado);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).InteresadoLicenciante != null)
                {
                    this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo(((LicenciaPatente)this._ventana.LicenciaPatente).InteresadoLicenciante);
                    this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                    this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoLicenciante != null)
                    {
                        this._interesadosLicenciante.Add((Interesado)this._ventana.InteresadoLicenciante);
                        this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                        this._ventana.LicencianteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciantesFiltrados, (Interesado)this._ventana.InteresadoLicenciante);
                        this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciante = primerInteresado;
                    this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                    this._ventana.LicencianteFiltrado = primerInteresado;
                    this._ventana.IdLicenciante = primerInteresado.Id.ToString();

                }
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._interesadosLicenciatario = new List<Interesado>();
                this._interesadosLicenciatario.Add(primerInteresado);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).InteresadoLicenciatario != null)
                {
                    this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo(((LicenciaPatente)this._ventana.LicenciaPatente).InteresadoLicenciatario);
                    this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                    this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoLicenciatario != null)
                    {
                        this._interesadosLicenciatario.Add((Interesado)this._ventana.InteresadoLicenciatario);
                        this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                        this._ventana.LicenciatarioFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciatariosFiltrados, (Interesado)this._ventana.InteresadoLicenciatario);
                        this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciatario = primerInteresado;
                    this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                    this._ventana.LicenciatarioFiltrado = primerInteresado;
                    this._ventana.IdLicenciatario = primerInteresado.Id.ToString();
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Método que carga los Apoderados implicados
        /// </summary>
        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Licenciante"))
            {
                this._agentesLicenciante = new List<Agente>();
                this._agentesLicenciante.Add(primerAgente);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).AgenteLicenciante != null)
                {
                    this._agentesLicenciante.Add((Agente)this._ventana.ApoderadoLicenciante);
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosLicencianteFiltrados, (Agente)this._ventana.ApoderadoLicenciante);
                    this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicenciante).Id;
                }
                else
                {
                    this._ventana.ApoderadoLicenciante = primerAgente;
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                    this._ventana.IdApoderadoLicenciante = primerAgente.Id;
                }
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._agentesLicenciatario = new List<Agente>();
                this._agentesLicenciatario.Add(primerAgente);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).AgenteLicenciatario != null)
                {
                    this._agentesLicenciatario.Add((Agente)this._ventana.ApoderadoLicenciatario);
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosLicenciatarioFiltrados, (Agente)this._ventana.ApoderadoLicenciatario);
                    this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatario).Id;
                }
                else
                {
                    this._ventana.ApoderadoLicenciatario = primerAgente;
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                    this._ventana.IdApoderadoLicenciatario = primerAgente.Id;
                }
            }
        }


        /// <summary>
        /// Método que carga los Poderes
        /// </summary>
        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Licenciante"))
            {
                this._poderesLicenciante = new List<Poder>();
                this._poderesLicenciante.Add(primerPoder);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).PoderLicenciante != null)
                {
                    this._poderesLicenciante.Add((Poder)this._ventana.PoderLicenciante);
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesLicencianteFiltrados, (Poder)this._ventana.PoderLicenciante);
                }
                else
                {
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = primerPoder;
                }

            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._poderesLicenciatario = new List<Poder>();
                this._poderesLicenciatario.Add(primerPoder);

                if (((LicenciaPatente)this._ventana.LicenciaPatente).PoderLicenciatario != null)
                {
                    this._poderesLicenciatario.Add((Poder)this._ventana.PoderLicenciatario);
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesLicenciatarioFiltrados, (Poder)this._ventana.PoderLicenciatario);
                }
                else
                {
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = primerPoder;
                }

            }
        }


        /// <summary>
        /// Método que dependiendo del estado de la pagina carga una licencia registrada
        /// o nueva
        /// </summary>
        public LicenciaPatente CargarLicenciaDeLaPantalla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            LicenciaPatente licencia = (LicenciaPatente)this._ventana.LicenciaPatente;

            if ((null != this._ventana.PatenteFiltrada) && (((Patente)this._ventana.PatenteFiltrada).Id != int.MinValue))
            {
                licencia.Patente = (Patente)this._ventana.PatenteFiltrada;
                licencia.InteresadoLicenciante = ((Patente)this._ventana.PatenteFiltrada).Interesado;
                licencia.AgenteLicenciante = ((Patente)this._ventana.PatenteFiltrada).Agente;
                licencia.PoderLicenciante = ((Patente)this._ventana.PatenteFiltrada).Poder;
            }

            if (null != this._ventana.InteresadoLicenciante)
                licencia.InteresadoLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id != int.MinValue ? (Interesado)this._ventana.InteresadoLicenciante : null;

            if (null != this._ventana.InteresadoLicenciatario)
                licencia.InteresadoLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id != int.MinValue ?
                                                                    (Interesado)this._ventana.InteresadoLicenciatario : null;

            if (null != this._ventana.ApoderadoLicencianteFiltrado)
                licencia.AgenteLicenciante = !((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals("") ? (Agente)this._ventana.ApoderadoLicencianteFiltrado : null;

            if (null != this._ventana.ApoderadoLicenciatarioFiltrado)
                licencia.AgenteLicenciatario = !((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals("") ?
                                                            (Agente)this._ventana.ApoderadoLicenciatarioFiltrado : null;

            if (null != this._ventana.PoderLicencianteFiltrado)
                licencia.PoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue ? (Poder)this._ventana.PoderLicencianteFiltrado : null;

            if (null != this._ventana.PoderLicenciatarioFiltrado)
                licencia.PoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue ?
                                                                (Poder)this._ventana.PoderLicenciatarioFiltrado : null;

            if (null != this._ventana.Boletin)
                licencia.Boletin = ((Boletin)this._ventana.Boletin).Id != int.MinValue ?
                                                    (Boletin)this._ventana.Boletin : null;


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            return licencia;
        }


        /// <summary>
        /// Método que habilita los campos
        /// </summary>
        public void CambiarAModificar()
        {
            this._ventana.HabilitarCampos = true;
            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
        }


        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del usuario
        /// </summary>
        public void Modificar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Pais
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    LicenciaPatente licencia = CargarLicenciaDeLaPantalla();
                    licencia.Patente = (Patente)this._ventana.Patente;

                    if (null != licencia.Patente)
                    {

                        int? exitoso = this._licenciaServicios.InsertarOModificarLicencia(licencia, UsuarioLogeado.Hash);

                        if ((!exitoso.Equals(null)) && (this._agregar == false))
                        {
                            this._ventana.HabilitarCampos = false;
                            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                        }
                        else if ((!exitoso.Equals(null)) && (this._agregar == true))
                        {
                            licencia.Id = exitoso.Value;
                            this.Navegar(new GestionarLicenciaPatentes(licencia));
                        }
                        else
                            this.Navegar(Recursos.MensajesConElUsuario.ErrorAlGenerarTraspaso, true);
                    }
                    else
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorTraspasoSinPatente, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }


        /// <summary>
        /// Metodo que se encarga de eliminar una Patente
        /// </summary>
        public void Eliminar()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._licenciaServicios.Eliminar((LicenciaPatente)this._ventana.LicenciaPatente, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.LicenciaEliminada;
                    this.Navegar(_paginaPrincipal);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
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


        /// <summary>
        /// Carga la lista de poderes por interesado
        /// </summary>
        public void LlenarListasPoderes(LicenciaPatente licencia)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (licencia.InteresadoLicenciante != null)
                this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciante);

            if (licencia.InteresadoLicenciatario != null)
                this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciatario);

            if (licencia.AgenteLicenciante != null)
                //this._poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciante);
                this._poderesApoderadosLicenciante = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado(licencia.AgenteLicenciante, licencia.InteresadoLicenciante);

            if (licencia.AgenteLicenciatario != null)
                //this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciatario);
                this._poderesApoderadosLicenciatario = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado(licencia.AgenteLicenciatario, licencia.InteresadoLicenciatario);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Valida la lista de poderes por interesado
        /// </summary>
        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            IList<Poder> listaIntereseccionLicenciante = new List<Poder>();
            IList<Poder> listaIntereseccionLicenciatario = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderLicenciatario = new Poder();

            listaIntereseccionLicenciante.Add(primerPoder);
            listaIntereseccionLicenciatario.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Licenciante")))
                        {
                            listaIntereseccionLicenciante.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Licenciatario")))
                        {
                            listaIntereseccionLicenciatario.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionLicenciante.Count != 0) && (tipo.Equals("Licenciante")))
                {
                    poderLicenciatario = (Poder)this._ventana.PoderLicencianteFiltrado;
                    this._poderesInterseccionLicenciante = listaIntereseccionLicenciante;
                    this._ventana.PoderesLicencianteFiltrados = listaIntereseccionLicenciante;
                    this._ventana.PoderLicencianteFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesLicencianteFiltrados, poderLicenciatario);
                }


                else if ((listaIntereseccionLicenciatario.Count != 0) && (tipo.Equals("Licenciatario")))
                {
                    poderLicenciatario = (Poder)this._ventana.PoderLicenciatarioFiltrado;
                    this._poderesInterseccionLicenciatario = listaIntereseccionLicenciatario;
                    this._ventana.PoderesLicenciatarioFiltrados = listaIntereseccionLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesLicenciatarioFiltrados, poderLicenciatario);
                }

                else
                    retorno = false;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        /// <summary>
        /// Carga la lista de Agentes y la lista de interesados
        /// </summary>
        public void LlenarListaAgenteEInteresado(Poder poder, string tipo, bool cargaInicial)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado interesado = new Interesado();
                IList<Agente> agentesInteresadoFiltrados;
                IList<Interesado> interesadosFiltrados = new List<Interesado>();
                Poder poderFiltrar = new Poder();

                Interesado primerInteresado = new Interesado(int.MinValue);
                Agente primerAgente = new Agente("");

                Agente agenteLicenciante = new Agente();

                agentesInteresadoFiltrados = new List<Agente>();

                if (tipo.Equals("Licenciante"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderLicencianteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderLicencianteFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderLicencianteFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderLicencianteFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.LicenciantesFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.LicencianteFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.LicencianteFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.LicencianteFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteLicenciante = (Agente)this._ventana.ApoderadoLicencianteFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosLicencianteFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoLicencianteFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteLicenciante);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                    }
                }
                else if (tipo.Equals("Licenciatario"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderLicenciatarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderLicenciatarioFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderLicenciatarioFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderLicenciatarioFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.LicenciatariosFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.LicenciatarioFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.LicenciatarioFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.LicenciatarioFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteLicenciante = (Agente)this._ventana.ApoderadoLicenciatarioFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosLicenciatarioFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoLicenciatarioFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteLicenciante);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                    }
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
        /// Carga la lista de Agentes
        /// </summary>
        private void LlenarListaAgente(Poder poder, string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Agente primerAgente = new Agente("");

            if (tipo.Equals("Licenciante"))
            {
                this._agentesLicenciante = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesLicenciante.Insert(0, primerAgente);
                this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._agentesLicenciatario = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesLicenciatario.Insert(0, primerAgente);
                this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Verifica si se cambio el Interesado
        /// </summary>
        public bool VerificarCambioInteresado(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (tipo.Equals("Licenciante"))
            {
                if ((((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals("")))
                    retorno = true;
            }
            if (tipo.Equals("Licenciatario"))
            {
                if ((((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals("")))
                    retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        /// <summary>
        /// Verifica si se cambio el Agente
        /// </summary>
        public bool VerificarCambioAgente(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (tipo.Equals("Licenciante"))
            {
                if (!(((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue))
                    retorno = true;
            }
            if (tipo.Equals("Licenciatario"))
            {
                if (!(((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue))
                    retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        /// <summary>
        /// Verifica si se cambio el Poder
        /// </summary>
        public bool VerificarCambioPoder(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (tipo.Equals("Licenciante"))
            {
                if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                    return true;
            }
            if (tipo.Equals("Licenciatario"))
            {
                if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                    return true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return false;
        }


        /// <summary>
        /// Vacia la lista de Interesados
        /// </summary>
        public void LimpiarListaInteresado(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            if (tipo.Equals("Licenciante"))
            {
                this._ventana.LicenciantesFiltrados = listaInteresados;
                this._ventana.LicencianteFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._ventana.LicenciatariosFiltrados = listaInteresados;
                this._ventana.LicenciatarioFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Vacia la lista de Agentes
        /// </summary>
        public void LimpiarListaAgente(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            if (tipo.Equals("Licenciante"))
            {
                this._ventana.ApoderadosLicencianteFiltrados = listaAgentes;
                this._ventana.ApoderadoLicencianteFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._ventana.ApoderadosLicenciatarioFiltrados = listaAgentes;
                this._ventana.ApoderadoLicenciatarioFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Vacia la lista de Poderes
        /// </summary>
        public void LimpiarListaPoder(string tipo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            if (tipo.Equals("Licenciante"))
            {
                this._ventana.PoderesLicencianteFiltrados = listaPoderes;
                this._ventana.PoderLicencianteFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._ventana.PoderesLicenciatarioFiltrados = listaPoderes;
                this._ventana.PoderLicenciatarioFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        public void IrVentanaAsociado()
        {
            if (((Patente)this._ventana.Patente != null) && (((Patente)this._ventana.Patente).Asociado != null))
            {
                Asociado asociado = ((Patente)this._ventana.Patente).Asociado.Id != int.MinValue ? ((Patente)this._ventana.Patente).Asociado : null;
                Navegar(new ConsultarAsociado(asociado, this._ventana, false));
            }
        }


        #region Patente

        public void IrConsultarPatentes()
        {
            //OJO       this.Navegar(new ConsultarLicenciasPatentes());
        }

        /// <summary>
        /// Metodo que carga las Patentes registradas
        /// </summary>
        private void CargarPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._patentes = new List<Patente>();
            Patente primeraPatente = new Patente(int.MinValue);
            this._patentes.Add(primeraPatente);

            if ((Patente)this._ventana.Patente != null)
            {
                this._patentes.Add((Patente)this._ventana.Patente);
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = (Patente)this._ventana.Patente;
                this._ventana.IdPatente = ((Patente)this._ventana.Patente).Id.ToString();



                if (null != ((Patente)this._ventana.Patente).Asociado)
                    this._ventana.PintarAsociado(((Patente)this._ventana.Patente).Asociado.TipoCliente.Id);
                else
                    this._ventana.PintarAsociado("5");

                IList<ListaDatosDominio> tiposPatentes = this._listaDatosDominioServicios.
                ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoPatente));
                ListaDatosDominio DatoDominio = new ListaDatosDominio();
                DatoDominio.Id = ((Patente)this._ventana.Patente).Tipo;
                DatoDominio = BuscarListaDeDominio(tiposPatentes, DatoDominio);
                if (null != DatoDominio)
                    this._ventana.Tipo = DatoDominio.Descripcion;
                else
                    this._ventana.Tipo = "";
            }
            else
            {
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = primeraPatente;
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Metodo que Consulta las Patentes
        /// </summary>
        public void ConsultarPatentes()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Patente primeraPatente = new Patente(int.MinValue);


                Patente patente = new Patente();
                IList<Patente> patentesFiltradas;
                patente.Descripcion = this._ventana.NombrePatenteFiltrar.ToUpper();
                patente.Id = this._ventana.IdPatenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPatenteFiltrar);

                if ((!patente.Descripcion.Equals("")) || (patente.Id != 0))
                    patentesFiltradas = this._patenteServicios.ObtenerPatentesFiltro(patente);
                else
                    patentesFiltradas = new List<Patente>();

                if (patentesFiltradas.ToList<Patente>().Count != 0)
                {
                    patentesFiltradas.Insert(0, primeraPatente);
                    this._ventana.PatentesFiltradas = patentesFiltradas.ToList<Patente>();
                    this._ventana.PatenteFiltrada = primeraPatente;
                }
                else
                {
                    patentesFiltradas.Insert(0, primeraPatente);
                    this._ventana.PatentesFiltradas = this._patentes;
                    this._ventana.PatenteFiltrada = primeraPatente;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que cambia la Patente
        /// </summary>
        public bool CambiarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.PatenteFiltrada != null)
                {
                    this._ventana.Patente = this._ventana.PatenteFiltrada;
                    this._ventana.NombrePatente = ((Patente)this._ventana.PatenteFiltrada).Descripcion;
                    this._ventana.IdPatente = ((Patente)this._ventana.Patente).Id.ToString();
                    this._ventana.InteresadoLicenciante = ((Patente)this._ventana.Patente).Interesado;

                    if (((Patente)this._ventana.Patente).LocalidadPatente != null)
                        this._ventana.EsPatenteNacional(!((Patente)this._ventana.Patente).LocalidadPatente.Equals("I"));
                    else
                        this._ventana.EsPatenteNacional(true);

                    if (((Patente)this._ventana.Patente).Interesado != null)
                        this._ventana.IdLicenciante = (((Patente)this._ventana.Patente).Interesado).Id.ToString();

                    IList<Interesado> listaAux = new List<Interesado>();
                    listaAux.Add(new Interesado(int.MinValue));

                    if (((Patente)this._ventana.PatenteFiltrada).Id != int.MinValue)
                    {
                        listaAux.Add((Interesado)this._ventana.InteresadoLicenciante);

                        this._ventana.LicenciantesFiltrados = listaAux;
                        this._ventana.LicencianteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciantesFiltrados,
                            (Interesado)this._ventana.InteresadoLicenciante);
                    }
                    else
                    {
                        this._ventana.LicenciantesFiltrados = listaAux;
                        this._ventana.IdLicenciante = int.MinValue.ToString();
                        this._ventana.LicencianteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciantesFiltrados,
                            new Interesado(int.MinValue));
                    }

                    this._ventana.ApoderadoLicenciante = ((Patente)this._ventana.Patente).Agente;
                    this._ventana.PoderLicenciante = ((Patente)this._ventana.Patente).Poder;


                    if (null != ((Patente)this._ventana.Patente).Asociado)
                        this._ventana.PintarAsociado(((Patente)this._ventana.Patente).Asociado.TipoCliente.Id);
                    else
                        this._ventana.PintarAsociado("5");

                    this._ventana.ConvertirEnteroMinimoABlanco();

                    IList<ListaDatosDominio> tiposPatentes = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoPatente));
                    ListaDatosDominio DatoDominio = new ListaDatosDominio();
                    DatoDominio.Id = ((Patente)this._ventana.Patente).Tipo;
                    DatoDominio = BuscarListaDeDominio(tiposPatentes, DatoDominio);
                    if (null != DatoDominio)
                        this._ventana.Tipo = DatoDominio.Descripcion;
                    else
                        this._ventana.Tipo = "";
                    retorno = true;
                }

                this._ventana.BorrarCerosInternacional();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        #endregion


        #region Licenciante

        /// <summary>
        /// Metodo que valida al licenciante seleccionado
        /// </summary>
        private void ValidarLicenciante()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (((Interesado)this._ventana.LicencianteFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", true);
                    }
                }
                else
                {
                    if (((LicenciaPatente)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", true);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", true);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", true);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", true);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante");

                        //this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", true);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);
                        ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante");

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", true);
                    }
                }
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Metodo que Consulta posibles licenciantes
        /// </summary>
        public void ConsultarLicenciantes()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

                Interesado interesado = new Interesado();
                IList<Interesado> interesadosFiltrados;
                interesado.Nombre = this._ventana.NombreLicencianteFiltrar.ToUpper();
                interesado.Id = this._ventana.IdLicencianteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdLicencianteFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.LicenciantesFiltrados = interesadosFiltrados;
                    this._ventana.LicencianteFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                    this._ventana.LicencianteFiltrado = primerInteresado;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que Consulta posibles Apoderados
        /// </summary>
        public void ConsultarApoderadosLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");


                Agente apoderadoLicenciante = new Agente();
                IList<Agente> agentesLicencianteFiltrados;
                apoderadoLicenciante.Nombre = this._ventana.NombreApoderadoLicencianteFiltrar.ToUpper();
                apoderadoLicenciante.Id = this._ventana.IdApoderadoLicencianteFiltrar.ToUpper();

                if ((!apoderadoLicenciante.Nombre.Equals("")) || (!apoderadoLicenciante.Id.Equals("")))
                    agentesLicencianteFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoLicenciante);
                else
                    agentesLicencianteFiltrados = new List<Agente>();

                if (agentesLicencianteFiltrados.Count != 0)
                {
                    agentesLicencianteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                    this._ventana.ApoderadosLicencianteFiltrados = agentesLicencianteFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesLicencianteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que Consulta los poderes del licenciante
        /// </summary>
        public void ConsultarPoderesLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                Poder poderLicenciante = new Poder();
                IList<Poder> poderesLicencianteFiltrados;

                if (!this._ventana.IdPoderLicencianteFiltrar.Equals(""))
                    poderLicenciante.Id = int.Parse(this._ventana.IdPoderLicencianteFiltrar);
                else
                    poderLicenciante.Id = 0;

                if (!this._ventana.FechaPoderLicencianteFiltrar.Equals(""))
                    poderLicenciante.Fecha = DateTime.Parse(this._ventana.FechaPoderLicencianteFiltrar);

                if ((!poderLicenciante.Fecha.Equals("")) || (poderLicenciante.Id != 0))
                    poderesLicencianteFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderLicenciante);
                else
                    poderesLicencianteFiltrados = new List<Poder>();

                if (poderesLicencianteFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesLicencianteFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = primerPoder;
                    this._ventana.PoderesLicencianteFiltrados = poderesLicencianteFiltrados;
                }
                else
                {
                    poderesLicencianteFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = primerPoder;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que valida el cambio de licenciante
        /// </summary>
        public bool CambiarLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicencianteFiltrado);
                            this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                            this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));

                            LimpiarListaPoder("Licenciante");

                            if ((this.ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante")))
                            {
                                this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                                this._ventana.NombreLicenciante = ((Interesado)this._ventana.LicencianteFiltrado).Nombre;
                                this._ventana.IdLicenciante = ((Interesado)this._ventana.LicencianteFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante"))
                            {
                                //this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Licenciante"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));
                            this._poderesLicenciante.Insert(0, primerPoder);
                            this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                            this._ventana.PoderLicencianteFiltrado = primerPoder;

                            this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));
                            this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicencianteFiltrado);
                            this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                            this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();
                            retorno = true;
                        }
                        else
                        {

                            this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));
                            this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicencianteFiltrado);
                            this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                            this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                    this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                    this._ventana.IdLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que valida el cambio de Apoderado
        /// </summary>
        public bool CambiarApoderadoLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> _poderesFiltrados = new List<Poder>();
            Poder poderABuscar = null;
            bool listaDePoderesValidada = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                            //-- 
                            //Para validar que el Agente que estoy seleccionando tenga Poderes con el Licenciante
                            Poder primerPoder = new Poder();
                            primerPoder.Id = int.MinValue;

                            _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicencianteFiltrado, (Interesado)this._ventana.LicencianteFiltrado);

                            if (_poderesFiltrados.Count != 0)
                            {
                                poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderLicencianteFiltrado);

                                if (poderABuscar != null)
                                {
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesLicencianteFiltrados = _poderesFiltrados;
                                    this._ventana.PoderLicencianteFiltrado = poderABuscar;
                                }
                                else
                                {
                                    this._ventana.Mensaje("Seleccione un poder que relacione al Apoderado con el Licenciante", 0);
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesLicencianteFiltrados = _poderesFiltrados;

                                }
                            }

                            else
                            {
                                this._ventana.Mensaje("Apoderado no posee poderes con el Licenciante", 0);
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesLicencianteFiltrados = _poderesFiltrados;
                            }

                            //--
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosLicenciante = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicencianteFiltrado, (Interesado)this._ventana.LicencianteFiltrado);

                            #region CODIGO ORIGINAL COMENTADO - NO BORRAR
                            //LimpiarListaPoder("Licenciante");
                            //listaDePoderesValidada = this.ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante"); 
                            #endregion

                            if (this._poderesApoderadosLicenciante.Count > 0)
                            {
                                this._poderesApoderadosLicenciante.Insert(0, new Poder(int.MinValue));
                                this._ventana.PoderesLicencianteFiltrados = this._poderesApoderadosLicenciante;
                                listaDePoderesValidada = true;
                            }
                            
                            if(listaDePoderesValidada)
                            {
                                this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                                this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                                this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                                retorno = true;
                            }
                            else
                            {
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciante"), 0);
                                this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                                this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                                this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                                retorno = true;
                            }
                        }
                    }
                    else
                    {
                        //if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                        if ((this._ventana.PoderLicencianteFiltrado != null) && (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue))
                        {
                            //--
                            this._ventana.Mensaje("Debe seleccionar un Licenciante", 0);
                            LimpiarListaPoder("Licenciante");
                            //--
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                            retorno = true;
                        }
                        else
                        {
                            //--
                            this._ventana.Mensaje("Debe seleccionar un Licenciante", 0);
                            //--
                            //this._poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicencianteFiltrado));
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                    this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                    this._ventana.IdApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que valida el cambio de poder del licenciante
        /// </summary>
        public bool CambiarPoderLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> _poderesFiltrados = new List<Poder>();
            Poder poderABuscar = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.LicencianteFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Licenciante");

                            LlenarListaAgente((Poder)this._ventana.PoderLicencianteFiltrado, "Licenciante");

                            this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Licenciante");

                            LimpiarListaAgente("Licenciante");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicencianteFiltrado, "Licenciante", false);

                            this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        //El Agente es diferente a Vacio
                        //--

                        Poder primerPoder = new Poder();
                        primerPoder.Id = int.MinValue;
                        _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicencianteFiltrado, (Interesado)this._ventana.LicencianteFiltrado);
                        if (_poderesFiltrados.Count != 0)
                        {
                            poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderLicencianteFiltrado);

                            if (poderABuscar != null)
                            {
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesLicencianteFiltrados = _poderesFiltrados;
                                this._ventana.PoderLicencianteFiltrado = poderABuscar;
                                this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                                this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else
                            {
                                //this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "interesado"), 0);
                                this._ventana.Mensaje("El Poder no pertenece al Licenciante", 0);
                                this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                                this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                                retorno = true;
                            }
                        }
                        else
                        {
                            this._ventana.Mensaje("El poder seleccionado no relaciona al Apoderado con el Licenciante", 0);
                            this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                            this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                    this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        #endregion


        #region Licenciatario

        /// <summary>
        /// Metodo que Valida el licenciatario seleccionado
        /// </summary>
        private void ValidarLicenciatario()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (((Interesado)this._ventana.LicenciatarioFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);
                        //this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", true);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", true);

                    else
                    {

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", true);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", true);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", true);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                    {

                        ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario");

                        //this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", true);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);
                        ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario");

                        //this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        //this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", true);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", true);
                    }
                }
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Metodo que carga los licenciatarios
        /// </summary>
        public void ConsultarLicenciatarios()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

                Interesado licenciaario = new Interesado();
                IList<Interesado> licenciaariosFiltrados;
                licenciaario.Nombre = this._ventana.NombreLicenciatarioFiltrar.ToUpper();
                licenciaario.Id = this._ventana.IdLicenciatarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdLicenciatarioFiltrar);

                if ((!licenciaario.Nombre.Equals("")) || (licenciaario.Id != 0))
                    licenciaariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(licenciaario);
                else
                    licenciaariosFiltrados = new List<Interesado>();

                if (licenciaariosFiltrados.ToList<Interesado>().Count != 0)
                {
                    licenciaariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.LicenciatariosFiltrados = licenciaariosFiltrados.ToList<Interesado>();
                    this._ventana.LicenciatarioFiltrado = primerInteresado;
                }
                else
                {
                    licenciaariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                    this._ventana.LicenciatarioFiltrado = primerInteresado;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que carga los apoderados
        /// </summary>
        public void ConsultarApoderadosLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");
                Agente apoderadoLicenciatario = new Agente();
                IList<Agente> agentesLicenciatarioFiltrados;
                apoderadoLicenciatario.Nombre = this._ventana.NombreApoderadoLicenciatarioFiltrar.ToUpper();
                apoderadoLicenciatario.Id = this._ventana.IdApoderadoLicenciatarioFiltrar.ToUpper();

                if ((!apoderadoLicenciatario.Nombre.Equals("")) || (!apoderadoLicenciatario.Id.Equals("")))
                    agentesLicenciatarioFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoLicenciatario);
                else
                    agentesLicenciatarioFiltrados = new List<Agente>();

                if (agentesLicenciatarioFiltrados.Count != 0)
                {
                    agentesLicenciatarioFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                    this._ventana.ApoderadosLicenciatarioFiltrados = agentesLicenciatarioFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesLicenciatarioFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que carga los poderes
        /// </summary>
        public void ConsultarPoderesLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);
                Poder poderLicenciatario = new Poder();
                IList<Poder> poderesLicenciatarioFiltrados;

                if (!this._ventana.IdPoderLicenciatarioFiltrar.Equals(""))
                    poderLicenciatario.Id = int.Parse(this._ventana.IdPoderLicenciatarioFiltrar);
                else
                    poderLicenciatario.Id = 0;

                if (!this._ventana.FechaPoderLicenciatarioFiltrar.Equals(""))
                    poderLicenciatario.Fecha = DateTime.Parse(this._ventana.FechaPoderLicenciatarioFiltrar);

                if ((!poderLicenciatario.Fecha.Equals("")) || (poderLicenciatario.Id != 0))
                    poderesLicenciatarioFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderLicenciatario);
                else
                    poderesLicenciatarioFiltrados = new List<Poder>();

                if (poderesLicenciatarioFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesLicenciatarioFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = primerPoder;
                    this._ventana.PoderesLicenciatarioFiltrados = poderesLicenciatarioFiltrados;
                }
                else
                {
                    poderesLicenciatarioFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = primerPoder;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que Valida el cambio de licenciatario
        /// </summary>
        public bool CambiarLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicenciatarioFiltrado);
                            this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                            this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));

                            LimpiarListaPoder("Licenciatario");

                            if ((this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario")))
                            {
                                this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
                                this._ventana.NombreLicenciatario = ((Interesado)this._ventana.LicenciatarioFiltrado).Nombre;
                                this._ventana.IdLicenciatario = ((Interesado)this._ventana.LicenciatarioFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciatario, _poderesApoderadosLicenciatario, "Licenciatario"))
                            {
                                //this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Licenciatario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));
                            this._poderesLicenciatario.Insert(0, primerPoder);
                            this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                            this._ventana.PoderLicenciatarioFiltrado = primerPoder;

                            this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));
                            this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicenciatarioFiltrado);
                            this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                            this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();
                            retorno = true;
                        }
                        else
                        {

                            this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));
                            this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicenciatarioFiltrado);
                            this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                            this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
                    this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                    this._ventana.IdLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que Valida el cambio de Apoderado
        /// </summary>
        public bool CambiarApoderadoLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> _poderesFiltrados = new List<Poder>();
            Poder poderABuscar = null;
            bool listaDePoderesValidada = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                            //-- 
                            //Para validar que el Agente que estoy seleccionando tenga Poderes con el Licenciatario
                            Poder primerPoder = new Poder();
                            primerPoder.Id = int.MinValue;

                            _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicenciatarioFiltrado, (Interesado)this._ventana.LicenciatarioFiltrado);

                            if (_poderesFiltrados.Count != 0)
                            {
                                poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderLicenciatarioFiltrado);

                                if (poderABuscar != null)
                                {
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesLicenciatarioFiltrados = _poderesFiltrados;
                                    this._ventana.PoderLicenciatarioFiltrado = poderABuscar;
                                }
                                else
                                {
                                    this._ventana.Mensaje("Seleccione un poder que relacione al Apoderado con el Licenciatario", 0);
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesLicenciatarioFiltrados = _poderesFiltrados;

                                }
                            }

                            else
                            {
                                this._ventana.Mensaje("Apoderado no posee poderes con el Licenciatario", 0);
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesLicenciatarioFiltrados = _poderesFiltrados;
                            }

                            //--
                            retorno = true;
                        }
                        else
                        {
                            //this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicenciatarioFiltrado));
                            this._poderesApoderadosLicenciatario = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicenciatarioFiltrado, (Interesado)this._ventana.LicenciatarioFiltrado);
                            
                            LimpiarListaPoder("Licenciatario");

                            listaDePoderesValidada = this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario");

                            //if ((this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario")))
                            if(listaDePoderesValidada)
                            {
                                this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                                this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                                this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                                retorno = true;
                            }
                            //else if (!this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario"))
                            else
                            {
                                //this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciatario"), 0);
                                this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                                this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                                this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                                retorno = true;
                            }
                        }
                    }
                    else
                    {
                        //if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                        if ((this._ventana.PoderLicenciatarioFiltrado != null) && (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue))
                        {
                            //--
                            this._ventana.Mensaje("Debe seleccionar un Licenciatario", 0);
                            LimpiarListaPoder("Licenciatario");
                            //--
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                            retorno = true;
                        }
                        else
                        {
                            //--
                            this._ventana.Mensaje("Debe seleccionar un Licenciatario", 0);
                            //LimpiarListaPoder("Licenciante");
                            //--
                            //this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicenciatarioFiltrado));
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                    this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                    this._ventana.IdApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que Valida el cambio de Poder
        /// </summary>
        public bool CambiarPoderLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> _poderesFiltrados = new List<Poder>();
            Poder poderABuscar = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.LicenciatarioFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Licenciatario");

                            LlenarListaAgente((Poder)this._ventana.PoderLicenciatarioFiltrado, "Licenciatario");

                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Licenciatario");

                            LimpiarListaAgente("Licenciatario");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatarioFiltrado, "Licenciatario", false);

                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        //El Agente es diferente a Vacio
                        //--

                        Poder primerPoder = new Poder();
                        primerPoder.Id = int.MinValue;
                        _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.ApoderadoLicenciatarioFiltrado, (Interesado)this._ventana.LicenciatarioFiltrado);
                        if (_poderesFiltrados.Count != 0)
                        {
                            poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderLicenciatarioFiltrado);

                            if (poderABuscar != null)
                            {
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesLicenciatarioFiltrados = _poderesFiltrados;
                                this._ventana.PoderLicenciatarioFiltrado = poderABuscar;
                                this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                                this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else
                            {
                                //this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "interesado"), 0);
                                this._ventana.Mensaje("El Poder no pertenece al Licenciatario", 0);
                                this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                                this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                                retorno = true;
                            }
                        }
                        else
                        {
                            this._ventana.Mensaje("El poder seleccionado no relaciona al Apoderado con el Licenciatario", 0);
                            this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                            this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                    this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco();

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
            finally
            {
                Mouse.OverrideCursor = null;
            }

            return retorno;
        }

        #endregion


        //public void IrImprimir(string nombreBoton)
        //{
        //    try
        //    {
        //        switch (nombreBoton)
        //        {
        //            case "_btnPlanilla":
        //                ImprimirPlanilla();
        //                break;
        //            case "_btnAnexo":
        //                ImprimirAnexo();
        //                break;
        //            case "_btnCarpeta":
        //                ImprimirCarpeta();
        //                break;
        //            case "_btnPlanillaVan":
        //                ImprimirPlanillaVan();
        //                break;
        //            case "_btnPlanillaVienen":
        //                ImprimirPlanillaVienen();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ExcepcionPaquetes, true);
        //    }
        //}

        //private void ImprimirPlanillaVienen()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MLICENCIAS";
        //        string procedimiento = "P5";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((LicenciaPatente)this._ventana.LicenciaPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVienen);
        //    }
        //}

        //private void ImprimirPlanillaVan()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MLICENCIAS";
        //        string procedimiento = "P4";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((LicenciaPatente)this._ventana.LicenciaPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVan);
        //    }
        //}

        //private void ImprimirCarpeta()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MLICENCIAS";
        //        string procedimiento = "P3";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((LicenciaPatente)this._ventana.LicenciaPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnCarpeta);
        //    }
        //}

        //private bool ValidarPatenteAntesDeImprimirCarpeta()
        //{
        //    return true;
        //}

        //private void ImprimirAnexo()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MLICENCIAS";
        //        string procedimiento = "P2";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((LicenciaPatente)this._ventana.LicenciaPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexo);
        //    }
        //}

        //private bool ValidarPatenteAntesDeImprimirAnexo()
        //{
        //    return true;
        //}

        //private void ImprimirPlanilla()
        //{
        //    if (ValidarPatenteAntesDeImprimirCarpeta())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MLICENCIAS";
        //        string procedimiento = "P1";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento(((LicenciaPatente)this._ventana.LicenciaPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanilla);
        //    }
        //}

        //private bool ValidarPatenteAntesDeImprimirPlanilla()
        //{
        //    return true;
        //}

        /// <summary>
        /// Metodo que consulta la Patente del Traspaso
        /// </summary>
        public void ConsultarPatenteDeTraspaso()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Patente patenteAux = new Patente();

                if (!this._ventana.IdPatente.Equals(String.Empty))
                {
                    patenteAux.Id = int.Parse(this._ventana.IdPatente);
                    IList<Patente> patentes = this._patenteServicios.ObtenerPatentesFiltro(patenteAux);
                    if (patentes.Count > 0)
                    {
                        this.Navegar(new GestionarPatente(patentes[0], this._ventana));
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
        /// Metodo que consulta los Interesados de una Licencia de Uso (Licenciante y Licenciatario)
        /// </summary>
        /// <param name="nombreBoton">Nombre del boton presionado</param>
        public void ConsultarInteresadosLicenciaUso(string nombreBoton)
        {
            Interesado interesadoAux = new Interesado();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (nombreBoton.Equals("_btnLicenciante"))
                {
                    if (!this._ventana.IdLicenciante.Equals(String.Empty))
                        interesadoAux.Id = int.Parse(this._ventana.IdLicenciante);
                    else
                        interesadoAux.Id = int.MinValue;
                }
                else if (nombreBoton.Equals("_btnLicenciatario"))
                {
                    if (!this._ventana.IdLicenciatario.Equals(String.Empty))
                        interesadoAux.Id = int.Parse(this._ventana.IdLicenciatario);
                    else
                        interesadoAux.Id = int.MinValue;
                }

                if (interesadoAux.Id != int.MinValue)
                {
                    IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoAux);
                    if (interesados.Count > 0)
                        this.Navegar(new ConsultarInteresado(interesados[0], this._ventana));
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