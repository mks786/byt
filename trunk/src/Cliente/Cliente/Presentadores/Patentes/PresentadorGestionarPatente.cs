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

        private bool _agregar;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarPatente(IGestionarPatente ventana, object patente)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;

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

                    this._ventana.ActivarControlesAlAgregar();
                }


                this._ventana.Patente = patente;

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

                if (this._agregar == false)
                {
                    Patente patente = (Patente)this._ventana.Patente;

                    InfoAdicional infoAdicional = new InfoAdicional("P." + patente.Id);

                    patente.InfoAdicional = this._infoAdicionalServicios.ConsultarPorId(infoAdicional);
                    patente.Operaciones = this._operacionServicios.ConsultarOperacionesPorPatente(patente);

                    IList<ListaDatosDominio> tipoPatente = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoPatente));
                    ListaDatosDominio primerTipoPatente = new ListaDatosDominio();
                    primerTipoPatente.Id = "NGN";
                    tipoPatente.Insert(0, primerTipoPatente);
                    this._ventana.TiposPatenteSolicitud = tipoPatente;
                    this._ventana.TipoPatenteSolicitud = this.BuscarTipoPatente(patente.Tipo, tipoPatente);

                    this._ventana.TiposPatenteDatos = tipoPatente;
                    this._ventana.TipoPatenteDatos = this.BuscarTipoPatente(patente.Tipo, tipoPatente);

                    IList<ListaDatosDominio> presentacionPatente = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiPresentacionPatente));
                    ListaDatosDominio primeraPresentacionPatente = new ListaDatosDominio();
                    primeraPresentacionPatente.Id = "NGN";
                    presentacionPatente.Insert(0, primeraPresentacionPatente);
                    this._ventana.PresentacionesPatenteSolicitud = presentacionPatente;
                    this._ventana.PresentacionPatenteSolicitud = this.BuscarPresentacionPatente(patente.Presentacion, presentacionPatente);

                    this._ventana.PresentacionesPatenteDatos = presentacionPatente;
                    this._ventana.PresentacionPatenteDatos = this.BuscarPresentacionPatente(patente.Presentacion, presentacionPatente);

                    IList<Pais> paises = this._paisServicios.ConsultarTodos();
                    Pais primerPais = new Pais();
                    primerPais.Id = int.MinValue;
                    paises.Insert(0, primerPais);
                    this._ventana.PaisesSolicitud = paises;
                    this._ventana.PaisSolicitud = this.BuscarPais(paises, patente.Pais);

                    this._ventana.PaisesDatos = paises;
                    this._ventana.PaisDatos = this.BuscarPais(paises, patente.Pais);

                    this._ventana.PoderDatosFiltrar = patente.Poder;
                    this._ventana.PoderSolicitudFiltrar = patente.Poder;

                    this._ventana.NumPoderSolicitud = patente.Poder != null ? patente.Poder.NumPoder : "";

                    IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                    StatusWeb primerStatus = new StatusWeb();
                    primerStatus.Id = "NGN";
                    statusWebs.Insert(0, primerStatus);
                    this._ventana.StatusesWebDatos = statusWebs;
                    this._ventana.StatusWebDatos = this.BuscarStatusWeb(statusWebs, patente.StatusWeb);

                    IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                    TipoEstado primerDetalle = new TipoEstado();
                    primerDetalle.Id = "NGN";
                    tipoEstados.Insert(0, primerDetalle);
                    this._ventana.DetallesDatos = tipoEstados;
                    this._ventana.DetalleDatos = BuscarDetalle((IList<TipoEstado>)this._ventana.DetallesDatos, patente.TipoEstado);


                    IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                    Servicio primerServicio = new Servicio();
                    primerServicio.Id = "NGN";
                    servicios.Insert(0, primerServicio);
                    this._ventana.SituacionesDatos = servicios;
                    this._ventana.SituacionDatos = this.BuscarServicio(servicios, patente.Servicio);


                    IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                    Boletin primerBoletin = new Boletin();
                    primerBoletin.Id = int.MinValue;
                    boletines.Insert(0, primerBoletin);
                    this._ventana.BoletinesOrdenPublicacionDatos = boletines;
                    this._ventana.BoletinesPublicacionDatos = boletines;
                    this._ventana.BoletinesConcesionDatos = boletines;
                    this._ventana.BoletinConcesionDatos = this.BuscarBoletin(boletines, patente.BoletinConcesion);
                    this._ventana.BoletinPublicacionDatos = this.BuscarBoletin(boletines, patente.BoletinPublicacion);
                    this._ventana.BoletinOrdenPublicacionDatos = this.BuscarBoletin(boletines, patente.BoletinOrdenPublicacion);

                    //if (File.Exists(ConfigurationManager.AppSettings["RutaImagenesDeMarcas"] + patente.Id + ".jpg"))
                    //{
                    //    patente.BEtiqueta = true;
                    //    this._ventana.PintarEtiqueta();

                    //}

                    this._abandonos = this._operacionServicios.ConsultarOperacionesPorPatente(patente);
                    this._ventana.AbandonoDatos = this._abandonos != null ? this._abandonos[0].Descripcion + " - "
                                                    + ((DateTime)this._abandonos[0].Fecha).ToShortDateString() : "";

                    this._anualidades = this._patenteServicios.ObtenerPatentesFiltro(patente).Last().Anualidades;

                    if ((null != this._anualidades) && (this._anualidades[0].QAnualidad != null) && (this._anualidades[0].FechaAnualidad != null))
                        this._ventana.AnualidadDatos = this._anualidades[0].QAnualidad + 1 + " - "
                                                        + ((DateTime)this._anualidades[0].FechaAnualidad).AddYears(1).ToShortDateString();


                    _inventores = this._inventorServicios.ConsultarInventoresPorPatente(patente);
                    patente.InfoBoles = this._infoBolServicios.ConsultarInfoBolesPorPatente(patente);

                    Auditoria auditoria = new Auditoria();
                    auditoria.Fk = ((Patente)this._ventana.Patente).Id;
                    auditoria.Tabla = "MYP_PATENTES";
                    this._auditorias = this._patenteServicios.AuditoriaPorFkyTabla(auditoria);

                    if (null != patente.InfoAdicional && !string.IsNullOrEmpty(patente.InfoAdicional.Id))
                        this._ventana.PintarInfoAdicionalSolicitud();

                    if (null != patente.InfoBoles && patente.InfoBoles.Count > 0)
                        this._ventana.PintarInfoBolDatos();

                    if ((null != this._inventores) && (this._inventores.Count > 0))
                    {
                        this._ventana.PintarInventoresDatos();
                        this._ventana.PintarInventoresSolicitud();
                    }

                    if ((null != this._auditorias) && (this._auditorias.Count > 0))
                        this._ventana.PintarAuditoriaDatos();
                }
                else
                {
                    this.CargarTipos();
                    this.CargarPresentaciones();
                    this.CargarPaises();
                    this.CargarSituaciones();
                    this.CargarDetalles();
                    this.CargarStatusWeb();
                    this.CargarBoletines();
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
            primerServicio.Id = "NGN";
            servicios.Insert(0, primerServicio);
            this._ventana.SituacionesDatos = servicios;
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

            this.Navegar(new ConsultarPatentes());

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

            patente.Operacion = "MODIFY";

            if (null != this._ventana.AgenteSolicitudFiltrar)
                patente.Agente = !((Agente)this._ventana.AgenteSolicitudFiltrar).Id.Equals("NGN") ? (Agente)this._ventana.AgenteSolicitudFiltrar : null;

            if (null != this._ventana.AsociadoSolicitud)
                patente.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            if (null != this._ventana.InteresadoSolicitud)
                patente.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            if (null != this._ventana.PoderSolicitudFiltrar)
                patente.Poder = !((Poder)this._ventana.PoderSolicitudFiltrar).Id.Equals("NGN") ? ((Poder)this._ventana.PoderSolicitudFiltrar) : null;

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

                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del Pais
                else
                {
                    Patente patente = CargarPatenteDeLaPantalla();

                    bool exitoso = this._patenteServicios.InsertarOModificar(patente, UsuarioLogeado.Hash);

                    if (exitoso)
                        this.Navegar(Recursos.MensajesConElUsuario.PatenteModificada, false);
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
            //this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["RutaBatEscrito"].ToString()
            //            + "\\" + ConfigurationManager.AppSettings["EscritoConsignacionDeJuramento"].ToString(),
            //            this._ventana.Fecha + " " + ((Agente)this._ventana.AgenteFiltrado).Id + " " + parametroPatentes);
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

        }

        /// <summary>
        /// Método que se encarga de llamar a los titulos en el servidor
        /// </summary>
        public void VerTitulo()
        {

        }

        /// <summary>
        /// Método que se encarga de llamar a las solicitudes en el servidor
        /// </summary>
        public void VerSolicitud()
        {

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
                    //this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
                    //this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    //this._ventana.IdAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Id.ToString();

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

            IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            if (filtrarEn == 0)
            {

                if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
                                         select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitudFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Nombre != null &&
                                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitudFiltrar.ToLower())
                                         select p;
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(this._ventana.IdAsociadoDatosFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Id == int.Parse(this._ventana.IdAsociadoDatosFiltrar)
                                         select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoDatosFiltrar))
                {
                    asociadosFiltrados = from p in asociadosFiltrados
                                         where p.Nombre != null &&
                                         p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoDatosFiltrar.ToLower())
                                         select p;
                }
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosSolicitud = this._asociados;
            }
            else
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosDatos = this._asociados;
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
            IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            Asociado primerAsociado = new Asociado();
            primerAsociado.Id = int.MinValue;
            asociados.Insert(0, primerAsociado);
            this._ventana.AsociadosSolicitud = asociados;
            this._ventana.AsociadosDatos = asociados;
            this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, patente.Asociado);
            this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, patente.Asociado);
            this._ventana.NombreAsociadoDatos = null != this._ventana.Patente ? ((Patente)this._ventana.Patente).Asociado.Nombre : "";
            this._ventana.NombreAsociadoSolicitud = null != this._ventana.Patente ? ((Patente)this._ventana.Patente).Asociado.Nombre : "";
            this._asociados = asociados;
            this._ventana.AsociadosEstanCargados = true;

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
                    //this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    //this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    //this._ventana.IdInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Id.ToString();
                    this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                    this._ventana.InteresadoEstadoSolicitud = interesadoAux.Estado != null ? interesadoAux.Estado : "";
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

                //if ((Interesado)this._ventana.InteresadoDatos != null)
                //{
                //    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
                //    this._ventana.InteresadoDatos = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);
                //    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                //    this._ventana.InteresadoSolicitud = (Interesado)this._ventana.InteresadoDatos;
                //    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                //    this._ventana.IdInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();
                //    this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                //    this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";
                //}

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

            IEnumerable<Interesado> interesadosFiltrados = this._interesados;

            if (filtrarEn == 0)
            {
                if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
                                           select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitudFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Nombre != null &&
                                           p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitudFiltrar.ToLower())
                                           select p;
                }
            }
            else
            {
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
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                else
                    this._ventana.InteresadosSolicitud = this._interesados;
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
            IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);
            this._ventana.InteresadosDatos = interesados;
            this._ventana.InteresadosSolicitud = interesados;
            this._interesados = interesados;

            if (this._agregar == false)
            {
                ((Patente)this._ventana.Patente).Interesado = this.BuscarInteresado(interesados, patente.Interesado);

                Interesado interesado = this.BuscarInteresado(interesados, patente.Interesado);
                this._ventana.InteresadoSolicitud = interesado;
                this._ventana.InteresadoDatos = interesado;
                interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);
                this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                this._ventana.InteresadoEstadoSolicitud = interesado.Ciudad;
                this._ventana.NombreInteresadoDatos = null != this._ventana.Patente ? ((Patente)this._ventana.Patente).Interesado.Nombre : "";
                this._ventana.NombreInteresadoSolicitud = null != this._ventana.Patente ? ((Patente)this._ventana.Patente).Interesado.Nombre : "";

            }

            this._ventana.InteresadosEstanCargados = true;

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
                }

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

                //if ((Poder)this._ventana.PoderDatos != null)
                //{
                //    this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderDatos).NumPoder;
                //    this._ventana.PoderSolicitud = (Poder)this._ventana.PoderDatos;
                //    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderDatos).NumPoder;
                //}

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

            Patente patente = null != this._ventana.Patente ? (Patente)this._ventana.Patente : new Patente();
            IList<Poder> poderes = this._poderServicios.ConsultarTodos();
            Poder poder = new Poder();
            poder.Id = int.MinValue;
            poderes.Insert(0, poder);
            this._ventana.PoderesDatosFiltrar = poderes;
            this._ventana.PoderesSolicitudFiltrar = poderes;

            if (this._agregar == false)
            {
                poder = this.BuscarPoder(poderes, patente.Poder);
                this._ventana.PoderSolicitud = poder != null ? poder.Id.ToString() : "";
                //this._ventana.PoderSolicitud = poder.Id.ToString();
                this._ventana.NumPoderSolicitud = poder != null ? poder.NumPoder : "";

                this._ventana.PoderDatos = poder != null ? poder.Id.ToString() : "";

            }

            this._ventana.PoderesEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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

        //public void MostrarEtiqueta()
        //{
        //    Patente marcaAux = ((Patente)this._ventana.Patente);
        //    if (((Patente)this._ventana.Patente).BEtiqueta)
        //    {
        //            EtiquetaMarca detalleEtiqueta = new EtiquetaMarca(ConfigurationManager.AppSettings["RutaImagenesDeMarcas"] + marcaAux.Id + ".jpg", marcaAux.Descripcion);
        //            detalleEtiqueta.ShowDialog();

        //    }
        //}

    }
}