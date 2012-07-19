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
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorAgregarMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IAgregarMarca _ventana;
        private bool _esMarcaDuplicada = false;

        private IMarcaServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IAgenteServicios _agenteServicios;
        private IPoderServicios _poderServicios;
        private IBoletinServicios _boletinServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IInteresadoServicios _interesadoServicios;
        private IInternacionalServicios _internacionalServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private ICorresponsalServicios _corresponsalServicios;
        private ICondicionServicios _condicionServicios;
        private IStatusWebServicios _statusWebServicios;

        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;
        private IList<Auditoria> _auditorias;
        private IList<Poder> _poderesInterseccion;

      

        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorAgregarMarca(IAgregarMarca ventana, object marcaDuplicada)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                Marca marca;

                if (marcaDuplicada != null)
                {
                    marca = (Marca)marcaDuplicada;
                    _esMarcaDuplicada = true;
                }
                else
                {
                    marca = new Marca(1);
                }

                marca.Nacional = new Nacional();
                marca.Internacional = new Internacional();
                if (!_esMarcaDuplicada)
                    marca.Interesado = new Interesado();
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
                this._statusWebServicios = (IStatusWebServicios)Activator.GetObject(typeof(IStatusWebServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["StatusWebServicios"]);
                this._internacionalServicios = (IInternacionalServicios)Activator.GetObject(typeof(IInternacionalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InternacionalServicios"]);

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
        /// Método que se encarga de duplicar una marca ya existente
        /// </summary>
        public void CargarDatosDeMarcaDuplicada()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Marca marca = (Marca)this._ventana.Marca;

                marca.Id = 1;

                marca.CodigoInscripcion = "";
                marca.CodigoRegistro = "";

                this._ventana.TipoMarcaDatos = this.BuscarTipoMarca((IList<ListaDatosDominio>)this._ventana.TipoMarcasDatos, marca.Tipo);

                this._ventana.Agente = this.BuscarAgente((IList<Agente>)this._ventana.Agentes, marca.Agente);

                this._ventana.PaisSolicitud = this.BuscarPais((IList<Pais>)this._ventana.PaisesSolicitud, marca.Pais);

                this._ventana.StatusWeb = this.BuscarStatusWeb((IList<StatusWeb>)this._ventana.StatusWebs, marca.StatusWeb);

                //Falta buscar Condiciones
                IList<Condicion> condiciones = this._condicionServicios.ConsultarTodos();
                Condicion primeraCondicion = new Condicion();
                primeraCondicion.Id = int.MinValue;
                condiciones.Insert(0, primeraCondicion);
                this._ventana.Condiciones = condiciones;

                ///Falta buscar TipoEstados
                IList<TipoEstado> tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                TipoEstado primerDetalle = new TipoEstado();
                primerDetalle.Id = "NGN";
                tipoEstados.Insert(0, primerDetalle);
                this._ventana.Detalles = tipoEstados;

                this._ventana.Servicio = this.BuscarServicio((IList<Servicio>)this._ventana.Servicios, marca.Servicio);

                //this._ventana.BoletinConcesion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesConcesion, marca.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesPublicacion, marca.BoletinPublicacion);

                Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marca.Interesado));
                this._ventana.NombreInteresadoDatos = interesado.Nombre;
                this._ventana.NombreInteresadoSolicitud = interesado.Nombre;
                this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;

                this._ventana.NombreAsociadoDatos = marca.Asociado != null ? marca.Asociado.Nombre : "";
                this._ventana.NombreAsociadoSolicitud = marca.Asociado != null ? marca.Asociado.Nombre : "";

                this._ventana.DescripcionCorresponsalSolicitud = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";
                this._ventana.DescripcionCorresponsalDatos = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";

                this._ventana.NumPoderDatos = marca.Poder != null ? marca.Poder.NumPoder : "";
                this._ventana.NumPoderSolicitud = marca.Poder != null ? marca.Poder.Id.ToString() : "";
                this._ventana.Sapi = marca.Poder != null ? marca.Poder.NumPoder : "";

                this._ventana.Sector = this.BuscarSector((IList<ListaDatosDominio>)this._ventana.Sectores, marca.Sector);
                this._ventana.TipoReproduccion = this.BuscarTipoReproduccion((IList<ListaDatosDominio>)this._ventana.TipoReproducciones, marca.Tipo);

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
        /// Método que se encarga de actualizar el título de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarMarca,
                Recursos.Ids.AgregarMarca);

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

                this.ActualizarTitulo();

                IList<ListaDatosDominio> tiposMarcas = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));
                ListaDatosDominio primerTipoMarca = new ListaDatosDominio();
                primerTipoMarca.Id = "NGN";
                tiposMarcas.Insert(0, primerTipoMarca);
                this._ventana.TipoMarcasDatos = tiposMarcas;
                this._ventana.TipoMarcasSolicitud = tiposMarcas;


                IList<Agente> agentes = this._agenteServicios.ConsultarTodos();
                Agente primerAgente = new Agente();
                primerAgente.Id = "NGN";
                agentes.Insert(0, primerAgente);
                this._ventana.Agentes = agentes;

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.PaisesSolicitud = paises;

                IList<StatusWeb> statusWebs = this._statusWebServicios.ConsultarTodos();
                StatusWeb primerStatus = new StatusWeb();
                primerStatus.Id = "NGN";
                statusWebs.Insert(0, primerStatus);
                this._ventana.StatusWebs = statusWebs;

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
                //Servicio primerServicio = new Servicio();
                //primerServicio.Id = "NGN";
                //servicios.Insert(0, primerServicio);
                this._ventana.Servicios = servicios;

                Marca marcaAux = new Marca();
                marcaAux.Servicio = new Servicio("MS");
                this._ventana.Servicio = this.BuscarServicio(servicios, marcaAux.Servicio);

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.BoletinesOrdenPublicacion = boletines;
                this._ventana.BoletinesPublicacion = boletines;
                this._ventana.BoletinesConcesion = boletines;
                this._ventana.BoletinOrdenPublicacion = this.BuscarBoletin(boletines, primerBoletin);
                this._ventana.BoletinConcesion = this.BuscarBoletin(boletines, primerBoletin);
                this._ventana.BoletinPublicacion = this.BuscarBoletin(boletines, primerBoletin);

               // Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marca.Interesado));
               

                //IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                //Boletin primerBoletin = new Boletin();
                //primerBoletin.Id = int.MinValue;
                //boletines.Insert(0, primerBoletin);
                //this._ventana.BoletinesOrdenPublicacion = boletines;
                //this._ventana.BoletinesPublicacion = boletines;
                //this._ventana.BoletinConcesion = boletines;

                IList<ListaDatosDominio> sectores = this._listaDatosDominioServicios.
                                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiSector));
                ListaDatosDominio primerSector = new ListaDatosDominio();
                primerSector.Id = "NGN";
                sectores.Insert(0, primerSector);
                this._ventana.Sectores = sectores;

                IList<ListaDatosDominio> tipoReproducciones = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiTipoReproduccion));
                ListaDatosDominio primerTipoReproduccion = new ListaDatosDominio();
                primerTipoReproduccion.Id = "NGN";
                tipoReproducciones.Insert(0, primerTipoReproduccion);
                this._ventana.TipoReproducciones = tipoReproducciones;

                IList<ListaDatosDominio> tipoClasesNacional = this._listaDatosDominioServicios.
                    ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaTipoClaseNacional));
                ListaDatosDominio primerTipoClase = new ListaDatosDominio();
                primerTipoClase.Id = "NGN";
                tipoClasesNacional.Insert(0, primerTipoClase);
                this._ventana.TiposClaseNacional = tipoClasesNacional;

                IList<Poder> poderes = new List<Poder>();
                Poder primerPoder = new Poder();
                primerPoder.Id = int.MinValue;
                poderes.Insert(0, primerPoder);
                this._ventana.PoderesDatos = poderes;
                this._ventana.PoderesSolicitud = poderes;

                if (_esMarcaDuplicada)
                    CargarDatosDeMarcaDuplicada();

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
        /// Método que realiza toda la lógica para agregar la Marca dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (ValidarPoder())
                {
                    Marca marca = (Marca)this._ventana.Marca;

                    marca.Operacion = "CREATE";

                    if (null != this._ventana.Agente)
                        marca.Agente = !((Agente)this._ventana.Agente).Id.Equals("NGN") ? (Agente)this._ventana.Agente : null;

                    if (null != this._ventana.AsociadoSolicitud)
                        marca.Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

                    if (null != this._ventana.BoletinOrdenPublicacion)
                        marca.BoletinOrdenPublicacion = ((Boletin)this._ventana.BoletinOrdenPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinOrdenPublicacion : null;

                    if (null != this._ventana.BoletinConcesion)
                        marca.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue ? (Boletin)this._ventana.BoletinConcesion : null;

                    if (null != this._ventana.BoletinPublicacion)
                        marca.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue ? (Boletin)this._ventana.BoletinPublicacion : null;

                    if (null != this._ventana.InteresadoSolicitud)
                        marca.Interesado = !((Interesado)this._ventana.InteresadoSolicitud).Id.Equals("NGN") ? ((Interesado)this._ventana.InteresadoSolicitud) : null;

                    if (null != this._ventana.Servicio)
                        marca.Servicio = !((Servicio)this._ventana.Servicio).Id.Equals("NGN") ? ((Servicio)this._ventana.Servicio) : null;

                    if (null != this._ventana.Detalle)
                        marca.TipoEstado = !((TipoEstado)this._ventana.Detalle).Id.Equals("") ? ((TipoEstado)this._ventana.Detalle) : null;

                    if (null != this._ventana.PoderSolicitud)
                        marca.Poder = ((Poder)this._ventana.PoderSolicitud).Id != int.MinValue ? ((Poder)this._ventana.PoderSolicitud) : null;

                    if (null != this._ventana.PaisSolicitud)
                        marca.Pais = ((Pais)this._ventana.PaisSolicitud).Id != int.MinValue ? ((Pais)this._ventana.PaisSolicitud) : null;

                    if (null != this._ventana.CorresponsalSolicitud)
                        marca.Corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? ((Corresponsal)this._ventana.CorresponsalSolicitud) : null;

                    if (null != this._ventana.Sector)
                        marca.Sector = !((ListaDatosDominio)this._ventana.Sector).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.Sector).Id : null;

                    if (null != this._ventana.TipoReproduccion)
                        marca.TipoRps = ((ListaDatosDominio)this._ventana.TipoReproduccion).Id[0];

                    if (null != this._ventana.TipoClaseNacional)
                        marca.TipoCnac = !((ListaDatosDominio)this._ventana.TipoClaseNacional).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoClaseNacional).Id : null;

                    if (null != this._ventana.TipoMarcaDatos)
                        marca.Tipo = !((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id.Equals("NGN") ? ((ListaDatosDominio)this._ventana.TipoMarcaDatos).Id : null;

                    if (string.IsNullOrEmpty(this._ventana.IdInternacional))
                        marca.Internacional = null;

                    if (string.IsNullOrEmpty(this._ventana.IdNacional))
                        marca.Nacional = null;

                    

                    int? exitoso = this._marcaServicios.InsertarOModificarMarca(marca, UsuarioLogeado.Hash);

                    if (!exitoso.Equals(null))
                    {
                        marca.Id = (int)exitoso;
                        this.Navegar(new ConsultarMarca(marca));
                    }
                }
                else
                {
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



        #region Metodos de los filtros de asociados

        /// <summary>
        /// Método que cambia el asociado solicitud seleccionado
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
        /// Método que cambia el asociado datos seleccionado
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
        /// Método que se encarga de buscar un asociado
        /// </summary>
        /// <param name="filtrarEn"></param>
        public void BuscarAsociado(int filtrarEn)
        {
            #region antigua busquedaAsociado
            //#region trace
            //if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //IEnumerable<Asociado> asociadosFiltrados = this._asociados;

            //if (filtrarEn == 0)
            //{
            //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoSolicitudFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Id == int.Parse(this._ventana.IdAsociadoSolicitudFiltrar)
            //                             select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoSolicitudFiltrar))
            //    {
            //        asociadosFiltrados = from p in asociadosFiltrados
            //                             where p.Nombre != null &&
            //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoSolicitudFiltrar.ToLower())
            //                             select p;
            //    }
            //}
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
            //// filtrarEn = 0 significa en el listview de la pestaña solicitud
            //// filtrarEn = 1 significa en el listview de la pestaña Datos 
            //if (filtrarEn == 0)
            //{
            //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
            //        this._ventana.AsociadosSolicitud = asociadosFiltrados.ToList<Asociado>();
            //    else
            //    {
            //        this._ventana.AsociadosSolicitud = this._asociados;
            //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            //    }
            //}
            //else
            //{
            //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
            //        this._ventana.AsociadosDatos = asociadosFiltrados.ToList<Asociado>();
            //    else
            //    {
            //        this._ventana.AsociadosDatos = this._asociados;
            //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            //    }
            //}
            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Asociado primerAsociado= new Asociado(int.MinValue);


            Asociado asociado = new Asociado();
            IList<Asociado> asociadosFiltrados;
            if (filtrarEn == 0)
            {
                asociado.Nombre = this._ventana.NombreAsociadoSolicitudFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoSolicitudFiltrar.Equals("")? 0
                                  : int.Parse(this._ventana.IdAsociadoSolicitudFiltrar);
            }
            else
            {
                asociado.Nombre = this._ventana.NombreAsociadoDatosFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoDatosFiltrar.Equals("") ? 0
                                  : int.Parse(this._ventana.IdAsociadoDatosFiltrar);
            }
            if ((!asociado.Nombre.Equals("")) || (asociado.Id != 0))
                asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociado);
            else
                asociadosFiltrados = new List<Asociado>();

            if (asociadosFiltrados.Count != 0)
            {
                asociadosFiltrados.Insert(0, primerAsociado);
                this._ventana.AsociadosSolicitud = asociadosFiltrados;
                this._ventana.AsociadoSolicitud = primerAsociado;
                this._ventana.AsociadosDatos = asociadosFiltrados;
                this._ventana.AsociadoDatos = primerAsociado;
            }
            else
            {
                asociadosFiltrados.Insert(0, primerAsociado);
                this._ventana.AsociadosSolicitud = this._asociados;
                this._ventana.AsociadoSolicitud = primerAsociado;
                this._ventana.AsociadosDatos = this._asociados;
                this._ventana.AsociadoDatos = primerAsociado;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        /// <summary>
        /// Método que se encarga de cargar los asociados
        /// </summary>
        public void CargarAsociados()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            //Mouse.OverrideCursor = Cursors.Wait;

            //Marca marca = (Marca)this._ventana.Marca;
            //IList<Asociado> asociados = this._asociadoServicios.ConsultarTodos();
            //Asociado primerAsociado = new Asociado();
            //primerAsociado.Id = int.MinValue;
            //asociados.Insert(0, primerAsociado);
            //this._ventana.AsociadosSolicitud = asociados;
            //this._ventana.AsociadosDatos = asociados;
            //this._asociados = asociados;
            //this._ventana.AsociadosEstanCargados = true;

            //if (_esMarcaDuplicada)
            //{
            //    this._ventana.AsociadoSolicitud = this.BuscarAsociado(asociados, marca.Asociado);
            //    this._ventana.AsociadoDatos = this.BuscarAsociado(asociados, marca.Asociado);
            //}

            //Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion

        #region Metodos de los filtros de interesados

        /// <summary>
        /// Método que cambia el interesado solicitud
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
        /// Método que se encarga de cambiar interesado datos
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
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);
                    this._ventana.InteresadoDatos = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.InteresadoDatos);
                    this._ventana.NombreInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.IdInteresadoDatos = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();

                    this._ventana.InteresadoSolicitud = (Interesado)this._ventana.InteresadoDatos;
                    this._ventana.NombreInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Nombre;
                    this._ventana.IdInteresadoSolicitud = ((Interesado)this._ventana.InteresadoDatos).Id.ToString();
                    this._ventana.InteresadoPaisSolicitud = interesadoAux.Pais != null ? interesadoAux.Pais.NombreEspanol : "";
                    this._ventana.InteresadoCiudadSolicitud = interesadoAux.Ciudad != null ? interesadoAux.Ciudad : "";
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
        /// Método que filtra interesado
        /// </summary>
        /// <param name="filtrarEn"></param>
        public void BuscarInteresado(int filtrarEn)
        {

            #region antigua busquedaInteresados
            //#region trace
            //if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion

            //IEnumerable<Interesado> interesadosFiltrados = this._interesados;

            //if (filtrarEn == 0)
            //{
            //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoSolicitudFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Id == int.Parse(this._ventana.IdInteresadoSolicitudFiltrar)
            //                               select p;
            //    }

            //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoSolicitudFiltrar))
            //    {
            //        interesadosFiltrados = from p in interesadosFiltrados
            //                               where p.Nombre != null &&
            //                               p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoSolicitudFiltrar.ToLower())
            //                               select p;
            //    }
            //}
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
            //// filtrarEn = 0 significa en el listview de la pestaña solicitud
            //// filtrarEn = 1 significa en el listview de la pestaña Datos 
            //if (filtrarEn == 0)
            //{
            //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
            //        this._ventana.InteresadosSolicitud = interesadosFiltrados.ToList<Interesado>();
            //    else
            //    {
            //        this._ventana.InteresadosSolicitud = this._interesados;
            //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            //    }
            //}
            //else
            //{
            //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
            //        this._ventana.InteresadosDatos = interesadosFiltrados.ToList<Interesado>();
            //    else
            //    {
            //        this._ventana.InteresadosDatos = this._interesados;
            //        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            //    }
            //}

            //#region trace
            //if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
            //    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            //#endregion
            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado primerInteresado = new Interesado(int.MinValue);


            Interesado interesado = new Interesado();
            IList<Interesado> interesadosFiltrados;
            if (filtrarEn == 0)
            {
                interesado.Nombre = this._ventana.NombreInteresadoSolicitudFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoSolicitudFiltrar.Equals("")? 0
                                    : int.Parse(this._ventana.IdInteresadoSolicitudFiltrar);


            }
            else
            {
                interesado.Nombre = this._ventana.NombreInteresadoDatosFiltrar.ToUpper();
                interesado.Id = this._ventana.IdInteresadoDatosFiltrar.Equals("") ? 0
                                    : int.Parse(this._ventana.IdInteresadoDatosFiltrar); 

            }
            if ((!interesado.Nombre.Equals("")) || (interesado.Id != 0))
                interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
            else
                interesadosFiltrados = new List<Interesado>();

            if (interesadosFiltrados.Count != 0)
            {
                interesadosFiltrados.Insert(0, interesado);
                this._ventana.InteresadosSolicitud = interesadosFiltrados;
                this._ventana.InteresadoSolicitud = interesado;
                this._ventana.InteresadosDatos = interesadosFiltrados;
                this._ventana.InteresadoDatos = interesado;
            }
            else
            {
                interesadosFiltrados.Insert(0, primerInteresado);
                this._ventana.InteresadosSolicitud = this._interesados;
                this._ventana.InteresadoSolicitud = primerInteresado;
                this._ventana.InteresadosDatos = this._interesados;
                this._ventana.InteresadoDatos = primerInteresado;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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

            //Mouse.OverrideCursor = Cursors.Wait;
            //Marca marca = (Marca)this._ventana.Marca;

            //IList<Interesado> interesados = this._interesadoServicios.ConsultarTodos();
            //this._ventana.InteresadosDatos = interesados;
            //this._ventana.InteresadosSolicitud = interesados;
            //this._interesados = interesados;
            //this._ventana.InteresadosEstanCargados = true;

            //if (_esMarcaDuplicada)
            //{
            //    Interesado interesado = this.BuscarInteresado(interesados, marca.Interesado);
            //    this._ventana.InteresadoSolicitud = interesado;
            //    this._ventana.InteresadoDatos = interesado;
            //}

            //Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        #endregion

        #region Metodos de los filtros de corresponsales

        /// <summary>
        /// Método que cambia el corresponsal solicitud
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

        public void CargarCorresponsales()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Marca marca = (Marca)this._ventana.Marca;

            IList<Corresponsal> corresponsales = this._corresponsalServicios.ConsultarTodos();
            Corresponsal primerCorresponsal = new Corresponsal();
            primerCorresponsal.Id = int.MinValue;
            corresponsales.Insert(0, primerCorresponsal);
            this._ventana.CorresponsalesSolicitud = corresponsales;
            this._ventana.CorresponsalesDatos = corresponsales;
            this._corresponsales = corresponsales;

            this._ventana.CorresponsalesEstanCargados = true;

            if (_esMarcaDuplicada)
            {
                this._ventana.CorresponsalDatos = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);
                this._ventana.CorresponsalSolicitud = this.BuscarCorresponsal(corresponsales, ((Marca)this._ventana.Marca).Corresponsal);
            }

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
                    this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderSolicitud).Id.ToString();
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderSolicitud).Id.ToString();
                    this._ventana.PoderDatos = (Poder)this._ventana.PoderSolicitud;
                    this._ventana.Sapi = ((Poder)this._ventana.PoderSolicitud).NumPoder;
                    this._ventana.ConvertirEnteroMinimoABlanco();
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
                    this._ventana.NumPoderDatos = ((Poder)this._ventana.PoderDatos).Id.ToString();
                    this._ventana.PoderSolicitud = (Poder)this._ventana.PoderDatos;
                    this._ventana.NumPoderSolicitud = ((Poder)this._ventana.PoderDatos).Id.ToString();
                    this._ventana.Sapi = ((Poder)this._ventana.PoderDatos).NumPoder;
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

            //Mouse.OverrideCursor = Cursors.Wait;

            //Marca marca = (Marca)this._ventana.Marca;
            //IList<Poder> poderes = this._poderServicios.ConsultarTodos();
            //Poder poder = new Poder();
            //poder.Id = int.MinValue;
            //poderes.Insert(0, poder);
            //this._ventana.PoderesDatos = poderes;
            //this._ventana.PoderesSolicitud = poderes;

            //this._ventana.PoderesEstanCargados = true;

            //if (_esMarcaDuplicada)
            //{
            //    this._ventana.PoderDatos = this.BuscarPoder(poderes, marca.Poder);
            //    this._ventana.PoderSolicitud = this.BuscarPoder(poderes, marca.Poder);
            //}

            //Mouse.OverrideCursor = null;

            Mouse.OverrideCursor = Cursors.Wait;

            CargarPoderesEntreInteresadoAgente();

            Marca marca = null != this._ventana.Marca ? (Marca)this._ventana.Marca : new Marca();
            if (this._poderesInterseccion.Count != 0)
            {
                this._ventana.PoderesSolicitud = this._poderesInterseccion;
                this._ventana.PoderesDatos = this._poderesInterseccion;
            }
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

            if ((this._ventana.InteresadoSolicitud != null) && (this._ventana.Agente!= null))
            {
                _poderesInterseccion = this._poderServicios
                    .ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.Agente, (Interesado)this._ventana.InteresadoSolicitud);

                if (_poderesInterseccion.Count() == 0)
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorInteresadoNoPoseePoderesConAgente, 0);
                }
                else
                {
                    this._ventana.mostrarLstPoderSolicitud();
                }

                _poderesInterseccion.Insert(0, new Poder(int.MinValue));
            }
            else
            {
                IList<Poder> poderes = new List<Poder>();
                Poder primerPoder = new Poder();
                primerPoder.Id = int.MinValue;
                poderes.Insert(0, primerPoder);
                this._poderesInterseccion = poderes;
            }

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
                if (!this._ventana.NumPoderSolicitud.Equals(""))
                {
                    IList<Poder> poderesAux = new List<Poder>();

                    poderesAux = this._poderServicios
                            .ObtenerPoderesEntreAgenteEInteresado((Agente)this._ventana.Agente, (Interesado)this._ventana.InteresadoSolicitud);


                    foreach (Poder poder in poderesAux)
                    {
                        if (poder.Id == int.Parse(this._ventana.NumPoderSolicitud))
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
        /// Método que consulta la clase internacional y lo pega en distingue
        /// </summary>
        public void TomarClaseInternacional()
        {
            Internacional internacionalAux = new Internacional();

            internacionalAux = _internacionalServicios.ConsultarPorId(new Internacional(int.Parse(this._ventana.IdInternacional)));

            this._ventana.DistingueSolicitud = internacionalAux.Descripcion;
            this._ventana.DistingueDatos = internacionalAux.Descripcion;
        }

        /// <summary>
        /// Método que se ecarga la descripcion de la situacion
        /// </summary>
        /// <param name="tab"></param>
        public void DescripcionDetalle()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.DetalleDescripcion = ((TipoEstado)this._ventana.Detalle).Descripcion;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }
    }
}
