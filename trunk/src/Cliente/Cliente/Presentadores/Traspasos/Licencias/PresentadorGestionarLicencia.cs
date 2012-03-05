﻿using System;
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
using Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.Licencias
{
    class PresentadorGestionarLicencia : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        private IGestionarLicencia _ventana;

        private IMarcaServicios _marcaServicios;
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
        private ILicenciaServicios _licenciaServicios;


        private IList<Interesado> _interesadosLicenciante;
        private IList<Interesado> _interesadosLicenciatario;
        private IList<Agente> _agentesLicenciatario;
        private IList<Agente> _agentesLicenciante;
        private IList<Marca> _marcas;

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
        public PresentadorGestionarLicencia(IGestionarLicencia ventana, object licencia)
        {
            try
            {

                this._ventana = ventana;               

                if (licencia != null)
                {
                    this._ventana.Licencia = licencia;
                    _agregar = false;
                }
                else
                {
                    Licencia licenciaAgregar = new Licencia();
                    this._ventana.Licencia = licenciaAgregar;

                    ((Licencia)this._ventana.Licencia).Fecha = DateTime.Now;
                    this._ventana.Marca = null;
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

                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
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
                this._licenciaServicios = (ILicenciaServicios)Activator.GetObject(typeof(ILicenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["LicenciaServicios"]);


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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarLicencia,
                Recursos.Ids.GestionarCambioPeticionario);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarLicencia,
                Recursos.Ids.GestionarLicencias);
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

                    this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
                    this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");

                    Licencia licencia = (Licencia)this._ventana.Licencia;


                    this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(licencia.Marca);

                    this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;

                    this._ventana.ApoderadoLicenciante = licencia.AgenteLicenciante;
                    this._ventana.ApoderadoLicenciatario = licencia.AgenteLicenciatario;
                    this._ventana.PoderLicenciante = licencia.PoderLicenciante;
                    this._ventana.PoderLicenciatario = licencia.PoderLicenciatario;


                    CargaBoletines();

                    this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, licencia.Boletin);

                    CargarMarca();

                    CargarInteresado("Licenciante");

                    CargarApoderado("Licenciante");

                    CargarPoder("Licenciante");

                    CargarInteresado("Licenciatario");

                    CargarApoderado("Licenciatario");

                    CargarPoder("Licenciatario");

                    LlenarListasPoderes((Licencia)this._ventana.Licencia);

                    ValidarLicenciante();

                    ValidarLicenciatario();

                    this._ventana.Boletin = licencia.Boletin;

                }
                else
                {
                    CargarMarca();

                    CargarInteresado("Licenciante");

                    CargarApoderado("Licenciante");

                    CargarPoder("Licenciante");

                    CargarInteresado("Licenciatario");

                    CargarApoderado("Licenciatario");

                    CargarPoder("Licenciatario");

                    CargaBoletines();
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void CargaBoletines()
        {
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;

        }

        private void CargarInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Licenciante"))
            {
                this._interesadosLicenciante = new List<Interesado>();

                this._interesadosLicenciante.Add(primerInteresado);

                if (((Licencia)this._ventana.Licencia).InteresadoLicenciante != null)
                {
                    this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo(((Licencia)this._ventana.Licencia).InteresadoLicenciante);
                    this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;

                    if ((Interesado)this._ventana.InteresadoLicenciante != null)
                    {
                        this._interesadosLicenciante.Add((Interesado)this._ventana.InteresadoLicenciante);
                        this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                        this._ventana.LicencianteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciantesFiltrados, (Interesado)this._ventana.InteresadoLicenciante);
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciante = primerInteresado;
                    this._ventana.LicenciantesFiltrados = this._interesadosLicenciante;
                    this._ventana.LicencianteFiltrado = primerInteresado;

                }
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._interesadosLicenciatario = new List<Interesado>();
                this._interesadosLicenciatario.Add(primerInteresado);

                if (((Licencia)this._ventana.Licencia).InteresadoLicenciatario != null)
                {
                    this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo(((Licencia)this._ventana.Licencia).InteresadoLicenciatario);
                    this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;

                    if ((Interesado)this._ventana.InteresadoLicenciatario != null)
                    {
                        this._interesadosLicenciatario.Add((Interesado)this._ventana.InteresadoLicenciatario);
                        this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                        this._ventana.LicenciatarioFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.LicenciatariosFiltrados, (Interesado)this._ventana.InteresadoLicenciatario);
                    }
                }
                else
                {
                    this._ventana.InteresadoLicenciatario = primerInteresado;
                    this._ventana.LicenciatariosFiltrados = this._interesadosLicenciatario;
                    this._ventana.LicenciatarioFiltrado = primerInteresado;
                }
            }
        }

        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Licenciante"))
            {
                this._agentesLicenciante = new List<Agente>();
                this._agentesLicenciante.Add(primerAgente);

                if (((Licencia)this._ventana.Licencia).AgenteLicenciante != null)
                {
                    this._agentesLicenciante.Add((Agente)this._ventana.ApoderadoLicenciante);
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosLicencianteFiltrados, (Agente)this._ventana.ApoderadoLicenciante);
                }
                else
                {
                    this._ventana.ApoderadoLicenciante = primerAgente;
                    this._ventana.ApoderadosLicencianteFiltrados = this._agentesLicenciante;
                    this._ventana.ApoderadoLicencianteFiltrado = primerAgente;
                }
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._agentesLicenciatario = new List<Agente>();
                this._agentesLicenciatario.Add(primerAgente);

                if (((Licencia)this._ventana.Licencia).AgenteLicenciatario != null)
                {
                    this._agentesLicenciatario.Add((Agente)this._ventana.ApoderadoLicenciatario);
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosLicenciatarioFiltrados, (Agente)this._ventana.ApoderadoLicenciatario);
                }
                else
                {
                    this._ventana.ApoderadoLicenciatario = primerAgente;
                    this._ventana.ApoderadosLicenciatarioFiltrados = this._agentesLicenciatario;
                    this._ventana.ApoderadoLicenciatarioFiltrado = primerAgente;
                }
            }
        }

        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Licenciante"))
            {
                this._poderesLicenciante = new List<Poder>();
                this._poderesLicenciante.Add(primerPoder);

                if (((Licencia)this._ventana.Licencia).PoderLicenciante != null)
                {
                    this._poderesLicenciante.Add((Poder)this._ventana.PoderLicenciante);
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesLicencianteFiltrados, (Poder)this._ventana.PoderLicenciante);
                }
                else
                {                    
                    this._ventana.PoderesLicencianteFiltrados = this._poderesLicenciante;
                    this._ventana.PoderLicencianteFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._poderesLicenciatario = new List<Poder>();
                this._poderesLicenciatario.Add(primerPoder);

                if (((Licencia)this._ventana.Licencia).PoderLicenciatario != null)
                {
                    this._poderesLicenciatario.Add((Poder)this._ventana.PoderLicenciatario);
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesLicenciatarioFiltrados, (Poder)this._ventana.PoderLicenciatario);
                }
                else
                {                    
                    this._ventana.PoderesLicenciatarioFiltrados = this._poderesLicenciatario;
                    this._ventana.PoderLicenciatarioFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");                    
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");
            }            
        }

        public Licencia CargarLicenciaDeLaPantalla()
        {

            Licencia licencia = (Licencia)this._ventana.Licencia;            

            if (null != this._ventana.MarcaFiltrada)
                licencia.Marca = ((Marca)this._ventana.MarcaFiltrada).Id != int.MinValue ? (Marca)this._ventana.MarcaFiltrada : null;

            if (null != this._ventana.InteresadoLicenciante)
                licencia.InteresadoLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Id != int.MinValue ? (Interesado)this._ventana.InteresadoLicenciante : null;

            if (null != this._ventana.InteresadoLicenciatario)
                licencia.InteresadoLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Id != int.MinValue ? (Interesado)this._ventana.InteresadoLicenciatario : null;

            if (null != this._ventana.ApoderadoLicencianteFiltrado)
                licencia.AgenteLicenciante = !((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals("") ? (Agente)this._ventana.ApoderadoLicencianteFiltrado : null;

            if (null != this._ventana.ApoderadoLicenciatarioFiltrado)
                licencia.AgenteLicenciatario = !((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals("") ? (Agente)this._ventana.ApoderadoLicenciatarioFiltrado : null;

            if (null != this._ventana.PoderLicencianteFiltrado)
                licencia.PoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue ? (Poder)this._ventana.PoderLicencianteFiltrado : null;

            if (null != this._ventana.PoderLicenciatarioFiltrado)
                licencia.PoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue ? (Poder)this._ventana.PoderLicenciatarioFiltrado : null;

            if (null != this._ventana.Boletin)
                licencia.Boletin = ((Boletin)this._ventana.Boletin).Id != int.MinValue ? (Boletin)this._ventana.Boletin : null;
        


            return licencia;
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

                //Modifica los datos del Pais
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    Licencia licencia = CargarLicenciaDeLaPantalla();

                    bool exitoso = this._licenciaServicios.InsertarOModificar(licencia, UsuarioLogeado.Hash);

                    if ((exitoso) && (this._agregar == false))
                        this.Navegar(Recursos.MensajesConElUsuario.LicenciaModificada, false);
                    else if ((exitoso) && (this._agregar == true))
                        this.Navegar(Recursos.MensajesConElUsuario.LicenciaInsertada, false);
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
        /// Metodo que se encarga de eliminar una Marca
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

                if (this._licenciaServicios.Eliminar((Licencia)this._ventana.Licencia, UsuarioLogeado.Hash))
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

        public void LlenarListasPoderes(Licencia licencia)
        {

            if (licencia.InteresadoLicenciante != null)
                this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciante);

            if (licencia.InteresadoLicenciatario != null)
                this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(licencia.InteresadoLicenciatario);

            if (licencia.AgenteLicenciante != null)
                this._poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciante);

            if (licencia.AgenteLicenciatario != null)
                this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(licencia.AgenteLicenciatario);
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
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

        private void LlenarListaAgente(Poder poder, string tipo)
        {
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
        }

        public bool VerificarCambioInteresado(string tipo)
        {
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

            return retorno;
        }

        public bool VerificarCambioAgente(string tipo)
        {
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

            return retorno;
        }

        public bool VerificarCambioPoder(string tipo)
        {
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

            return false;
        }

        public void LimpiarListaInteresado(string tipo)
        {
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
        }

        public void LimpiarListaAgente(string tipo)
        {
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
        }

        public void LimpiarListaPoder(string tipo)
        {
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
        }    

        #region Marca

        public void IrConsultarMarcas()
        {
            this.Navegar(new ConsultarMarcas());
        }

        private void CargarMarca()
        {
            this._marcas = new List<Marca>();
            Marca primeraMarca = new Marca(int.MinValue);
            this._marcas.Add(primeraMarca);

            if ((Marca)this._ventana.Marca != null)
            {
                this._marcas.Add((Marca)this._ventana.Marca);
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = (Marca)this._ventana.Marca;
            }
            else
            {
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.MarcaFiltrada = primeraMarca;
            }
        }

        public void ConsultarMarcas()
        { 
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Marca primeraMarca = new Marca(int.MinValue);

               
                Marca marca = new Marca();
                IList<Marca> marcasFiltradas;
                marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
                marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);

                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                else
                    marcasFiltradas = new List<Marca>();

                if (marcasFiltradas.ToList<Marca>().Count != 0)
                {
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                    this._ventana.MarcaFiltrada = primeraMarca;
                }
                else
                {
                    marcasFiltradas.Insert(0, primeraMarca);
                    this._ventana.MarcasFiltradas = this._marcas;
                    this._ventana.MarcaFiltrada = primeraMarca;
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

        public bool CambiarMarca()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.MarcaFiltrada != null)
                {
                    this._ventana.Marca = this._ventana.MarcaFiltrada;
                    this._ventana.NombreMarca = ((Marca)this._ventana.MarcaFiltrada).Descripcion;
                    retorno = true;
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

            return retorno;
        }

        #endregion

        #region Licenciante

        private void ValidarLicenciante()
        {
            if (((Interesado)this._ventana.LicencianteFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante,"Licenciante", true);

                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                    }
                }
                else
                {
                    if (((Licencia)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);

                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoLicencianteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);

                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicencianteFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante");

                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciante, "Licenciante", true);
                        ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante");

                        this._ventana.GestionarBotonConsultarInteresados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciante", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciante", false);
                    }
                }
            }
        }

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
                    agentesLicencianteFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoLicenciante);
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
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciante, _poderesApoderadosLicenciante, "Licenciante"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
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
                            retorno = true;
                        }
                        else
                        {

                            this._poderesLicenciante = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicencianteFiltrado));
                            this._ventana.InteresadoLicenciante = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicencianteFiltrado);
                            this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoLicenciante = this._ventana.LicencianteFiltrado;
                    this._ventana.NombreLicenciante = ((Interesado)this._ventana.InteresadoLicenciante).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");

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

        public bool CambiarApoderadoLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

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
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicencianteFiltrado));

                            LimpiarListaPoder("Licenciante");

                            if ((this.ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante")))
                            {
                                this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                                this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciante, this._poderesApoderadosLicenciante, "Licenciante"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciante"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderLicencianteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosLicenciante = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicencianteFiltrado));
                            this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                            this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoLicenciante = this._ventana.ApoderadoLicencianteFiltrado;
                    this._ventana.NombreApoderadoLicenciante = ((Agente)this._ventana.ApoderadoLicencianteFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");

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

        public bool CambiarPoderLicenciante()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

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
                        this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                        this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderLicenciante = this._ventana.PoderLicencianteFiltrado;
                    this._ventana.IdPoderLicenciante = ((Poder)this._ventana.PoderLicencianteFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciante");

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

        private void ValidarLicenciatario()
        {
            if (((Interesado)this._ventana.LicenciatarioFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);

                    else
                    {

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);

                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);

                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id == int.MinValue)
                    {

                        ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario");

                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderLicenciatario, "Licenciatario", true);
                        ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario");

                        this._ventana.GestionarBotonConsultarInteresados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Licenciatario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Licenciatario", false);
                    }
                }
            }
        }

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
                    agentesLicenciatarioFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoLicenciatario);
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
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciatario, _poderesApoderadosLicenciatario, "Licenciatario"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");
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
                            retorno = true;
                        }
                        else
                        {

                            this._poderesLicenciatario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.LicenciatarioFiltrado));
                            this._ventana.InteresadoLicenciatario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.LicenciatarioFiltrado);
                            this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoLicenciatario = this._ventana.LicenciatarioFiltrado;
                    this._ventana.NombreLicenciatario = ((Interesado)this._ventana.InteresadoLicenciatario).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");

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

        public bool CambiarApoderadoLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

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
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicenciatarioFiltrado));

                            LimpiarListaPoder("Licenciatario");

                            if ((this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario")))
                            {
                                this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                                this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesLicenciatario, this._poderesApoderadosLicenciatario, "Licenciatario"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Licenciatario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderLicenciatarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosLicenciatario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoLicenciatarioFiltrado));
                            this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                            this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoLicenciatario = this._ventana.ApoderadoLicenciatarioFiltrado;
                    this._ventana.NombreApoderadoLicenciatario = ((Agente)this._ventana.ApoderadoLicenciatarioFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");

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

        public bool CambiarPoderLicenciatario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

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
                        this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                        this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderLicenciatario = this._ventana.PoderLicenciatarioFiltrado;
                    this._ventana.IdPoderLicenciatario = ((Poder)this._ventana.PoderLicenciatarioFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Licenciatario");

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