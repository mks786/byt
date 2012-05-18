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
using System.Windows;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.ControlesByT.Ventanas;
using System.Text;

namespace Trascend.Bolet.Cliente.Presentadores.MarcasTercero
{
    class PresentadorGestionarMarcaTercero : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool _agregar = true;

        private IGestionarMarcaTercero _ventana;

        private IMarcaTerceroServicios _marcaTerceroServicios;
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
        private IMarcaServicios _marcaServicios;
        private IMarcaBaseTerceroServicios _marcaBaseTerceroServicios;
        private IEstadoMarcaServicios _estadoMarcaServicios;
        private ITipoBaseServicios _tipoBaseServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;
        private IList<Auditoria> _auditorias;
        private IList<Marca> _marcas;
        private IList<MarcaBaseTercero> _marcasBaseTercero;

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorGestionarMarcaTercero(IGestionarMarcaTercero ventana, object marcaTercero)
        {
            try
            {

                this._ventana = ventana;
                if (marcaTercero != null)
                {
                    this._ventana.MarcaTercero = marcaTercero;
                    if (((MarcaTercero)marcaTercero).Internacional == null)
                        ((MarcaTercero)marcaTercero).Internacional = new Internacional();
                    else
                        ((MarcaTercero)marcaTercero).Internacional = new Internacional(((MarcaTercero)marcaTercero).Internacional.Id);

                    if (((MarcaTercero)marcaTercero).Nacional == null)
                        ((MarcaTercero)marcaTercero).Nacional = new Nacional();
                    else
                        ((MarcaTercero)marcaTercero).Nacional = new Nacional(((MarcaTercero)marcaTercero).Nacional.Id);

                    _agregar = false;
                }
                else
                {
                    MarcaTercero marcaTerceroAgregar = new MarcaTercero();
                    this._ventana.Marca = null;
                    if (((MarcaTercero)marcaTerceroAgregar).BoletinConcesion == null)
                        ((MarcaTercero)marcaTerceroAgregar).BoletinConcesion = new Boletin();
                    if (((MarcaTercero)marcaTerceroAgregar).BoletinPublicacion == null)
                        ((MarcaTercero)marcaTerceroAgregar).BoletinPublicacion = new Boletin();
                    if (((MarcaTercero)marcaTerceroAgregar).Asociado == null)
                        ((MarcaTercero)marcaTerceroAgregar).Asociado = new Asociado();
                    if (((MarcaTercero)marcaTerceroAgregar).Interesado == null)
                        ((MarcaTercero)marcaTerceroAgregar).Interesado = new Interesado();
                    if (((MarcaTercero)marcaTerceroAgregar).AsociadoTercero == null)

                    this._ventana.MarcaTercero = marcaTerceroAgregar;
                    this._ventana.MostrarByt();
                    CambiarAModificar();
                   
                }


                this._marcaTerceroServicios = (IMarcaTerceroServicios)Activator.GetObject(typeof(IMarcaTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaTerceroServicios"]);
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
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._marcaBaseTerceroServicios = (IMarcaBaseTerceroServicios)Activator.GetObject(typeof(IMarcaBaseTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaBaseTerceroServicios"]);
                this._estadoMarcaServicios = (IEstadoMarcaServicios)Activator.GetObject(typeof(IEstadoMarcaServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EstadoMarcaServicios"]);
                this._tipoBaseServicios = (ITipoBaseServicios)Activator.GetObject(typeof(ITipoBaseServicios),
                      ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoBaseServicios"]);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcaTercero,
                Recursos.Ids.ConsultarMarcaTercero);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcaTercero, "");

                if (_agregar == false)
                {
                    MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
                    EstadoMarca estadoMarca = new EstadoMarca();
                    TipoBase tipoBase = new TipoBase();

                    //  Anaqua anaqua = new Anaqua();
                    //  anaqua.IdMarcaTercero = marcaTercero.Id;
                    InfoAdicional infoAdicional = new InfoAdicional("M." + marcaTercero.Id);

                    //  marcaTercero.InfoBoles = this._infoBolServicios.ConsultarInfoBolesPorMarcaTercero(marcaTercero);
                    //  marcaTercero.Operaciones = this._operacionServicios.ConsultarOperacionesPorMarcaTercero(marcaTercero);
                    //  marcaTercero.Busquedas = this._busquedaServicios.ConsultarBusquedasPorMarcaTercero(marcaTercero);

                    marcaTercero.InfoAdicional = this._infoAdicionalServicios.ConsultarPorId(infoAdicional);
                    //  marcaTercero.Anaqua = this._anaquaServicios.ConsultarPorId(anaqua);

                    //IList<ListaDatosDominio> tiposMarcaTerceros = this._listaDatosDominioServicios.
                    //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCateroriaMarcaTercero));
                    //ListaDatosDominio primerTipoMarcaTercero = new ListaDatosDominio();
                    //primerTipoMarcaTercero.Id = "NGN";
                    //tiposMarcaTerceros.Insert(0, primerTipoMarcaTercero);
                    //this._ventana.TipoMarcaTerceroDatos = tiposMarcaTerceros;
                    //this._ventana.TipoMarcaTerceroSolicitud = tiposMarcaTerceros;
                    //this._ventana.TipoMarcaTerceroDatos = this.BuscarTipoMarca(tiposMarcaTerceros, marcaTercero.Tipo);

                    CargaComboBox();

                    this._ventana.ComentarioClienteEspanol = marcaTercero.ComentarioEsp;
                    this._ventana.ComentarioClienteIngles = marcaTercero.ComentarioIng;
                    this._ventana.Anexo = marcaTercero.Anexo.ToString();
                    //IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                    //StatusWeb primerStatus = new StatusWeb();
                    //primerStatus.Id = "NGN";
                    //statusWebs.Insert(0, primerStatus);
                    //this._ventana.StatusWebs = statusWebs;
                    //this._ventana.StatusWeb = this.BuscarStatusWeb(statusWebs, marcaTercero.StatusWeb);

                    //IList<Condicion> condiciones = this._condicionServicios.ConsultarTodos();
                    //Condicion primeraCondicion = new Condicion();
                    //primeraCondicion.Id = int.MinValue;
                    //condiciones.Insert(0, primeraCondicion);
                    //this._ventana.Condiciones = condiciones;

                    Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marcaTercero.Interesado));
                    //this._ventana.NombreInteresadoDatos = interesado.Nombre;
                    this._ventana.NombreInteresadoSolicitud = interesado.Nombre;
                    this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                    this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
                    this._ventana.TipoCbx = marcaTercero.Tipo;
                    //this._ventana.InteresadoSolicitud = marcaTercero.Interesado;

                    //this._ventana.NombreAsociadoDatos = marcaTercero.Asociado != null ? marcaTercero.Asociado.Nombre : "";
                    this._ventana.NombreAsociadoSolicitud = marcaTercero.Asociado != null ? marcaTercero.Asociado.Nombre : "";
                    //this._ventana.TipoCbx = this.BuscarTipoMarca((IList<ListaDatosDominio>)this._ventana.TiposCbx, marcaTercero.Tipo);

                    //this._ventana.DescripcionCorresponsalSolicitud = marcaTercero.Corresponsal != null ? marcaTercero.Corresponsal.Descripcion : "";
                    //this._ventana.DescripcionCorresponsalDatos = marcaTercero.Corresponsal != null ? marcaTercero.Corresponsal.Descripcion : "";

                    //this._ventana.NumPoderDatos = marcaTercero.Poder != null ? marcaTercero.Poder.NumPoder : "";

                    //IList<ListaDatosDominio> sectores = this._listaDatosDominioServicios.
                    //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiSector));
                    //ListaDatosDominio primerSector = new ListaDatosDominio();
                    //primerSector.Id = "NGN";
                    //sectores.Insert(0, primerSector);
                    //this._ventana.Sectores = sectores;
                    //this._ventana.Sector = this.BuscarSector(sectores, marcaTercero.Sector);

                    //IList<ListaDatosDominio> tipoReproducciones = this._listaDatosDominioServicios.
                    //    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoReproduccion));
                    //ListaDatosDominio primerTipoReproduccion = new ListaDatosDominio();
                    //primerTipoReproduccion.Id = "NGN";
                    //tipoReproducciones.Insert(0, primerTipoReproduccion);
                    //this._ventana.TipoReproducciones = tipoReproducciones;
                    //this._ventana.TipoReproduccion = this.BuscarTipoReproduccion(tipoReproducciones, marcaTercero.Tipo);

                    Auditoria auditoria = new Auditoria();
                    // auditoria.Fk = ((MarcaTercero)this._ventana.MarcaTercero).Id;
                    auditoria.Tabla = "MYP_MARCAS_TER";
                    // OJO!!  this._auditorias = this._marcaTerceroServicios.AuditoriaPorFkyTabla(auditoria);

                    //if (null != marcaTercero.InfoAdicional && !string.IsNullOrEmpty(marcaTercero.InfoAdicional.Id))
                    //    this._ventana.PintarInfoAdicional();

                    //if (null != marcaTercero.Anaqua)
                    //    this._ventana.PintarAnaqua();

                    if (null != marcaTercero.InfoBoles && marcaTercero.InfoBoles.Count > 0)
                        this._ventana.PintarInfoBoles();

                    //if (null != marcaTercero.Operaciones && marcaTercero.Operaciones.Count > 0)
                    //    this._ventana.PintarOperaciones();

                    if (null != marcaTercero.Busquedas && marcaTercero.Busquedas.Count > 0)
                        this._ventana.PintarBusquedas();

                    //if (null != this._auditorias && this._auditorias.Count > 0)
                    //    this._ventana.PintarAuditoria();

                    this._ventana.BorrarCeros();
                }
                else 
                {
                    MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
                    CargaComboBox();
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
        /// Método que carga la ventana de consulta marcaTerceros
        /// </summary>
        public void IrConsultarMarcasTercero()
        {
            this.Navegar(new ConsultarMarcasTercero());
        }


        public void CargaComboBox()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
            EstadoMarca estadoMarca = new EstadoMarca();
            TipoBase tipoBase = new TipoBase();

            IList<Pais> paises = this._paisServicios.ConsultarTodos();
            Pais primerPais = new Pais();
            primerPais.Id = int.MinValue;
            paises.Insert(0, primerPais);
            this._ventana.PaisesSolicitud = paises;
            if (!_agregar)
                 this._ventana.PaisSolicitud = this.BuscarPais(paises, marcaTercero.Pais);

            IList<EstadoMarca> EstadosMarca = this._estadoMarcaServicios.ConsultarTodos();
            EstadoMarca primerEstadoMarca = new EstadoMarca();
            primerEstadoMarca.Id = "A";
            EstadosMarca.Insert(0, primerEstadoMarca);
            this._ventana.EstadosMarcaSolicitud = EstadosMarca;
            if (!_agregar)
                  this._ventana.EstadoMarcaSolicitud = this.BuscarEstadoMarca(EstadosMarca, estadoMarca);


            IList<TipoBase> TipoBase = this._tipoBaseServicios.ConsultarTodos();
            TipoBase primerTipoBase = new TipoBase();
            primerEstadoMarca.Id = "A";
            TipoBase.Insert(0, primerTipoBase);
            this._ventana.TiposBaseSolicitud = TipoBase;
            if (!_agregar)
                  this._ventana.TipoBaseSolicitud = this.BuscarTipoBase(TipoBase, tipoBase);

            IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
            TipoEstado primerDetalle = new TipoEstado();
            primerDetalle.Id = "NGN";
            tipoEstados.Insert(0, primerDetalle);
            this._ventana.Estados = tipoEstados;

            IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
            Servicio primerServicio = new Servicio();
            primerServicio.Id = "NGN";
            servicios.Insert(0, primerServicio);
            this._ventana.Situaciones = servicios;
            if (!_agregar)
                 this._ventana.Situacion = this.BuscarServicio(servicios, marcaTercero.Servicio);

            IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
            Boletin primerBoletin = new Boletin();
            primerBoletin.Id = int.MinValue;
            boletines.Insert(0, primerBoletin);
            this._ventana.BoletinesOrdenPublicacion = boletines;
            this._ventana.BoletinesPublicacion = boletines;
            this._ventana.BoletinesConcesion = boletines;
            if (!_agregar)
            {
                this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, marcaTercero.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, marcaTercero.BoletinPublicacion);
            }

            IList<ListaDatosDominio> tiposMarcas = this._listaDatosDominioServicios.
            ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));
            ListaDatosDominio primerTipoMarca = new ListaDatosDominio();
            primerTipoMarca.Id = "NGN";
            tiposMarcas.Insert(0, primerTipoMarca);
            this._ventana.TiposCbx = tiposMarcas;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        
        }

        /// <summary>
        /// Método que guardar los datos de la ventana y los almacena en las variables
        /// </summary>
        /// <returns></returns>
        public MarcaTercero CargarMarcaTerceroDeLaPantalla()
        {

            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;

            if(!_agregar)
                marcaTercero.Operacion = "MODIFY";
            else
                marcaTercero.Operacion = "CREATE";
           
            if (null != this._ventana.AsociadoSolicitud)
                marcaTercero.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

            if (null != this._ventana.BoletinConcesion)
                marcaTercero.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

            if (null != this._ventana.BoletinPublicacion)
                marcaTercero.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

            if (null != this._ventana.InteresadoSolicitud)
                marcaTercero.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

            if (null != this._ventana.Situacion)
                marcaTercero.Servicio = !((Servicio)this._ventana.Situacion).Id.Equals("NGN") ? ((Servicio)this._ventana.Situacion) : null;

            if (null != this._ventana.PaisSolicitud)
                marcaTercero.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

            if (null != ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero)
                marcaTercero.MarcasBaseTercero = ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero;

            //if (null != this._ventana.TipoCbx)
            //    marcaTercero.Tipo = !((ListaDatosDominio)this._ventana.TipoCbx).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoCbx).Id : null;

            marcaTercero.ComentarioEsp = this._ventana.ComentarioClienteEspanol;
            marcaTercero.ComentarioIng = this._ventana.ComentarioClienteIngles;

            //if (null != this._ventana.StatusWeb)
            //    marcaTercero.StatusWeb = ((StatusWeb)this._ventana.StatusWeb).Id.Equals("NGN") ? ((StatusWeb)this._ventana.StatusWeb) : null;

            //if (null != this._ventana.CorresponsalSolicitud)
            //    marcaTercero.Corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? ((Corresponsal)this._ventana.CorresponsalSolicitud) : null;

            //if (null != this._ventana.Sector)
            //    marcaTercero.Sector = !((ListaDatosDominio)this._ventana.Sector).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.Sector).Id : null;

            //if (null != this._ventana.TipoReproduccion)
            //    marcaTercero.TipoRps = ((ListaDatosDominio)this._ventana.TipoReproduccion).Id[0];

            //if (null != this._ventana.TipoMarcaTerceroDatos)
            //    marcaTercero.Tipo = !((ListaDatosDominio)this._ventana.TipoMarcaTerceroDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoMarcaTerceroDatos).Id : null;


          

            //if (string.IsNullOrEmpty(this._ventana.IdNacional
            //   marcaTercero.Nacional = null;


            return marcaTercero;
        }

        /// <summary>
        /// Método que se encarga de cambiar el boton y habilitar los campos de la ventana para
        /// su modificación
        /// </summary>
        public void CambiarAModificar()
        {
            this._ventana.HabilitarCampos = true;
            this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
        }

        /// <summary>
        /// Metodo que valida si los campos necesarios para crear una MarcaTercero no esten Vacios
        /// </summary>
        public bool ValidarCampos(MarcaTercero MarcaTerceroValidar) 
        {
            bool bandera = false;
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if ((MarcaTerceroValidar.Asociado != null) &&(MarcaTerceroValidar.Asociado.Id !=0))
            {
                if ((MarcaTerceroValidar.Interesado != null) && (MarcaTerceroValidar.Interesado.Id != 0))
                {
                    if ((MarcaTerceroValidar.Servicio !=null))
                    {
                        if (MarcaTerceroValidar.Descripcion != "")
                        {
                            bandera = true;
                        }
                        else 
                        {
                            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaMarcaTerceroSinNombre);
                        }
                    }
                    else 
                    {
                        this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaMarcaTerceroSinSituacion);
                    }
                }
                else
                {
                    this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaMarcaTerceroSinInteresado);
                }
            }
            else
            {
                this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.AlertaMarcaTerceroSinAsociado);
            }
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

           // return bandera;
            return bandera;
        }

        /// <summary>
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos de la MarcaTercero
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

                //Modifica los datos de la marcaTercero
                else
                {
                    MarcaTercero marcaTercero = CargarMarcaTerceroDeLaPantalla();

                    if (ValidarCampos(marcaTercero))
                    {

                        bool exitoso = this._marcaTerceroServicios.InsertarOModificar(marcaTercero, UsuarioLogeado.Hash);


                        if (exitoso)
                            this.Navegar(Recursos.MensajesConElUsuario.MarcaTerceroModificado, false);
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
        /// Metodo que se encarga de eliminar una MarcaTercero
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

        #region Metodos Ir
        /// <summary>
        /// Método que se encarga de mostrar la ventana de información adicional
        /// </summary>
        /// <param name="tab"></param>
        public void IrInfoAdicional(string tab)
        {
       // NO USA     this.Navegar(new GestionarInfoAdicional(CargarMarcaTerceroDeLaPantalla(), tab));
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de InfoBoles
        /// </summary>
        public void IrInfoBoles()
        {

      //NO USA      this.Navegar(new ListaInfoBoles(CargarMarcaTerceroDeLaPantalla()));
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de las operaciones de la MarcaTercero
        /// </summary>
        public void IrOperaciones()
        {
     //NO USA       this.Navegar(new ListaOperaciones(CargarMarcaTerceroDeLaPantalla()));
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de Anaqua de la MarcaTercero
        /// </summary>
        public void IrAnaqua()
        {
      //NO USA      this.Navegar(new GestionarAnaqua(CargarMarcaTerceroDeLaPantalla()));
        }

        /// <summary>
        /// Método que se encarga de mostrar la ventana de la lista de búsquedas de la marcaTercero
        /// </summary>
        /// <param name="tab"></param>
        public void IrBusquedas(string tab)
        {
      //NO USA      this.Navegar(new ListaBusquedas(CargarMarcaTerceroDeLaPantalla(), tab));
        }

        #endregion

    
        
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
        /// Método que se encarga de duplicar la marcaTercero
        /// </summary>
        public void Duplicar()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

         //   this.Navegar(new AgregarMarcaTercero(CargarMarcaTerceroDeLaPantalla()));

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

        /// <summary>
        /// Método que carga lista de MarcasByt
        /// </summary>
        /// <returns>true si se realizó correctamente</returns>
        public bool CargarMarcasByt()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;
            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
            if ((null != marcaTercero.MarcasBaseTercero) && (marcaTercero.MarcasBaseTercero.Count != 0))
            {
                this._ventana.MarcasByt = null;
                this._ventana.MarcasByt = marcaTercero.MarcasBaseTercero;
                retorno = true;
               // this.LimpiarMarcasByt(marcaTercero);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que limpia lista de anexos de carta
        /// </summary>
        /// <param name="carta"></param>
        private void LimpiarMarcasByt(MarcaTercero marcaTercero)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<int> indices = new List<int>();
            foreach (MarcaBaseTercero marcasBaseTercero in marcaTercero.MarcasBaseTercero)
            {
                int index = 0;
                foreach (MarcaBaseTercero marcasBaseTerceroTotal in this._marcasBaseTercero)
                {
                    if (marcaTercero.Id == marcasBaseTerceroTotal.MarcaTercero.Id)
                    {
                        indices.Insert(0, index);
                    }
                    index++;
                }
            }

            foreach (int indice in indices)
            {
                this._marcasBaseTercero.RemoveAt(indice);
            }

            ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = this._marcasBaseTercero;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        public bool AgregarMarcaByt()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<MarcaBaseTercero> marcasBaseTercero;
            bool retorno = false;
            if ("" != this._ventana.NombreMarca)
            {
                if (null == ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero)
                    marcasBaseTercero = new List<MarcaBaseTercero>();
                else
                    marcasBaseTercero = ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero;

                MarcaBaseTercero aux = new MarcaBaseTercero();
                MarcaTercero marcaT = new MarcaTercero();
                string NombreDeMarca = this._ventana.NombreMarca;
                marcaT.Id = ((MarcaTercero)this._ventana.MarcaTercero).Id;
                marcaT.Anexo = ((MarcaTercero)this._ventana.MarcaTercero).Anexo;
                aux.MarcaTercero = marcaT;
                aux.Pais = ((Pais)this._ventana.PaisSolicitud);
                aux.TipoDeBase = ((TipoBase)this._ventana.TipoBaseSolicitud);
                 if ((bool)this._ventana.Byt.IsChecked)
                     { 
                        aux.Marca = ((Marca)this._ventana.MarcaFiltrada);
                        aux.NombreMarca = ((Marca)this._ventana.MarcaFiltrada).Descripcion; 
                        aux.Internacional = ((Marca)aux.Marca).Internacional;
                        aux.Nacional = ((Marca)aux.Marca).Nacional;
                        if (null != this._ventana.TipoBaseSolicitud)
                            aux.TipoDeBase = ((TipoBase)this._ventana.TipoBaseSolicitud).Id != null ? ((TipoBase)this._ventana.TipoBaseSolicitud) : null;
                        aux.NombreTipoBase = ((TipoBase)this._ventana.TipoBaseSolicitud).Descripcion;
              
                    }
                 else
                     {
                        Marca nueva = new Marca();
                        aux.Internacional = new Internacional();
                        aux.Nacional = new Nacional(); 
                        nueva.Descripcion = NombreDeMarca;
                        aux.Marca = nueva;
                        aux.NombreMarca = NombreDeMarca;
                        if (null!= aux.Marca.Internacional)
                             aux.Marca.Internacional.Id = Int32.Parse(this._ventana.IdInternacionalByt);
                        if (null!=aux.Marca.Nacional)
                            aux.Marca.Nacional.Id = Int32.Parse(this._ventana.IdNacionalByt);
                        aux.Internacional.Id = Int32.Parse(this._ventana.IdInternacionalByt);
                        aux.Nacional.Id = Int32.Parse(this._ventana.IdInternacionalByt);
                        aux.Tipo = ((TipoBase)this._ventana.TipoBaseSolicitud).Descripcion;
                        aux.TipoDeBase = null;
                     }

                marcasBaseTercero.Add(aux);
                ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = marcasBaseTercero;
                this._ventana.MarcasByt = marcasBaseTercero.ToList<MarcaBaseTercero>();
               // this._marcasBaseTercero.Remove((MarcaBaseTercero)this._ventana.MarcaTercero);
               // ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = this._marcasBaseTercero.ToList<MarcaBaseTercero>();
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Metodo que deshabilita los Marcas en el Byt
        /// </summary>
        /// <returns>retorno true si se deshabilitó</returns>
        public bool DeshabilitarMarcasByt()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<MarcaBaseTercero> marcasBaseTercero;
            bool respuesta = false;

            if (null != ((MarcaBaseTercero)this._ventana.MarcaByt))
            {
                if (null == ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero)
                    marcasBaseTercero = new List<MarcaBaseTercero>();
                else
                    marcasBaseTercero = ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero;

                marcasBaseTercero.Remove((MarcaBaseTercero)this._ventana.MarcaByt);
                ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = marcasBaseTercero;
             //   this._marcasBaseTercero.Add((MarcaBaseTercero)this._ventana.MarcaByt);
                this._ventana.MarcasByt = marcasBaseTercero.ToList<MarcaBaseTercero>();
            //    ((MarcaTercero)this._ventana.MarcaTercero).MarcasBaseTercero = this._marcasBaseTercero.ToList<MarcaBaseTercero>();

                if (marcasBaseTercero.Count == 0)
                    respuesta = true;

            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return respuesta;
        }
        #region Metodos de los filtros de asociados

        public void CambiarAsociadoSolicitud()
        {
            try
            {
                if ((Asociado)this._ventana.AsociadoSolicitud != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.AsociadoSolicitud);
                    this._ventana.NombreAsociadoSolicitud = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                    //this._ventana.AsociadoDatos = (Asociado)this._ventana.AsociadoSolicitud;
                    //this._ventana.NombreAsociadoDatos = ((Asociado)this._ventana.AsociadoSolicitud).Nombre;
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreAsociadoSolicitud = "";
                //this._ventana.NombreAsociadoDatos = "";
            }
        }

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

        public void BuscarAsociado(int filtrarEn)
        {
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
            //else
            //{

            //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoDatosFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Id == int.Parse(this._ventana.IdAsociadoDatosFiltrar)
            //                             select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoDatosFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Nombre != null &&
            //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoDatosFiltrar.ToLower())
            //                             select p;
            //    }
            //}

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (asociadosFiltrados.ToList<Asociado>().Count != 0)
                    this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
                else
                    this._ventana.AsociadosSolicitud = this._asociados;
            }
            //else
            //{
            //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
            //        this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
            //    else
            //        this._ventana.AsociadosDatos = this._asociados;
            //}
        }

        public void CargarAsociados()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
            IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            Asociado primerAsociado = new Asociado();
            primerAsociado.Id = int.MinValue;
            asociados.Insert(0, primerAsociado);
            this._ventana.AsociadosSolicitud = asociados;
            //this._ventana.AsociadosDatos = asociados;
            this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, marcaTercero.Asociado);
            //this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marcaTercero.Asociado);
            //this._ventana.NombreAsociadoDatos = ((MarcaTercero)this._ventana.MarcaTercero).Asociado.Nombre;
            this._ventana.NombreAsociadoSolicitud = ((MarcaTercero)this._ventana.MarcaTercero).Asociado.Nombre;
            this._asociados = asociados;
            this._ventana.AsociadosEstanCargados = true;

            Mouse.OverrideCursor = null;
        }

        #endregion

        #region Metodos de los filtros de interesados

        public void CambiarInteresadoSolicitud()
        {
            try
            {
                if ((Interesado)this._ventana.InteresadoSolicitud != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoSolicitud);
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    //this._ventana.InteresadoDatos = (Interesado)this._ventana.InteresadoSolicitud;
                    //this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoSolicitud).Nombre;
                    if (interesadoAux != null)
                    {

                        this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                        this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";
                    }
                }
            }
            catch (ApplicationException e)
            {
                this._ventana.NombreInteresadoSolicitud = "";
                //this._ventana.NombreInteresadoDatos = "";
                this._ventana.InteresadoPaisSolicitud = "";
                this._ventana.InteresadoCiudadSolicitud = "";
            }
        }

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

        public void BuscarInteresado(int filtrarEn)
        {
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
            //else
            //{
            //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoDatosFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Id == int.Parse(this._ventana.IdInteresadoDatosFiltrar)
            //                               select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoDatosFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Nombre != null &&
            //                               p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoDatosFiltrar.ToLower())
            //                               select p;
            //    }
            //}

            // filtrarEn = 0 significa en el listview de la pestaña solicitud
            // filtrarEn = 1 significa en el listview de la pestaña Datos 
            if (filtrarEn == 0)
            {
                if (interesadosFiltrados.ToList<Interesado>().Count != 0)
                    this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
                else
                    this._ventana.InteresadosSolicitud = this._interesados;
            }
            //else
            //{
            //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
            //        this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
            //    else
            //        this._ventana.InteresadosDatos = this._interesados;
            //}
        }

        public void CargarInteresados()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;

            IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            Interesado primerInteresado = new Interesado();
            primerInteresado.Id = int.MinValue;
            interesados.Insert(0, primerInteresado);
            //this._ventana.InteresadosDatos = interesados;
            this._ventana.InteresadosSolicitud = interesados;
            ((MarcaTercero)this._ventana.MarcaTercero).Interesado = this.BuscarInteresado(interesados, marcaTercero.Interesado);
            Interesado interesado = this.BuscarInteresado(interesados, marcaTercero.Interesado);
            this._ventana.InteresadoSolicitud = interesado;
            //this._ventana.InteresadoDatos = interesado;
            interesado = this._interesadoServicios.ConsultarInteresadoConTodo(interesado);
            this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
            this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
            //this._ventana.NombreInteresadoDatos = ((MarcaTercero)this._ventana.MarcaTercero).Interesado.Nombre;
            this._ventana.NombreInteresadoSolicitud = ((MarcaTercero)this._ventana.MarcaTercero).Interesado.Nombre;
            this._interesados = interesados;

            this._ventana.InteresadosEstanCargados = true;

            Mouse.OverrideCursor = null;
        }

        #endregion

        #region Metodos de los filtros de corresponsales

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
        //    this._ventana.CorresponsalDatos = this.BuscarCorresponsal(corresponsales, ((MarcaTercero)this._ventana.MarcaTercero).Corresponsal);
        //    this._ventana.CorresponsalSolicitud = this.BuscarCorresponsal(corresponsales, ((MarcaTercero)this._ventana.MarcaTercero).Corresponsal);

        //    this._ventana.DescripcionCorresponsalDatos = null == ((MarcaTercero)this._ventana.MarcaTercero).Corresponsal ?
        //                                                 null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //    this._ventana.DescripcionCorresponsalSolicitud = null == ((MarcaTercero)this._ventana.MarcaTercero).Corresponsal ?
        //                                                     null : ((Corresponsal)this._ventana.CorresponsalSolicitud).Descripcion;
        //    this._corresponsales = corresponsales;

        //    this._ventana.CorresponsalesEstanCargados = true;

        //    Mouse.OverrideCursor = null;
        //}

        #endregion

        #region Metodos de la lista de poderes


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

        public void CargarPoderes()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            MarcaTercero marcaTercero = (MarcaTercero)this._ventana.MarcaTercero;
            IList<Poder> poderes = this._poderServicios.ConsultarTodos();
            Poder poder = new Poder();
            poder.Id = int.MinValue;
            poderes.Insert(0, poder);
            //this._ventana.PoderesDatos = poderes;
            //this._ventana.PoderDatos = this.BuscarPoder(poderes, marcaTercero.Poder);

            this._ventana.PoderesEstanCargados = true;

            Mouse.OverrideCursor = null;
        }

        #endregion

        #region Marca

        /// <summary>
        /// Metodo que carga las Marcas registradas
        /// </summary>
        private void CargarMarca()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

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
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }

        /// <summary>
        /// Metodo que Consulta las Marcas
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
        /// Metodo que cambia la Marca
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
                    if (null != ((Marca)this._ventana.MarcaFiltrada).Pais)
                         this._ventana.PaisSolicitud = ((Marca)this._ventana.MarcaFiltrada).Pais.NombreEspanol;
                    //if (null != ((Marca)this._ventana.MarcaFiltrada).Internacional)
                    //     this._ventana.IdInternacionalByt = ((Marca)this._ventana.MarcaFiltrada).Internacional.Descripcion;
                    //if (null != ((Marca)this._ventana.MarcaFiltrada).Nacional)
                    //      this._ventana.IdNacionalByt = ((Marca)this._ventana.MarcaFiltrada).Nacional.Descripcion;
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


        //#region IMPRIMIR
        ///// <summary>
        ///// Método que se encarga de cargar la ventana de impresión de la MarcaTercero
        ///// </summary>
        ///// <param name="nombreBoton"></param>
        //public void IrImprimir(string nombreBoton)
        //{
        //    try
        //    {
        //        switch (nombreBoton)
        //        {
        //            case "_btnFM02":
        //                ImprimirFM02("Normal");
        //                break;
        //            case "_btnFM02Venen":
        //                ImprimirFM02Venen("Normal");
        //                break;
        //            case "_btnAnexoFM02":
        //                ImprimirAnexoFM02("Normal");
        //                break;
        //            case "_btnLFM02":
        //                ImprimirFM02("Laser");
        //                break;
        //            case "_btnLFM02Venen":
        //                ImprimirFM02Venen("Laser");
        //                break;
        //            case "_btnLAnexoFM02":
        //                ImprimirAnexoFM02("Laser");
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void ImprimirFM02(string modo)
        //{
        //    if (ValidarMarcaTerceroAntesDeImprimirFM02())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MARCAS";
        //        string procedimiento = modo.Equals("Laser") ? "P31" : "P1";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento((MarcaTercero)this._ventana.MarcaTercero, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
        //        if (planilla != null)
        //        {
        //            //Impresion _ventana =
        //            //    new Impresion(Recursos.Etiquetas.btnFM02, planilla.Folio.Replace("\n", Environment.NewLine));
        //            //_ventana.ShowDialog();

        //            ////Llamado al archivo .bat 
        //            ////if (_ventana.ClickImprimir)
        //            ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
        //            ////ConfigurationManager.AppSettings["txtPrint"]);

        //            //parametro.Via = 0;
        //            //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

        //            this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnFM02);
        //        }
        //    }
        //}

        //private void ImprimirFM02Venen(string modo)
        //{
        //    if (ValidarMarcaTerceroAntesDeImprimirFM02Venen())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MARCAS";
        //        string procedimiento = modo.Equals("Laser") ? "P32" : "P2";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento((MarcaTercero)this._ventana.MarcaTercero, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
        //        if (planilla != null)
        //        {
        //            //Impresion _ventana =
        //            //    new Impresion(Recursos.Etiquetas.btnFM02Venen, planilla.Folio.Replace("\n", Environment.NewLine));

        //            //_ventana.ShowDialog();

        //            ////Llamado al archivo .bat 
        //            ////if (_ventana.ClickImprimir)
        //            ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
        //            ////ConfigurationManager.AppSettings["txtPrint"]);

        //            //parametro.Via = 0;
        //            //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

        //            this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnFM02Venen);
        //        }
        //    }
        //}

        //private void ImprimirAnexoFM02(string modo)
        //{
        //    if (ValidarMarcaTerceroAntesDeImprimirAnexoFM02())
        //    {
        //        string paqueteProcedimiento = "PCK_MYP_MARCAS";
        //        string procedimiento = modo.Equals("Laser") ? "P33" : "P3";
        //        ParametroProcedimiento parametro =
        //            new ParametroProcedimiento((MarcaTercero)this._ventana.MarcaTercero, UsuarioLogeado, 1, paqueteProcedimiento, procedimiento);

        //        Planilla planilla = this._planillaServicios.ImprimirProcedimiento(parametro);
        //        if (planilla != null)
        //        {
        //            //Impresion _ventana =
        //            //    new Impresion(Recursos.Etiquetas.btnAnexoFM02, planilla.Folio.Replace("\n", Environment.NewLine));

        //            //_ventana.ShowDialog();

        //            ////Llamado al archivo .bat 
        //            ////if (_ventana.ClickImprimir)
        //            ////    this.EjecutarArchivoBAT(ConfigurationManager.AppSettings["batPrint"], 
        //            ////ConfigurationManager.AppSettings["txtPrint"]);

        //            //parametro.Via = 0;
        //            //planilla = this._planillaServicios.ImprimirProcedimiento(parametro);

        //            this.LlamarProcedimientoDeBaseDeDatos(parametro, Recursos.Etiquetas.btnAnexoFM02);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Método que realiza todas las validaciones de la MarcaTercero antes de imprimir
        ///// </summary>
        ///// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        //private bool ValidarMarcaTerceroAntesDeImprimirFM02()
        //{
        //    bool retorno = true;

        //    MarcaTercero marcaTercero = CargarMarcaTerceroDeLaPantalla();

        //    if ((null == marcaTercero.Poder) || (marcaTercero.Poder.NumPoder.Equals("")))
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinNumeroDePoder) == retorno : retorno;

        //    if (((this._ventana.ClaseInternacional.Equals("")) && (this._ventana.ClaseNacional.Equals(""))) && (retorno))
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinClase) == retorno : retorno;

        //    if ((marcaTercero.EtiquetaDescripcion.Equals("")) && (retorno))
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinDescripcionDelSigno) == retorno : retorno;

        //    if ((null == marcaTercero.Distingue) || (marcaTercero.Distingue.Equals("")) && (retorno))
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinDistingue) == retorno : retorno;

        //    return retorno;
        //}

        ///// <summary>
        ///// Método que realiza todas las validaciones de la MarcaTercero antes de imprimir
        ///// </summary>
        ///// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        //private bool ValidarMarcaTerceroAntesDeImprimirFM02Venen()
        //{
        //    bool retorno = true;

        //    MarcaTercero marcaTercero = CargarMarcaTerceroDeLaPantalla();

        //    if ((null == marcaTercero.Distingue) || (marcaTercero.Distingue.Equals("")))
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinDistingue) == retorno : retorno;

        //    else if (marcaTercero.Distingue.Length > 1800)
        //        retorno = retorno ?
        //            this._ventana.MensajeAlerta(Recursos.MensajesConElUsuario.MarcaTerceroSinDistingue) == retorno : retorno;

        //    return retorno;
        //}

        ///// <summary>
        ///// Método que realiza todas las validaciones de la MarcaTercero antes de imprimir
        ///// </summary>
        ///// <returns>true en caso de que todo este correcto, false en caso contrario</returns>
        //private bool ValidarMarcaTerceroAntesDeImprimirAnexoFM02()
        //{
        //    bool retorno = true;

        //    return retorno;
        //}

        //#endregion
    }
}
