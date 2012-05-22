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
using Trascend.Bolet.Cliente.Contratos.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.Traspasos.Cesiones
{
    class PresentadorGestionarCesion : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        private IGestionarCesion _ventana;

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
        private ICesionServicios _cesionServicios;

        private IList<Interesado> _interesadosCedente;
        private IList<Interesado> _interesadosCesionario;
        private IList<Agente> _agentesCesionario;
        private IList<Agente> _agentesCedente;
        private IList<Marca> _marcas;

        private IList<Poder> _poderesCedente;
        private IList<Poder> _poderesCesionario;

        private IList<Poder> _poderesApoderadosCedente;
        private IList<Poder> _poderesApoderadosCesionario;

        private IList<Poder> _poderesInterseccionCedente;
        private IList<Poder> _poderesInterseccionCesionario;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCesion(IGestionarCesion ventana, object cesion)
        {
            try
            {

                this._ventana = ventana;

                if (cesion != null)
                {
                    this._ventana.Cesion = cesion;
                    _agregar = false;
                }
                else
                {
                    Cesion cesionAgregar = new Cesion();
                    this._ventana.Cesion = cesionAgregar;

                    ((Cesion)this._ventana.Cesion).FechaCesion = DateTime.Now;
                    this._ventana.Marca = null;
                    this._ventana.PoderCedente = null;
                    this._ventana.PoderCesionario = null;
                    this._ventana.InteresadoCedente = null;
                    this._ventana.InteresadoCesionario = null;
                    this._ventana.ApoderadoCedente = null;
                    this._ventana.ApoderadoCesionario = null;
                    this._ventana.Boletin = null;

                    CambiarAModificar();

                    this._ventana.TextoBotonRegresar = Recursos.Etiquetas.btnCancelar;

                    this._ventana.ActivarControlesAlAgregar();
                }



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
                this._cesionServicios = (ICesionServicios)Activator.GetObject(typeof(ICesionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionServicios"]);

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarCesion,
                Recursos.Ids.GestionarCesion);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCesion,
                Recursos.Ids.GestionarCesion);
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

                    this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                    this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

                    Cesion cesion = (Cesion)this._ventana.Cesion;

                    this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(cesion.Marca);

                    this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;

                    this._ventana.ApoderadoCedente = cesion.AgenteCedente;
                    this._ventana.ApoderadoCesionario = cesion.AgenteCesionario;
                    this._ventana.PoderCedente = cesion.PoderCedente;
                    this._ventana.PoderCesionario = cesion.PoderCesionario;

                    CargaBoletines();

                    this._ventana.Boletin = this.BuscarBoletin((IList<Boletin>)this._ventana.Boletines, cesion.BoletinPublicacion);

                    CargarMarca();

                    CargarInteresado("Cedente");

                    CargarApoderado("Cedente");

                    CargarPoder("Cedente");

                    CargarInteresado("Cesionario");

                    CargarApoderado("Cesionario");

                    CargarPoder("Cesionario");

                    LlenarListasPoderes((Cesion)this._ventana.Cesion);

                    ValidarCedente();

                    ValidarCesionario();

                }
                else
                {
                    CargarMarca();

                    CargarInteresado("Cedente");

                    CargarInteresado("Cesionario");

                    CargarPoder("Cedente");

                    CargarPoder("Cesionario");

                    CargarApoderado("Cesionario");

                    CargarApoderado("Cedente");

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

        /// <summary>
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;
        }

        /// <summary>
        /// Método que carga el interesado
        /// </summary>
        private void CargarInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Cedente"))
            {
                this._interesadosCedente = new List<Interesado>();

                this._interesadosCedente.Add(primerInteresado);

                if (((Cesion)this._ventana.Cesion).Cedente != null)
                {
                    this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo(((Cesion)this._ventana.Cesion).Cedente);
                    this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;

                    if ((Interesado)this._ventana.InteresadoCedente != null)
                    {
                        this._interesadosCedente.Add((Interesado)this._ventana.InteresadoCedente);
                        this._ventana.CedentesFiltrados = this._interesadosCedente;
                        this._ventana.CedenteFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.CedentesFiltrados, (Interesado)this._ventana.InteresadoCedente);
                    }
                }
                else
                {
                    this._ventana.InteresadoCedente = primerInteresado;
                    this._ventana.CedentesFiltrados = this._interesadosCedente;
                    this._ventana.CedenteFiltrado = primerInteresado;

                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._interesadosCesionario = new List<Interesado>();
                this._interesadosCesionario.Add(primerInteresado);

                if (((Cesion)this._ventana.Cesion).Cesionario != null)
                {
                    this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo(((Cesion)this._ventana.Cesion).Cesionario);
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;

                    if ((Interesado)this._ventana.InteresadoCesionario != null)
                    {
                        this._interesadosCesionario.Add((Interesado)this._ventana.InteresadoCesionario);
                        this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                        this._ventana.CesionarioFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.CesionariosFiltrados, (Interesado)this._ventana.InteresadoCesionario);
                    }
                }
                else
                {
                    this._ventana.InteresadoCesionario = primerInteresado;
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                    this._ventana.CesionarioFiltrado = primerInteresado;
                }
            }
        }

        /// <summary>
        /// Método que carga el apoderado
        /// </summary>
        private void CargarApoderado(string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Cedente"))
            {
                this._agentesCedente = new List<Agente>();
                this._agentesCedente.Add(primerAgente);

                if (((Cesion)this._ventana.Cesion).AgenteCedente != null)
                {
                    this._agentesCedente.Add((Agente)this._ventana.ApoderadoCedente);
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosCedenteFiltrados, (Agente)this._ventana.ApoderadoCedente);
                }
                else
                {
                    this._ventana.ApoderadoCedente = primerAgente;
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._agentesCesionario = new List<Agente>();
                this._agentesCesionario.Add(primerAgente);

                if (((Cesion)this._ventana.Cesion).AgenteCesionario != null)
                {
                    this._agentesCesionario.Add((Agente)this._ventana.ApoderadoCesionario);
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.ApoderadosCesionarioFiltrados, (Agente)this._ventana.ApoderadoCesionario);
                }
                else
                {
                    this._ventana.ApoderadoCesionario = primerAgente;
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
                }
            }
        }

        /// <summary>
        /// Método que carga el poder
        /// </summary>
        private void CargarPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);

            if (tipo.Equals("Cedente"))
            {
                this._poderesCedente = new List<Poder>();
                this._poderesCedente.Add(primerPoder);

                if (((Cesion)this._ventana.Cesion).PoderCedente != null)
                {
                    this._poderesCedente.Add((Poder)this._ventana.PoderCedente);
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesCedenteFiltrados, (Poder)this._ventana.PoderCedente);
                }
                else
                {
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                }
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._poderesCesionario = new List<Poder>();
                this._poderesCesionario.Add(primerPoder);

                if (((Cesion)this._ventana.Cesion).PoderCesionario != null)
                {
                    this._poderesCesionario.Add((Poder)this._ventana.PoderCesionario);
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesCesionarioFiltrados, (Poder)this._ventana.PoderCesionario);
                }
                else
                {
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = primerPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");
                }
            }
        }

        /// <summary>
        /// Método que segun el estatus de la pagina carga una cesion registrada
        /// o una cesion nueva
        /// </summary>
        public Cesion CargarCesionDeLaPantalla()
        {

            Cesion cesion = (Cesion)this._ventana.Cesion;

            if (null != this._ventana.MarcaFiltrada)
                cesion.Marca = ((Marca)this._ventana.MarcaFiltrada).Id != int.MinValue ? (Marca)this._ventana.MarcaFiltrada : null;

            if (null != this._ventana.CedenteFiltrado)
                cesion.Cedente = ((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue ? (Interesado)this._ventana.CedenteFiltrado : null;

            if (null != this._ventana.CesionarioFiltrado)
                cesion.Cesionario = ((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue ? (Interesado)this._ventana.CesionarioFiltrado : null;

            if (null != this._ventana.ApoderadoCedenteFiltrado)
                cesion.AgenteCedente = !((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("") ? (Agente)this._ventana.ApoderadoCedenteFiltrado : null;

            if (null != this._ventana.ApoderadoCesionarioFiltrado)
                cesion.AgenteCesionario = !((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals("") ? (Agente)this._ventana.ApoderadoCesionarioFiltrado : null;

            if (null != this._ventana.PoderCedenteFiltrado)
                cesion.PoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue ? (Poder)this._ventana.PoderCedenteFiltrado : null;

            if (null != this._ventana.PoderCesionarioFiltrado)
                cesion.PoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue ? (Poder)this._ventana.PoderCesionarioFiltrado : null;

            if (null != this._ventana.Boletin)
                cesion.BoletinPublicacion = ((Boletin)this._ventana.Boletin).Id != int.MinValue ? (Boletin)this._ventana.Boletin : null;

            return cesion;
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

                //Modifica los datos de la cesion
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    Cesion cesion = CargarCesionDeLaPantalla();

                    bool exitoso = this._cesionServicios.InsertarOModificar(cesion, UsuarioLogeado.Hash);

                    if ((exitoso) && (this._agregar == false))
                        this.Navegar(Recursos.MensajesConElUsuario.CesionModificada, false);
                    else if ((exitoso) && (this._agregar == true))
                        this.Navegar(Recursos.MensajesConElUsuario.CesionInsertada, false);
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
        /// Metodo que se encarga de eliminar una cesion
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

                if (this._cesionServicios.Eliminar((Cesion)this._ventana.Cesion, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.CesionEliminada;
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
        /// Método que carga los poderes
        /// </summary>
        public void LlenarListasPoderes(Cesion cesion)
        {

            if (cesion.Cedente != null)
                this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cedente);

            if (cesion.Cesionario != null)
                this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(cesion.Cesionario);

            if (cesion.AgenteCedente != null)
                this._poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCedente);

            if (cesion.AgenteCesionario != null)
                this._poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(cesion.AgenteCesionario);
        }

        /// <summary>
        /// Método que valida los poderes
        /// </summary>
        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB, string tipo)
        {
            bool retorno = false;
            IList<Poder> listaIntereseccionCedente = new List<Poder>();
            IList<Poder> listaIntereseccionCesionario = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderActual = new Poder();

            listaIntereseccionCedente.Add(primerPoder);
            listaIntereseccionCesionario.Add(primerPoder);

            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if ((poderA.Id == poderB.Id) && (tipo.Equals("Cedente")))
                        {
                            listaIntereseccionCedente.Add(poderA);
                            retorno = true;
                        }

                        else if ((poderA.Id == poderB.Id) && (tipo.Equals("Cesionario")))
                        {
                            listaIntereseccionCesionario.Add(poderA);
                            retorno = true;
                        }
                    }

                }

                if ((listaIntereseccionCedente.Count != 0) && (tipo.Equals("Cedente")))
                {
                    poderActual = (Poder)this._ventana.PoderCedenteFiltrado;
                    this._poderesInterseccionCedente = listaIntereseccionCedente;
                    this._ventana.PoderesCedenteFiltrados = listaIntereseccionCedente;
                    this._ventana.PoderCedenteFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesCedenteFiltrados, poderActual);
                }


                else if ((listaIntereseccionCesionario.Count != 0) && (tipo.Equals("Cesionario")))
                {
                    poderActual = (Poder)this._ventana.PoderCesionarioFiltrado;
                    this._poderesInterseccionCesionario = listaIntereseccionCesionario;
                    this._ventana.PoderesCesionarioFiltrados = listaIntereseccionCesionario;
                    this._ventana.PoderCesionarioFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesCesionarioFiltrados, poderActual);
                }

                else
                    retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método que carga los interesados y los agentes
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

                Agente agenteActual = new Agente();

                agentesInteresadoFiltrados = new List<Agente>();

                if (tipo.Equals("Cedente"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderCedenteFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderCedenteFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.CedentesFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.CedenteFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.CedenteFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.CedenteFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoCedenteFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCedenteFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoCedenteFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                    }
                }
                else if (tipo.Equals("Cesionario"))
                {
                    if (poder.Id == null)
                        poderFiltrar.Id = this._ventana.IdPoderCesionarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderCesionarioFiltrar);
                    else
                        poderFiltrar.Id = poder.Id;

                    if (poderFiltrar.Id != 0)
                    {
                        interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderCesionarioFiltrado);
                        agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderCesionarioFiltrado);
                    }

                    if (interesado != null)
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        interesadosFiltrados.Add(interesado);
                        this._ventana.CesionariosFiltrados = interesadosFiltrados;

                        if (cargaInicial)
                            this._ventana.CesionarioFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                        else
                            this._ventana.CesionarioFiltrado = primerInteresado;
                    }
                    else
                    {
                        interesadosFiltrados.Insert(0, primerInteresado);
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.CesionarioFiltrado = primerInteresado;
                    }

                    if (agentesInteresadoFiltrados.Count != 0)
                    {
                        agenteActual = (Agente)this._ventana.ApoderadoCesionarioFiltrado;
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCesionarioFiltrados = agentesInteresadoFiltrados;
                        this._ventana.ApoderadoCesionarioFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                    }
                    else
                    {
                        agentesInteresadoFiltrados.Insert(0, primerAgente);
                        this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                        this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
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
        /// Método que carga los agentes
        /// </summary>
        private void LlenarListaAgente(Poder poder, string tipo)
        {
            Agente primerAgente = new Agente("");

            if (tipo.Equals("Cedente"))
            {
                this._agentesCedente = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesCedente.Insert(0, primerAgente);
                this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                this._ventana.ApoderadoCedenteFiltrado = primerAgente;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._agentesCesionario = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
                this._agentesCesionario.Insert(0, primerAgente);
                this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
            }
        }

        /// <summary>
        /// Método que verifica el cambio del interesado
        /// </summary>
        public bool VerificarCambioInteresado(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Cedente"))
            {
                if ((((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")))
                    retorno = true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if ((((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals("")))
                    retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Método que verifica el cambio de agente
        /// </summary>
        public bool VerificarCambioAgente(string tipo)
        {
            bool retorno = false;

            if (tipo.Equals("Cedente"))
            {
                if (!(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue))
                    retorno = true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if (!(((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals("")) || (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue))
                    retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Método que verifica el cambio de poder
        /// </summary>
        public bool VerificarCambioPoder(string tipo)
        {
            if (tipo.Equals("Cedente"))
            {
                if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                    return true;
            }
            if (tipo.Equals("Cesionario"))
            {
                if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Método que borra la lista de interesados
        /// </summary>
        public void LimpiarListaInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.CedentesFiltrados = listaInteresados;
                this._ventana.CedenteFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.CesionariosFiltrados = listaInteresados;
                this._ventana.CesionarioFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
                this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
            }
        }

        /// <summary>
        /// Método que borra la lista de agentes
        /// </summary>
        public void LimpiarListaAgente(string tipo)
        {
            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.ApoderadosCedenteFiltrados = listaAgentes;
                this._ventana.ApoderadoCedenteFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.ApoderadosCesionarioFiltrados = listaAgentes;
                this._ventana.ApoderadoCesionarioFiltrado = BuscarAgente(listaAgentes, primerAgente);
                this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
            }
        }

        /// <summary>
        /// Método que borra la lista de poder
        /// </summary>
        public void LimpiarListaPoder(string tipo)
        {
            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            if (tipo.Equals("Cedente"))
            {
                this._ventana.PoderesCedenteFiltrados = listaPoderes;
                this._ventana.PoderCedenteFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
            }
            else if (tipo.Equals("Cesionario"))
            {
                this._ventana.PoderesCesionarioFiltrados = listaPoderes;
                this._ventana.PoderCesionarioFiltrado = BuscarPoder(listaPoderes, primerPoder);
                this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
            }
        }

        #region Marca

        /// <summary>
        /// Método que consulta las marcas
        /// </summary>
        public void IrConsultarMarcas()
        {
            this.Navegar(new ConsultarMarcas());
        }

        /// <summary>
        /// Método que carga la Marca
        /// </summary>
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

        /// <summary>
        /// Método que consulta las marcas
        /// </summary>
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

        /// <summary>
        /// Método que cambia las marcas
        /// </summary>
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

        #region Cedente

        /// <summary>
        /// Método que valida el cedente seleccionado
        /// </summary>
        private void ValidarCedente()
        {
            if (((Interesado)this._ventana.CedenteFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente");

                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedente, "Cedente", true);
                        ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente");

                        this._ventana.GestionarBotonConsultarInteresados("Cedente", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cedente", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cedente", false);
                    }
                }
            }
        }

        /// <summary>
        /// Método que consulta los cedentes
        /// </summary>
        public void ConsultarCedentes()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);


                Interesado cesionario = new Interesado();
                IList<Interesado> cesionariosFiltrados;
                cesionario.Nombre = this._ventana.NombreCedenteFiltrar.ToUpper();
                cesionario.Id = this._ventana.IdCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCedenteFiltrar);

                if ((!cesionario.Nombre.Equals("")) || (cesionario.Id != 0))
                    cesionariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cesionario);
                else
                    cesionariosFiltrados = new List<Interesado>();

                if (cesionariosFiltrados.ToList<Interesado>().Count != 0)
                {
                    cesionariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.CedentesFiltrados = cesionariosFiltrados.ToList<Interesado>();
                    this._ventana.CedenteFiltrado = primerInteresado;
                }
                else
                {
                    cesionariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.CedentesFiltrados = this._interesadosCedente;
                    this._ventana.CedenteFiltrado = primerInteresado;
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
        /// Método que consulta a los apoderados
        /// </summary>
        public void ConsultarApoderadosCedente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");


                Agente apoderadoCedente = new Agente();
                IList<Agente> agentesCedenteFiltrados;
                apoderadoCedente.Nombre = this._ventana.NombreApoderadoCedenteFiltrar.ToUpper();
                apoderadoCedente.Id = this._ventana.IdApoderadoCedenteFiltrar.ToUpper();

                if ((!apoderadoCedente.Nombre.Equals("")) || (!apoderadoCedente.Id.Equals("")))
                {
                    agentesCedenteFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoCedente);
                }
                else
                    agentesCedenteFiltrados = new List<Agente>();

                if (agentesCedenteFiltrados.Count != 0)
                {
                    agentesCedenteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
                    this._ventana.ApoderadosCedenteFiltrados = agentesCedenteFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesCedenteFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;
                    this._ventana.ApoderadoCedenteFiltrado = primerAgente;
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
        /// Método que consulta los poderes
        /// </summary>
        public void ConsultarPoderesCedente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);


                Poder poderCedente = new Poder();
                IList<Poder> poderesCedenteFiltrados;

                if (!this._ventana.IdPoderCedenteFiltrar.Equals(""))
                    poderCedente.Id = int.Parse(this._ventana.IdPoderCedenteFiltrar);
                else
                    poderCedente.Id = 0;

                if (!this._ventana.FechaPoderCedenteFiltrar.Equals(""))
                    poderCedente.Fecha = DateTime.Parse(this._ventana.FechaPoderCedenteFiltrar);

                if ((!poderCedente.Fecha.Equals("")) || (poderCedente.Id != 0))
                    poderesCedenteFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCedente);
                else
                    poderesCedenteFiltrados = new List<Poder>();

                if (poderesCedenteFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesCedenteFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = primerPoder;
                    this._ventana.PoderesCedenteFiltrados = poderesCedenteFiltrados;
                }
                else
                {
                    poderesCedenteFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                    this._ventana.PoderCedenteFiltrado = primerPoder;
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
        /// Método que Cambia el cedente
        /// </summary>
        public bool CambiarCedente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CedenteFiltrado);
                            this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));

                            LimpiarListaPoder("Cedente");

                            if ((this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente")))
                            {
                                this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                                this._ventana.NombreCedente = ((Interesado)this._ventana.CedenteFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCedente, _poderesApoderadosCedente, "Cedente"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cedente"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));
                            this._poderesCedente.Insert(0, primerPoder);
                            this._ventana.PoderesCedenteFiltrados = this._poderesCedente;
                            this._ventana.PoderCedenteFiltrado = primerPoder;

                            this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));
                            this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CedenteFiltrado);
                            this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                            retorno = true;
                        }
                        else
                        {

                            this._poderesCedente = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CedenteFiltrado));
                            this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CedenteFiltrado);
                            this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                    this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");

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
        /// Método que cambia el apoderado
        /// </summary>
        public bool CambiarApoderadoCedente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCedenteFiltrado));

                            LimpiarListaPoder("Cedente");

                            if ((this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente")))
                            {
                                this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                                this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCedente, this._poderesApoderadosCedente, "Cedente"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cedente"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCedente = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCedenteFiltrado));
                            this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                            this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                    this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");

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
        /// Método que cambia el poder
        /// </summary>
        public bool CambiarPoderCedente()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderCedenteFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoCedenteFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.CedenteFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Cedente");

                            LlenarListaAgente((Poder)this._ventana.PoderCedenteFiltrado, "Cedente");

                            this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                            this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Cedente");

                            LimpiarListaAgente("Cedente");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCedenteFiltrado, "Cedente", false);

                            this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                            this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                        this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                    this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cedente");

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

        #region Cesionario

        /// <summary>
        /// Método que valida el cesionario
        /// </summary>
        private void ValidarCesionario()
        {
            if (((Interesado)this._ventana.CesionarioFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);

                    else
                    {

                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);

                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                    {

                        ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario");

                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionario, "Cesionario", true);
                        ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario");

                        this._ventana.GestionarBotonConsultarInteresados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarApoderados("Cesionario", false);
                        this._ventana.GestionarBotonConsultarPoderes("Cesionario", false);
                    }
                }
            }
        }

        /// <summary>
        /// Método que consulta los cesionarios
        /// </summary>
        public void ConsultarCesionarios()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Interesado primerInteresado = new Interesado(int.MinValue);


                Interesado cesionario = new Interesado();
                IList<Interesado> cesionariosFiltrados;
                cesionario.Nombre = this._ventana.NombreCesionarioFiltrar.ToUpper();
                cesionario.Id = this._ventana.IdCesionarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCesionarioFiltrar);

                if ((!cesionario.Nombre.Equals("")) || (cesionario.Id != 0))
                    cesionariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cesionario);
                else
                    cesionariosFiltrados = new List<Interesado>();

                if (cesionariosFiltrados.ToList<Interesado>().Count != 0)
                {
                    cesionariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.CesionariosFiltrados = cesionariosFiltrados.ToList<Interesado>();
                    this._ventana.CesionarioFiltrado = primerInteresado;
                }
                else
                {
                    cesionariosFiltrados.Insert(0, primerInteresado);
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;
                    this._ventana.CesionarioFiltrado = primerInteresado;
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
        /// Método que consulta apoderados
        /// </summary>
        public void ConsultarApoderadosCesionario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");


                Agente apoderadoCesionario = new Agente();
                IList<Agente> agentesCesionarioFiltrados;
                apoderadoCesionario.Nombre = this._ventana.NombreApoderadoCesionarioFiltrar.ToUpper();
                apoderadoCesionario.Id = this._ventana.IdApoderadoCesionarioFiltrar.ToUpper();

                if ((!apoderadoCesionario.Nombre.Equals("")) || (!apoderadoCesionario.Id.Equals("")))
                    agentesCesionarioFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoCesionario);
                else
                    agentesCesionarioFiltrados = new List<Agente>();

                if (agentesCesionarioFiltrados.Count != 0)
                {
                    agentesCesionarioFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
                    this._ventana.ApoderadosCesionarioFiltrados = agentesCesionarioFiltrados.ToList<Agente>();
                }
                else
                {
                    agentesCesionarioFiltrados.Insert(0, primerAgente);
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;
                    this._ventana.ApoderadoCesionarioFiltrado = primerAgente;
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
        /// Método que consulta los poderes
        /// </summary>
        public void ConsultarPoderesCesionario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);

                Poder poderCesionario = new Poder();
                IList<Poder> poderesCesionarioFiltrados;

                if (!this._ventana.IdPoderCesionarioFiltrar.Equals(""))
                    poderCesionario.Id = int.Parse(this._ventana.IdPoderCesionarioFiltrar);
                else
                    poderCesionario.Id = 0;

                if (!this._ventana.FechaPoderCesionarioFiltrar.Equals(""))
                    poderCesionario.Fecha = DateTime.Parse(this._ventana.FechaPoderCesionarioFiltrar);

                if ((!poderCesionario.Fecha.Equals("")) || (poderCesionario.Id != 0))
                    poderesCesionarioFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCesionario);
                else
                    poderesCesionarioFiltrados = new List<Poder>();

                if (poderesCesionarioFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesCesionarioFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = primerPoder;
                    this._ventana.PoderesCesionarioFiltrados = poderesCesionarioFiltrados;
                }
                else
                {
                    poderesCesionarioFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                    this._ventana.PoderCesionarioFiltrado = primerPoder;
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
        /// Método que cambia el cesionario
        /// </summary>
        public bool CambiarCesionario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CesionarioFiltrado);
                            this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));

                            LimpiarListaPoder("Cesionario");

                            if ((this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario")))
                            {
                                this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                                this._ventana.NombreCesionario = ((Interesado)this._ventana.CesionarioFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCesionario, _poderesApoderadosCesionario, "Cesionario"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Cesionario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id == int.MinValue)
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));
                            this._poderesCesionario.Insert(0, primerPoder);
                            this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;
                            this._ventana.PoderCesionarioFiltrado = primerPoder;

                            this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));
                            this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CesionarioFiltrado);
                            this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                            retorno = true;
                        }
                        else
                        {

                            this._poderesCesionario = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.CesionarioFiltrado));
                            this._ventana.InteresadoCesionario = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.CesionarioFiltrado);
                            this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.InteresadoCesionario).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

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
        /// Método que cambia apoderados
        /// </summary>
        public bool CambiarApoderadoCesionario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                            this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCesionarioFiltrado));

                            LimpiarListaPoder("Cesionario");

                            if ((this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario")))
                            {
                                this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                                this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesCesionario, this._poderesApoderadosCesionario, "Cesionario"))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Cesionario"), 0);
                            }
                        }
                    }
                    else
                    {
                        if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                        {
                            this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                            this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                        else
                        {
                            this._poderesApoderadosCesionario = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.ApoderadoCesionarioFiltrado));
                            this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                            this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                    this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

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
        /// Método que cambia el poder
        /// </summary>
        public bool CambiarPoderCesionario()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Poder)this._ventana.PoderCesionarioFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.ApoderadoCesionarioFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.CesionarioFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente("Cesionario");

                            LlenarListaAgente((Poder)this._ventana.PoderCesionarioFiltrado, "Cesionario");

                            this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                            this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado("Cesionario");

                            LimpiarListaAgente("Cesionario");

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderCesionarioFiltrado, "Cesionario", false);

                            this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                            this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                        this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                        retorno = true;
                    }
                }
                else
                {
                    this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                    this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
                    retorno = true;
                }

                this._ventana.ConvertirEnteroMinimoABlanco("Cesionario");

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


        public void IrImprimir(string nombreBoton)
        {
            try
            {
                switch (nombreBoton)
                {
                    case "_btnPlanilla":
                        ImprimirPlanilla();
                        break;
                    case "_btnAnexo":
                        ImprimirAnexo();
                        break;
                    case "_btnCarpeta":
                        ImprimirCarpeta();
                        break;
                    case "_btnPlanillaVan":
                        ImprimirPlanillaVan();
                        break;
                    case "_btnPlanillaVienen":
                        ImprimirPlanillaVienen();
                        break;
                    default:
                        break;
                }
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ExcepcionPaquetes, true);
            }
        }

        private void ImprimirPlanillaVienen()
        {
            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MCESIONES";
                string procedimiento = "P5";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Cesion)this._ventana.Cesion).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVienen);
            }
        }

        private void ImprimirPlanillaVan()
        {
            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MCESIONES";
                string procedimiento = "P4";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Cesion)this._ventana.Cesion).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVan);
            }
        }

        private void ImprimirCarpeta()
        {
            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MCESIONES";
                string procedimiento = "P3";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Cesion)this._ventana.Cesion).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnCarpeta);
            }
        }

        private bool ValidarMarcaAntesDeImprimirCarpeta()
        {
            return true;
        }

        private void ImprimirAnexo()
        {
            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MCESIONES";
                string procedimiento = "P2";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Cesion)this._ventana.Cesion).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexo);
            }
        }

        private bool ValidarMarcaAntesDeImprimirAnexo()
        {
            return true;
        }

        private void ImprimirPlanilla()
        {
            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MCESIONES";
                string procedimiento = "P1";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Cesion)this._ventana.Cesion).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanilla);
            }
        }

        private bool ValidarMarcaAntesDeImprimirPlanilla()
        {
            return true;
        }
    }
}