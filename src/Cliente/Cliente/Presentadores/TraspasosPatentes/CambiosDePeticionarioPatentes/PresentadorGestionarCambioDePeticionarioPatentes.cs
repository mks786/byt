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
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CambiosDePeticionarioPatentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosPeticionarioPatentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.CambiosDePeticionarioPatentes
{
    class PresentadorGestionarCambioDePeticionarioPatentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        private IGestionarCambioDePeticionarioPatentes _ventana;

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
        private ICambioPeticionarioPatenteServicios _cambioPeticionarioPatenteServicios;

        private IList<Interesado> _interesadosAnterior;
        private IList<Interesado> _interesadosActual;
        private IList<Agente> _agentesActual;
        private IList<Agente> _agentesAnterior;
        private IList<Patente> _patentes;

        private IList<Poder> _poderesAnterior;
        private IList<Poder> _poderesActual;

        private IList<Poder> _poderesApoderadosAnterior;
        private IList<Poder> _poderesApoderadosActual;

        private IList<Poder> _poderesInterseccionAnterior;
        private IList<Poder> _poderesInterseccionActual;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCambioDePeticionarioPatentes(IGestionarCambioDePeticionarioPatentes ventana, object cambioPeticionario)
        {
            try
            {

                this._ventana = ventana;

                if (cambioPeticionario != null)
                {
                    this._ventana.CambioPeticionarioPatente = cambioPeticionario;
                    _agregar = false;
                }
                else
                {
                    CambioPeticionarioPatente cambioPeticionarioAgregar = new CambioPeticionarioPatente();
                    this._ventana.CambioPeticionarioPatente = cambioPeticionarioAgregar;

                    ((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).FechaPeticionario = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.PoderAnterior = null;
                    this._ventana.PoderActual = null;
                    this._ventana.InteresadoAnterior = null;
                    this._ventana.InteresadoActual = null;
                    this._ventana.ApoderadoAnterior = null;
                    this._ventana.ApoderadoActual = null;

                    CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

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
                this._cambioPeticionarioPatenteServicios = (ICambioPeticionarioPatenteServicios)Activator.GetObject(typeof(ICambioPeticionarioPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioPeticionarioPatenteServicios"]);

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarCambioPeticionarioPatente,
                Recursos.Ids.GestionarCambioPeticionario);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCambioPeticionarioPatente,
                Recursos.Ids.GestionarCambioPeticionario);
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
                    this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                    this._ventana.ConvertirEnteroMinimoABlanco("Actual");

                    CambioPeticionarioPatente cesion = (CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente;

                    this._ventana.Patente = this._patenteServicios.ConsultarPatenteConTodo(cesion.Patente);

                    this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;

                    this._ventana.ApoderadoAnterior = cesion.AgenteAnterior;
                    this._ventana.ApoderadoActual = cesion.AgenteActual;
                    this._ventana.PoderAnterior = cesion.PoderAnterior;
                    this._ventana.PoderActual = cesion.PoderActual;

                    CargarPatente();

                    CargarInteresado("Anterior");

                    CargarApoderado("Anterior");

                    CargarPoder("Anterior");

                    CargarInteresado("Actual");

                    CargarApoderado("Actual");

                    CargarPoder("Actual");

                    LlenarListasPoderes((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente);

                    ValidarAnterior();

                    ValidarActual();

                    CargaBoletines();

                }
                else
                {
                    CargarPatente();

                    CargarInteresado("Anterior");

                    CargarApoderado("Anterior");

                    CargarPoder("Anterior");

                    CargarInteresado("Actual");

                    CargarApoderado("Actual");

                    CargarPoder("Actual");

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
            CambioPeticionarioPatente cambioPeticionario = (CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente;
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;
            if (_agregar == false)
                this._ventana.Boletin = BuscarBoletin(boletines, cambioPeticionario.BoletinPublicacion);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        private void CargarInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Anterior"))
            {
                this._interesadosAnterior = new List<Interesado>();

                this._interesadosAnterior.Add(primerInteresado);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).InteresadoAnterior != null)
                {
                    this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).InteresadoAnterior);
                    this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                    this._ventana.IdAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoAnterior != null)
                    {
                        this._interesadosAnterior.Add((Interesado)this._ventana.InteresadoAnterior);
                        this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                        this._ventana.AnteriorFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.AnteriorsFiltrados, (Interesado)this._ventana.InteresadoAnterior);                        
                    }
                }
                else
                {
                    this._ventana.InteresadoAnterior = primerInteresado;                    
                    this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                    this._ventana.AnteriorFiltrado = primerInteresado;

                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._interesadosActual = new List<Interesado>();
                this._interesadosActual.Add(primerInteresado);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).InteresadoActual != null)
                {
                    this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).InteresadoActual);
                    this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                    this._ventana.IdActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoActual != null)
                    {
                        this._interesadosActual.Add((Interesado)this._ventana.InteresadoActual);                        
                        this._ventana.ActualsFiltrados = this._interesadosActual;
                        this._ventana.ActualFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.ActualsFiltrados, (Interesado)this._ventana.InteresadoActual);
                    }
                }
                else
                {
                    this._ventana.InteresadoActual = primerInteresado;                    
                    this._ventana.ActualsFiltrados = this._interesadosActual;
                    this._ventana.ActualFiltrado = primerInteresado;
                }   
            }

            this._ventana.ConvertirEnteroMinimoABlanco();
        }

        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Anterior"))
            {
                this._agentesAnterior = new List<Agente>();                
                this._agentesAnterior.Add(primerAgente);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).AgenteAnterior != null)
                {
                    this._agentesAnterior.Add((Agente)this._ventana.ApoderadoAnterior);
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosAnteriorFiltrados, (Agente)this._ventana.ApoderadoAnterior);
                    this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnterior).Id.ToString();
                }
                else
                {
                    this._ventana.ApoderadoAnterior = primerAgente;                    
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._agentesActual = new List<Agente>();
                this._agentesActual.Add(primerAgente);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).AgenteActual != null)
                {
                    this._agentesActual.Add((Agente)this._ventana.ApoderadoActual);
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosActualFiltrados, (Agente)this._ventana.ApoderadoActual);
                    this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActual).Id.ToString();
                }
                else
                {
                    this._ventana.ApoderadoActual = primerAgente;                    
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
                }       
            }

            this._ventana.ConvertirEnteroMinimoABlanco();
        }

        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Anterior"))
            {
                this._poderesAnterior = new List<Poder>();                
                this._poderesAnterior.Add(primerPoder);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).PoderAnterior != null)
                {
                    this._poderesAnterior.Add((Poder)this._ventana.PoderAnterior);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesAnteriorFiltrados, (Poder)this._ventana.PoderAnterior);
                }
                else
                {                    
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = this.BuscarPoder(this._poderesAnterior, this._poderesAnterior[0]);
                    this._ventana.ConvertirEnteroMinimoABlanco("Anterior");                                                          
                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._poderesActual = new List<Poder>();
                this._poderesActual.Add(primerPoder);

                if (((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente).PoderActual != null)
                {
                    this._poderesActual.Add((Poder)this._ventana.PoderActual);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesActualFiltrados, (Poder)this._ventana.PoderActual);
                }
                else
                {
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = this.BuscarPoder(this._poderesActual, this._poderesActual[0]);
                    this._ventana.ConvertirEnteroMinimoABlanco("Actual");  
                }     
            }
        }
      
        public CambioPeticionarioPatente CargarCambioPeticionarioDeLaPantalla()
        {

            CambioPeticionarioPatente cambioPeticionario = (CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente;

            if (null != this._ventana.Patente)
            {
                cambioPeticionario.Patente = ((Patente) this._ventana.Patente).Id != int.MinValue
                                                 ? (Patente) this._ventana.Patente : null;
                cambioPeticionario.InteresadoAnterior = ((Patente)this._ventana.Patente).Interesado;
                cambioPeticionario.AgenteAnterior = ((Patente)this._ventana.Patente).Agente;
                cambioPeticionario.PoderAnterior = ((Patente)this._ventana.Patente).Poder;

            }

            if (null != this._ventana.InteresadoAnterior)
                cambioPeticionario.InteresadoAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id != int.MinValue ? 
                                                                        (Interesado)this._ventana.InteresadoAnterior : null;

            if (null != this._ventana.InteresadoActual)
                cambioPeticionario.InteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id != int.MinValue ?
                                                                    (Interesado)this._ventana.InteresadoActual : null;

            if (null != this._ventana.ApoderadoActual)
                cambioPeticionario.AgenteActual = !((Agente)this._ventana.ApoderadoActual).Id.Equals("") ?
                                                                (Agente)this._ventana.ApoderadoActual : null;

            if (null != this._ventana.ApoderadoAnterior)
                cambioPeticionario.AgenteAnterior = !((Agente)this._ventana.ApoderadoAnterior).Id.Equals("") ? 
                                                                    (Agente)this._ventana.ApoderadoAnterior : null;

            if (null != this._ventana.PoderActual)
                cambioPeticionario.PoderActual = ((Poder)this._ventana.PoderActual).Id != int.MinValue ?
                                                                    (Poder)this._ventana.PoderActual : null;

            if (null != this._ventana.PoderAnterior)
                cambioPeticionario.PoderAnterior = ((Poder)this._ventana.PoderAnterior).Id != int.MinValue ? 
                                                                    (Poder)this._ventana.PoderAnterior : null;

            if (null != this._ventana.Boletin)
                cambioPeticionario.BoletinPublicacion = ((Boletin)this._ventana.Boletin).Id != int.MinValue ? 
                                                                        (Boletin)this._ventana.Boletin : null;     
 

            return cambioPeticionario;
        }

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

                //Modifica o inserta los datos del Cambio Peticionario
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    CambioPeticionarioPatente cambioPeticionario = CargarCambioPeticionarioDeLaPantalla();

                    bool exitoso = this._cambioPeticionarioPatenteServicios.InsertarOModificar(cambioPeticionario, UsuarioLogeado.Hash);

                    if ((exitoso) && (this._agregar == false))
                        this.Navegar(new GestionarCambioPeticionarioPatentes(cambioPeticionario));
                    else if ((exitoso) && (this._agregar == true))
                        this.Navegar(new GestionarCambioPeticionarioPatentes(cambioPeticionario));
                    else
                        this.Navegar(Recursos.MensajesConElUsuario.ErrorAlGenerarTraspaso, true);
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

                if (this._cambioPeticionarioPatenteServicios.Eliminar((CambioPeticionarioPatente)this._ventana.CambioPeticionarioPatente, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.CambioPeticionarioEliminado;
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

        public void LlenarListasPoderes(CambioPeticionarioPatente cesion)
        {

            if (cesion.InteresadoAnterior != null)
                this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(cesion.InteresadoAnterior);

            if (cesion.InteresadoActual != null)
                this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(cesion.InteresadoActual);

            if (cesion.AgenteAnterior != null)
                this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteAnterior);

            if (cesion.AgenteActual != null)
                this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteActual);
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {

            bool retorno = false;
            IList<Poder> listaIntereseccionAnterior = new List<Poder>();
            IList<Poder> listaIntereseccionActual = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderActual = new Poder();

            listaIntereseccionAnterior.Add(primerPoder);
            listaIntereseccionActual.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Anterior")))
                        {
                            listaIntereseccionAnterior.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Actual")))
                        {
                            listaIntereseccionActual.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionAnterior.Count != 0) && (tipo.Equals("Anterior")))
                {
                    poderActual = (Poder)this._ventana.PoderAnteriorFiltrado;
                    this._poderesInterseccionAnterior = listaIntereseccionAnterior;
                    this._ventana.PoderesAnteriorFiltrados = listaIntereseccionAnterior;
                    this._ventana.PoderAnteriorFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesAnteriorFiltrados, poderActual);
                }


                else if ((listaIntereseccionActual.Count != 0) && (tipo.Equals("Actual")))
                {
                    poderActual = (Poder)this._ventana.PoderActualFiltrado;
                    this._poderesInterseccionActual = listaIntereseccionActual;
                    this._ventana.PoderesActualFiltrados = listaIntereseccionActual;
                    this._ventana.PoderActualFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesActualFiltrados, poderActual);
                }

                else
                    retorno = false;
            }

            return retorno;
        }

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

                Agente agenteActual = new Agente();

                agentesInteresadoFiltrados = new List<Agente>();

                if (tipo.Equals("Anterior"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderAnteriorFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderAnteriorFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderAnteriorFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.AnteriorsFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.AnteriorFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.AnteriorFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.AnteriorFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoAnteriorFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosAnteriorFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoAnteriorFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                    }
                }
                else if (tipo.Equals("Actual"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderActualFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderActualFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderActualFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.ActualsFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.ActualFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.ActualFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ActualFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoActualFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosActualFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoActualFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoActualFiltrado = primerAgente;
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

        private void LlenarListaAgente(Poder poder, string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Anterior"))
            {
                this._agentesAnterior = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesAnterior.Insert(0, primerAgente);
                this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
            }
            else if (tipo.Equals("Actual"))
            {
                this._agentesActual = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesActual.Insert(0, primerAgente);
                this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                this._ventana.ApoderadoActualFiltrado = primerAgente;
            }
        }

        public bool VerificarCambioInteresado(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Anterior"))
            {
                if ((((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")))
                    retorno = true;
            }
            if (tipo.Equals("Actual"))
            {
                if ((((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals("")))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioAgente(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Anterior"))
            {
                if (!(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")) || (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue))
                    retorno = true;
            }
            if (tipo.Equals("Actual"))
            {
                if (!(((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals("")) || (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue))
                    retorno = true;
            }

            return retorno;
        }

        public bool VerificarCambioPoder(string tipo)
        {
            if (tipo.Equals("Anterior"))
            {
                if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                    return true;
            }
            if (tipo.Equals("Actual"))
            {
                if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                    return true;
            }

            return false;
        }

        public void LimpiarListaInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.AnteriorsFiltrados = listaInteresados;
                this._ventana.AnteriorFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.ActualsFiltrados = listaInteresados;
                this._ventana.ActualFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
            }
        }

        public void LimpiarListaAgente(string tipo)
        {
            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.ApoderadosAnteriorFiltrados = listaAgentes;
                this._ventana.ApoderadoAnteriorFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.ApoderadosActualFiltrados = listaAgentes;
                this._ventana.ApoderadoActualFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
            }
        }

        public void LimpiarListaPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            if (tipo.Equals("Anterior"))
            {
                this._ventana.PoderesAnteriorFiltrados = listaPoderes;
                this._ventana.PoderAnteriorFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
            }
            else if (tipo.Equals("Actual"))
            {
                this._ventana.PoderesActualFiltrados = listaPoderes;
                this._ventana.PoderActualFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
            }
        }             

        #region Patente

        //public void IrConsultarPatentes()
        //{
        //    this.Navegar(new ConsultarPatentes());
        //}

        private void CargarPatente()
        {
            this._patentes = new List<Patente>();
            Patente primeraPatente = new Patente(int.MinValue);
            this._patentes.Add(primeraPatente);

            if ((Patente)this._ventana.Patente != null)
            {
                this._patentes.Add((Patente)this._ventana.Patente);
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = (Patente)this._ventana.Patente;
                this._ventana.IdPatente = ((Patente)this._ventana.Patente).Id.ToString();
            }
            else
            {
                this._ventana.PatentesFiltradas = this._patentes;
                this._ventana.PatenteFiltrada = primeraPatente;
            }

            this._ventana.ConvertirEnteroMinimoABlanco();
        }

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
                    this._ventana.IdPatente = ((Patente)this._ventana.PatenteFiltrada).Id.ToString();
                    this._ventana.InteresadoAnterior = ((Patente)this._ventana.Patente).Interesado;
                    this._ventana.ApoderadoAnterior = ((Patente)this._ventana.Patente).Agente;
                    this._ventana.PoderAnterior = ((Patente)this._ventana.Patente).Poder;
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

        #region Anterior

        private void ValidarAnterior()
        {
            if (((Interesado)this._ventana.AnteriorFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                    
                    else
                    {                     
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);
                       
                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior");                        

                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnterior, "Anterior", true);
                        ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior");   

                        this._ventana.GestionarBotonConsultarInteresados("Anterior", false);
                        this._ventana.GestionarBotonConsultarApoderados("Anterior", false);
                        this._ventana.GestionarBotonConsultarPoderes("Anterior", false);
                    }
                }
            }
        }

        public void ConsultarAnteriors()
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
                interesado.Nombre = this._ventana.NombreAnteriorFiltrar.ToUpper();
                interesado.Id = this._ventana.IdAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdAnteriorFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.AnteriorsFiltrados = interesadosFiltrados;
                    this._ventana.AnteriorFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.AnteriorsFiltrados = this._interesadosAnterior;
                    this._ventana.AnteriorFiltrado = primerInteresado;
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

        public void ConsultarApoderadosAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");

                
                Agente apoderadoAnterior = new Agente();
                IList<Agente> agentesAnteriorFiltrados;
                apoderadoAnterior.Nombre = this._ventana.NombreApoderadoAnteriorFiltrar.ToUpper();
                apoderadoAnterior.Id = this._ventana.IdApoderadoAnteriorFiltrar.ToUpper();

                if ((!apoderadoAnterior.Nombre.Equals("")) || (!apoderadoAnterior.Id.Equals("")))
                    agentesAnteriorFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoAnterior);
                else
                    agentesAnteriorFiltrados = new List<Agente>();

                if (agentesAnteriorFiltrados.Count != 0)
                {
                    agentesAnteriorFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
                    this._ventana.ApoderadosAnteriorFiltrados = agentesAnteriorFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesAnteriorFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosAnteriorFiltrados = this._agentesAnterior;
                    this._ventana.ApoderadoAnteriorFiltrado = primerAgente;
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

        public void ConsultarPoderesAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                
                Poder poderAnterior = new Poder();
                IList<Poder> poderesAnteriorFiltrados;

                if (!this._ventana.IdPoderAnteriorFiltrar.Equals(""))
                    poderAnterior.Id = int.Parse(this._ventana.IdPoderAnteriorFiltrar);
                else
                    poderAnterior.Id = 0;

                if (!this._ventana.FechaPoderAnteriorFiltrar.Equals(""))
                    poderAnterior.Fecha = DateTime.Parse(this._ventana.FechaPoderAnteriorFiltrar);

                if ((!poderAnterior.Fecha.Equals("")) || (poderAnterior.Id != 0))
                    poderesAnteriorFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderAnterior);
                else
                    poderesAnteriorFiltrados = new List<Poder>();

                if (poderesAnteriorFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesAnteriorFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = primerPoder;
                    this._ventana.PoderesAnteriorFiltrados = poderesAnteriorFiltrados;
                }
                else
                {
                    poderesAnteriorFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                    this._ventana.PoderAnteriorFiltrado = primerPoder;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }


                Mouse.OverrideCursor = null;

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

        public bool CambiarAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.AnteriorFiltrado);
                            this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                            this._ventana.IdAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));

                            LimpiarListaPoder("Anterior");

                            if ((this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior")))
                            {
                                this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
                                this._ventana.NombreAnterior = ((Interesado)this._ventana.AnteriorFiltrado).Nombre;
                                this._ventana.IdAnterior = ((Interesado)this._ventana.AnteriorFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesAnterior, _poderesApoderadosAnterior, "Anterior"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Anterior"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));
                            this._poderesAnterior.Insert(0, primerPoder);
                            this._ventana.PoderesAnteriorFiltrados = this._poderesAnterior;
                            this._ventana.PoderAnteriorFiltrado = primerPoder;

                            this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));
                            this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.AnteriorFiltrado);
                            this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                            this._ventana.IdAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();
                            retorno = true;
                        }
                        else
                        {

                            this._poderesAnterior = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.AnteriorFiltrado));
                            this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.AnteriorFiltrado);
                            this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                            this._ventana.IdAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoAnterior = this._ventana.AnteriorFiltrado;
                    this._ventana.NombreAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                    this._ventana.IdAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();
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

        public bool CambiarApoderadoAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                            this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                            this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoAnteriorFiltrado));

                            LimpiarListaPoder("Anterior");

                            if ((this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior")))
                            {
                                this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                                this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                                this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesAnterior, this._poderesApoderadosAnterior, "Anterior"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Anterior"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                            this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                            this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosAnterior = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoAnteriorFiltrado));
                            this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                            this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                            this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoAnterior = this._ventana.ApoderadoAnteriorFiltrado;
                    this._ventana.NombreApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Nombre;
                    this._ventana.IdApoderadoAnterior = ((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.ToString();
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

        public bool CambiarPoderAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderAnteriorFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoAnteriorFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.AnteriorFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Anterior");

                            LlenarListaAgente((Poder)this._ventana.PoderAnteriorFiltrado, "Anterior");

                            this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                            this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Anterior");

                            LimpiarListaAgente("Anterior");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderAnteriorFiltrado, "Anterior", false);

                            this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                            this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                        this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderAnterior = this._ventana.PoderAnteriorFiltrado;
                    this._ventana.IdPoderAnterior = ((Poder)this._ventana.PoderAnteriorFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Anterior");

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

        #region Actual

        private void ValidarActual()
        {
            if (((Interesado)this._ventana.ActualFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);                        
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)               
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                  
                    else
                    {
                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                      
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)                                           
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    
                    else
                    {                       
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                        
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)
                    {
                     
                        ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual");

                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }
                    else
                    {                        
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActual, "Actual", true);
                        ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual");
                       
                        this._ventana.GestionarBotonConsultarInteresados("Actual", false);
                        this._ventana.GestionarBotonConsultarApoderados("Actual", false);
                        this._ventana.GestionarBotonConsultarPoderes("Actual", false);
                    }
                }
            }
        }

        public void ConsultarActuals()
        { 
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);

               
                Interesado interesadoActual = new Interesado();
                IList<Interesado> interesadoActualsFiltrados;
                interesadoActual.Nombre = this._ventana.NombreActualFiltrar.ToUpper();
                interesadoActual.Id = this._ventana.IdActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdActualFiltrar);

                if ((!interesadoActual.Nombre.Equals("")) || (interesadoActual.Id != 0))
                    interesadoActualsFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoActual);
                else
                    interesadoActualsFiltrados = new List<Interesado>();

                if (interesadoActualsFiltrados.ToList<Interesado>().Count != 0)
                {
                    interesadoActualsFiltrados.Insert(0, primerInteresado);
                    this._ventana.ActualsFiltrados = interesadoActualsFiltrados.ToList<Interesado>();
                    this._ventana.ActualFiltrado = primerInteresado;
                }
                else
                {
                    interesadoActualsFiltrados.Insert(0, primerInteresado);
                    this._ventana.ActualsFiltrados = this._interesadosActual;
                    this._ventana.ActualFiltrado = primerInteresado;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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

        public void ConsultarApoderadosActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");

                
                Agente apoderadoActual = new Agente();
                IList<Agente> agentesActualFiltrados;
                apoderadoActual.Nombre = this._ventana.NombreApoderadoActualFiltrar.ToUpper();
                apoderadoActual.Id = this._ventana.IdApoderadoActualFiltrar.ToUpper();

                if ((!apoderadoActual.Nombre.Equals("")) || (!apoderadoActual.Id.Equals("")))
                    agentesActualFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoActual);
                else
                    agentesActualFiltrados = new List<Agente>();

                if (agentesActualFiltrados.Count != 0)
                {
                    agentesActualFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
                    this._ventana.ApoderadosActualFiltrados = agentesActualFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesActualFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosActualFiltrados = this._agentesActual;
                    this._ventana.ApoderadoActualFiltrado = primerAgente;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }

                Mouse.OverrideCursor = null;

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

        public void ConsultarPoderesActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                
                Poder poderActual = new Poder();
                IList<Poder> poderesActualFiltrados;

                if (!this._ventana.IdPoderActualFiltrar.Equals(""))
                    poderActual.Id = int.Parse(this._ventana.IdPoderActualFiltrar);
                else
                    poderActual.Id = 0;

                if (!this._ventana.FechaPoderActualFiltrar.Equals(""))
                    poderActual.Fecha = DateTime.Parse(this._ventana.FechaPoderActualFiltrar);

                if ((!poderActual.Fecha.Equals("")) || (poderActual.Id != 0))
                    poderesActualFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderActual);
                else
                    poderesActualFiltrados = new List<Poder>();

                if (poderesActualFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesActualFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = primerPoder;
                    this._ventana.PoderesActualFiltrados = poderesActualFiltrados;
                }
                else
                {
                    poderesActualFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesActualFiltrados = this._poderesActual;
                    this._ventana.PoderActualFiltrado = primerPoder;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }


                Mouse.OverrideCursor = null;

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

        public bool CambiarActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.ActualFiltrado);
                            this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));

                            LimpiarListaPoder("Actual");

                            if ((this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual")))
                            {
                                this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
                                this._ventana.NombreActual = ((Interesado)this._ventana.ActualFiltrado).Nombre;
                                this._ventana.IdActual = ((Interesado)this._ventana.ActualFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesActual, _poderesApoderadosActual, "Actual"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Actual");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Actual"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));
                            this._poderesActual.Insert(0, primerPoder);
                            this._ventana.PoderesActualFiltrados = this._poderesActual;
                            this._ventana.PoderActualFiltrado = primerPoder;

                            this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.ActualFiltrado);
                            this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                        else
                        {

                            this._poderesActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.ActualFiltrado));
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.ActualFiltrado);
                            this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoActual = this._ventana.ActualFiltrado;
                    this._ventana.NombreActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                    this._ventana.IdActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
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

        public bool CambiarApoderadoActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                            this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                            this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoActualFiltrado));

                            LimpiarListaPoder("Actual");

                            if ((this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual")))
                            {
                                this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                                this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                                this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesActual, this._poderesApoderadosActual, "Actual"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Actual");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Actual"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                            this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                            this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosActual = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoActualFiltrado));
                            this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                            this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                            this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoActual = this._ventana.ApoderadoActualFiltrado;
                    this._ventana.NombreApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Nombre;
                    this._ventana.IdApoderadoActual = ((Agente)this._ventana.ApoderadoActualFiltrado).Id.ToString();
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

        public bool CambiarPoderActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderActualFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoActualFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.ActualFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Actual");

                            LlenarListaAgente((Poder)this._ventana.PoderActualFiltrado, "Actual");

                            this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                            this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Actual");

                            LimpiarListaAgente("Actual");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderActualFiltrado, "Actual", false);

                            this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                            this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                        this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderActual = this._ventana.PoderActualFiltrado;
                    this._ventana.IdPoderActual = ((Poder)this._ventana.PoderActualFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Actual");

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

    }
}