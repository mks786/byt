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
using Trascend.Bolet.Cliente.Ventanas.Corresponsales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Interesados;
using Trascend.Bolet.Cliente.Ventanas.Poderes;

using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes;
using Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes;

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
        private IListaDatosValoresServicios _listaDatosValoresServicios;
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
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
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

               
                IList<Condicion> condiciones = this._condicionServicios.ConsultarTodos();
                Condicion primeraCondicion = new Condicion();
                primeraCondicion.Id = int.MinValue;
                condiciones.Insert(0, primeraCondicion);
                this._ventana.Condiciones = condiciones;
                Condicion condicionBuscada = new Condicion();
                condicionBuscada.Id = ((Marca)this._ventana.Marca).NumeroCondiciones;
                this._ventana.Condicion = this.BuscarCondicion(condiciones, condicionBuscada);
                

                ///Falta buscar TipoEstados
                //IList<TipoEstado> tipoEstados = new List<TipoEstado>();
                //tipoEstados = this._tipoEstadoServicios.ConsultarTodos();
                //TipoEstado primerDetalle = new TipoEstado();
                //primerDetalle.Id = "NGN";
                //tipoEstados.Insert(0, primerDetalle);
                //this._ventana.Detalles = tipoEstados;

                this._ventana.Servicio = this.BuscarServicio((IList<Servicio>)this._ventana.Servicios, marca.Servicio);

                //this._ventana.BoletinConcesion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesConcesion, marca.BoletinConcesion);
                this._ventana.BoletinPublicacion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesPublicacion, marca.BoletinPublicacion);

                Interesado interesado = (this._interesadoServicios.ConsultarInteresadoConTodo(marca.Interesado));
                this._ventana.NombreInteresadoDatos = interesado.Nombre;
                this._ventana.NombreInteresadoSolicitud = interesado.Nombre;
                this._ventana.InteresadoPaisSolicitud = interesado.Pais.NombreEspanol;
                this._ventana.InteresadoCiudadSolicitud = interesado.Ciudad;
                this._ventana.IdInteresadoDatos = interesado.Id != int.MinValue ? interesado.Id.ToString() : "";
                this._ventana.IdInteresadoSolicitud = interesado.Id != 0 ? interesado.Id.ToString() : "";
                IList<Interesado> lstInteresadosMarcaDuplicada = new List<Interesado>();
                Interesado primerInteresado = new Interesado();
                interesado.Id = int.MinValue;
                lstInteresadosMarcaDuplicada.Add(primerInteresado);
                lstInteresadosMarcaDuplicada.Add(marca.Interesado);
                this._ventana.InteresadosSolicitud = lstInteresadosMarcaDuplicada;
                this._ventana.InteresadoSolicitud = this.BuscarInteresado(lstInteresadosMarcaDuplicada, marca.Interesado);
                this._ventana.InteresadosDatos = lstInteresadosMarcaDuplicada;
                this._ventana.InteresadoDatos = this.BuscarInteresado(lstInteresadosMarcaDuplicada, marca.Interesado);


                Asociado asociado = (this._asociadoServicios.ConsultarAsociadoConTodo(marca.Asociado));
                IList<Asociado> lstAsociadosMarcaDuplicada = new List<Asociado>();
                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                lstAsociadosMarcaDuplicada.Add(primerAsociado);
                lstAsociadosMarcaDuplicada.Add(asociado);
                this._ventana.AsociadosSolicitud = lstAsociadosMarcaDuplicada;
                this._ventana.AsociadoSolicitud = this.BuscarAsociado(lstAsociadosMarcaDuplicada, marca.Asociado);
                this._ventana.AsociadosDatos = lstAsociadosMarcaDuplicada;
                this._ventana.AsociadoDatos = this.BuscarAsociado(lstAsociadosMarcaDuplicada, marca.Asociado);

                //Corresponsal corresponsal = marca.Corresponsal;
                IList<Corresponsal> lstCorresponsalMarcaDuplicada = new List<Corresponsal>();
                Corresponsal primerCorresponsal = new Corresponsal();
                primerCorresponsal.Id = int.MinValue;
                lstCorresponsalMarcaDuplicada.Add(primerCorresponsal);
                lstCorresponsalMarcaDuplicada.Add(marca.Corresponsal);
                this._ventana.CorresponsalesSolicitud = lstCorresponsalMarcaDuplicada;
                this._ventana.CorresponsalesDatos = lstCorresponsalMarcaDuplicada;
                this._ventana.CorresponsalSolicitud = this._ventana.CorresponsalDatos = this.BuscarCorresponsal(lstCorresponsalMarcaDuplicada, marca.Corresponsal);
               
                
                this._ventana.NombreAsociadoDatos = marca.Asociado != null ? marca.Asociado.Nombre : "";
                this._ventana.NombreAsociadoSolicitud = marca.Asociado != null ? marca.Asociado.Nombre : "";
                this._ventana.IdAsociadoDatos = marca.Asociado != null && ((Asociado)marca.Asociado).Id != int.MinValue ? ((Asociado)marca.Asociado).Id.ToString() : "";
                this._ventana.IdAsociadoSolicitud = marca.Asociado != null && ((Asociado)marca.Asociado).Id != int.MinValue ? ((Asociado)marca.Asociado).Id.ToString() : "";

                this._ventana.DescripcionCorresponsalSolicitud = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";
                this._ventana.DescripcionCorresponsalDatos = marca.Corresponsal != null ? marca.Corresponsal.Descripcion : "";
                this._ventana.IdCorresponsalDatos = marca.Corresponsal != null && ((Corresponsal)marca.Corresponsal).Id != int.MinValue ? ((Corresponsal)marca.Corresponsal).Id.ToString() : "";
                this._ventana.IdCorresponsalSolicitud = marca.Corresponsal != null && ((Corresponsal)marca.Corresponsal).Id != int.MinValue ? ((Corresponsal)marca.Corresponsal).Id.ToString() : "";

               // this._ventana.NumPoderDatos = marca.Poder != null ? marca.Poder.NumPoder : "";
                this._ventana.NumPoderDatos = marca.Poder != null ? marca.Poder.Id.ToString() : "";
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
                CargarAsociadoInternacionalVacio();

                this._ventana.IdMarcaOrigenSolicitud = null;
                this._ventana.IdMarcaOrigenDatos = null;


                CalcularSaldos();

                #region Internacional

                IList<Pais> paisesInternacionales = this._paisServicios.ConsultarTodos();
                Pais primerPaisInt = new Pais();
                primerPaisInt.Id = int.MinValue;
                paisesInternacionales.Insert(0, primerPaisInt);
                this._ventana.PaisesInternacionales = paisesInternacionales;
                this._ventana.PaisesInternacionalesDatos = paisesInternacionales;

                IList<ListaDatosValores> localidades = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiLocalidadMarca));

                ListaDatosValores primerLocalidad = new ListaDatosValores();
                primerLocalidad.Descripcion = string.Empty;
                primerLocalidad.Valor = "NGN";

                localidades.Insert(0, primerLocalidad);
                this._ventana.TipoClaseInternacionales = localidades;
                this._ventana.TipoClaseInternacionalesDatos = localidades;

                #endregion

                if (_esMarcaDuplicada)
                {
                   CargarDatosDeMarcaDuplicada();
                }

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
                        marca.TipoEstado = !((TipoEstado)this._ventana.Detalle).Id.Equals("NGN") ? ((TipoEstado)this._ventana.Detalle) : null;

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

                    if (null != this._ventana.IdMarcaOrigenSolicitud && this._ventana.IdMarcaOrigenSolicitud != "")
                        marca.MarcaOrigen = int.Parse(this._ventana.IdMarcaOrigenSolicitud);

                    //if (marca.TipoEstado.Id.Equals("NGN"))
                    //{
                    //    marca.TipoEstado.Id = string.Empty;
                    //}

                    if (ValidarMarcaInternacional())
                    {

                        if (!this._ventana.EsMarcaNacional)
                            marca = AgregarDatosInternacionales(marca);
                        else
                            marca.LocalidadMarca = "N";

                        // Se modifica la validacion para poder ingresar una marca nueva si posee un interesado
                        // el poder es opcional y puede asignarse despues
                        //if (marca.Poder != null)
                        if(marca.Interesado != null)
                        {

                            int? exitoso = this._marcaServicios.InsertarOModificarMarca(marca, UsuarioLogeado.Hash);

                            if (!exitoso.Equals(null))
                            {
                                marca.Id = (int)exitoso;
                                this.Navegar(new ConsultarMarca(marca, null));
                            }

                        }
                        else
                            //this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorSinPoder, 0);
                            this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorSinInteresado, 0);
                    }
                    else
                    {
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorMarcaInternacional, 0);
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
        /// Método que se encarga de validar que los campos necesarios sean correctos para el ingreso de la marca
        /// </summary>
        /// <returns></returns>
        private bool ValidarMarcaInternacional()
        {
            bool retorno = true;

            if (!this._ventana.EsMarcaNacional)
                if (((Pais)this._ventana.PaisInternacional).Id == int.MinValue)
                    retorno = false;
                else
                    if (((ListaDatosValores)this._ventana.TipoClaseInternacional).Valor.Equals("NGN"))
                        retorno = false;
                    else
                        if (this._ventana.ClaseInternacional.Equals(string.Empty))
                            retorno = false;

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de agregar los datos pertenecientes a las marcas internacionales a la marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        private Marca AgregarDatosInternacionales(Marca marca)
        {
            try
            {
                marca.LocalidadMarca = "I";
                marca.ClasificacionInternacional = ((ListaDatosValores)this._ventana.TipoClaseInternacionalDatos).Valor;
                marca.PaisInternacional = (Pais)this._ventana.PaisInternacional;
                marca.AsociadoInternacional = (Asociado)this._ventana.AsociadoInternacional;
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

            return marca;
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

                    CalcularSaldos();
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

            Asociado primerAsociado = new Asociado(int.MinValue);


            Asociado asociado = new Asociado();
            IList<Asociado> asociadosFiltrados;
            if (filtrarEn == 0)
            {
                asociado.Nombre = this._ventana.NombreAsociadoSolicitudFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoSolicitudFiltrar.Equals("") ? 0
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

                    this._ventana.PoderesDatos = null;
                    this._ventana.PoderesSolicitud = null;
                    this._ventana.PoderDatos = null;
                    this._ventana.PoderSolicitud = null;

                    this._ventana.NumPoderDatos = string.Empty;
                    this._ventana.NumPoderSolicitud = string.Empty;
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


                    this._ventana.PoderesDatos = null;
                    this._ventana.PoderesSolicitud = null;
                    this._ventana.PoderDatos = null;
                    this._ventana.PoderSolicitud = null;


                    this._ventana.NumPoderDatos = string.Empty;
                    this._ventana.NumPoderSolicitud = string.Empty;
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
                interesado.Id = this._ventana.IdInteresadoSolicitudFiltrar.Equals("") ? 0
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
                    ((Marca)this._ventana.Marca).Poder = (Poder)this._ventana.PoderSolicitud;
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

        /// <summary>
        /// Método que carga los poderes existentes entre un agente y un interesado
        /// </summary>
        private void CargarPoderesEntreInteresadoAgente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            if ((this._ventana.InteresadoSolicitud != null) && (this._ventana.Agente != null))
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



        public void VerificarMarcaOrigenEscrita(String tabFiltrar)
        {
            

            switch (tabFiltrar)
            {
                case ("_txtIdMarcaOrigenSolicitud"):
                    if ((this._ventana.IdMarcaOrigenSolicitud == null) || this._ventana.IdMarcaOrigenSolicitud.Equals(String.Empty) || this._ventana.IdMarcaOrigenSolicitud.Equals(""))
                        this._ventana.Mensaje("Haga doble click sobre el campo de texto para obtener la Marca de Origen", 0);
                    
                    break;

                case ("_txtIdMarcaOrigenDatos"):
                    if ((this._ventana.IdMarcaOrigenDatos == null) || this._ventana.IdMarcaOrigenDatos.Equals(String.Empty) || this._ventana.IdMarcaOrigenDatos.Equals(""))
                        this._ventana.Mensaje("Haga doble click sobre el campo de texto para obtener la Marca de Origen", 0);
                    break;
            }

        }


        /// <summary>
        /// Método que consulta la clase internacional y lo pega en distingue
        /// </summary>
        public void TomarClaseInternacional()
        {

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                Internacional internacionalAux = new Internacional();

                internacionalAux = _internacionalServicios.ConsultarPorId(new Internacional(int.Parse(this._ventana.IdInternacional)));

                if (internacionalAux != null)
                {
                    this._ventana.DistingueSolicitud = internacionalAux.Descripcion;
                    this._ventana.DistingueDatos = internacionalAux.Descripcion;
                }

                else
                {
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ClaseInternacionalNoExiste, 0);
                    this._ventana.IdInternacional = String.Empty;
                }

               
                //this._ventana.DistingueSolicitud = internacionalAux.Descripcion;
                //this._ventana.DistingueDatos = internacionalAux.Descripcion;
                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


            }

            catch (Exception e)
            {
                logger.Error(e.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
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


        /// <summary>
        /// Método que consulta al asociado
        /// </summary>
        /// <returns></returns>
        public bool ConsultarAsociado()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if ((!this._ventana.IdAsociadoInternacionalFiltrar.Equals(string.Empty)) || (!this._ventana.NombreAsociadoInternacionalFiltrar.Equals(string.Empty)))
                {
                    Asociado asociadoAux = new Asociado();
                    asociadoAux.Id = !this._ventana.IdAsociadoInternacionalFiltrar.Equals(string.Empty) ? int.Parse(this._ventana.IdAsociadoInternacionalFiltrar) : 0;
                    asociadoAux.Nombre = !this._ventana.NombreAsociadoInternacionalFiltrar.Equals(string.Empty) ? this._ventana.NombreAsociadoInternacionalFiltrar : string.Empty;

                    IList<Asociado> resultados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoAux);

                    resultados.Insert(0, new Asociado());

                    this._ventana.AsociadosInternacionalesDatos = resultados;
                    this._ventana.AsociadosInternacionales = resultados;

                    retorno = true;
                }

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


        /// <summary>
        /// Método que consulta el asociado desde la pestana de datos
        /// </summary>
        /// <returns></returns>
        public bool ConsultarAsociadoDatos()
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                if ((!this._ventana.IdAsociadoInternacionalFiltrarDatos.Equals(string.Empty)) || (!this._ventana.NombreAsociadoInternacionalFiltrarDatos.Equals(string.Empty)))
                {
                    Asociado asociadoAux = new Asociado();
                    asociadoAux.Id = !this._ventana.IdAsociadoInternacionalFiltrarDatos.Equals(string.Empty) ? int.Parse(this._ventana.IdAsociadoInternacionalFiltrarDatos) : 0;
                    asociadoAux.Nombre = !this._ventana.NombreAsociadoInternacionalFiltrarDatos.Equals(string.Empty) ? this._ventana.NombreAsociadoInternacionalFiltrarDatos : string.Empty;

                    IList<Asociado> resultados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoAux);

                    resultados.Insert(0, new Asociado());

                    this._ventana.AsociadosInternacionalesDatos = resultados;
                    this._ventana.AsociadosInternacionales = resultados;

                    retorno = true;
                }

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


        /// <summary>
        /// Método que consulta el asociado desde la pestana de solicitud
        /// </summary>
        /// <returns></returns>
        public bool CambiarAsociadoInternacionalSolicitud()
        {
            bool retorno = false;

            try
            {
                if (this._ventana.AsociadoInternacional != null)
                {
                    if (((Asociado)this._ventana.AsociadoInternacional).Id != 0)
                    {
                        this._ventana.TextoAsociadoInternacional = ((Asociado)this._ventana.AsociadoInternacional).Nombre;
                        this._ventana.AsociadoInternacionalDatos = this._ventana.AsociadoInternacional;
                        this._ventana.AsociadoInternacional = this._ventana.AsociadoInternacional;
                    }
                    else
                    {
                        this._ventana.AsociadoInternacional = null;
                        this._ventana.AsociadoInternacionalDatos = null;
                    }
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return retorno;
        }


        /// <summary>
        /// Método que se encarga de cambiar el asociado internacional
        /// </summary>
        /// <returns></returns>
        public bool CambiarAsociadoInternacionalDatos()
        {
            bool retorno = false;

            try
            {
                if (this._ventana.AsociadoInternacionalDatos != null)
                {
                    if (((Asociado)this._ventana.AsociadoInternacionalDatos).Id != 0)
                    {
                        this._ventana.TextoAsociadoInternacional = ((Asociado)this._ventana.AsociadoInternacionalDatos).Nombre;
                        this._ventana.AsociadoInternacional = this._ventana.AsociadoInternacional;
                        this._ventana.AsociadoInternacionalDatos = this._ventana.AsociadoInternacional;
                    }
                    else
                    {
                        this._ventana.AsociadoInternacional = null;
                        this._ventana.AsociadoInternacionalDatos = null;
                    }

                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return retorno;
        }


        public void IrVentanaAsociado()
        {
            if ((Asociado)this._ventana.AsociadoSolicitud != null)
            {
                Asociado asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;
                Navegar(new ConsultarAsociado(asociado, this._ventana,false));
            }
        }


        public void IrVentanaConsultarMarca(String filtrarEnTab)
        {
            if(filtrarEnTab.Equals("_btnIrMarcaOrigenSolicitud"))
                ((Marca)this._ventana.Marca).MarcaOrigen = int.Parse(this._ventana.IdMarcaOrigenSolicitud);

            else if(filtrarEnTab.Equals("_btnIrMarcaOrigenDatos"))
                ((Marca)this._ventana.Marca).MarcaOrigen = int.Parse(this._ventana.IdMarcaOrigenDatos);
           

            
            if (((Marca)this._ventana.Marca).MarcaOrigen.HasValue)
            {
                int? codigoMarcaOrigen = ((Marca)this._ventana.Marca).MarcaOrigen;
                int codigoMarcaBuscar = codigoMarcaOrigen ?? default(int);

                Marca marcaOrigen = new Marca (codigoMarcaBuscar);
                Navegar(new ConsultarMarca(marcaOrigen,this._ventana,true));


            }
        }


        public void IrVentanaInteresado()
        {
            if ((Interesado)this._ventana.InteresadoSolicitud != null)
            {
                Interesado interesado = ((Interesado)this._ventana.InteresadoSolicitud).Id != int.MinValue ? (Interesado)this._ventana.InteresadoSolicitud : null;
                Navegar(new ConsultarInteresado(interesado, this._ventana));
            }
        }


        public void IrVentanaPoder()
        {
            if (((Poder)this._ventana.PoderSolicitud != null) && (((Poder)this._ventana.PoderSolicitud).Id != int.MinValue))
            {
                Poder poder = ((Marca)this._ventana.Marca).Poder.Id != int.MinValue ? ((Marca)this._ventana.Marca).Poder : null;
                Navegar(new ConsultarPoder(poder, this._ventana));
            }
        }


        public void IrVentanaCorresponsal()
        {
            if ((Corresponsal)this._ventana.CorresponsalSolicitud != null)
            {
                Corresponsal corresponsal = ((Corresponsal)this._ventana.CorresponsalSolicitud).Id != int.MinValue ? (Corresponsal)this._ventana.CorresponsalSolicitud : null;
                Navegar(new AgregarCorresponsal(this._ventana, corresponsal));
            }
        }


        public void CargarAsociadoInternacionalVacio()
        {
            IList<Asociado> asociados = new List<Asociado>();
            asociados.Add(new Asociado());
            this._ventana.AsociadosInternacionalesDatos = asociados;
            this._ventana.AsociadosInternacionales = asociados;
        }


        public void IrVentanaImprimirEdoCuenta()
        {
            if ((Asociado)this._ventana.AsociadoSolicitud != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;
                Navegar(new  PendientesRpt("2", Asociado));
                
            }
        }

        public void IrVentanaFacturacionDatos()
        {
            if ((Marca)this._ventana.Marca != null)
            {
                Marca Marca = ((Marca)this._ventana.Marca).Id != int.MinValue ? (Marca)this._ventana.Marca : null;
                string Id = System.Convert.ToString(Marca.Id);
                Navegar(new ConsultarFacVistaFacturaServicios(Id, "M"));

            }
        }

        public void CalcularSaldos()
        {
            if ((Asociado)this._ventana.AsociadoSolicitud != null)
            {
                Asociado Asociado = ((Asociado)this._ventana.AsociadoSolicitud).Id != int.MinValue ? (Asociado)this._ventana.AsociadoSolicitud : null;

                double? w_1, w_2, w_3, w_4, w_5, w_6, msaldope;
                w_1 = 0;
                w_2 = 0;
                w_3 = 0;
                w_4 = 0;
                w_5 = 0;
                w_6 = 0;
                msaldope = 0;
                string moneda = "";
                int casociado = Asociado.Id;
                int? dias = 30;
                CalcularSaldosAsociado(casociado, dias, ref w_1, ref w_2, ref w_3, ref w_4, ref w_5, ref w_6, ref msaldope, ref  moneda);

                if (moneda == "US")
                {

                    this._ventana.SaldoVencidoSolicitud = System.Convert.ToString(w_2);
                    this._ventana.SaldoPorVencerSolicitud = System.Convert.ToString(w_4);
                    this._ventana.TotalSolicitud = System.Convert.ToString(w_2 + w_4);

                }
                else
                {
                    this._ventana.SaldoVencidoSolicitud = System.Convert.ToString(w_1);
                    this._ventana.SaldoPorVencerSolicitud = System.Convert.ToString(w_3);
                    this._ventana.TotalSolicitud = System.Convert.ToString(w_1 + w_3);
                }
            }

        }

        /// <summary>
        /// Metodo para buscar una Marca de Origen seleccionada en la lista
        /// </summary>
        /// <param name="filtrarEnTab"></param>
        public void BuscarMarcaOrigen(String filtrarEnTab)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Marca marcaABuscar = new Marca();

            switch (filtrarEnTab)
            {
                case "_btnConsultarMarcaOrigenSolicitud":
                    marcaABuscar.Id = this._ventana.IdMarcaOrigenSolicitudFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaOrigenSolicitudFiltrar);
                    break;

                case "_btnConsultarMarcaOrigenDatos":
                    marcaABuscar.Id = this._ventana.IdMarcaOrigenDatosFiltrar.Equals("") ? 0 : int.Parse(this._ventana.IdMarcaOrigenDatosFiltrar);
                    break;
            }

            IList<Marca> marcasFiltradas = this._marcaServicios.ObtenerMarcasFiltro(marcaABuscar);

            if (marcasFiltradas.Count != 0)
            {
                Marca primeraMarca = new Marca();
                primeraMarca.Id = int.MinValue;
                marcasFiltradas.Insert(0, primeraMarca);
                this._ventana.MarcaOrigenSolicitudLst = marcasFiltradas;
                this._ventana.MarcaOrigenDatosLst = marcasFiltradas;
               // this._ventana.MarcaOrigenDatos = marcasFiltradas;
                //this._ventana.PatenteMadreDatos = patentesFiltradas;
            }
            else
            {
                switch (filtrarEnTab)
                {
                    case "_btnConsultarMarcaOrigenSolicitud":
                        this._ventana.IdMarcaOrigenSolicitudFiltrar = null;
                        break;

                    case "_btnConsultarMarcaOrigenDatos":
                        this._ventana.IdMarcaOrigenDatosFiltrar = null;
                        break;
                }

            }

        }



        /// <summary>
        /// Metodo para cambiar la marca de origen cuando se selecciona la lista de marcas de origen en 
        /// la pestana Solicitud de la ventana ConsultarMarca
        /// </summary>
        public void CambiarMarcaOrigen(String tabFiltrar)
        {

            Marca marcaOrigen;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (tabFiltrar.Equals("Solicitud"))
                {
                    if (((Marca)this._ventana.MarcaOrigenSolicitudSelec) != null && ((Marca)this._ventana.MarcaOrigenSolicitudSelec).Id != int.MinValue)
                    {
                        marcaOrigen = this._marcaServicios.ConsultarMarcaConTodo(((Marca)this._ventana.MarcaOrigenSolicitudSelec));
                        this._ventana.IdMarcaOrigenSolicitud = marcaOrigen.Id.ToString();
                        this._ventana.IdMarcaOrigenDatos = marcaOrigen.Id.ToString();
                        ((Marca)this._ventana.Marca).MarcaOrigen = marcaOrigen.Id;
                    }

                    else
                    {
                        hacerNullMarcaDeOrigen();

                    }
                }

                if (tabFiltrar.Equals("Datos"))
                {
                    if (((Marca)this._ventana.MarcaOrigenDatosSelec) != null && ((Marca)this._ventana.MarcaOrigenDatosSelec).Id != int.MinValue)
                    {
                        marcaOrigen = this._marcaServicios.ConsultarMarcaConTodo(((Marca)this._ventana.MarcaOrigenDatosSelec));
                        this._ventana.IdMarcaOrigenSolicitud = marcaOrigen.Id.ToString();
                        this._ventana.IdMarcaOrigenDatos = marcaOrigen.Id.ToString();
                        ((Marca)this._ventana.Marca).MarcaOrigen = marcaOrigen.Id;
                    }

                    else
                    {
                        hacerNullMarcaDeOrigen();
                    }
                }


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

            }
            catch (ApplicationException e)
            {
                this._ventana.IdMarcaOrigenSolicitud = null;
                this._ventana.IdMarcaOrigenDatos = null;
            }

        }


        public void hacerNullMarcaDeOrigen()
        {
            this._ventana.IdMarcaOrigenSolicitud = null;
            this._ventana.IdMarcaOrigenDatos = null;
            ((Marca)this._ventana.Marca).MarcaOrigen = null;
        }


    }
}
