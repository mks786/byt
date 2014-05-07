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
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.Patentes
{
    class PresentadorConsultarPatentes : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IConsultarPatentes _ventana;
        private IPatenteServicios _patenteServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private IBoletinServicios _boletinServicios;
        private IAnualidadServicios _anualidadServicios;
        private ICesionPatenteServicios _cesionServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private IPaisServicios _paisServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;


        private IList<Patente> _patentes;
        private IList<CesionPatente> _cesiones;
        private IList<Asociado> _asociados;
        private IList<Asociado> _interesados;


        private int _filtroValido;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultarPatentes(IConsultarPatentes ventana)
        {
            try
            {
                this._ventana = ventana;
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                   ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._cesionServicios = (ICesionPatenteServicios)Activator.GetObject(typeof(ICesionPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CesionPatenteServicios"]);
                this._anualidadServicios = (IAnualidadServicios)Activator.GetObject(typeof(IAnualidadServicios),
                  ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AnualidadServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
            }
        }


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarPatente,
                Recursos.Ids.ConsultarPatente);
        }


        /// <summary>
        /// Método que carga los datos iniciales a mostrar en la página
        /// </summary>
        public void CargarPagina()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this._ventana.PatenteParaFiltrar = new Patente();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ActualizarTitulo();

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.BoletinesOrdenPublicacion = boletines;
                this._ventana.BoletinesPublicacion = boletines;
                this._ventana.BoletinesConcesion = boletines;

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

                IList<Pais> paises = this._paisServicios.ConsultarTodos();
                Pais primerPais = new Pais();
                primerPais.Id = int.MinValue;
                paises.Insert(0, primerPais);
                this._ventana.Paises = paises;
                this._ventana.PaisesInt = paises;

                IList<ListaDatosValores> tiposBusqueda = this._listaDatosValoresServicios.
                                ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiLocalidadMarca));
                this._ventana.TiposBusqueda = tiposBusqueda;
                this._ventana.TipoBusqueda = this.BuscarTipoDeBusqueda(tiposBusqueda);

                IList<ListaDatosValores> origenAsociados =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrigenClienteAsociado));
                ListaDatosValores primerOrigenAsociado = new ListaDatosValores();
                primerOrigenAsociado.Id = "NGN";
                origenAsociados.Insert(0, primerOrigenAsociado);
                this._ventana.OrigenesAsociados = origenAsociados;

                IList<ListaDatosValores> origenInteresados =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrigenClienteAsociado));
                ListaDatosValores primerOrigenInteresado = new ListaDatosValores();
                primerOrigenInteresado.Id = "NGN";
                origenInteresados.Insert(0, primerOrigenInteresado);
                this._ventana.OrigenesInteresados = origenInteresados;

                IList<ListaDatosValores> origenesPatente =
                            this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiOrigenClienteAsociado));
                ListaDatosValores primerOrigenPatente = new ListaDatosValores();
                primerOrigenPatente.Id = "NGN";
                origenesPatente.Insert(0, primerOrigenPatente);
                this._ventana.OrigenesPatente = origenesPatente;

                this._ventana.TotalHits = "0";
                this._ventana.FocoPredeterminado();

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
        /// Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        /// por pantalla
        /// </summary>
        public void Consultar()
        {
            int flagError = 0; //Se usa para saber que entidad da error y poder capturarlo en la exception, 1 = Asociado, 2 = Interesado
            Patente patenteError = new Patente(); //Se usa para navegar a la pagina de gestionar patente en caso de haber ocurrido un error
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                _filtroValido = 0;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                //dos filtros sean utilizados

                Patente patenteAuxiliar = new Patente();
                

                patenteAuxiliar = (Patente)this._ventana.PatenteParaFiltrar;

                patenteAuxiliar.Id = int.MinValue;


                               
               
                this._ventana.PatenteParaFiltrar = patenteAuxiliar;
                patenteAuxiliar = ObtenerPatenteFiltro();

                if (_filtroValido >= 2)
                {
                    this._patentes = this._patenteServicios.ObtenerPatentesFiltro(patenteAuxiliar);

                    IList<Patente> patentesDesinfladas = new List<Patente>();

                    foreach (var patente in this._patentes)
                    {
                        patenteError = patente;

                        patenteAuxiliar = new Patente(patente.Id);
                        Asociado asociadoAuxiliar = new Asociado();
                        Interesado interesadoAuxiliar = new Interesado();
                        Servicio servicioAuxiliar = new Servicio();

                        patenteAuxiliar.PrimeraReferencia = patente.PrimeraReferencia;
                        patenteAuxiliar.Descripcion = patente.Descripcion != null ? patente.Descripcion : "";

                        flagError = 1;
                        if ((null != patente.Asociado) && (!string.IsNullOrEmpty(patente.Asociado.Nombre)))
                        {
                            asociadoAuxiliar = patente.Asociado;
                            patenteAuxiliar.Asociado = asociadoAuxiliar;
                        }

                        flagError = 2;
                        if ((null != patente.Interesado) && (!string.IsNullOrEmpty(patente.Interesado.Nombre)))
                        {
                            interesadoAuxiliar = patente.Interesado;
                            patenteAuxiliar.Interesado = interesadoAuxiliar;
                        }

                        if ((patente.Servicio != null) && (!string.IsNullOrEmpty(patente.Servicio.Descripcion)))
                        {
                            servicioAuxiliar = patente.Servicio;
                            patenteAuxiliar.Servicio = servicioAuxiliar;
                        }

                        flagError = 0;

                        patenteAuxiliar.CodigoInscripcion = patente.CodigoInscripcion;

                        patenteAuxiliar.FechaPublicacion = patente.FechaPublicacion != null ? patente.FechaPublicacion : null;

                        patenteAuxiliar.CodigoPatenteInternacional = patente.CodigoPatenteInternacional;

                        patenteAuxiliar.CorrelativoExpediente = patente.CorrelativoExpediente;

                        patentesDesinfladas.Add(patenteAuxiliar);

                    }

                    this._ventana.Resultados = patentesDesinfladas;
                    this._ventana.TotalHits = patentesDesinfladas.Count.ToString();
                    if (patentesDesinfladas.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);


                Mouse.OverrideCursor = null;


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }

            catch (NHibernate.LazyInitializationException ex)
            {
                logger.Error(ex.Message);

                if (flagError == 1)
                {
                    this.Navegar(Recursos.MensajesConElUsuario.ErrorConsultandoAsociado + " " + patenteError.Asociado.Id, true);
                }
                else if (flagError == 2)
                {
                    this.Navegar(Recursos.MensajesConElUsuario.ErrorConsultandoInteresado + " " + patenteError.Interesado.Id, true);
                }
                else
                    this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, true);
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
        /// Método que invoca una nueva página "GestionarPatente" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrGestionarPatente()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.Patente != null)
            {
                ((Patente)this._ventana.Patente).Anualidades =
                    this._anualidadServicios.ConsultarAnualidadesPorPatente((Patente)this._ventana.Patente);
                this.Navegar(new GestionarPatente(this._ventana.Patente, this._ventana));
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que ordena una columna
        /// </summary>
        public void OrdenarColumna(GridViewColumnHeader column)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            String field = column.Tag as String;

            if (this._ventana.CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Remove(this._ventana.CurAdorner);
                this._ventana.ListaResultados.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            this._ventana.ListaResultados.Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que busca las patentes registradas
        /// </summary>
        public void BuscarPatente()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Patente patente = new Patente();
            IEnumerable<Patente> patentesFiltradas;
            patente.Descripcion = this._ventana.NombrePatente.ToUpper();
            patente.Id = this._ventana.Id.Equals("") ? 0 : int.Parse(this._ventana.Id);
            if ((!patente.Descripcion.Equals("")) || (patente.Id != 0))
                patentesFiltradas = this._patenteServicios.ObtenerPatentesFiltro(patente);
            else
                patentesFiltradas = new List<Patente>();

            if (patentesFiltradas.ToList<Patente>().Count != 0)
                this._ventana.Patentes = patentesFiltradas.ToList<Patente>();
            else
                this._ventana.Patentes = this._patentes;

            Mouse.OverrideCursor = null;
        }


        /// <summary>
        /// Método que devuelve la patente que se utilizara para realizar el filtrado
        /// </summary>
        /// <returns>Patente cargada con el filtro</returns>
        private Patente ObtenerPatenteFiltro()
        {
            //Patente patenteAuxiliar = new Patente();

            

            Patente patenteAuxiliar = ((Patente)this._ventana.PatenteParaFiltrar);
            

            patenteAuxiliar.PrimeraReferencia = ((Patente)this._ventana.PatenteParaFiltrar).PrimeraReferencia;
            //Patente patenteAuxiliar = new Patente();
            //patenteAuxiliar.PrimeraReferencia = null != ((Patente)this._ventana.PatenteParaFiltrar).PrimeraReferencia ? ((Patente)this._ventana.PatenteParaFiltrar).PrimeraReferencia : null;

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                patenteAuxiliar = TomarDatosPatenteFiltro(patenteAuxiliar);

                if (this._ventana.BoletinesEstaSeleccionado)
                {
                    patenteAuxiliar = TomarDatosPatenteFiltroBoletines(patenteAuxiliar);
                }

                if (this._ventana.PrioridadesEstaSeleccionado)
                {
                    patenteAuxiliar = TomarDatosPatenteFiltroPrioridades(patenteAuxiliar);
                }

                if (this._ventana.TYREstaSeleccionado)
                {
                    patenteAuxiliar = TomarDatosMarcaFiltroTYR(patenteAuxiliar);
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
            finally
            {
            }
            return patenteAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la patente con los datos del check Prioridades
        /// </summary>
        /// <param name="patenteAuxiliar">Patente a cargar</param>
        /// <returns>patente cargada con los datos de Prioridades</returns>
        private Patente TomarDatosPatenteFiltroPrioridades(Patente patenteAuxiliar)
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (!this._ventana.IdPrioridad.Equals(""))
            {
                _filtroValido = 2;
                patenteAuxiliar.CPrioridad = this._ventana.IdPrioridad;
            }

            if (!this._ventana.FechaPrioridad.Equals(""))
            {
                _filtroValido = 2;

                DateTime fechaPrioridad = DateTime.Parse(this._ventana.FechaPrioridad);
                patenteAuxiliar.FechaPrioridad = fechaPrioridad;
            }

            if ((null != this._ventana.PaisPrioridad) && (!((Pais)this._ventana.PaisPrioridad).Id.Equals("NGN")))
            {
                _filtroValido = 2;
                patenteAuxiliar.Pais = new Pais();
                patenteAuxiliar.Pais = ((Pais)this._ventana.PaisPrioridad);
            }
            else
                patenteAuxiliar.Pais = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            return patenteAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la patente con los datos del check Boletines
        /// </summary>
        /// <param name="patenteAuxiliar">Patente a cargar</param>
        /// <returns>patente cargada con los datos de Boletines</returns>
        private Patente TomarDatosPatenteFiltroBoletines(Patente patenteAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion



            if (((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue)
            {
                _filtroValido = 2;
                patenteAuxiliar.BoletinConcesion = new Boletin();
                patenteAuxiliar.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion);
            }
            else
                patenteAuxiliar.BoletinConcesion = null;

            if (((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue)
            {
                _filtroValido = 2;
                patenteAuxiliar.BoletinPublicacion = new Boletin();
                patenteAuxiliar.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion);
            }
            else
                patenteAuxiliar.BoletinPublicacion = null;

            if (((Boletin)this._ventana.BoletinOrdenPublicacion).Id != int.MinValue)
            {
                _filtroValido = 2;
                patenteAuxiliar.BoletinOrdenPublicacion = new Boletin();
                patenteAuxiliar.BoletinOrdenPublicacion = ((Boletin)this._ventana.BoletinOrdenPublicacion);
            }
            else
                patenteAuxiliar.BoletinOrdenPublicacion = null;


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return patenteAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la patente con los datos del filtro
        /// </summary>
        /// <param name="patenteAuxiliar">Patente a cargar</param>
        /// <returns>patente cargada con los datos del filtro</returns>
        private Patente TomarDatosPatenteFiltro(Patente patenteAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                patenteAuxiliar.LocalidadPatente = ((ListaDatosValores)this._ventana.TipoBusqueda).Valor;

                if (!this._ventana.Id.Equals(""))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.Id = int.Parse(this._ventana.Id);
                }

                if (this._ventana.OrigenPatente != null)
                {
                    _filtroValido = 2;
                    patenteAuxiliar.OrigenPatente = ((ListaDatosValores)this._ventana.OrigenPatente).Valor;
                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    //patenteAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    patenteAuxiliar.Asociado = new Asociado();
                    patenteAuxiliar.Asociado.Id = ((Asociado)this._ventana.Asociado).Id;
                    _filtroValido = 2;
                }
                else
                    patenteAuxiliar.Asociado = null;

                if ((null != this._ventana.OrigenAsociado) && (!((ListaDatosValores)this._ventana.OrigenAsociado).Id.Equals("NGN")))
                {
                    if (patenteAuxiliar.Asociado != null)
                    {
                        patenteAuxiliar.Asociado.OrigenCliente = ((ListaDatosValores)this._ventana.OrigenAsociado).Valor;
                        _filtroValido = 2;
                    }
                    else
                    {
                        patenteAuxiliar.Asociado = new Asociado();
                        patenteAuxiliar.Asociado.Id = int.MinValue;
                        patenteAuxiliar.Asociado.OrigenCliente = ((ListaDatosValores)this._ventana.OrigenAsociado).Valor;
                        _filtroValido = 2;
                    }
                }

                if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                {
                    //patenteAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                    patenteAuxiliar.Interesado = new Interesado();
                    patenteAuxiliar.Interesado.Id = ((Interesado)this._ventana.Interesado).Id; 
                    _filtroValido = 2;
                }
                else
                    patenteAuxiliar.Interesado = null;

                if ((null != this._ventana.OrigenInteresado) && (!((ListaDatosValores)this._ventana.OrigenInteresado).Id.Equals("NGN")))
                {
                    if (patenteAuxiliar.Interesado != null)
                    {
                        patenteAuxiliar.Interesado.OrigenCliente = ((ListaDatosValores)this._ventana.OrigenInteresado).Valor;
                        _filtroValido = 2;
                    }
                    else
                    {
                        patenteAuxiliar.Interesado = new Interesado();
                        patenteAuxiliar.Interesado.Id = int.MinValue;
                        patenteAuxiliar.Interesado.OrigenCliente = ((ListaDatosValores)this._ventana.OrigenInteresado).Valor;
                        _filtroValido = 2;
                    }
                }


                if (!this._ventana.NombrePatente.Equals(""))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.Descripcion = this._ventana.NombrePatente.ToUpper();
                }

                if (!this._ventana.Observacion.Equals(""))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.Observacion = this._ventana.Observacion;
                }


                if (!this._ventana.Fecha.Equals(""))
                {

                    DateTime fechaInscripcion = DateTime.Parse(this._ventana.Fecha);
                    _filtroValido = 2;
                    patenteAuxiliar.FechaInscripcion = fechaInscripcion;
                    
                    //DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    //_filtroValido = 2;
                    //patenteAuxiliar.FechaPublicacion = fechaPublicacion;

                    
                }

                if (!this._ventana.Solicitud.Equals(""))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.CodigoInscripcion = this._ventana.Solicitud;
                }

                if (!((TipoEstado)this._ventana.Detalle).Id.Equals("NGN"))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.TipoEstado = new TipoEstado();
                    patenteAuxiliar.TipoEstado = ((TipoEstado)this._ventana.Detalle);
                }
                else
                    patenteAuxiliar.TipoEstado = null;

                if (!((Servicio)this._ventana.Servicio).Id.Equals("NGN"))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.Servicio = new Servicio();
                    patenteAuxiliar.Servicio = ((Servicio)this._ventana.Servicio);
                }
                else
                    patenteAuxiliar.Servicio = null;


                if ((null != patenteAuxiliar.PrimeraReferencia) && (!patenteAuxiliar.PrimeraReferencia.Equals("")))
                {
                    _filtroValido = 2;
                }



                if (patenteAuxiliar.LocalidadPatente.Equals("I"))
                {
                    if (!this._ventana.IdInternacional.Equals(string.Empty))
                    {
                        _filtroValido = 2;
                        patenteAuxiliar.CodigoPatenteInternacional = int.Parse(this._ventana.IdInternacional);
                    }
                    else { patenteAuxiliar.CodigoPatenteInternacional = 0; }

                    if (!this._ventana.IdCorrelativoInternacional.Equals(string.Empty))
                    {
                        _filtroValido = 2;
                        patenteAuxiliar.CorrelativoExpediente = int.Parse(this._ventana.IdCorrelativoInternacional);
                    }
                    else { patenteAuxiliar.CorrelativoExpediente = 0; }

                    if ((this._ventana.PaisInt != null) && !((Pais)this._ventana.PaisInt).Id.Equals("NGN"))
                    {
                        _filtroValido = 2;
                        patenteAuxiliar.PaisInternacional = new Pais();
                        patenteAuxiliar.PaisInternacional = ((Pais)this._ventana.PaisInt);
                    }
                    else { patenteAuxiliar.PaisInternacional = null; }

                    if (!this._ventana.ReferenciaAsociado.Equals(string.Empty))
                    {
                        _filtroValido = 2;
                        patenteAuxiliar.ReferenciaAsociadoInternacional = this._ventana.ReferenciaAsociado;
                    }

                    if (!this._ventana.ReferenciaInteresado.Equals(string.Empty))
                    {
                        _filtroValido = 2;
                        patenteAuxiliar.ReferenciaInteresadoInternacional = this._ventana.ReferenciaInteresado;
                    }

                    else
                    {
                        _filtroValido = 2;
                    }
                }

                else
                {
                    _filtroValido = 2;
                }



                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Debug(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
            }

            return patenteAuxiliar;
        }


        #region TYR

        /// <summary>
        /// Método que devuelve la patente con los datos del check TYR
        /// </summary>
        /// <param name="marcaAuxiliar">Patente a cargar</param>
        /// <returns>Patente cargada con los datos de TYR</returns>
        private Patente TomarDatosMarcaFiltroTYR(Patente patenteAuxiliar)
        {
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if (!this._ventana.CodigoRegistro.Equals(""))
                if(!this._ventana.NumeroCodigoRegistro.Equals(""))
                {
                    //patenteAuxiliar.CodigoRegistro = this._ventana.CodigoRegistro.ToUpper();
                    patenteAuxiliar.CodigoRegistro = this._ventana.NumeroCodigoRegistro.ToUpper();
                    _filtroValido = 2;
                }
                //else
                //    this._ventana.CodigoRegistro = string.Empty;


                if (!this._ventana.FechaRegistro.Equals(""))
                {
                    DateTime fechaRegistro = DateTime.Parse(this._ventana.FechaRegistro);
                    _filtroValido = 2;
                    patenteAuxiliar.FechaRegistro = fechaRegistro;
                }

                if (!this._ventana.ExpCambioPendiente.Equals(""))
                {
                    _filtroValido = 2;
                    patenteAuxiliar.ExpCambioPendiente = this._ventana.ExpCambioPendiente;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Debug(ex.Message);
            }

            return patenteAuxiliar;
        }

        #endregion


        #region Interesado


        /// <summary>
        /// Método que se encarga de buscar el interesado definido en el filtro
        /// </summary>
        public void BuscarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado interesadoABuscar = new Interesado();

            //interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
            //                       int.Parse(this._ventana.IdInteresadoFiltrar) : 0;

            interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                                   int.Parse(this._ventana.IdInteresadoFiltrar) : int.MinValue;

            interesadoABuscar.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                                       this._ventana.NombreInteresadoFiltrar.ToUpper() : "";

            //if ((interesadoABuscar.Id != 0) || !(interesadoABuscar.Nombre.Equals("")))
            if ((interesadoABuscar.Id != int.MinValue) || !(interesadoABuscar.Nombre.Equals("")))
            {
                IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                interesados.Insert(0, new Interesado(int.MinValue));
                this._ventana.Interesados = interesados;
            }
            else
            {
                this._ventana.Interesados = this._interesados;
                //this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Mensaje("Ingrese criterios validos para la busqueda del Interesado", 1);
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Metodo que cambia el texto del interesado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el interesado haya sido valido, false en caso contrario</returns>
        public bool CambiarInteresado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Interesado != null)
            {
                this._ventana.InteresadoFiltro = ((Interesado)this._ventana.Interesado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;

        }


        #endregion


        #region Asociado


        /// <summary>
        /// Método que se encarga de buscar el asociado definido en el filtro
        /// </summary>
        public void BuscarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Mouse.OverrideCursor = Cursors.Wait;

            Asociado asociadoABuscar = new Asociado();

            //asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
            //                     int.Parse(this._ventana.IdAsociadoFiltrar) : 0;

            asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdAsociadoFiltrar) : int.MinValue;

            asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                     this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

            //if ((asociadoABuscar.Id != 0) || !(asociadoABuscar.Nombre.Equals("")))
            if ((asociadoABuscar.Id != int.MinValue) || !(asociadoABuscar.Nombre.Equals("")))
            {
                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);
                asociados.Insert(0, new Asociado(int.MinValue));
                this._ventana.Asociados = asociados;

            }
            else
            {
                this._ventana.Mensaje("Ingrese criterios validos para la busqueda del Asociado", 1);
                //this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                this._ventana.Asociados = this._asociados;
            }

            Mouse.OverrideCursor = null;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Metodo que cambia el texto del Asociado en la interfaz
        /// </summary>
        /// <returns>true en caso de que el Asociado haya sido valido, false en caso contrario</returns>
        public bool CambiarAsociado()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            if (this._ventana.Asociado != null)
            {
                this._ventana.AsociadoFiltro = ((Asociado)this._ventana.Asociado).Nombre;
                retorno = true;
            }

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        #endregion

    }
}
