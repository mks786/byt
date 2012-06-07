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
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.ControlesByT.Ventanas;
using System.Text;
using System.IO;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorConsultarMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultarMarca _ventana;

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
        private IPlanillaServicios _planillaServicios;
        private IInternacionalServicios _internacionalServicios;
        private IRenovacionServicios _renovacionServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;
        private IList<Auditoria> _auditorias;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarMarca(IConsultarMarca ventana, object marca)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;

                if (((Marca)marca).Internacional == null)
                    ((Marca)marca).Internacional = new Internacional();
                else
                    ((Marca)marca).Internacional = new Internacional(((Marca)marca).Internacional.Id);

                if (((Marca)marca).Nacional == null)
                    ((Marca)marca).Nacional = new Nacional();
                else
                    ((Marca)marca).Nacional = new Nacional(((Marca)marca).Nacional.Id);

                this._ventana.Marca = marca;

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
                this._planillaServicios = (IPlanillaServicios)Activator.GetObject(typeof(IPlanillaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PlanillaServicios"]);
                this._internacionalServicios = (IInternacionalServicios)Activator.GetObject(typeof(IInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InternacionalServicios"]);
                this._renovacionServicios = (IRenovacionServicios)Activator.GetObject(typeof(IRenovacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RenovacionServicios"]);

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

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcas,
                Recursos.Ids.ConsultarMarcas);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarca, "");

                Marca marca = (Marca)this._ventana.Marca;

                Anaqua anaqua = new Anaqua();
                anaqua.IdMarca = marca.Id;
                InfoAdicional infoAdicional = new InfoAdicional("M." + marca.Id);

                marca.InfoBoles = this._infoBolServicios.ConsultarInfoBolesPorMarca(marca);
                marca.Operaciones = this._operacionServicios.ConsultarOperacionesPorMarca(marca);
                marca.Busquedas = this._busquedaServicios.ConsultarBusquedasPorMarca(marca);

                marca.InfoAdicional = this._infoAdicionalServicios.ConsultarPorId(infoAdicional);
                marca.Anaqua = this._anaquaServicios.ConsultarPorId(anaqua);

                IList<ListaDatosDominio> tiposMarcas = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));
                ListaDatosDominio primerTipoMarca = new ListaDatosDominio();
                primerTipoMarca.Id = "NGN";
                tiposMarcas.Insert(0, primerTipoMarca);
                this._ventana.TipoMarcasDatos = tiposMarcas;
                this._ventana.TipoMarcasSolicitud = tiposMarcas;
                this._ventana.TipoMarcaDatos = this.BuscarTipoMarca(tiposMarcas, marca.Tipo);

                IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
                Agente primerAgente = new Agente();
                primerAgente.Id = "NGN";
                agentes.Insert(0, primerAgente);
                this._ventana.Agentes = agentes;
                this._ventana.Agente = this.BuscarAgente(agentes, marca.Agente);

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.PaisesSolicitud = paises;
                this._ventana.PaisSolicitud = this.BuscarPais(paises, marca.Pais);

                IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                StatusWeb primerStatus = new StatusWeb();
                primerStatus.Id = "NGN";
                statusWebs.Insert(0, primerStatus);
                this._ventana.StatusWebs = statusWebs;
                this._ventana.StatusWeb = this.BuscarStatusWeb(statusWebs, marca.StatusWeb);

                IList<Condicion> condiciones = this._condicionServicios.ConsultarTodos();
                Condicion primeraCondicion = new Condicion();
                primeraCondicion.Id = int.MinValue;
                condiciones.Insert(0, primeraCondicion);
                this._ventana.Condiciones = condiciones;

                IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                TipoEstado primerDetalle = new TipoEstado();
                primerDetalle.Id = "NGN";
                tipoEstados.Insert(0, primerDetalle);
                this._ventana.Detalles = tipoEstados;

                IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                Servicio primerServicio = new Servicio();
                primerServicio.Id = "NGN";
                servicios.Insert(0, primerServicio);
                this._ventana.Servicios = servicios;
                this._ventana.Servicio = this.BuscarServicio(servicios, marca.Servicio);

                
                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.BoletinesOrdenPublicacion = boletines;
                this._ventana.BoletinesPublicacion = boletines;
                this._ventana.BoletinesConcesion = boletines;
                this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, marca.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, marca.BoletinPublicacion);

                Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marca.Interesado));
                this._ventana.NombreInteresadoDatos = interesado.Nombre;
                this._ventana.NombreInteresadoSolicitud = interesado.Nombre;
                this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
                //this._ventana.InteresadoSolicitud = marca.Interesado;

                this._ventana.NombreAsociadoDatos = marca.Asociado != null ? marca.Asociado.Nombre : "";
                this._ventana.NombreAsociadoSolicitud = marca.Asociado != null ? marca.Asociado.Nombre : "";

                if (null != marca.Asociado)
                this._ventana.PintarAsociado(marca.Asociado.TipoCliente.Id);
                    
                

                if (marca.Corresponsal != null)
                {
                    this._ventana.DescripcionCorresponsalSolicitud = marca.Corresponsal.GetType().Equals(typeof(Corresponsal)) ? marca.Corresponsal.Descripcion : "";
                    this._ventana.DescripcionCorresponsalDatos = marca.Corresponsal.GetType().Equals(typeof(Corresponsal)) ? marca.Corresponsal.Descripcion : "";
                }

                this._ventana.NumPoderDatos = marca.Poder != null ? marca.Poder.NumPoder : "";
                this._ventana.NumPoderSolicitud = marca.Poder != null ? marca.Poder.NumPoder : "";

                IList<ListaDatosDominio> sectores = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiSector));
                ListaDatosDominio primerSector = new ListaDatosDominio();
                primerSector.Id = "NGN";
                sectores.Insert(0, primerSector);
                this._ventana.Sectores = sectores;
                this._ventana.Sector = this.BuscarSector(sectores, marca.Sector);

                IList<ListaDatosDominio> tipoReproducciones = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoReproduccion));
                ListaDatosDominio primerTipoReproduccion = new ListaDatosDominio();
                primerTipoReproduccion.Id = "NGN";
                tipoReproducciones.Insert(0, primerTipoReproduccion);
                this._ventana.TipoReproducciones = tipoReproducciones;
                this._ventana.TipoReproduccion = this.BuscarTipoReproduccion(tipoReproducciones, marca.Tipo);

                IList<ListaDatosDominio> tipoClasesNacional = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaTipoClaseNacional));
                ListaDatosDominio primerTipoClase = new ListaDatosDominio();
                primerTipoClase.Id = "NGN";
                tipoClasesNacional.Insert(0, primerTipoClase);
                this._ventana.TiposClaseNacional = tipoClasesNacional;
                this._ventana.TipoClaseNacional = this.BuscarClaseNacional(tipoClasesNacional, marca.TipoCnac);

                //string prueba = ConfigurationManager.AppSettings["RutaImagenesDeMarcas"] + marca.Id + ".BMP";

                if (File.Exists(ConfigurationManager.AppSettings["RutaImagenesDeMarcas"] + marca.Id + ".BMP"))
                {
                    marca.BEtiqueta = true;
                    this._ventana.PintarEtiqueta();

                }

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Marca)this._ventana.Marca).Id;
                auditoria.Tabla = "MYP_MARCAS";
                this._auditorias = this._marcaServicios.AuditoriaPorFkyTabla(auditoria);
                
                Renovacion renovacion = new Renovacion();
                renovacion.Marca = marca;
                IList<Renovacion> renovaciones = this._renovacionServicios.ObtenerRenovacionFiltro(renovacion);

                if (renovaciones.Count > 0)
                    this._ventana.PintarRenovacion();

                if (null != marca.InfoAdicional && !string.IsNullOrEmpty(marca.InfoAdicional.Id))
                    this._ventana.PintarInfoAdicional();

                if (null != marca.Anaqua)
                    this._ventana.PintarAnaqua();

                if (null != marca.InfoBoles && marca.InfoBoles.Count > 0)
                    this._ventana.PintarInfoBoles();

                if (null != marca.Operaciones && marca.Operaciones.Count > 0)
                    this._ventana.PintarOperaciones();

                if (null != marca.Busquedas && marca.Busquedas.Count > 0)
                    this._ventana.PintarBusquedas();

                if (null != this._auditorias && this._auditorias.Count > 0)
                    this._ventana.PintarAuditoria();

                this._ventana.BorrarCeros();

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
        /// Método que carga la ventana de consulta marcas
        /// </summary>
        public void IrConsultarMarcas()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ConsultarMarcas());

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que guardar los datos de la ventana y los almacena en las variables
        /// </summary>
        /// <returns></returns>
        public Marca CargarMarcaDeLaPantalla()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Marca marca = (Marca)this._ventana.Marca;

            marca.Operacion = "MODIFY";

            if (null != this._ventana.Agente)
                marca.Agente = !((Agente)this._ventana.Agente).Id.Equals("NGN") ? (Agente)this._ventana.Agente : null;

            if (null != this._ventana.AsociadoSolicitud)
                marca.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            if (null != this._ventana.BoletinConcesion)
                marca.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

            if (null != this._ventana.BoletinPublicacion)
                marca.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

            if (null != this._ventana.InteresadoSolicitud)
                marca.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            if (null != this._ventana.Servicio)
                marca.Servicio = !((Servicio)this._ventana.Servicio).Id.Equals("NGN") ? ((Servicio)this._ventana.Servicio) : null;

            if (null != this._ventana.PoderSolicitud)
                marca.Poder = !((Poder)this._ventana.PoderSolicitud).Id.Equals("NGN") ? ((Poder)this._ventana.PoderSolicitud) : null;

            if (null != this._ventana.PaisSolicitud)
                marca.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

            if (null != this._ventana.StatusWeb)
                marca.StatusWeb = ((StatusWeb)this._ventana.StatusWeb).Id.Equals("NGN") ? ((StatusWeb)this._ventana.StatusWeb) : null;

            if (null != this._ventana.CorresponsalSolicitud)
                marca.Corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? ((Corresponsal)this._ventana.CorresponsalSolicitud) : null;

            if (null != this._ventana.Sector)
                marca.Sector = !((ListaDatosDominio)this._ventana.Sector).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.Sector).Id : null;

            if (null != this._ventana.TipoClaseNacional)
                marca.TipoCnac = !((ListaDatosDominio)this._ventana.TipoClaseNacional).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoClaseNacional).Id : null;

            if (null != this._ventana.TipoReproduccion)
                marca.TipoRps = ((ListaDatosDominio)this._ventana.TipoReproduccion).Id[0];

            if (null != this._ventana.TipoMarcaDatos)
                marca.Tipo = !((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id : null;

            if (string.IsNullOrEmpty(this._ventana.IdInternacional))
                marca.Internacional = null;

            if (string.IsNullOrEmpty(this._ventana.IdNacional))
                marca.Nacional = null;

            return marca;

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
        /// modifica los datos de la Marca
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
                    Marca marca = CargarMarcaDeLaPantalla();

                    bool exitoso = this._marcaServicios.InsertarOModificar(marca, UsuarioLogeado.Hash);

                    if (exitoso)
                        this.Navegar(Recursos.MensajesConElUsuario.MarcaModificada, false);
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
        

                /// <summary>
        /// Método que se ecarga la descripcion de la situacion
        /// </summary>
        /// <param name="tab"></param>
        public void DescripcionSituacion()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.SituacionDescripcion = ((Servicio)this._ventana.Servicio).Descripcion;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
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

            this.Navegar(new GestionarInfoAdicional(CargarMarcaDeLaPantalla(), tab));

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

            this.Navegar(new ListaInfoBoles(CargarMarcaDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de las operaciones de la Marca
        /// </summary>
        public void IrOperaciones()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaOperaciones(CargarMarcaDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de Anaqua de la Marca
        /// </summary>
        public void IrAnaqua()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new GestionarAnaqua(CargarMarcaDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de la lista de búsquedas de la marca
        /// </summary>
        /// <param name="tab"></param>
        public void IrBusquedas(string tab)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaBusquedas(CargarMarcaDeLaPantalla(), tab));

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
        /// Método que se encarga de duplicar la marca
        /// </summary>
        public void Duplicar()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new AgregarMarca(CargarMarcaDeLaPantalla()));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

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
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                this._ventana.NombreAsociadoDatos = "";
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
                this._ventana.NombreAsociadoDatos = "";
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
                {
                    this._ventana.AsociadosSolicitud = this._asociados;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
            }
            else
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
                else
                {
                    this._ventana.AsociadosDatos = this._asociados;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
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

            Marca marca = (Marca)this._ventana.Marca;
            IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            Asociado primerAsociado = new Asociado();
            primerAsociado.Id = int.MinValue;
            asociados.Insert(0, primerAsociado);
            this._ventana.AsociadosSolicitud = asociados;
            this._ventana.AsociadosDatos = asociados;
            this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, marca.Asociado);
            this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marca.Asociado);
            this._ventana.NombreAsociadoDatos = ((Marca)this._ventana.Marca).Asociado.Nombre;
            this._ventana.NombreAsociadoSolicitud = ((Marca)this._ventana.Marca).Asociado.Nombre;
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
                    this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    this._ventana.IdInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Id.ToString();
                    this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                    this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";

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
                this._ventana.NombreInteresadoDatos = "";
                this._ventana.InteresadoPaisSolicitud = "";
                this._ventana.InteresadoCiudadSolicitud = "";
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

                if ((Interesado)this._ventana.InteresadoDatos != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
                    this._ventana.InteresadoDatos = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.InteresadoSolicitud = (Interesado)this._ventana.InteresadoDatos;
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.IdInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();
                    this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                    this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";

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
                this._ventana.NombreInteresadoDatos = "";
                this._ventana.InteresadoPaisSolicitud = "";
                this._ventana.InteresadoCiudadSolicitud = "";
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
                if (!string.IsNullOrEmpty(this._ventana.IdInteresadoDatosFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Id == int.Parse(this._ventana.IdInteresadoDatosFiltrar)
                                           select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoDatosFiltrar))
                {
                    interesadosFiltrados = from p in interesadosFiltrados
                                           where p.Nombre != null &&
                                           p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoDatosFiltrar.ToLower())
                                           select p;
                }
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                else
                {
                    this._ventana.InteresadosSolicitud = this._interesados;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
            }
            else
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
                else
                {
                    this._ventana.InteresadosDatos = this._interesados;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
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
            Marca marca = (Marca)this._ventana.Marca;

            IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);
            this._ventana.InteresadosDatos = interesados;
            this._ventana.InteresadosSolicitud = interesados;
            ((Marca)this._ventana.Marca).Interesado = this.BuscarInteresado(interesados, marca.Interesado);
            Interesado interesado = this.BuscarInteresado(interesados, marca.Interesado);
            this._ventana.InteresadoSolicitud = interesado;
            this._ventana.InteresadoDatos = interesado;
            interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);
            this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
            this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
            this._ventana.NombreInteresadoDatos = ((Marca)this._ventana.Marca).Interesado.Nombre;
            this._ventana.NombreInteresadoSolicitud = ((Marca)this._ventana.Marca).Interesado.Nombre;
            this._interesados = interesados;

            this._ventana.InteresadosEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion

        #region Metodos de los filtros de corresponsales

        /// <summary>
        /// Método que cambia corresponsal solicitud
        /// </summary>
        public void CambiarCorresponsalSolicitud()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Corresponsal)this._ventana.CorresponsalSolicitud != null)
                {
                    //Corresponsal corresponsal = this._corresponsalServicios.ConsultarCorresponsalConTodo((Corresponsal)this._ventana.CorresponsalSolicitud);
                    this._ventana.DescripcionCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
                    this._ventana.IdCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id.ToString();

                    this._ventana.CorresponsalDatos = (Corresponsal)this._ventana.CorresponsalSolicitud;
                    this._ventana.DescripcionCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
                    this._ventana.IdCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id.ToString();

                    this._ventana.ConvertirEnteroMinimoABlanco();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.DescripcionCorresponsalDatos = "";
                this._ventana.DescripcionCorresponsalSolicitud = "";
            }
        }

        /// <summary>
        /// Método que cambia corresponsal datos
        /// </summary>
        public void CambiarCorresponsalDatos()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Corresponsal)this._ventana.CorresponsalDatos != null)
                {
                    //Corresponsal corresponsal = this._corresponsalServicios.ConsultarCorresponsalConTodo((Corresponsal)this._ventana.CorresponsalDatos);
                    this._ventana.DescripcionCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalDatos).Descripcion;
                    this._ventana.IdCorresponsalDatos = ((Corresponsal)this._ventana.CorresponsalDatos).Id.ToString();
                    this._ventana.CorresponsalSolicitud = (Corresponsal)this._ventana.CorresponsalDatos;

                    this._ventana.DescripcionCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalDatos).Descripcion;
                    this._ventana.IdCorresponsalSolicitud = ((Corresponsal)this._ventana.CorresponsalDatos).Id.ToString();

                    this._ventana.ConvertirEnteroMinimoABlanco();
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.DescripcionCorresponsalDatos = "";
                this._ventana.DescripcionCorresponsalSolicitud = "";
            }
        }

        /// <summary>
        /// Método que filtra un corresponsal
        /// </summary>
        /// <param name="filtrarEn"></param>
        public void BuscarCorresponsal(int filtrarEn)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IEnumerable<Corresponsal> corresponsalesFiltrados = this._corresponsales;

            if (filtrarEn == 0)
            {
                if (!string.IsNullOrEmpty(this._ventana.IdCorresponsalSolicitudFiltrar))
                {
                    corresponsalesFiltrados = from p in corresponsalesFiltrados
                                              where p.Id == int.Parse(this._ventana.IdCorresponsalSolicitudFiltrar)
                                              select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.DescripcionCorresponsalSolicitudFiltrar))
                {
                    corresponsalesFiltrados = from p in corresponsalesFiltrados
                                              where p.Descripcion != null &&
                                              p.Descripcion.ToLower().Contains(this._ventana.DescripcionCorresponsalSolicitudFiltrar.ToLower())
                                              select p;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this._ventana.IdCorresponsalDatosFiltrar))
                {
                    corresponsalesFiltrados = from p in corresponsalesFiltrados
                                              where p.Id == int.Parse(this._ventana.IdCorresponsalDatosFiltrar)
                                              select p;
                }

                if (!string.IsNullOrEmpty(this._ventana.DescripcionCorresponsalDatosFiltrar))
                {
                    corresponsalesFiltrados = from p in corresponsalesFiltrados
                                              where p.Descripcion != null &&
                                              p.Descripcion.ToLower().Contains(this._ventana.DescripcionCorresponsalDatosFiltrar.ToLower())
                                              select p;
                }
            }

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (corresponsalesFiltrados.ToList<Corresponsal>().Count != 0)
                    this._ventana.CorresponsalesSolicitud = corresponsalesFiltrados.ToList<Corresponsal>();
                else
                {
                    this._ventana.CorresponsalesSolicitud = this._corresponsales;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
            }
            else
            {
                if (corresponsalesFiltrados.ToList<Corresponsal>().Count != 0)
                    this._ventana.CorresponsalesDatos = corresponsalesFiltrados.ToList<Corresponsal>();
                else
                {
                    this._ventana.CorresponsalesDatos = this._corresponsales;
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que carga los corresponsales
        /// </summary>
        public void CargarCorresponsales()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            IList<Corresponsal> corresponsales = this._corresponsalServicios.ConsultarTodos();
            Corresponsal primerCorresponsal = new Corresponsal();
            primerCorresponsal.Id = int.MinValue;
            corresponsales.Insert(0, primerCorresponsal);
            this._ventana.CorresponsalesSolicitud = corresponsales;
            this._ventana.CorresponsalesDatos = corresponsales;
            this._ventana.CorresponsalDatos = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);
            this._ventana.CorresponsalSolicitud = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);

            this._ventana.DescripcionCorresponsalDatos = null == ((Marca)this._ventana.Marca).Corresponsal ?
                                                         null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
            this._ventana.DescripcionCorresponsalSolicitud = null == ((Marca)this._ventana.Marca).Corresponsal ?
                                                             null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
            this._corresponsales = corresponsales;

            this._ventana.CorresponsalesEstanCargados = true;

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

                if ((Poder)this._ventana.PoderSolicitud != null)
                {
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderSolicitud).NumPoder;
                    this._ventana.PoderDatos = (Poder)this._ventana.PoderSolicitud;
                    this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderSolicitud).NumPoder;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
                this._ventana.NumPoderDatos = "";
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

                if ((Poder)this._ventana.PoderDatos != null)
                {
                    this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderDatos).NumPoder;
                    this._ventana.PoderSolicitud = (Poder)this._ventana.PoderDatos;
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderDatos).NumPoder;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException e)
            {
                this._ventana.NumPoderSolicitud = "";
                this._ventana.NumPoderDatos = "";
            }
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

            Marca marca = (Marca)this._ventana.Marca;
            IList<Poder> poderes = this._poderServicios.ConsultarTodos();
            Poder poder = new Poder();
            poder.Id = int.MinValue;
            poderes.Insert(0, poder);
            this._ventana.PoderesDatos = poderes;
            this._ventana.PoderesSolicitud = poderes;
            this._ventana.PoderDatos = this.BuscarPoder(poderes, marca.Poder);
            this._ventana.PoderSolicitud = this.BuscarPoder(poderes, marca.Poder);

            this._ventana.PoderesEstanCargados = true;

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion

        #region Impresiones

        /// <summary>
        /// Método que se encarga de cargar la ventana de impresión de la Marca
        /// </summary>
        /// <param name="nombreBoton"></param>
        public void IrImprimir(string nombreBoton)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                switch (nombreBoton)
                {
                    case "_btnFM02":
                        ImprimirFM02("Normal");
                        break;
                    case "_btnFM02Venen":
                        ImprimirFM02Venen("Normal");
                        break;
                    case "_btnAnexoFM02":
                        ImprimirAnexoFM02("Normal");
                        break;
                    case "_btnLFM02":
                        ImprimirFM02("Laser");
                        break;
                    case "_btnLFM02Venen":
                        ImprimirFM02Venen("Laser");
                        break;
                    case "_btnLAnexoFM02":
                        ImprimirAnexoFM02("Laser");
                        break;
                    case "_btnCarpeta":
                        ImprimirCarpeta("Normal");
                        break;
                    default:
                        break;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ExcepcionPaquetes, true);
            }
        }

        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        private void ImprimirFM02(string modo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (ValidarMarcaAntesDeImprimirFM02())
            {
                string paqueteProcedimiento = "PCK_MYP_MARCAS";
                string procedimiento = modo.Equals("Laser") ? "P31" : "P1";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Marca)this._ventana.Marca).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                if (planilla != null)
                {
                    //Impresion _ventana =
                    //    new Impresion(Recursos.Etiquetas.btnFM02, planilla.Folio.Replace("\n", Environment.NewLine));
                    //_ventana.ShowDialog();

                    ////Llamado al archivo .bat 
                    ////if (_ventana.ClickImprimir)
                    ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
                    ////ConfigurationManager.AppSettings["txtPrint"]);

                    //parametro.Via = 0;
                    //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

                    this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnFM02);
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de llamar al formato de impresión FM02Venen
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        private void ImprimirFM02Venen(string modo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (ValidarMarcaAntesDeImprimirFM02Venen())
            {
                string paqueteProcedimiento = "PCK_MYP_MARCAS";
                string procedimiento = modo.Equals("Laser") ? "P32" : "P2";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Marca)this._ventana.Marca).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                if (planilla != null)
                {
                    //Impresion _ventana =
                    //    new Impresion(Recursos.Etiquetas.btnFM02Venen, planilla.Folio.Replace("\n", Environment.NewLine));

                    //_ventana.ShowDialog();

                    ////Llamado al archivo .bat 
                    ////if (_ventana.ClickImprimir)
                    ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
                    ////ConfigurationManager.AppSettings["txtPrint"]);

                    //parametro.Via = 0;
                    //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

                    this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnFM02Venen);
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de llamar al formato de impresión AnexoFM02
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        private void ImprimirAnexoFM02(string modo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (ValidarMarcaAntesDeImprimirAnexoFM02())
            {
                string paqueteProcedimiento = "PCK_MYP_MARCAS";
                string procedimiento = modo.Equals("Laser") ? "P33" : "P3";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Marca)this._ventana.Marca).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                if (planilla != null)
                {
                    //Impresion _ventana =
                    //    new Impresion(Recursos.Etiquetas.btnAnexoFM02, planilla.Folio.Replace("\n", Environment.NewLine));

                    //_ventana.ShowDialog();

                    ////Llamado al archivo .bat 
                    ////if (_ventana.ClickImprimir)
                    ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
                    ////ConfigurationManager.AppSettings["txtPrint"]);

                    //parametro.Via = 0;
                    //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

                    this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexoFM02);
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de llamar al formato de impresión Carpeta
        /// </summary>
        /// <param name="modo">Láser en caso de ser Láser, o normal en caso de ser normal</param>
        private void ImprimirCarpeta(string modo)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (ValidarMarcaAntesDeImprimirCarpeta())
            {
                string paqueteProcedimiento = "PCK_MYP_MARCAS";
                string procedimiento = modo.Equals("Laser") ? "" : "P12";
                ParametroProcedimiento parametro =
                    new ParametroProcedimiento(((Marca)this._ventana.Marca).Id, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

                Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
                if (planilla != null)
                {
                    //Impresion _ventana =
                    //    new Impresion(Recursos.Etiquetas.btnAnexoFM02, planilla.Folio.Replace("\n", Environment.NewLine));

                    //_ventana.ShowDialog();

                    ////Llamado al archivo .bat 
                    ////if (_ventana.ClickImprimir)
                    ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
                    ////ConfigurationManager.AppSettings["txtPrint"]);

                    //parametro.Via = 0;
                    //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

                    this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnCarpeta);
                }
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que realiza todas las validaciones de la Marca antes de imprimir
        /// </summary>
        /// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        private bool ValidarMarcaAntesDeImprimirFM02()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = true;

            Marca marca = CargarMarcaDeLaPantalla();

            if ((null == marca.Poder) || (marca.Poder.NumPoder.Equals("")))
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinNumeroDePoder) == retorno : retorno;

            if (((this._ventana.ClaseInternacional.Equals("")) && (this._ventana.ClaseNacional.Equals(""))) && (retorno))
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinClase) == retorno : retorno;

            if ((marca.EtiquetaDescripcion.Equals("")) && (retorno))
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinDescripcionDelSigno) == retorno : retorno;

            if ((null == marca.Distingue) || (marca.Distingue.Equals("")) && (retorno))
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinDistingue) == retorno : retorno;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que realiza todas las validaciones de la Marca antes de imprimir
        /// </summary>
        /// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        private bool ValidarMarcaAntesDeImprimirFM02Venen()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = true;

            Marca marca = CargarMarcaDeLaPantalla();

            if ((null == marca.Distingue) || (marca.Distingue.Equals("")))
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinDistingue) == retorno : retorno;

            else if (marca.Distingue.Length > 1800)
                retorno = retorno ?
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaSinDistingue) == retorno : retorno;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que realiza todas las validaciones de la Marca antes de imprimir
        /// </summary>
        /// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        private bool ValidarMarcaAntesDeImprimirAnexoFM02()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = true;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que realiza todas las validaciones de la Marca antes de imprimir
        /// </summary>
        /// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        private bool ValidarMarcaAntesDeImprimirCarpeta()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return true;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion

        /// <summary>
        ///Método que realiza el llamado al explorador para abrir el cartel de la marca
        /// </summary>
        public void GenerarCartel()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.IrURL(ConfigurationManager.AppSettings["UrlGenerarCartel"] + ((Marca)this._ventana.Marca).Id);

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
        /// Método que se encarga de abrir el certificado de la marca en formato .pdf
        /// </summary>
        public void Certificado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["rutaCertificados"].ToString() + ((Marca)this._ventana.Marca).Id + ".pdf");

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

        /// <summary>
        /// Método que consulta la clase internacional y lo pega en distingue
        /// </summary>
        public void TomarClaseInternacional()
        {
            Internacional internacionalAux = new Internacional();

            internacionalAux = _internacionalServicios.ConsultarPorId(new Internacional(int.Parse(this._ventana.IdInternacional)));

            this._ventana.DistingueSolicitud = internacionalAux.Descripcion;
            this._ventana.DistingueDatos = internacionalAux.Descripcion;
        }

        public void MostrarEtiqueta()
        {
            Marca marcaAux = ((Marca)this._ventana.Marca);
            if (((Marca)this._ventana.Marca).BEtiqueta)
            {
                    EtiquetaMarca detalleEtiqueta = new EtiquetaMarca(ConfigurationManager.AppSettings["RutaImagenesDeMarcas"] + marcaAux.Id + ".BMP", marcaAux.Descripcion);
                    detalleEtiqueta.ShowDialog();

            }
        }

        public void BuscarCarta(int p)
        {
            throw new NotImplementedException();
        }

        public void IrRenovacionDeMarca()
        {
            this.Navegar(new ConsultarRenovaciones(this._ventana.Marca));
        }
    }
}
