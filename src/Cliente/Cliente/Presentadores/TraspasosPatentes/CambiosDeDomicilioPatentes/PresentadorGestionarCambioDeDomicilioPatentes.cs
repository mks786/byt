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
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CambiosDeDomicilioPatentes;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeDomicilioPatentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;

namespace Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.CambiosDeDomicilioPatentes
{
    class PresentadorGestionarCambioDeDomicilioPatentes : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private bool _agregar = true;
        private IGestionarCambioDeDomicilioPatentes _ventana;

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
        private ICambioDeDomicilioPatenteServicios _cambioDeDomicilioPatenteServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;
        private IList<Auditoria> _auditorias;
        private IList<Patente> _patentes;
        private IList<Interesado> _interesadosAnterior;
        private IList<Interesado> _interesadosActual;
        private IList<Agente> _agentesApoderados;
        private IList<Poder> _poderes;

        private IList<Poder> _poderesInterseccion;
        private IList<Poder> _poderesInteresadoActual;
        private IList<Poder> _poderesApoderado;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCambioDeDomicilioPatentes(IGestionarCambioDeDomicilioPatentes ventana, object cambioDeDomicilio)
        {
            try
            {

                this._ventana = ventana;

                if (cambioDeDomicilio != null)
                {
                    this._ventana.CambioDeDomicilioPatente = cambioDeDomicilio;
                    _agregar = false;
                    CambiarAModificar();
                }
                else
                {
                    CambioDeDomicilioPatente cambioDeDomicilioAgregar = new CambioDeDomicilioPatente();
                    this._ventana.CambioDeDomicilioPatente = cambioDeDomicilioAgregar;

                    ((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).FechaDomicilio = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.Poder = null;
                    this._ventana.InteresadoAnterior = null;
                    this._ventana.InteresadoActual = null;
                    this._ventana.AgenteApoderado = null;

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
                this._cambioDeDomicilioPatenteServicios = (ICambioDeDomicilioPatenteServicios)Activator.GetObject(typeof(ICambioDeDomicilioPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioPatenteServicios"]);

                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }




        /// <summary>
        /// Constructor Predeterminado que recibe una ventana padre
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCambioDeDomicilioPatentes(IGestionarCambioDeDomicilioPatentes ventana, object cambioDeDomicilio, object ventanaPadre)
        {
            try
            {

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;

                if (cambioDeDomicilio != null)
                {
                    this._ventana.CambioDeDomicilioPatente = cambioDeDomicilio;
                    _agregar = false;
                    CambiarAModificar();
                }
                else
                {
                    CambioDeDomicilioPatente cambioDeDomicilioAgregar = new CambioDeDomicilioPatente();
                    this._ventana.CambioDeDomicilioPatente = cambioDeDomicilioAgregar;

                    ((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).FechaDomicilio = DateTime.Now;
                    this._ventana.Patente = null;
                    this._ventana.Poder = null;
                    this._ventana.InteresadoAnterior = null;
                    this._ventana.InteresadoActual = null;
                    this._ventana.AgenteApoderado = null;

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
                this._cambioDeDomicilioPatenteServicios = (ICambioDeDomicilioPatenteServicios)Activator.GetObject(typeof(ICambioDeDomicilioPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CambioDeDomicilioPatenteServicios"]);

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
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarCambioDeDomicilioPatente,
                Recursos.Ids.GestionarCambioDeDomicilio);
            else
                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCambioDeDomicilioPatente,
                Recursos.Ids.GestionarCambioDeDomicilio);
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
                    CambioDeDomicilioPatente cambioDeDomicilio = (CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente;

                    if (((CambioDeDomicilioPatente)cambioDeDomicilio).Patente != null)
                        this._ventana.Patente = this._patenteServicios.ConsultarPatenteConTodo(((CambioDeDomicilioPatente)cambioDeDomicilio).Patente);

                    this._ventana.NombrePatente = ((Patente)this._ventana.Patente).Descripcion;
                    this._ventana.AgenteApoderado = ((CambioDeDomicilioPatente)cambioDeDomicilio).Agente;
                    this._ventana.Poder = cambioDeDomicilio.Poder;
                    
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

                    CargarPatente();

                    CargarInteresado("Anterior");

                    CargarInteresado("Actual");

                    CargarApoderado();

                    CargarPoder();

                    LlenarListasPoderes((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente);

                    ValidarInteresado();

                    CargaBoletines();

                }
                else
                {
                    CargarPatente();

                    CargarInteresado("Anterior");

                    CargarInteresado("Actual");

                    CargarApoderado();

                    CargarPoder();

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

        private void CargarInteresado(string tipo)
        {
            Interesado primerInteresado = new Interesado(int.MinValue);

            if (tipo.Equals("Anterior"))
            {
                this._interesadosAnterior = new List<Interesado>();

                this._interesadosAnterior.Add(primerInteresado);

                if (((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).InteresadoAnterior != null)
                {
                    this._ventana.InteresadoAnterior = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).InteresadoAnterior);
                    this._ventana.NombreInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnterior).Nombre;
                    this._ventana.IdInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoAnterior != null)
                    {
                        this._interesadosAnterior.Add((Interesado)this._ventana.InteresadoAnterior);
                        this._ventana.InteresadosAnteriorFiltrados = this._interesadosAnterior;
                        this._ventana.InteresadoAnteriorFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosAnteriorFiltrados, (Interesado)this._ventana.InteresadoAnterior);
                        this._ventana.IdInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id.ToString();
                    }
                }
                else
                {
                    this._ventana.InteresadoAnterior = primerInteresado;
                    this._ventana.InteresadosAnteriorFiltrados = this._interesadosAnterior;
                    this._ventana.InteresadoAnteriorFiltrado = primerInteresado;
                    this._ventana.IdInteresadoActual = primerInteresado.Id.ToString();

                }
            }
            else if (tipo.Equals("Actual"))
            {
                this._interesadosActual = new List<Interesado>();

                this._interesadosActual.Add(primerInteresado);

                if (((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).InteresadoActual != null)
                {
                    this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).InteresadoActual);
                    this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                    this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();

                    if ((Interesado)this._ventana.InteresadoActual != null)
                    {
                        this._interesadosActual.Add((Interesado)this._ventana.InteresadoActual);
                        this._ventana.InteresadosActualFiltrados = this._interesadosActual;
                        this._ventana.InteresadoActualFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosActualFiltrados, (Interesado)this._ventana.InteresadoActual);
                        this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                    }
                }
                else
                {
                    this._ventana.InteresadoActual = primerInteresado;
                    this._ventana.InteresadosActualFiltrados = this._interesadosActual;
                    this._ventana.InteresadoActualFiltrado = primerInteresado;
                    this._ventana.IdInteresadoActual = primerInteresado.Id.ToString();

                }
            }
        }

        //public void IrConsultarPatentes()
        //{
        //    this.Navegar(new ConsultarPatentes());
        //}

        public CambioDeDomicilioPatente CargarCambioDeDomicilioDeLaPantalla()
        {

            CambioDeDomicilioPatente cambioDeDomicilio = (CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente;

            if ((null != this._ventana.PatenteFiltrado) && (((Patente)this._ventana.PatenteFiltrado).Id != int.MinValue))
            {
                cambioDeDomicilio.Patente = (Patente)this._ventana.PatenteFiltrado;
                cambioDeDomicilio.InteresadoAnterior = ((Patente)this._ventana.PatenteFiltrado).Interesado;
                cambioDeDomicilio.Agente = ((Patente)this._ventana.PatenteFiltrado).Agente;
                cambioDeDomicilio.Poder = ((Patente)this._ventana.PatenteFiltrado).Poder;
            }

            if (null != this._ventana.InteresadoAnterior)
                cambioDeDomicilio.InteresadoAnterior = ((Interesado)this._ventana.InteresadoAnterior).Id != int.MinValue ?
                                                                        (Interesado)this._ventana.InteresadoAnterior : null;

            if (null != this._ventana.InteresadoActual)
                cambioDeDomicilio.InteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id != int.MinValue ?
                                                                        (Interesado)this._ventana.InteresadoActual : null;

            if (null != this._ventana.AgenteApoderado)
                cambioDeDomicilio.Agente = !((Agente)this._ventana.AgenteApoderado).Id.Equals("") ?
                                                        (Agente)this._ventana.AgenteApoderado : null;

            if (null != this._ventana.Poder)
                cambioDeDomicilio.Poder = ((Poder)this._ventana.Poder).Id != int.MinValue ?
                                                            (Poder)this._ventana.Poder : null;

            if (null != this._ventana.Boletin)
                cambioDeDomicilio.BoletinPublicacion = ((Boletin)this._ventana.Boletin).Id != int.MinValue ?
                                                                        (Boletin)this._ventana.Boletin : null;


            return cambioDeDomicilio;
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

                //Modifica o inserta los datos del Cambio de Domicilio
                else if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnAceptar)
                {
                    CambioDeDomicilioPatente cambioDeDomicilio = CargarCambioDeDomicilioDeLaPantalla();
                    cambioDeDomicilio.Patente = (Patente)this._ventana.Patente;

                    if (null != cambioDeDomicilio.Patente)
                    {

                        int? exitoso =
                            this._cambioDeDomicilioPatenteServicios.InsertarOModificarCambioDeDomicilio(
                                cambioDeDomicilio, UsuarioLogeado.Hash);

                        if ((!exitoso.Equals(null)) && (this._agregar == false))
                        {
                            this._ventana.HabilitarCampos = false;
                            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar;
                        }
                        else if ((!exitoso.Equals(null)) && (this._agregar == true))
                        {
                            cambioDeDomicilio.Id = exitoso.Value;
                            this.Navegar(new GestionarCambioDeDomicilioPatentes(cambioDeDomicilio));
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
        /// Metodo que se encarga de eliminar un Cambio de Domicilio
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

                if (this._cambioDeDomicilioPatenteServicios.Eliminar((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.CambioDeDomicilioEliminado;
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
        /// Método que carga los boletines registrados
        /// </summary>
        private void CargaBoletines()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            CambioDeDomicilioPatente cambioDomicilio = (CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente;
            Boletin primerBoletin = new Boletin(int.MinValue);
            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            boletines.Insert(0, primerBoletin);
            this._ventana.Boletines = boletines;
            if (_agregar == false)
                this._ventana.Boletin = BuscarBoletin(boletines, cambioDomicilio.BoletinPublicacion);

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


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

        public void LlenarListaAgenteEInteresado(Poder poder, bool cargaInicial)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                Interesado interesado = new Interesado();
                IList<Agente> agentesInteresadoFiltrados;
                IList<Interesado> interesadosFiltrados = new List<Interesado>();
                Poder poderFiltrar = new Poder();

                Interesado primerInteresado = new Interesado(int.MinValue);
                Agente primerAgente = new Agente("");

                Agente agenteActual = new Agente();

                agentesInteresadoFiltrados = new List<Agente>();

                if (poder.Id == null)
                    poderFiltrar.Id = this._ventana.IdPoderFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdPoderFiltrar);
                else
                    poderFiltrar.Id = poder.Id;

                if (poderFiltrar.Id != 0)
                {
                    interesado = this._interesadoServicios.ObtenerInteresadosDeUnPoder((Poder)this._ventana.PoderFiltrado);
                    agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesDeUnPoder((Poder)this._ventana.PoderFiltrado);
                }

                if (interesado != null)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    interesadosFiltrados.Add(interesado);
                    this._ventana.InteresadosActualFiltrados = interesadosFiltrados;

                    if (cargaInicial)
                        this._ventana.InteresadoActualFiltrado = this.BuscarInteresado(interesadosFiltrados, interesado);
                    else
                        this._ventana.InteresadoActualFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.InteresadoActualFiltrado = primerInteresado;
                }

                if (agentesInteresadoFiltrados.Count != 0)
                {
                    agenteActual = (Agente)this._ventana.AgenteApoderadoFiltrado;
                    agentesInteresadoFiltrados.Insert(0, primerAgente);
                    this._ventana.AgenteApoderadoFiltrados = agentesInteresadoFiltrados;
                    this._ventana.AgenteApoderadoFiltrado = BuscarAgente(agentesInteresadoFiltrados, agenteActual);
                }
                else
                {
                    agentesInteresadoFiltrados.Insert(0, primerAgente);
                    this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                    this._ventana.AgenteApoderadoFiltrado = primerAgente;
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

        }

        private void LlenarListaAgente(Poder poder)
        {
            Agente primerAgente = new Agente("");

            this._agentesApoderados = this._agenteServicios.ObtenerAgentesDeUnPoder(poder);
            this._agentesApoderados.Insert(0, primerAgente);
            this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
            this._ventana.AgenteApoderadoFiltrado = primerAgente;
        }

        #region Patentes

        private void CargarPatente()
        {
            this._patentes = new List<Patente>();
            Patente primeraPatente = new Patente(int.MinValue);
            this._patentes.Add(primeraPatente);

            if ((Patente)this._ventana.Patente != null)
            {
                this._patentes.Add((Patente)this._ventana.Patente);
                this._ventana.PatentesFiltrados = this._patentes;
                this._ventana.PatenteFiltrado = (Patente)this._ventana.Patente;
                this._ventana.IdPatente = ((Patente)this._ventana.Patente).Id.ToString();
            }
            else
            {
                this._ventana.PatentesFiltrados = this._patentes;
                this._ventana.PatenteFiltrado = primeraPatente;
            }

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
                    this._ventana.PatentesFiltrados = patentesFiltradas.ToList<Patente>();
                    this._ventana.PatenteFiltrado = primeraPatente;
                }
                else
                {
                    patentesFiltradas.Insert(0, primeraPatente);
                    this._ventana.PatentesFiltrados = this._patentes;
                    this._ventana.PatenteFiltrado = primeraPatente;
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


                if (this._ventana.PatenteFiltrado != null)
                {
                    this._ventana.Patente = this._ventana.PatenteFiltrado;
                    this._ventana.NombrePatente = ((Patente)this._ventana.PatenteFiltrado).Descripcion;
                    this._ventana.IdPatente = ((Patente)this._ventana.PatenteFiltrado).Id.ToString();
                    this._patentes.RemoveAt(0);
                    this._patentes.Add((Patente)this._ventana.PatenteFiltrado);

                    if (((Patente)this._ventana.Patente).LocalidadPatente != null)
                        this._ventana.EsPatenteNacional(!((Patente)this._ventana.Patente).LocalidadPatente.Equals("I"));

                    this._ventana.InteresadoAnterior = ((Patente)this._ventana.Patente).Interesado;

                    if (((Patente)this._ventana.Patente).Interesado != null)
                        this._ventana.IdInteresadoAnterior = (((Patente)this._ventana.Patente).Interesado).Id.ToString();

                    IList<Interesado> listaAux = new List<Interesado>();
                    listaAux.Add(new Interesado(int.MinValue));

                    if (((Patente)this._ventana.PatenteFiltrado).Id != int.MinValue)
                    {
                        listaAux.Add((Interesado)this._ventana.InteresadoAnterior);

                        this._ventana.InteresadosAnteriorFiltrados = listaAux;
                        this._ventana.InteresadoAnteriorFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosAnteriorFiltrados,
                            (Interesado)this._ventana.InteresadoAnterior);
                    }
                    else
                    {
                        this._ventana.InteresadosAnteriorFiltrados = listaAux;
                        this._ventana.IdInteresadoAnterior = int.MinValue.ToString();
                        this._ventana.InteresadoAnteriorFiltrado = this.BuscarInteresado((IList<Interesado>)this._ventana.InteresadosAnteriorFiltrados,
                            new Interesado(int.MinValue));
                    }

                    this._ventana.AgenteApoderado = ((Patente)this._ventana.Patente).Agente;
                    this._ventana.Poder = ((Patente)this._ventana.Patente).Poder;
                    this._ventana.ConvertirEnteroMinimoABlanco();
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

        #region InteresadoAnterior

        public void ConsultarInteresadosAnterior()
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
                interesado.Nombre = this._ventana.NombreInteresadoAnteriorFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoAnteriorFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoAnteriorFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosAnteriorFiltrados = interesadosFiltrados;
                    this._ventana.InteresadoAnteriorFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosAnteriorFiltrados = this._interesadosAnterior;
                    this._ventana.InteresadoAnteriorFiltrado = primerInteresado;
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

        public bool CambiarInteresadoAnterior()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.InteresadoAnteriorFiltrado != null)
                {
                    this._ventana.InteresadoAnterior =
                        this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoAnteriorFiltrado);
                    this._ventana.NombreInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnteriorFiltrado).Nombre;
                    this._ventana.IdInteresadoAnterior = ((Interesado)this._ventana.InteresadoAnteriorFiltrado).Id.ToString();

                    this._interesadosAnterior.RemoveAt(0);
                    this._interesadosAnterior.Add((Interesado)this._ventana.InteresadoAnteriorFiltrado);
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

        #region InteresadoActual

        private void ValidarInteresado()
        {
            if (((Interesado)this._ventana.InteresadoActualFiltrado).Id == int.MinValue)
            {
                if (((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue)
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.Poder, true);

                        //this._ventana.GestionarBotonConsultarInteresado(false);
                        //this._ventana.GestionarBotonConsultarApoderado(false);
                        this._ventana.GestionarBotonConsultarInteresado(true);
                        this._ventana.GestionarBotonConsultarApoderado(true);
                    }
                }
                else
                {
                    if (((CambioDeDomicilioPatente)this._ventana.PoderFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarInteresado(false);
                        this._ventana.GestionarBotonConsultarInteresado(true);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.Poder, true);

                        //this._ventana.GestionarBotonConsultarInteresado(false);
                        //this._ventana.GestionarBotonConsultarApoderado(false);
                        //this._ventana.GestionarBotonConsultarPoder(false);
                        this._ventana.GestionarBotonConsultarInteresado(true);
                        this._ventana.GestionarBotonConsultarApoderado(true);
                        this._ventana.GestionarBotonConsultarPoder(true);
                    }

                }
            }
            else
            {
                if (((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals(""))
                {
                    if (((Poder)this._ventana.PoderFiltrado).Id == int.MinValue)
                        //this._ventana.GestionarBotonConsultarPoder(false);
                        this._ventana.GestionarBotonConsultarPoder(true);

                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.Poder, true);

                        //this._ventana.GestionarBotonConsultarInteresado(false);
                        //this._ventana.GestionarBotonConsultarApoderado(false);
                        //this._ventana.GestionarBotonConsultarPoder(false);

                        this._ventana.GestionarBotonConsultarInteresado(true);
                        this._ventana.GestionarBotonConsultarApoderado(true);
                        this._ventana.GestionarBotonConsultarPoder(true);

                    }
                }
                else
                {
                    if (((Poder)this._ventana.PoderFiltrado).Id == int.MinValue)
                    {
                        ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado);

                        //this._ventana.GestionarBotonConsultarPoder(false);
                        this._ventana.GestionarBotonConsultarPoder(true);
                    }
                    else
                    {
                        LlenarListaAgenteEInteresado((Poder)this._ventana.Poder, true);
                        ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado);

                        //this._ventana.PoderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.AgenteApoderado,
                        //    (Interesado)this._ventana.InteresadoActual);
                        //this._ventana.PoderFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesFiltrados, (Poder)this._ventana.Poder);

                        //this._ventana.GestionarBotonConsultarInteresado(false);
                        //this._ventana.GestionarBotonConsultarApoderado(false);
                        //this._ventana.GestionarBotonConsultarPoder(false);

                        this._ventana.GestionarBotonConsultarInteresado(true);
                        this._ventana.GestionarBotonConsultarApoderado(true);
                        this._ventana.GestionarBotonConsultarPoder(true);
                    }
                }
            }
        }

        public void ConsultarInteresadosActual()
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
                interesado.Nombre = this._ventana.NombreInteresadoActualFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoActualFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdInteresadoActualFiltrar);

                if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                    interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
                else
                    interesadosFiltrados = new List<Interesado>();

                if (interesadosFiltrados.Count != 0)
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosActualFiltrados = interesadosFiltrados;
                    this._ventana.InteresadoActualFiltrado = primerInteresado;
                }
                else
                {
                    interesadosFiltrados.Insert(0, primerInteresado);
                    this._ventana.InteresadosActualFiltrados = this._interesadosActual;
                    this._ventana.InteresadoActualFiltrado = primerInteresado;
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

        public bool CambiarInteresadoActual()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((Interesado)this._ventana.InteresadoActualFiltrado).Id != int.MinValue)
                {
                    if (!((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals(""))
                    {
                        //if (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue)
                        if ((this._ventana.PoderFiltrado != null) && (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue))
                        {
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoActualFiltrado);
                            this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                        else
                        {
                            this._poderesInteresadoActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.InteresadoActualFiltrado));

                            LimpiarListaPoder();

                            if ((this.ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado)))
                            {
                                this._ventana.InteresadoActual = this._ventana.InteresadoActualFiltrado;
                                this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActualFiltrado).Nombre;
                                this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActualFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else if (!this.ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado))
                            {
                                this._ventana.ConvertirEnteroMinimoABlanco();
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderConAgente, "Actual"), 0);
                            }
                        }
                    }
                    else
                    {
                        //if (((Poder)this._ventana.PoderFiltrado).Id == int.MinValue)
                        if ((this._ventana.PoderFiltrado != null) && (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue))
                        {
                            Poder primerPoder = new Poder(int.MinValue);

                            this._poderesInteresadoActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.InteresadoActualFiltrado));
                            this._poderesInteresadoActual.Insert(0, primerPoder);
                            this._ventana.PoderesFiltrados = this._poderesInteresadoActual;
                            this._ventana.PoderFiltrado = primerPoder;

                            this._poderesInteresadoActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.InteresadoActualFiltrado));
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoActualFiltrado);
                            this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                        else
                        {

                            this._poderesInteresadoActual = this._poderServicios.ConsultarPoderesPorInteresado(((Interesado)_ventana.InteresadoActualFiltrado));
                            this._ventana.InteresadoActual = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoActualFiltrado);
                            this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                            this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
                            retorno = true;
                        }
                    }

                }
                else
                {
                    this._ventana.InteresadoActual = this._ventana.InteresadoActualFiltrado;
                    this._ventana.NombreInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Nombre;
                    this._ventana.IdInteresadoActual = ((Interesado)this._ventana.InteresadoActual).Id.ToString();
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

        public bool VerificarCambioInteresado()
        {
            bool retorno = false;

            if ((((Interesado)this._ventana.InteresadoActualFiltrado).Id != int.MinValue) || !(((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals("")))
                retorno = true;

            return retorno;
        }

        public void LimpiarListaInteresado()
        {
            Interesado primerInteresado = new Interesado(int.MinValue);
            IList<Interesado> listaInteresados = new List<Interesado>();
            listaInteresados.Add(primerInteresado);

            this._ventana.InteresadosActualFiltrados = listaInteresados;
            this._ventana.InteresadoActualFiltrado = BuscarInteresado(listaInteresados, primerInteresado);
            this._ventana.InteresadoActual = this._ventana.InteresadoActualFiltrado;
        }

        #endregion

        #region Agente Apoderado

        private void CargarApoderado()
        {
            Agente primerAgente = new Agente("");

            this._agentesApoderados = new List<Agente>();
            this._agentesApoderados.Add(primerAgente);

            if ((Agente)this._ventana.AgenteApoderado != null)
            {
                this._agentesApoderados.Add((Agente)this._ventana.AgenteApoderado);
                this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
                this._ventana.AgenteApoderadoFiltrado = this.BuscarAgente((IList<Agente>)this._ventana.AgenteApoderadoFiltrados, (Agente)this._ventana.AgenteApoderado);
                this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderado).Id;
            }
            else
            {
                this._ventana.AgenteApoderado = primerAgente;
                this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
                this._ventana.AgenteApoderadoFiltrado = primerAgente;
                this._ventana.IdAgenteApoderado = primerAgente.Id;
            }

        }

        public void ConsultarApoderados()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Agente primerAgente = new Agente("");


                Agente apoderadoInteresado = new Agente();
                IList<Agente> agentesInteresadoFiltrados;
                apoderadoInteresado.Nombre = this._ventana.NombreAgenteApoderadoFiltrar.ToUpper();
                apoderadoInteresado.Id = this._ventana.IdAgenteFiltrar.ToUpper();

                if ((!apoderadoInteresado.Nombre.Equals("")) || (!apoderadoInteresado.Id.Equals("")))
                    agentesInteresadoFiltrados = this._agenteServicios.ObtenerAgentesSinPoderesFiltro(apoderadoInteresado);
                else
                    agentesInteresadoFiltrados = new List<Agente>();

                if (agentesInteresadoFiltrados.ToList<Agente>().Count != 0)
                {
                    agentesInteresadoFiltrados.Insert(0, primerAgente);
                    this._ventana.AgenteApoderadoFiltrados = agentesInteresadoFiltrados;
                    this._ventana.AgenteApoderadoFiltrado = primerAgente;
                }
                else
                {
                    agentesInteresadoFiltrados.Insert(0, primerAgente);
                    this._ventana.AgenteApoderadoFiltrados = this._agentesApoderados;
                    this._ventana.AgenteApoderadoFiltrado = primerAgente;
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

        public bool CambiarApoderado()
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

                if (!((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals(""))
                {
                    if (((Interesado)this._ventana.InteresadoActual).Id != int.MinValue)
                    {
                        //if (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue)
                        if ((this._ventana.PoderFiltrado != null) && (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue))
                        {
                            this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                            this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                            this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
                            //--
                            Poder primerPoder = new Poder();
                            primerPoder.Id = int.MinValue;

                            _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.AgenteApoderadoFiltrado, (Interesado)this._ventana.InteresadoActual);

                            if (_poderesFiltrados.Count != 0)
                            {
                                poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderFiltrado);

                                if (poderABuscar != null)
                                {
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesFiltrados = _poderesFiltrados;
                                    this._ventana.PoderFiltrado = poderABuscar;
                                }
                                else
                                {
                                    this._ventana.Mensaje("Seleccione un poder que relacione al Apoderado con Interesado Actual", 0);
                                    _poderesFiltrados.Insert(0, primerPoder);
                                    this._ventana.PoderesFiltrados = _poderesFiltrados;

                                }
                            }

                            else
                            {
                                this._ventana.Mensaje("Apoderado no posee poderes con el interesado", 0);
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesFiltrados = _poderesFiltrados;
                            }


                            //--
                            retorno = true;
                        }
                        else
                        {
                            //this._poderesApoderado = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.AgenteApoderadoFiltrado));
                            this._poderesApoderado = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)_ventana.AgenteApoderadoFiltrado, (Interesado)this._ventana.InteresadoActual);

                            LimpiarListaPoder();

                            listaDePoderesValidada = this.ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado);

                            //if ((this.ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado)))
                            if(listaDePoderesValidada)
                            {
                                
                                this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                                this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                                this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
                                retorno = true;
                            }
                            //else if (!this.ValidarListaDePoderes(this._poderesInteresadoActual, this._poderesApoderado))
                            else
                            {
                                
                                this._ventana.ConvertirEnteroMinimoABlanco();
                                this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "Actual"), 0);
                                this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                                this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                                this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
                                retorno = true;
                            }
                        }
                    }
                    else
                    {
                        if ((this._ventana.PoderFiltrado != null) && (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue))
                        {
                            this._ventana.Mensaje("Seleccione un Interesado Actual", 0);
                            this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                            this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                            this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
                            retorno = true;
                        }
                        else
                        {
                            this._ventana.Mensaje("Seleccione un Interesado Actual", 0);
                            //this._poderesApoderado = this._poderServicios.ConsultarPoderesPorAgente(((Agente)_ventana.AgenteApoderadoFiltrado));
                            this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                            this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                            this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;
                    this._ventana.NombreAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Nombre;
                    this._ventana.IdAgenteApoderado = ((Agente)this._ventana.AgenteApoderadoFiltrado).Id;
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

        public bool VerificarCambioAgente()
        {
            bool retorno = false;

            if (!(((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals("")) || (((Interesado)this._ventana.InteresadoActualFiltrado).Id != int.MinValue))
                retorno = true;

            return retorno;
        }

        public void LimpiarListaAgente()
        {
            Agente primerAgente = new Agente("");
            IList<Agente> listaAgentes = new List<Agente>();
            listaAgentes.Add(primerAgente);

            this._ventana.AgenteApoderadoFiltrados = listaAgentes;
            this._ventana.AgenteApoderadoFiltrado = BuscarAgente(listaAgentes, primerAgente);
            this._ventana.AgenteApoderado = this._ventana.AgenteApoderadoFiltrado;

        }

        #endregion

        #region Poder

        private void CargarPoder()
        {
            Poder primerPoder = new Poder(int.MinValue);

            this._poderes = new List<Poder>();
            this._poderes.Add(primerPoder);

            if (((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).Poder != null)
            {
                this._poderes.Add((Poder)this._ventana.Poder);
                this._ventana.PoderesFiltrados = this._poderes;
                this._ventana.PoderFiltrado = this.BuscarPoder((IList<Poder>)this._ventana.PoderesFiltrados, (Poder)this._ventana.Poder);
            }
            else
            {
                this._ventana.PoderesFiltrados = this._poderes;
                this._ventana.PoderFiltrado = this.BuscarPoder(this._poderes, this._poderes[0]);
            }


        }

        public void ConsultarPoderes()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Poder primerPoder = new Poder(int.MinValue);


                Poder poder = new Poder();
                IList<Poder> poderesFiltrados;

                if (!this._ventana.IdPoderFiltrar.Equals(""))
                    poder.Id = int.Parse(this._ventana.IdPoderFiltrar);
                else
                    poder.Id = 0;

                if (!this._ventana.FechaPoderFiltrar.Equals(""))
                    poder.Fecha = DateTime.Parse(this._ventana.FechaPoderFiltrar);

                if ((!poder.Fecha.Equals("")) || (poder.Id != 0))
                    poderesFiltrados = this._poderServicios.ObtenerPoderesFiltro(poder);
                else
                    poderesFiltrados = new List<Poder>();

                if (poderesFiltrados.ToList<Poder>().Count != 0)
                {
                    poderesFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesFiltrados = poderesFiltrados;
                    this._ventana.PoderFiltrado = primerPoder;
                }
                else
                {
                    poderesFiltrados.Insert(0, primerPoder);
                    this._ventana.PoderesFiltrados = this._poderes;
                    this._ventana.PoderFiltrado = primerPoder;
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

        public bool CambiarPoder()
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

                if (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue)
                {
                    if (((Agente)this._ventana.AgenteApoderadoFiltrado).Id.Equals(""))
                    {
                        if (((Interesado)this._ventana.InteresadoActualFiltrado).Id != int.MinValue)
                        {
                            LimpiarListaAgente();

                            LlenarListaAgente((Poder)this._ventana.PoderFiltrado);

                            this._ventana.Poder = this._ventana.PoderFiltrado;
                            this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
                            retorno = true;

                        }
                        else
                        {
                            LimpiarListaInteresado();

                            LimpiarListaAgente();

                            LlenarListaAgenteEInteresado((Poder)this._ventana.PoderFiltrado, false);

                            this._ventana.Poder = this._ventana.PoderFiltrado;
                            this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                    else
                    {
                        //El Agente es diferente a Vacio
                        //--

                        Poder primerPoder = new Poder();
                        primerPoder.Id = int.MinValue;
                        _poderesFiltrados = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.AgenteApoderadoFiltrado, (Interesado)this._ventana.InteresadoActualFiltrado);
                        if (_poderesFiltrados.Count != 0)
                        {
                            poderABuscar = this.BuscarPoder(_poderesFiltrados, (Poder)this._ventana.PoderFiltrado);

                            if (poderABuscar != null)
                            {
                                _poderesFiltrados.Insert(0, primerPoder);
                                this._ventana.PoderesFiltrados = _poderesFiltrados;
                                this._ventana.PoderFiltrado = poderABuscar;
                                this._ventana.Poder = this._ventana.PoderFiltrado;
                                this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
                                retorno = true;
                            }
                            else
                            {
                                //this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorAgenteNoPoseePoderConInteresado, "interesado"), 0);
                                this._ventana.Mensaje("El Poder no pertenece al Interesado", 0);
                                this._ventana.Poder = this._ventana.PoderFiltrado;
                                this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
                                retorno = true;
                            }
                        }

                        //--
                        else
                        {
                            this._ventana.Mensaje(string.Format(Recursos.MensajesConElUsuario.ErrorPoderFiltradoNoAsociadoInteresado), 0);
                            this._ventana.Poder = this._ventana.PoderFiltrado;
                            this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
                            retorno = true;
                        }
                    }
                }
                else
                {
                    this._ventana.Poder = this._ventana.PoderFiltrado;
                    this._ventana.IdPoder = ((Poder)this._ventana.PoderFiltrado).Id.ToString();
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

        public void LlenarListasPoderes(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            if (cambioDeDomicilio.InteresadoActual != null)
                this._poderesInteresadoActual = this._poderServicios.ConsultarPoderesPorInteresado(cambioDeDomicilio.InteresadoActual);

            if (cambioDeDomicilio.Agente != null)
                //this._poderesApoderado = this._poderServicios.ConsultarPoderesPorAgente(cambioDeDomicilio.Agente);
                this._poderesApoderado = this._poderServicios.ObtenerPoderesEntreAgenteEInteresado(cambioDeDomicilio.Agente, cambioDeDomicilio.InteresadoActual);
        }

        public bool ValidarListaDePoderes(IList<Poder> listaPoderesA, IList<Poder> listaPoderesB)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            bool retorno = false;
            IList<Poder> listaIntereseccionInteresado = new List<Poder>();
            Poder primerPoder = new Poder(int.MinValue);

            Poder poderActual = new Poder();

            listaIntereseccionInteresado.Add(primerPoder);


            if ((listaPoderesA.Count != 0) && (listaPoderesB.Count != 0))
            {
                foreach (Poder poderA in listaPoderesA)
                {
                    foreach (Poder poderB in listaPoderesB)
                    {
                        if (poderA.Id == poderB.Id)
                        {
                            listaIntereseccionInteresado.Add(poderA);
                            retorno = true;
                        }

                    }

                }


                if (listaIntereseccionInteresado.Count != 0)
                {
                    poderActual = (Poder)this._ventana.PoderFiltrado;
                    this._poderesInterseccion = listaIntereseccionInteresado;
                    this._ventana.PoderesFiltrados = listaIntereseccionInteresado;
                    this._ventana.PoderFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesFiltrados, poderActual);

                    if (this._ventana.PoderFiltrado == null)
                    {
                        this._ventana.PoderFiltrado = BuscarPoder((IList<Poder>)this._ventana.PoderesFiltrados, new Poder(int.MinValue));
                    }
                }
                else
                    retorno = false;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        public bool VerificarCambioPoder()
        {
            bool retorno = false;

            if (((Poder)this._ventana.PoderFiltrado).Id != int.MinValue)
                retorno = true;

            return retorno;
        }

        public void LimpiarListaPoder()
        {
            Poder primerPoder = new Poder(int.MinValue);
            IList<Poder> listaPoderes = new List<Poder>();
            listaPoderes.Add(primerPoder);

            this._ventana.PoderesFiltrados = listaPoderes;
            this._ventana.PoderFiltrado = BuscarPoder(listaPoderes, primerPoder);
            this._ventana.Poder = this._ventana.PoderFiltrado;

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
                throw ex;
            }
        }

        private void ImprimirPlanilla()
        {
            if (ValidarPatenteAntesDeImprimirPlanilla())
            {
                string paqueteProcedimiento = "PCK_MYP_PDOMICILIOS";
                string procedimiento = "P1";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanilla);
            }
        }

        private void ImprimirPlanillaVienen()
        {
            if (ValidarPatenteAntesDeImprimirPlanillaVienen())
            {
                string paqueteProcedimiento = "PCK_MYP_PDOMICILIOS";
                string procedimiento = "P4";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVienen);
            }
        }

        private void ImprimirPlanillaVan()
        {
            if (ValidarPatenteAntesDeImprimirPlanillaVan())
            {
                string paqueteProcedimiento = "PCK_MYP_PDOMICILIOS";
                string procedimiento = "P3";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnPlanillaVan);
            }
        }

        private void ImprimirAnexo()
        {
            if (ValidarPatenteAntesDeImprimirAnexo())
            {
                string paqueteProcedimiento = "PCK_MYP_PDOMICILIOS";
                string procedimiento = "P2";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((CambioDeDomicilioPatente)this._ventana.CambioDeDomicilioPatente).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexo);
            }
        }

        private bool ValidarPatenteAntesDeImprimirPlanilla()
        {
            return true;
        }

        private bool ValidarPatenteAntesDeImprimirPlanillaVan()
        {
            return true;
        }

        private bool ValidarPatenteAntesDeImprimirPlanillaVienen()
        {
            return true;
        }

        private bool ValidarPatenteAntesDeImprimirAnexo()
        {
            return true;
        }
    }
}
