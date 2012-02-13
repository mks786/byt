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
       
        private IList<Interesado> _interesadosCedente;
        private IList<Interesado> _interesadosCesionario;
        private IList<Agente> _agentesCesionario;
        private IList<Agente> _agentesCedente;        
        private IList<Marca> _marcas;
        private IList<Poder> _poderesCedente;
        private IList<Poder> _poderesCesionario;
       
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarCesion(IGestionarCesion ventana, object cesion)
        {
            try
            {

                this._ventana = ventana;

                //if (((Marca)fusion).Internacional == null)
                //    ((Marca)fusion).Internacional = new Internacional();
                //else
                //    ((Marca)fusion).Internacional = new Internacional(((Marca)fusion).Internacional.Id);

                //if (((Marca)fusion).Nacional == null)
                //    ((Marca)fusion).Nacional = new Nacional();
                //else
                //    ((Marca)fusion).Nacional = new Nacional(((Marca)fusion).Nacional.Id);

                this._ventana.Cesion = cesion;

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

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarca, "");

                Cesion cesion = (Cesion)this._ventana.Cesion;
               

                this._ventana.Marca = this._marcaServicios.ConsultarMarcaConTodo(cesion.Marca);
                this._ventana.InteresadoCedente = this._interesadoServicios.ConsultarInteresadoConTodo(cesion.Cedente);
                
                this._ventana.NombreMarca = ((Marca)this._ventana.Marca).Descripcion;
                
                this._ventana.NombreCedente = ((Interesado)this._ventana.InteresadoCedente).Nombre;            
                this._ventana.ApoderadoCedente = cesion.AgenteCedente;
                this._ventana.ApoderadoCesionario = cesion.AgenteCesionario;
                

                this._marcas = new List<Marca>();
                this._interesadosCedente = new List<Interesado>();
                this._agentesCedente = new List<Agente>();

                this._marcas.Add((Marca)this._ventana.Marca);
                this._interesadosCedente.Add((Interesado)this._ventana.InteresadoCedente);               
                
                this._ventana.MarcasFiltradas = this._marcas;
                this._ventana.CedentesFiltrados = this._interesadosCedente;                

                //fusion.InfoBoles = this._infoBolServicios.ConsultarInfoBolesPorMarca(fusion);
                //fusion.Operaciones = this._operacionServicios.ConsultarOperacionesPorMarca(fusion);
                //fusion.Busquedas = this._busquedaServicios.ConsultarBusquedasPorMarca(fusion);

                //fusion.InfoAdicional = this._infoAdicionalServicios.ConsultarPorId(infoAdicional);
                //fusion.Anaqua = this._anaquaServicios.ConsultarPorId(anaqua);

                //IList<ListaDatosDominio> tiposMarcas = this._listaDatosDominioServicios.
                //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));
                //ListaDatosDominio primerTipoMarca = new ListaDatosDominio();
                //primerTipoMarca.Id = "NGN";
                //tiposMarcas.Insert(0, primerTipoMarca);
                //this._ventana.TipoMarcasDatos = tiposMarcas;
                //this._ventana.TipoMarcasSolicitud = tiposMarcas;
                //this._ventana.TipoMarcaDatos = this.BuscarTipoMarca(tiposMarcas, marca.Tipo);

                //IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
                //Agente primerAgente = new Agente();
                //primerAgente.Id = "NGN";
                //agentes.Insert(0, primerAgente);
                //this._ventana.Agentes = agentes;
                //this._ventana.Agente = this.BuscarAgente(agentes, marca.Agente);

                //IList<Pais> paises = this._paisServicios.ConsultarTodos();
                //Pais primerPais = new Pais();
                //primerPais.Id = int.MinValue;
                //paises.Insert(0, primerPais);
                //this._ventana.PaisesSolicitud = paises;
                //this._ventana.PaisSolicitud = this.BuscarPais(paises, marca.Pais);

                //IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                //StatusWeb primerStatus = new StatusWeb();
                //primerStatus.Id = "NGN";
                //statusWebs.Insert(0, primerStatus);
                //this._ventana.StatusWebs = statusWebs;
                //this._ventana.StatusWeb = this.BuscarStatusWeb(statusWebs, marca.StatusWeb);

                //IList<Condicion> condiciones = this._condicionServicios.ConsultarTodos();
                //Condicion primeraCondicion = new Condicion();
                //primeraCondicion.Id = int.MinValue;
                //condiciones.Insert(0, primeraCondicion);
                //this._ventana.Condiciones = condiciones;

                //IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                //TipoEstado primerDetalle = new TipoEstado();
                //primerDetalle.Id = "NGN";
                //tipoEstados.Insert(0, primerDetalle);
                //this._ventana.Detalles = tipoEstados;

                //IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                //Servicio primerServicio = new Servicio();
                //primerServicio.Id = "NGN";
                //servicios.Insert(0, primerServicio);
                //this._ventana.Servicios = servicios;
                //this._ventana.Servicio = this.BuscarServicio(servicios, marca.Servicio);


                //IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                //Boletin primerBoletin = new Boletin();
                //primerBoletin.Id = int.MinValue;
                //boletines.Insert(0, primerBoletin);
                //this._ventana.BoletinesOrdenPublicacion = boletines;
                //this._ventana.BoletinesPublicacion = boletines;
                //this._ventana.BoletinConcesion = boletines;
                //this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, marca.BoletinConcesion);
                //this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, marca.BoletinPublicacion);

                //Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marca.Interesado));
                //this._ventana.NombreInteresadoDatos = interesado.Nombre;
                //this._ventana.NombreInteresadoSolicitud = interesado.Nombre;
                //this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                //this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
                ////this._ventana.InteresadoSolicitud = marca.Interesado;

                //this._ventana.NombreAsociadoDatos = marca.Asociado != null ? marca.Asociado.Nombre : "";
                //this._ventana.NombreAsociadoSolicitud = marca.Asociado != null ? marca.Asociado.Nombre : "";

                //this._ventana.DescripcionCorresponsalSolicitud = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";
                //this._ventana.DescripcionCorresponsalDatos = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";

                //this._ventana.NumPoderDatos = marca.Poder != null ? marca.Poder.NumPoder : "";
                //this._ventana.NumPoderSolicitud = marca.Poder != null ? marca.Poder.NumPoder : "";

                //IList<ListaDatosDominio> sectores = this._listaDatosDominioServicios.
                //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiSector));
                //ListaDatosDominio primerSector = new ListaDatosDominio();
                //primerSector.Id = "NGN";
                //sectores.Insert(0, primerSector);
                //this._ventana.Sectores = sectores;
                //this._ventana.Sector = this.BuscarSector(sectores, marca.Sector);

                //IList<ListaDatosDominio> tipoReproducciones = this._listaDatosDominioServicios.
                //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoReproduccion));
                //ListaDatosDominio primerTipoReproduccion = new ListaDatosDominio();
                //primerTipoReproduccion.Id = "NGN";
                //tipoReproducciones.Insert(0, primerTipoReproduccion);
                //this._ventana.TipoReproducciones = tipoReproducciones;
                //this._ventana.TipoReproduccion = this.BuscarTipoReproduccion(tipoReproducciones, marca.Tipo);

                //Auditoria auditoria = new Auditoria();
                //auditoria.Fk = ((Marca)this._ventana.Marca).Id;
                //auditoria.Tabla = "MYP_MARCAS";
                //this._auditorias = this._marcaServicios.AuditoriaPorFkyTabla(auditoria);

                //if (null != marca.InfoAdicional && !string.IsNullOrEmpty(marca.InfoAdicional.Id))
                //    this._ventana.PintarInfoAdicional();

                //if (null != marca.Anaqua)
                //    this._ventana.PintarAnaqua();

                //if (null != marca.InfoBoles && marca.InfoBoles.Count > 0)
                //    this._ventana.PintarInfoBoles();

                //if (null != marca.Operaciones && marca.Operaciones.Count > 0)
                //    this._ventana.PintarOperaciones();

                //if (null != marca.Busquedas && marca.Busquedas.Count > 0)
                //    this._ventana.PintarBusquedas();

                //if (null != this._auditorias && this._auditorias.Count > 0)
                //    this._ventana.PintarAuditoria();

                //this._ventana.BorrarCeros();

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

        public void IrConsultarMarcas() 
        { 
            this.Navegar(new ConsultarMarcas());
        }

        public Cesion CargarCesionDeLaPantalla()
        {

            Cesion cesion = (Cesion)this._ventana.Cesion;

            //marca.Operacion = "MODIFY";

            //if (null != this._ventana.Agente)
            //    marca.Agente = !((Agente)this._ventana.Agente).Id.Equals("NGN") ? (Agente)this._ventana.Agente : null;

            //if (null != this._ventana.AsociadoSolicitud)
            //    marca.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            //if (null != this._ventana.BoletinConcesion)
            //    marca.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

            //if (null != this._ventana.BoletinPublicacion)
            //    marca.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

            //if (null != this._ventana.InteresadoSolicitud)
            //    marca.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            //if (null != this._ventana.Servicio)
            //    marca.Servicio = !((Servicio)this._ventana.Servicio).Id.Equals("NGN") ? ((Servicio)this._ventana.Servicio) : null;

            //if (null != this._ventana.PoderSolicitud)
            //    marca.Poder = !((Poder)this._ventana.PoderSolicitud).Id.Equals("NGN") ? ((Poder)this._ventana.PoderSolicitud) : null;

            //if (null != this._ventana.PaisSolicitud)
            //    marca.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

            //if (null != this._ventana.StatusWeb)
            //    marca.StatusWeb = ((StatusWeb)this._ventana.StatusWeb).Id.Equals("NGN") ? ((StatusWeb)this._ventana.StatusWeb) : null;

            //if (null != this._ventana.CorresponsalSolicitud)
            //    marca.Corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? ((Corresponsal)this._ventana.CorresponsalSolicitud) : null;

            //if (null != this._ventana.Sector)
            //    marca.Sector = !((ListaDatosDominio)this._ventana.Sector).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.Sector).Id : null;

            //if (null != this._ventana.TipoReproduccion)
            //    marca.TipoRps = ((ListaDatosDominio)this._ventana.TipoReproduccion).Id[0];

            //if (null != this._ventana.TipoMarcaDatos)
            //    marca.Tipo = !((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id : null;
            
            //if(string.IsNullOrEmpty(this._ventana.IdInternacional))
            //    marca.Internacional = null;

            //if(string.IsNullOrEmpty(this._ventana.IdNacional))
            //    marca.Nacional = null;

            return cesion;
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
                    Cesion cesion = CargarCesionDeLaPantalla();

                    //bool exitoso = this._marcaServicios.InsertarOModificar(fusion, UsuarioLogeado.Hash);

                    //if (exitoso)
                    //    this.Navegar(Recursos.MensajesConElUsuario.MarcaModificada, false);
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
        /// Metodo que se encarga de eliminar una Marca
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if (this._anexoServicios.Eliminar((Anexo)this._ventana.Anexo, UsuarioLogeado.Hash))
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

        //public void IrInfoAdicional(string tab)
        //{
        //    this.Navegar(new GestionarInfoAdicional(CargarMarcaDeLaPantalla(), tab));
        //}

        //public void IrInfoBoles()
        //{

        //    this.Navegar(new ListaInfoBoles(CargarMarcaDeLaPantalla()));
        //}

        //public void IrOperaciones()
        //{
        //    this.Navegar(new ListaOperaciones(CargarMarcaDeLaPantalla()));
        //}

        //public void IrAnaqua()
        //{
        //    this.Navegar(new GestionarAnaqua(CargarMarcaDeLaPantalla()));
        //}
        
        //public void IrBusquedas(string tab)
        //{
        //    this.Navegar(new ListaBusquedas(CargarMarcaDeLaPantalla(), tab));
        //}

        //public void Auditoria()
        //{
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion


        //        this.Navegar(new ListaAuditorias(_auditorias));


        //        #region trace
        //        if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(ex.Message, true);
        //    }
        //    catch (RemotingException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, true);
        //    }
        //    catch (SocketException ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
        //    }
        //}

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

        //#region Metodos de los filtros de asociados

        //public void CambiarAsociadoSolicitud()
        //{
        //    try
        //    {
        //        if ((Asociado)this._ventana.AsociadoSolicitud != null)
        //        {
        //            Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSolicitud);
        //            this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
        //            this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
        //            this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NombreAsociadoSolicitud = "";
        //        this._ventana.NombreAsociadoDatos = "";
        //    }
        //}

        //public void CambiarAsociadoDatos()
        //{
        //    try
        //    {
        //        if ((Asociado)this._ventana.AsociadoDatos != null)
        //        {
        //            Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoDatos);
        //            this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoDatos).Nombre;
        //            this._ventana.AsociadoSolicitud = (Asociado)this._ventana.AsociadoDatos;
        //            this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoDatos).Nombre;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NombreAsociadoSolicitud = "";
        //        this._ventana.NombreAsociadoDatos = "";
        //    }
        //}

        //public void BuscarAsociado(int filtrarEn)
        //{
        //    IEnumerable<Asociado> asociadosFiltrados = this._asociados;

        //    if (filtrarEn == 0)
        //    {

        //        if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
        //        {
        //            asociadosFiltrados = from p in asociadosFiltrados
        //                                 where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
        //                                 select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitudFiltrar))
        //        {
        //            asociadosFiltrados = from p in asociadosFiltrados
        //                                 where p.Nombre != null &&
        //                                 p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitudFiltrar.ToLower())
        //                                 select p;
        //        }
        //    }
        //    else
        //    {

        //        if (!string.IsNullOrEmpty(this._ventana.IdAsociadoDatosFiltrar))
        //        {
        //            asociadosFiltrados = from p in asociadosFiltrados
        //                                 where p.Id == int.Parse(this._ventana.IdAsociadoDatosFiltrar)
        //                                 select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoDatosFiltrar))
        //        {
        //            asociadosFiltrados = from p in asociadosFiltrados
        //                                 where p.Nombre != null &&
        //                                 p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoDatosFiltrar.ToLower())
        //                                 select p;
        //        }
        //    }

        //    // filtrarEn = 0 significa en el listview de la pestaña solicitud
        //    // filtrarEn = 1 significa en el listview de la pestaña Datos 
        //    if (filtrarEn == 0)
        //    {
        //        if (asociadosFiltrados.ToList<Asociado>().Count != 0)
        //            this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
        //        else
        //            this._ventana.AsociadosSolicitud = this._asociados;
        //    }
        //    else
        //    {
        //        if (asociadosFiltrados.ToList<Asociado>().Count != 0)
        //            this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
        //        else
        //            this._ventana.AsociadosDatos = this._asociados;
        //    }
        //}

        //public void CargarAsociados()
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;

        //    Marca marca = (Marca)this._ventana.Marca;
        //    IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
        //    Asociado primerAsociado = new Asociado();
        //    primerAsociado.Id = int.MinValue;
        //    asociados.Insert(0, primerAsociado);
        //    this._ventana.AsociadosSolicitud = asociados;
        //    this._ventana.AsociadosDatos = asociados;
        //    this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, marca.Asociado);
        //    this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marca.Asociado);
        //    this._ventana.NombreAsociadoDatos = ((Marca)this._ventana.Marca).Asociado.Nombre;
        //    this._ventana.NombreAsociadoSolicitud = ((Marca)this._ventana.Marca).Asociado.Nombre;
        //    this._asociados = asociados;
        //    this._ventana.AsociadosEstanCargados = true;

        //    Mouse.OverrideCursor = null;
        //}

        //#endregion

        //#region Metodos de los filtros de interesados

        //public void CambiarInteresadoSolicitud()
        //{
        //    try
        //    {
        //        if ((Interesado)this._ventana.InteresadoSolicitud != null)
        //        {
        //            Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
        //            this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
        //            this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
        //            this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
        //            this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
        //            this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NombreInteresadoSolicitud = "";
        //        this._ventana.NombreInteresadoDatos = "";
        //        this._ventana.InteresadoPaisSolicitud = "";
        //        this._ventana.InteresadoCiudadSolicitud = "";
        //    }
        //}

        //public void CambiarInteresadoDatos()
        //{
        //    try
        //    {
        //        if ((Interesado)this._ventana.InteresadoDatos != null)
        //        {
        //            Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
        //            this._ventana.InteresadoDatos = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);
        //            this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
        //            this._ventana.InteresadoSolicitud = (Interesado)this._ventana.InteresadoDatos;
        //            this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
        //            this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
        //            this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NombreInteresadoSolicitud = "";
        //        this._ventana.NombreInteresadoDatos = "";
        //        this._ventana.InteresadoPaisSolicitud = "";
        //        this._ventana.InteresadoCiudadSolicitud = "";
        //    }
        //}

        //public void BuscarInteresado(int filtrarEn)
        //{
        //    IEnumerable<Interesado> interesadosFiltrados = this._interesados;

        //    if (filtrarEn == 0)
        //    {
        //        if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
        //        {
        //            interesadosFiltrados = from p in interesadosFiltrados
        //                                   where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
        //                                   select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitudFiltrar))
        //        {
        //            interesadosFiltrados = from p in interesadosFiltrados
        //                                   where p.Nombre != null &&
        //                                   p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitudFiltrar.ToLower())
        //                                   select p;
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(this._ventana.IdInteresadoDatosFiltrar))
        //        {
        //            interesadosFiltrados = from p in interesadosFiltrados
        //                                   where p.Id == int.Parse(this._ventana.IdInteresadoDatosFiltrar)
        //                                   select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoDatosFiltrar))
        //        {
        //            interesadosFiltrados = from p in interesadosFiltrados
        //                                   where p.Nombre != null &&
        //                                   p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoDatosFiltrar.ToLower())
        //                                   select p;
        //        }
        //    }

        //    // filtrarEn = 0 significa en el listview de la pestaña solicitud
        //    // filtrarEn = 1 significa en el listview de la pestaña Datos 
        //    if (filtrarEn == 0)
        //    {
        //        if (interesadosFiltrados.ToList<Interesado>().Count != 0)
        //            this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
        //        else
        //            this._ventana.InteresadosSolicitud = this._interesados;
        //    }
        //    else
        //    {
        //        if (interesadosFiltrados.ToList<Interesado>().Count != 0)
        //            this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
        //        else
        //            this._ventana.InteresadosDatos = this._interesados;
        //    }
        //}

        //public void CargarInteresados()
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;
        //    Marca marca = (Marca)this._ventana.Marca;

        //    IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
        //    Interesado primerInteresado = new Interesado();
        //    primerInteresado.Id = int.MinValue;
        //    interesados.Insert(0, primerInteresado);
        //    this._ventana.InteresadosDatos = interesados;
        //    this._ventana.InteresadosSolicitud = interesados;
        //    ((Marca)this._ventana.Marca).Interesado = this.BuscarInteresado(interesados, marca.Interesado);
        //    Interesado interesado = this.BuscarInteresado(interesados, marca.Interesado);
        //    this._ventana.InteresadoSolicitud = interesado;
        //    this._ventana.InteresadoDatos = interesado;
        //    interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);
        //    this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
        //    this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
        //    this._ventana.NombreInteresadoDatos = ((Marca)this._ventana.Marca).Interesado.Nombre;
        //    this._ventana.NombreInteresadoSolicitud = ((Marca)this._ventana.Marca).Interesado.Nombre;
        //    this._interesados = interesados;

        //    this._ventana.InteresadosEstanCargados = true;

        //    Mouse.OverrideCursor = null;
        //}

        //#endregion

        //#region Metodos de los filstros de corresponsales

        //public void CambiarCorresponsalSolicitud()
        //{
        //    try
        //    {
        //        if ((Corresponsal)this._ventana.CorresponsalSolicitud != null)
        //        {
        //            //Corresponsal corresponsal = this._corresponsalServicios.ConsultarCorresponsalConTodo((Corresponsal)this._ventana.CorresponsalSolicitud);
        //            this._ventana.DescripcionCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //            this._ventana.CorresponsalDatos = (Corresponsal)this._ventana.CorresponsalSolicitud;
        //            this._ventana.DescripcionCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.DescripcionCorresponsalDatos = "";
        //        this._ventana.DescripcionCorresponsalSolicitud = "";
        //    }
        //}

        //public void CambiarCorresponsalDatos()
        //{
        //    try
        //    {
        //        if ((Corresponsal)this._ventana.CorresponsalDatos != null)
        //        {
        //            //Corresponsal corresponsal = this._corresponsalServicios.ConsultarCorresponsalConTodo((Corresponsal)this._ventana.CorresponsalDatos);
        //            this._ventana.DescripcionCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalDatos).Descripcion;
        //            this._ventana.CorresponsalSolicitud = (Corresponsal)this._ventana.CorresponsalDatos;
        //            this._ventana.DescripcionCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalDatos).Descripcion;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.DescripcionCorresponsalDatos = "";
        //        this._ventana.DescripcionCorresponsalSolicitud = "";
        //    }
        //}

        //public void BuscarCorresponsal(int filtrarEn)
        //{
        //    IEnumerable<Corresponsal> corresponsalesFiltrados = this._corresponsales;

        //    if (filtrarEn == 0)
        //    {
        //        if (!string.IsNullOrEmpty(this._ventana.IdCorresponsalSolicitudFiltrar))
        //        {
        //            corresponsalesFiltrados = from p in corresponsalesFiltrados
        //                                      where p.Id == int.Parse(this._ventana.IdCorresponsalSolicitudFiltrar)
        //                                      select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.DescripcionCorresponsalSolicitudFiltrar))
        //        {
        //            corresponsalesFiltrados = from p in corresponsalesFiltrados
        //                                      where p.Descripcion != null &&
        //                                      p.Descripcion.ToLower().Contains(this._ventana.DescripcionCorresponsalSolicitudFiltrar.ToLower())
        //                                      select p;
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(this._ventana.IdCorresponsalDatosFiltrar))
        //        {
        //            corresponsalesFiltrados = from p in corresponsalesFiltrados
        //                                      where p.Id == int.Parse(this._ventana.IdCorresponsalDatosFiltrar)
        //                                      select p;
        //        }

        //        if (!string.IsNullOrEmpty(this._ventana.DescripcionCorresponsalDatosFiltrar))
        //        {
        //            corresponsalesFiltrados = from p in corresponsalesFiltrados
        //                                      where p.Descripcion != null &&
        //                                      p.Descripcion.ToLower().Contains(this._ventana.DescripcionCorresponsalDatosFiltrar.ToLower())
        //                                      select p;
        //        }
        //    }

        //    // filtrarEn = 0 significa en el listview de la pestaña solicitud
        //    // filtrarEn = 1 significa en el listview de la pestaña Datos 
        //    if (filtrarEn == 0)
        //    {
        //        if (corresponsalesFiltrados.ToList<Corresponsal>().Count != 0)
        //            this._ventana.CorresponsalesSolicitud = corresponsalesFiltrados.ToList<Corresponsal>();
        //        else
        //            this._ventana.CorresponsalesSolicitud = this._asociados;
        //    }
        //    else
        //    {
        //        if (corresponsalesFiltrados.ToList<Corresponsal>().Count != 0)
        //            this._ventana.CorresponsalesDatos = corresponsalesFiltrados.ToList<Corresponsal>();
        //        else
        //            this._ventana.CorresponsalesDatos = this._corresponsales;
        //    }
        //}

        //public void CargarCorresponsales()
        //{

        //    Mouse.OverrideCursor = Cursors.Wait;

        //    IList<Corresponsal> corresponsales = this._corresponsalServicios.ConsultarTodos();
        //    Corresponsal primerCorresponsal = new Corresponsal();
        //    primerCorresponsal.Id = int.MinValue;
        //    corresponsales.Insert(0, primerCorresponsal);
        //    this._ventana.CorresponsalesSolicitud = corresponsales;
        //    this._ventana.CorresponsalesDatos = corresponsales;
        //    this._ventana.CorresponsalDatos = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);
        //    this._ventana.CorresponsalSolicitud = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);

        //    this._ventana.DescripcionCorresponsalDatos = null == ((Marca)this._ventana.Marca).Corresponsal ?
        //                                                 null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //    this._ventana.DescripcionCorresponsalSolicitud = null == ((Marca)this._ventana.Marca).Corresponsal ?
        //                                                     null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //    this._corresponsales = corresponsales;

        //    this._ventana.CorresponsalesEstanCargados = true;

        //    Mouse.OverrideCursor = null;
        //}

        //#endregion

        //#region Metodos de la lista de poderes

        //public void CambiarPoderSolicitud()
        //{
        //    try
        //    {
        //        if ((Poder)this._ventana.PoderSolicitud != null)
        //        {
        //            this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderSolicitud).NumPoder;
        //            this._ventana.PoderDatos = (Poder)this._ventana.PoderSolicitud;
        //            this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderSolicitud).NumPoder;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NumPoderSolicitud = "";
        //        this._ventana.NumPoderDatos = "";
        //    }
        //}

        //public void CambiarPoderDatos()
        //{
        //    try
        //    {
        //        if ((Poder)this._ventana.PoderDatos != null)
        //        {
        //            this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderDatos).NumPoder;
        //            this._ventana.PoderSolicitud = (Poder)this._ventana.PoderDatos;
        //            this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderDatos).NumPoder;
        //        }
        //    }
        //    catch (ApplicationException e)
        //    {
        //        this._ventana.NumPoderSolicitud = "";
        //        this._ventana.NumPoderDatos = "";
        //    }
        //}

        //public void CargarPoderes()
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;

        //    Marca marca = (Marca)this._ventana.Marca;
        //    IList<Poder> poderes = this._poderServicios.ConsultarTodos();
        //    Poder poder = new Poder();
        //    poder.Id = int.MinValue;
        //    poderes.Insert(0, poder);
        //    this._ventana.PoderesDatos = poderes;
        //    this._ventana.PoderesSolicitud = poderes;
        //    this._ventana.PoderDatos = this.BuscarPoder(poderes, marca.Poder);
        //    this._ventana.PoderSolicitud = this.BuscarPoder(poderes, marca.Poder);

        //    this._ventana.PoderesEstanCargados = true;

        //    Mouse.OverrideCursor = null;
        //}

        //#endregion


        //public void IrImprimir(string nombreBoton)
        //{
        //    try
        //    {
        //        if (nombreBoton.Equals("_btn" + Recursos.Etiquetas.btnFM02))
        //        {
        //            if (validarMarcaAntesDeImprimirFM02())
        //            {
        //                string retorno = this._marcaServicios.ImprimirFM02((Marca)this._ventana.Marca);
        //            }
        //        }
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private bool validarMarcaAntesDeImprimirFM02()
        //{
        //    bool retorno = false;

        //    return true;
        //}        

        public void ConsultarMarcas()
        {

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Marca marca = new Marca();
                IEnumerable<Marca> marcasFiltradas;
                marca.Descripcion = this._ventana.NombreMarcaFiltrar.ToUpper();
                marca.Id = this._ventana.IdMarcaFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaFiltrar);

                if ((!marca.Descripcion.Equals("")) || (marca.Id != 0))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marca);
                else
                    marcasFiltradas = new List<Marca>();

                if (marcasFiltradas.ToList<Marca>().Count != 0)
                    this._ventana.MarcasFiltradas = marcasFiltradas.ToList<Marca>();
                else
                    this._ventana.MarcasFiltradas = this._marcas;

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

        public void ConsultarCedentes()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado cedente = new Interesado();
                IEnumerable<Interesado> cedentesFiltrados;
                cedente.Nombre = this._ventana.NombreCedenteFiltrar.ToUpper();
                cedente.Id = this._ventana.IdCedenteFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCedenteFiltrar);

                if ((!cedente.Nombre.Equals("")) || (cedente.Id != 0))
                    cedentesFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cedente);
                else
                    cedentesFiltrados = new List<Interesado>();

                if (cedentesFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.CedentesFiltrados = cedentesFiltrados.ToList<Interesado>();
                else
                    this._ventana.CedentesFiltrados = this._interesadosCedente;

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

        public void ConsultarApoderadosCedente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoCedente = new Agente();
                IEnumerable<Agente> agentesCedenteFiltrados;
                apoderadoCedente.Nombre = this._ventana.NombreApoderadoCedenteFiltrar.ToUpper();
                apoderadoCedente.Id = this._ventana.IdApoderadoCedenteFiltrar.ToUpper();

                if ((!apoderadoCedente.Nombre.Equals("")) || (!apoderadoCedente.Id.Equals("")))
                    agentesCedenteFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoCedente);
                else
                    agentesCedenteFiltrados = new List<Agente>();

                if (agentesCedenteFiltrados.ToList<Agente>().Count != 0)
                    this._ventana.ApoderadosCedenteFiltrados = agentesCedenteFiltrados.ToList<Agente>();
                else
                    this._ventana.ApoderadosCedenteFiltrados = this._agentesCedente;

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

        public void ConsultarPoderesCedente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderCedente = new Poder();
                IEnumerable<Poder> poderesCedenteFiltrados;
                
                poderCedente.Id = int.Parse(this._ventana.IdPoderCedenteFiltrar);

                if (!this._ventana.FechaPoderCedenteFiltrar.Equals(""))
                    poderCedente.Fecha = DateTime.Parse(this._ventana.FechaPoderCedenteFiltrar);

                if ((!poderCedente.Fecha.Equals("")) || (poderCedente.Id != 0))
                    poderesCedenteFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCedente);
                else
                    poderesCedenteFiltrados = new List<Poder>();

                if (poderesCedenteFiltrados.ToList<Poder>().Count != 0)
                    this._ventana.PoderesCedenteFiltrados = poderesCedenteFiltrados.ToList<Poder>();
                else
                    this._ventana.PoderesCedenteFiltrados = this._poderesCedente;

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

        public void ConsultarCesionarios()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Interesado cesionario = new Interesado();
                IEnumerable<Interesado> cesionariosFiltrados;
                cesionario.Nombre = this._ventana.NombreCesionarioFiltrar.ToUpper();
                cesionario.Id = this._ventana.IdCesionarioFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdCesionarioFiltrar);

                if ((!cesionario.Nombre.Equals("")) || (cesionario.Id != 0))
                    cesionariosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(cesionario);
                else
                    cesionariosFiltrados = new List<Interesado>();

                if (cesionariosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.CesionariosFiltrados = cesionariosFiltrados.ToList<Interesado>();
                else
                    this._ventana.CesionariosFiltrados = this._interesadosCesionario;

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

        public void ConsultarApoderadosCesionario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Agente apoderadoCesionario = new Agente();
                IEnumerable<Agente> agentesCesionarioFiltrados;
                apoderadoCesionario.Nombre = this._ventana.NombreApoderadoCesionarioFiltrar.ToUpper();
                apoderadoCesionario.Id = this._ventana.IdApoderadoCesionarioFiltrar.ToUpper();

                if ((!apoderadoCesionario.Nombre.Equals("")) || (!apoderadoCesionario.Id.Equals("")))
                    agentesCesionarioFiltrados = this._agenteServicios.ObtenerAgentesFiltro(apoderadoCesionario);
                else
                    agentesCesionarioFiltrados = new List<Agente>();

                if (agentesCesionarioFiltrados.ToList<Agente>().Count != 0)
                    this._ventana.ApoderadosCesionarioFiltrados = agentesCesionarioFiltrados.ToList<Agente>();
                else
                    this._ventana.ApoderadosCesionarioFiltrados = this._agentesCesionario;

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

        public void ConsultarPoderesCesionario()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                Poder poderCesionario = new Poder();
                IEnumerable<Poder> poderesCesionarioFiltrados;

                poderCesionario.Id = int.Parse(this._ventana.IdPoderCesionarioFiltrar);

                if (!this._ventana.FechaPoderCesionarioFiltrar.Equals(""))
                    poderCesionario.Fecha = DateTime.Parse(this._ventana.FechaPoderCesionarioFiltrar);

                if ((!poderCesionario.Fecha.Equals("")) || (poderCesionario.Id != 0))
                    poderesCesionarioFiltrados = this._poderServicios.ObtenerPoderesFiltro(poderCesionario);
                else
                    poderesCesionarioFiltrados = new List<Poder>();

                if (poderesCesionarioFiltrados.ToList<Poder>().Count != 0)
                    this._ventana.PoderesCesionarioFiltrados = poderesCesionarioFiltrados.ToList<Poder>();
                else
                    this._ventana.PoderesCesionarioFiltrados = this._poderesCesionario;

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

        public bool CambiarMarca()
        {
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

            return retorno;
        }

        public bool CambiarCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.CedenteFiltrado != null)
                {
                    this._ventana.InteresadoCedente = this._ventana.CedenteFiltrado;
                    this._ventana.NombreCedente = ((Interesado)this._ventana.CedenteFiltrado).Nombre;
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

            return retorno;
        }

        public bool CambiarApoderadoCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.ApoderadoCedenteFiltrado != null)
                {
                    this._ventana.ApoderadoCedente = this._ventana.ApoderadoCedenteFiltrado;
                    this._ventana.NombreApoderadoCedente = ((Agente)this._ventana.ApoderadoCedenteFiltrado).Nombre;
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

            return retorno;
        }

        public bool CambiarPoderCedente()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.PoderCedenteFiltrado != null)
                {
                    this._ventana.PoderCedente = this._ventana.PoderCedenteFiltrado;
                    this._ventana.IdPoderCedente = ((Poder)this._ventana.PoderCedenteFiltrado).Id.ToString();
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

            return retorno;
        }

        public bool CambiarCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.CesionarioFiltrado != null)
                {
                    this._ventana.InteresadoCesionario = this._ventana.CesionarioFiltrado;
                    this._ventana.NombreCesionario = ((Interesado)this._ventana.CesionarioFiltrado).Nombre;
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

            return retorno;
        }

        public bool CambiarPoderCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.PoderCesionarioFiltrado != null)
                {
                    this._ventana.PoderCesionario = this._ventana.PoderCesionarioFiltrado;
                    this._ventana.IdPoderCesionario = ((Poder)this._ventana.PoderCesionarioFiltrado).Id.ToString();
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

            return retorno;
        }                

        public bool CambiarApoderadoCesionario()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if (this._ventana.ApoderadoCesionarioFiltrado != null)
                {
                    this._ventana.ApoderadoCesionario = this._ventana.ApoderadoCesionarioFiltrado;
                    this._ventana.NombreApoderadoCesionario = ((Agente)this._ventana.ApoderadoCesionarioFiltrado).Nombre;
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

            return retorno;
        }
    }
}
