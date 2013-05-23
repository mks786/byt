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
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.MarcasTercero;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Presentadores.MarcasTercero
{
    class PresentadorConsultaMarcasTercero : PresentadorBase
    {

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConsultaMarcasTercero _ventana;


        private IMarcaTerceroServicios _marcaServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private ICorresponsalServicios _corresponsalServicios;
        private IServicioServicios _servicioServicios;
        private ITipoEstadoServicios _tipoEstadoServicios;
        private IEstadoMarcaServicios _estadoMarcaServicios;
        private IPaisServicios _paisServicios;
        private ICondicionServicios _condicionServicios;
        private IBoletinServicios _boletinServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IMarcaBaseTerceroServicios _marcaBaseTerceroServicios;


        private IList<MarcaTercero> _marcas;
        private IList<Asociado> _asociados;
        private IList<Interesado> _interesados;
        private IList<Corresponsal> _corresponsales;


        private int _filtroValido;//Variable utilizada para limitar a que el filtro se ejecute solo cuando 
        //dos filtros sean utilizados


        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="ventana">página que satisface el contrato</param>
        public PresentadorConsultaMarcasTercero(IConsultaMarcasTercero ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                MarcaTercero marca = new MarcaTercero();
                marca.Internacional = new Internacional();
                marca.Nacional = new Nacional();

                this._ventana.MarcaTerceroParaFiltrar = marca;

                this._marcaServicios = (IMarcaTerceroServicios)Activator.GetObject(typeof(IMarcaTerceroServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaTerceroServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);
                this._corresponsalServicios = (ICorresponsalServicios)Activator.GetObject(typeof(ICorresponsalServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CorresponsalServicios"]);
                this._servicioServicios = (IServicioServicios)Activator.GetObject(typeof(IServicioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ServicioServicios"]);
                this._tipoEstadoServicios = (ITipoEstadoServicios)Activator.GetObject(typeof(ITipoEstadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEstadoServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._condicionServicios = (ICondicionServicios)Activator.GetObject(typeof(ICondicionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CondicionServicios"]);
                this._boletinServicios = (IBoletinServicios)Activator.GetObject(typeof(IBoletinServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["BoletinServicios"]);
                this._estadoMarcaServicios = (IEstadoMarcaServicios)Activator.GetObject(typeof(IEstadoMarcaServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EstadoMarcaServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._marcaBaseTerceroServicios = (IMarcaBaseTerceroServicios)Activator.GetObject(typeof(IMarcaBaseTerceroServicios),
                        ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaBaseTerceroServicios"]);

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
        /// Método que se encarga de actualizar el títutlo de la ventana
        /// </summary>
        public void ActualizarTitulo()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarMarcasTercero,
                Recursos.Ids.ConsultarMarcasTercero);

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

                this._ventana.LimpiarCampos();

                IList<Asociado> asociados = new List<Asociado>();
                Asociado primerAsociado = new Asociado();
                primerAsociado.Id = int.MinValue;
                asociados.Insert(0, primerAsociado);
                this._ventana.Asociados = asociados;
                this._asociados = asociados;

                IList<Interesado> interesados = new List<Interesado>();
                Interesado primerInteresado = new Interesado();
                primerInteresado.Id = int.MinValue;
                interesados.Insert(0, primerInteresado);
                this._ventana.Interesados = interesados;
                this._interesados = interesados;

                IList<EstadoMarca> tipoEstados = this._estadoMarcaServicios.ConsultarTodos();
                EstadoMarca primerDetalle = new EstadoMarca();
                primerDetalle.Id = "NGN";
                tipoEstados.Insert(0, primerDetalle);
                this._ventana.Detalles = tipoEstados;


                IList<ListaDatosValores> listaDatosValores = this._listaDatosValoresServicios.
                    ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiCategoriaTipoDeCaso));
                ListaDatosValores primerDatoValor = new ListaDatosValores();
                primerDatoValor.Id = "NGN";
                listaDatosValores.Insert(0, primerDatoValor);
                this._ventana.TiposDeCasos = listaDatosValores;

                //IList<ListaDatosValores> tipodecasos = this._listaDatosValoresServicios.ConsultarTodos();
                //ListaDatosValores primerTipoDeCaso = new ListaDatosValores();
                //primerTipoDeCaso.Id = "NGN";
                //tipodecasos.Insert(0, primerTipoDeCaso);
                //this._ventana.TiposDeCasos = tipodecasos;

                IList<Servicio> servicios = this._servicioServicios.ConsultarTodos();
                Servicio primerServicio = new Servicio();
                primerServicio.Id = "NGN";
                servicios.Insert(0, primerServicio);
                this._ventana.Servicios = servicios;
                //this._ventana.Servicio = this.BuscarServicio(servicios, marca.Servicio);

                IList<Boletin> boletines = this._boletinServicios.ConsultarTodos();
                Boletin primerBoletin = new Boletin();
                primerBoletin.Id = int.MinValue;
                boletines.Insert(0, primerBoletin);
                this._ventana.BoletinesPublicacion = boletines;
                this._ventana.BoletinesConcesion = boletines;

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
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                bool NullFK;


                _filtroValido = 0;

                MarcaTercero MarcaTerceroAuxiliar = ObtenerMarcaTerceroFiltro();

                if (_filtroValido >= 2)
                {
                    this._marcas = this._marcaServicios.ObtenerMarcaTerceroFiltro(MarcaTerceroAuxiliar);

                    IList<MarcaTercero> marcasDesinfladas = new List<MarcaTercero>();

                    foreach (var marca in this._marcas)
                    {
                        NullFK = false;
                        MarcaTerceroAuxiliar = new MarcaTercero(marca.Id.ToUpper());
                        MarcaTerceroAuxiliar.Anexo = marca.Anexo;
                        Asociado asociadoAuxiliar = new Asociado();
                        Interesado interesadoAuxiliar = new Interesado();

                        MarcaTerceroAuxiliar.Descripcion = marca.Descripcion != null ? marca.Descripcion : "";

                        try
                        {


                            if ((marca.Asociado != null) && (!string.IsNullOrEmpty(marca.Asociado.Nombre)))
                            {

                                asociadoAuxiliar.Nombre = marca.Asociado.Nombre;
                                MarcaTerceroAuxiliar.Asociado = asociadoAuxiliar;

                            }
                        }
                        catch (NHibernate.LazyInitializationException ex)
                        {

                            marca.Asociado = null;
                            NullFK = true;
                        }

                        try
                        {



                            if ((marca.Interesado != null) && (!string.IsNullOrEmpty(marca.Interesado.Nombre)))
                            {

                                interesadoAuxiliar.Nombre = marca.Interesado.Nombre;
                                MarcaTerceroAuxiliar.Interesado = interesadoAuxiliar;



                            }
                        }
                        catch (NHibernate.LazyInitializationException ex)
                        {
                            marca.Interesado = null;
                            NullFK = true;
                        }

                        MarcaTerceroAuxiliar.Nacional = marca.Nacional;
                        MarcaTerceroAuxiliar.Internacional = marca.Internacional;
                        MarcaTerceroAuxiliar.CodigoInscripcion = marca.CodigoInscripcion;

                        MarcaTerceroAuxiliar.FechaPublicacion = marca.FechaPublicacion != null ? marca.FechaPublicacion : null;
                        if (!NullFK)
                            marcasDesinfladas.Add(MarcaTerceroAuxiliar);

                    }
                    // marcasDesinfladas = _marcas;
                    this._ventana.Resultados = marcasDesinfladas;
                    // this._ventana.Resultados = _marcas;
                    this._ventana.TotalHits = marcasDesinfladas.Count.ToString();
                    if (marcasDesinfladas.Count == 0)
                        this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
                }
                else
                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto, 0);

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
        /// Método que devuelve la marca que se utilizara para realizar el filtrado
        /// </summary>
        /// <returns>MarcaTercero cargada con el filtro</returns>
        private MarcaTercero ObtenerMarcaTerceroFiltro()
        {
            //  MarcaTercero marcaAuxiliar = ((MarcaTercero)this._ventana.MarcaTerceroParaFiltrar);
            MarcaTercero marcaAuxiliar = new MarcaTercero();
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (ValidarBusquedaDeMarcaTerceros())
                {

                    if (this._ventana.NacionalEstaSeleccionado)
                    {
                        marcaAuxiliar = TomarDatosMarcaTerceroFiltroNacional(marcaAuxiliar);
                    }

                    if (this._ventana.BoletinesEstaSeleccionado)
                    {
                        marcaAuxiliar = TomarDatosMarcaTerceroFiltroBoletines(marcaAuxiliar);
                    }

                }
                else
                {
                    //Error ya que debe seleccionar al menos un check
                    //this._ventana.Mensaje()
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
                Mouse.OverrideCursor = null;
            }
            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check TYR
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de TYR</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroTYR(MarcaTercero marcaAuxiliar)
        {
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //if ((null != this._ventana.Condicion) && (((Condicion)this._ventana.Condicion).Id != int.MinValue))
                //{
                //    //marcaAuxiliar.Condicion = (Condicion)this._ventana.Condicion;
                //    //_filtroValido = 2;
                //}

                //if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                //{
                //    marcaAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                //    _filtroValido = 2;
                //}

                //if ((null != this._ventana.Corresponsal) && (((Corresponsal)this._ventana.Corresponsal).Id != int.MinValue))
                //{
                //    marcaAuxiliar.Corresponsal = (Corresponsal)this._ventana.Corresponsal;
                //    _filtroValido = 2;
                //}

                //if (!this._ventana.DescripcionFiltrar.Equals(""))
                //{
                //    _filtroValido = 2;
                //    marcaAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                //}

                //if (!this._ventana.Fecha.Equals(""))
                //{
                //    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                //    _filtroValido = 2;
                //    marcaAuxiliar.FechaPublicacion = fechaPublicacion;
                //}

                //if (!((TipoEstado)this._ventana.Detalle).Id.Equals("NGN"))
                //{
                //    _filtroValido = 2;
                //    marcaAuxiliar.TipoEstado = ((TipoEstado)this._ventana.Detalle);
                //}

                //if (!((Servicio)this._ventana.Detalle).Id.Equals("NGN"))
                //{
                //    _filtroValido = 2;
                //    marcaAuxiliar.Servicio = ((Servicio)this._ventana.Detalle);
                //}

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Debug(ex.Message);
            }

            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check Prioridades
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de Prioridades</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroPrioridades(MarcaTercero marcaAuxiliar)
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check Indicadores
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de Indicadores</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroIndicadores(MarcaTercero marcaAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check Boletines
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de Boletines</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroBoletines(MarcaTercero marcaAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            _filtroValido = 2;

            if (((Boletin)this._ventana.BoletinConcesion).Id != int.MinValue)
            {
                marcaAuxiliar.BoletinConcesion = ((Boletin)this._ventana.BoletinConcesion);
            }

            if (((Boletin)this._ventana.BoletinPublicacion).Id != int.MinValue)
            {
                marcaAuxiliar.BoletinPublicacion = ((Boletin)this._ventana.BoletinPublicacion);
            }


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check Nacional
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de Nacional</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroNacional(MarcaTercero marcaAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                if (!this._ventana.Id.Equals(""))
                {
                    _filtroValido = 2;
                    marcaAuxiliar.Id = this._ventana.Id.ToUpper();

                }

                if (this._ventana.Solicitud != "")
                {
                    _filtroValido = 2;
                    marcaAuxiliar.CodigoInscripcion = this._ventana.Solicitud.ToUpper();

                }

                if ((null != this._ventana.Asociado) && (((Asociado)this._ventana.Asociado).Id != int.MinValue))
                {
                    marcaAuxiliar.Asociado = (Asociado)this._ventana.Asociado;
                    _filtroValido = 2;
                }

                if ((null != this._ventana.Interesado) && (((Interesado)this._ventana.Interesado).Id != int.MinValue))
                {
                    marcaAuxiliar.Interesado = (Interesado)this._ventana.Interesado;
                    _filtroValido = 2;
                }

                if (!this._ventana.DescripcionFiltrar.Equals(""))
                {
                    _filtroValido = 2;
                    marcaAuxiliar.Descripcion = this._ventana.DescripcionFiltrar.ToUpper();
                }

                if (!this._ventana.Fecha.Equals(""))
                {
                    DateTime fechaPublicacion = DateTime.Parse(this._ventana.Fecha);
                    _filtroValido = 2;
                    marcaAuxiliar.FechaPublicacion = fechaPublicacion;
                }

                if (this._ventana.ClaseInternacional != "")
                {
                    _filtroValido = 2;
                    Internacional inter = new Internacional();
                    inter.Id = int.Parse(this._ventana.ClaseInternacional);
                    marcaAuxiliar.Internacional = inter;

                }

                if (this._ventana.ClaseNacional != "")
                {
                    _filtroValido = 2;
                    Nacional nac = new Nacional();
                    nac.Id = int.Parse(this._ventana.ClaseNacional);
                    marcaAuxiliar.Nacional = nac;

                }

                if (!this._ventana.Distingue.Equals(""))
                {
                    _filtroValido = 2;
                    marcaAuxiliar.Id = this._ventana.Distingue.ToUpper();

                }

                if (!((EstadoMarca)this._ventana.Detalle).Id.Equals("NGN"))
                {
                    _filtroValido = 2;
                    marcaAuxiliar.EstadoMarca = ((EstadoMarca)this._ventana.Detalle).Id;
                }

                if ((null != this._ventana.TipoDeCaso) && ((ListaDatosValores)this._ventana.TipoDeCaso).Id != "NGN")
                {
                    _filtroValido = 2;
                    marcaAuxiliar.CasoT = ((ListaDatosValores)this._ventana.TipoDeCaso).Valor;
                }

                if (!((Servicio)this._ventana.Servicio).Id.Equals("NGN"))
                {
                    _filtroValido = 2;
                    marcaAuxiliar.Servicio = ((Servicio)this._ventana.Servicio);
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

            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que devuelve la marca con los datos del check Internacional
        /// </summary>
        /// <param name="marcaAuxiliar">MarcaTercero a cargar</param>
        /// <returns>marca cargada con los datos de Internacional</returns>
        private MarcaTercero TomarDatosMarcaTerceroFiltroInternacional(MarcaTercero marcaAuxiliar)
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion


            #region trace

            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return marcaAuxiliar;
        }


        /// <summary>
        /// Método que valida que la pantalla de busqueda se encuentre seleccionado al menos un check
        /// </summary>
        /// <returns>true en caso de que haya al menos uno seleccionado, false en caso contrario</returns>
        private bool ValidarBusquedaDeMarcaTerceros()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = true;

            if (!this._ventana.NacionalEstaSeleccionado)

                if (!this._ventana.BoletinesEstaSeleccionado)


                    retorno = false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }


        /// <summary>
        /// Método que invoca una nueva página "ConsultarPoder" y la instancia con el objeto seleccionado
        /// </summary>
        public void IrConsultarMarcaTercero()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            if (this._ventana.MarcaTerceroSeleccionada != null)
            {
                MarcaTercero marcaTerceroParaNavegar = null;
                bool encontrada = false;
                int cont = 0;
                while (!encontrada)
                {
                    MarcaTercero marcaTercero = this._marcas[cont];
                    if ((marcaTercero.Id == ((MarcaTercero)this._ventana.MarcaTerceroSeleccionada).Id) &&
                        (marcaTercero.Anexo == ((MarcaTercero)this._ventana.MarcaTerceroSeleccionada).Anexo))
                    {
                        marcaTerceroParaNavegar = marcaTercero;
                        encontrada = true;
                    }
                    cont++;
                }
                MarcaBaseTercero MarcaABuscar = new MarcaBaseTercero();
                MarcaABuscar.MarcaTercero = new MarcaTercero(marcaTerceroParaNavegar.Id);
                MarcaABuscar.MarcaTercero.Anexo = marcaTerceroParaNavegar.Anexo;
                marcaTerceroParaNavegar.MarcasBaseTercero = _marcaBaseTerceroServicios.ConsultarMarcasBasePorId(MarcaABuscar);

                //Constructor sin la ventana padre
                //this.Navegar(new GestionarMarcaTercero(marcaTerceroParaNavegar));

                //Constructor que carga la ventana padre para regresarme a ella
                this.Navegar(new GestionarMarcaTercero(marcaTerceroParaNavegar, this._ventana));
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


        //public void BuscarAsociado()
        //{
        //    IEnumerable<Asociado> asociadosFiltrados = (IList<Asociado>)this._asociados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdAsociadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreAsociadoFiltrar))
        //    {
        //        asociadosFiltrados = from p in asociadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreAsociadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (asociadosFiltrados.ToList<Asociado>().Count != 0)
        //        this._ventana.Asociados = asociadosFiltrados.ToList<Asociado>();
        //    else
        //        this._ventana.Asociados = this._asociados;
        //}

        //public void BuscarInteresado()
        //{
        //    IEnumerable<Interesado> interesadosFiltrados = (IList<Interesado>)this._interesados;

        //    if (!string.IsNullOrEmpty(this._ventana.IdInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Id == int.Parse(this._ventana.IdInteresadoFiltrar)
        //                             select p;
        //    }

        //    if (!string.IsNullOrEmpty(this._ventana.NombreInteresadoFiltrar))
        //    {
        //        interesadosFiltrados = from p in interesadosFiltrados
        //                             where p.Nombre != null &&
        //                             p.Nombre.ToLower().Contains(this._ventana.NombreInteresadoFiltrar.ToLower())
        //                             select p;
        //    }

        //    if (interesadosFiltrados.ToList<Interesado>().Count != 0)
        //        this._ventana.Interesados = interesadosFiltrados.ToList<Interesado>();
        //    else
        //        this._ventana.Interesados = this._interesados;
        //}


        /// <summary>
        /// Método que limpia los campos de búsqueda
        /// </summary>
        public void LimpiarCampos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this._ventana.TotalHits = "0";
            this._ventana.Resultados = null;

            this._ventana.GestionarVisibilidadFiltroNacional(true);

            this._ventana.GestionarVisibilidadLimpiarFiltros();


            #region Nacional

            this._ventana.NacionalEstaSeleccionado = true;
            this._ventana.Id = null;
            this._ventana.DescripcionFiltrar = null;
            this._ventana.Fecha = null;

            this._ventana.AsociadoFiltro = null;
            this._ventana.IdAsociadoFiltrar = null;
            this._ventana.NombreAsociadoFiltrar = null;
            this._ventana.Asociados = null;

            this._ventana.IdInteresadoFiltrar = null;
            this._ventana.NombreInteresadoFiltrar = null;
            this._ventana.InteresadoFiltro = null;
            this._ventana.Interesados = null;
            this._ventana.ClaseInternacional = "";
            this._ventana.ClaseNacional = "";
            this._ventana.Distingue = "";
            this._ventana.Solicitud = "";



            this._ventana.Servicio = this.BuscarServicio((IList<Servicio>)this._ventana.Servicios, new Servicio("NGN"));
            //this._ventana.Detalle = this.BuscarDetalle((IList<TipoEstado>)this._ventana.Detalles, new TipoEstado("NGN"));
            this._ventana.Detalle = ((IList<EstadoMarca>)this._ventana.Detalles)[0];

            #endregion

            #region Boletines

            this._ventana.BoletinesEstaSeleccionado = false;
            this._ventana.BoletinPublicacion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesPublicacion, new Boletin(int.MinValue));
            this._ventana.BoletinConcesion = this.BuscarBoletin((IList<Boletin>)this._ventana.BoletinesConcesion, new Boletin(int.MinValue));

            #endregion

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


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

            interesadoABuscar.Id = !this._ventana.IdInteresadoFiltrar.Equals("") ?
                                   int.Parse(this._ventana.IdInteresadoFiltrar) : 0;

            interesadoABuscar.Nombre = !this._ventana.NombreInteresadoFiltrar.Equals("") ?
                                       this._ventana.NombreInteresadoFiltrar.ToUpper() : "";

            if ((interesadoABuscar.Id != 0) || !(interesadoABuscar.Nombre.Equals("")))
            {
                IList<Interesado> interesados = this._interesadoServicios.ObtenerInteresadosFiltro(interesadoABuscar);
                interesados.Insert(0, new Interesado(int.MinValue));
                this._ventana.Interesados = interesados;
            }
            else
            {
                this._ventana.Interesados = this._interesados;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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

            asociadoABuscar.Id = !this._ventana.IdAsociadoFiltrar.Equals("") ?
                                 int.Parse(this._ventana.IdAsociadoFiltrar) : 0;

            asociadoABuscar.Nombre = !this._ventana.NombreAsociadoFiltrar.Equals("") ?
                                     this._ventana.NombreAsociadoFiltrar.ToUpper() : "";

            if ((asociadoABuscar.Id != 0) || !(asociadoABuscar.Nombre.Equals("")))
            {
                IList<Asociado> asociados = this._asociadoServicios.ObtenerAsociadosFiltro(asociadoABuscar);
                asociados.Insert(0, new Asociado(int.MinValue));
                this._ventana.Asociados = asociados;

            }
            else
            {
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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


        #region MarcaTercero

        public void BuscarMarcaTercero()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Mouse.OverrideCursor = Cursors.Wait;
                MarcaTercero marca = new MarcaTercero();
                IEnumerable<MarcaTercero> marcasFiltradas;
                //marca.Descripcion = this._ventana.NombreMarcaTerceroFiltrar.ToUpper();
                //marca.Id = this._ventana.IdMarcaTerceroFiltrar;
                if ((!marca.Descripcion.Equals("")) || (marca.Id != ""))
                    marcasFiltradas = this._marcaServicios.ObtenerMarcaTerceroFiltro(marca);
                else
                    marcasFiltradas = new List<MarcaTercero>();

                //if (marcasFiltradas.ToList<MarcaTercero>().Count != 0)
                //    this._ventana.MarcasTercero = marcasFiltradas.ToList<MarcaTercero>();
                //else
                //    this._ventana.MarcasTercero = this._marcas;

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

        //public bool ElegirMarcaTercero()
        //{
        //    bool retorno = false;
        //    if (this._ventana.MarcaTercero != null)
        //    {
        //        retorno = true;
        //       // this._ventana.NombreMarcaTercero = ((MarcaTercero)this._ventana.MarcaTercero).Descripcion;
        //    }

        //    return retorno;
        //}

        #endregion
    }
}
