using System;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.CadenaDeCambio;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.Cliente.Ventanas.Cartas;

namespace Trascend.Bolet.Cliente.Presentadores.CadenaDeCambio
{
    class PresentadorGestionarCadenaDeCambios : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ICadenaDeCambiosServicios _cadenaDeCambiosServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IMarcaServicios _marcaServicios;
        private IPatenteServicios _patenteServicios;
        private ICartaServicios _cartaServicios;
        private IOperacionServicios _operacionServicios;
        private IGestionarCadenaDeCambios _ventana;
        private bool _agregar = false;

        /// <summary>
        /// Constructor predeterminado que recibe una cadena de cambios y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="cadenaCambios">Cadena de Cambios a mostrar en la ventana</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public PresentadorGestionarCadenaDeCambios(IGestionarCadenaDeCambios ventana, object cadenaCambios, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                if (cadenaCambios != null)
                    this._ventana.CadenaDeCambios = (CadenaDeCambios)cadenaCambios;
                else
                {
                    this._agregar = true;
                    CadenaDeCambios cadenaDeCambios = new CadenaDeCambios();
                    this._ventana.CadenaDeCambios = cadenaDeCambios;
                    this._ventana.HabilitarCampos = true;
                }

                this._cadenaDeCambiosServicios = (ICadenaDeCambiosServicios)Activator.GetObject(typeof(ICadenaDeCambiosServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CadenaDeCambiosServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._marcaServicios = (IMarcaServicios)Activator.GetObject(typeof(IMarcaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MarcaServicios"]);
                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._operacionServicios = (IOperacionServicios)Activator.GetObject(typeof(IOperacionServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["OperacionServicios"]);

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

        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarCadenaDeCambios,
                       Recursos.Ids.CadenaDeCambios);
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

                IList<ListaDatosValores> tiposCadenaCambios =
                    this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(new ListaDatosValores(Recursos.Etiquetas.cbiTipoCadenaCambios));
                this._ventana.TiposCadenaCambios = tiposCadenaCambios;

                if (this._agregar)
                {
                    this._ventana.IdCadenaDeCambios = String.Empty;
                    this._ventana.CodigoOperacionCadenaCambios = String.Empty;
                }
                else
                {
                    ListaDatosValores tipoCadenaCambios = new ListaDatosValores();
                    tipoCadenaCambios.Descripcion = ((CadenaDeCambios)this._ventana.CadenaDeCambios).TipoCambioDescripcion;
                    tipoCadenaCambios.Valor = ((CadenaDeCambios)this._ventana.CadenaDeCambios).TipoCambio;
                    this._ventana.TipoCadenaCambios = this.BuscarListaDeDatosValores(tiposCadenaCambios, tipoCadenaCambios);
                    this._ventana.MostarBotonOperaciones();
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
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Metodo que inserta una nueva Cadena de Cambios o actualiza una ya existente
        /// </summary>
        public void Aceptar()
        {
            int idContador = 1;
            CadenaDeCambios cadenaCambiosFiltro = new CadenaDeCambios();
            bool exitoso;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CadenaDeCambios cadenaCambiosPantalla = ObtenerCadenaDeCambiosPantalla();
                
                if(this._ventana.TipoCadenaCambios != null)
                {
                    if (this._agregar)
                    {
                        cadenaCambiosFiltro.CodigoOperacion = cadenaCambiosPantalla.CodigoOperacion;
                        cadenaCambiosFiltro.TipoCambio = ((ListaDatosValores)this._ventana.TipoCadenaCambios).Valor;
                        
                        IList<CadenaDeCambios> cadenasCambios = this._cadenaDeCambiosServicios.ObtenerCadenasCambioFiltro(cadenaCambiosFiltro);
                        if (cadenasCambios.Count > 0)
                        {
                            idContador = cadenasCambios.Count;
                            idContador++; 
                            cadenaCambiosPantalla.Id = idContador;
                            cadenaCambiosPantalla.TipoCambio = cadenaCambiosFiltro.TipoCambio;
                        }
                        else
                        {
                            cadenaCambiosPantalla.Id = idContador;
                            cadenaCambiosPantalla.TipoCambio = cadenaCambiosFiltro.TipoCambio;
                        }
                    }
                    
                    exitoso = this._cadenaDeCambiosServicios.InsertarOModificar(cadenaCambiosPantalla, UsuarioLogeado.Hash);

                    if (exitoso)
                    {
                        this._ventana.Mensaje("Cadena de Cambios guardada con éxito", 2);
                        if (this._agregar)
                            this.RegresarVentanaPadre();
                    }

                }
                else
                    this._ventana.Mensaje("Debe elegir el Tipo de Cadena de Cambio",0);

                
                

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que obtiene la cadena de camhios de se encuentra en la pantalla
        /// </summary>
        /// <returns></returns>
        private CadenaDeCambios ObtenerCadenaDeCambiosPantalla()
        {

            CadenaDeCambios cadenaCambios = new CadenaDeCambios();

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                cadenaCambios = (CadenaDeCambios)this._ventana.CadenaDeCambios;

                if (!this._ventana.IdCarta.Equals(String.Empty))
                {
                    Carta carta = new Carta();
                    carta.Id = int.Parse(this._ventana.IdCarta);
                    IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(carta);
                    if (cartas.Count > 0)
                    {
                        cadenaCambios.Carta = cartas[0];
                    }
                    else
                        cadenaCambios.Carta = null;
                }
                else
                    cadenaCambios.Carta = null;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }

            return cadenaCambios;
        }


        /// <summary>
        /// Metodo que permite consultar un codigo de operacion, ya sea de marca o de patente
        /// </summary>
        public void IrConsultarCodigoOperacion()
        {
            String tipoSeleccionado = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._ventana.TipoCadenaCambios != null)
                {
                    tipoSeleccionado = ((ListaDatosValores)this._ventana.TipoCadenaCambios).Valor;
                    if ((this._ventana.CodigoOperacionCadenaCambios != null) && (!this._ventana.CodigoOperacionCadenaCambios.Equals(String.Empty)))
                    {
                        if (tipoSeleccionado.Equals("M"))
                        {
                            Marca marcaAux = new Marca();
                            marcaAux.Id = int.Parse(this._ventana.CodigoOperacionCadenaCambios);
                            IList<Marca> marcas = this._marcaServicios.ObtenerMarcasFiltro(marcaAux);
                            if (marcas.Count > 0)
                            {
                                this.Navegar(new ConsultarMarca(marcas[0], this._ventana));
                            }
                            else
                                this._ventana.Mensaje("El código de la Marca no existe", 0);
                        }
                        else if (tipoSeleccionado.Equals("P"))
                        {
                            Patente patenteAux = new Patente();
                            patenteAux.Id = int.Parse(this._ventana.CodigoOperacionCadenaCambios);
                            IList<Patente> patentes = this._patenteServicios.ObtenerPatentesFiltro(patenteAux);
                            if (patentes.Count > 0)
                            {
                                this.Navegar(new GestionarPatente(patentes[0], this._ventana));
                            }
                            else
                                this._ventana.Mensaje("El código de la Patente no existe", 0);
                        }
                    }
                    else
                        this._ventana.Mensaje("El campo del Código de Operación (Marca/Patente) se encuentra vacio", 0);
                }
                else
                    this._ventana.Mensaje("Para poder consultar el Código de Operación necesita definir el tipo de cadena de cambios", 0);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo que muestra el detalle de una Carta de una Cadena de Cambios
        /// </summary>
        public void ConsultarCarta()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.IdCarta.Equals(String.Empty))
                {
                    Carta cartaAux = new Carta();
                    cartaAux.Id = int.Parse(this._ventana.IdCarta);
                    IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaAux);
                    if (cartas.Count > 0)
                    {
                        Carta cartaCadenaDeCambio = cartas[0];
                        this.Navegar(new ConsultarCarta(cartaCadenaDeCambio, this._ventana));
                    }
                    else
                        this._ventana.Mensaje("La Carta para esta Cadena de Cambios no existe", 0);
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }


        /// <summary>
        /// Metodo que muestra la lista de Operaciones que implica esa cadena de cambios
        /// </summary>
        public void IrVerOperaciones()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                CadenaDeCambios cadenaCambios = ObtenerCadenaDeCambiosPantalla();

                Operacion operacionAux = new Operacion();
                operacionAux.CadenaDeCambios = cadenaCambios.Id;
                operacionAux.CodigoAplicada = cadenaCambios.CodigoOperacion;
                operacionAux.Servicio = new Servicio("");

                if (cadenaCambios.TipoCambio.Equals("M"))
                {
                    operacionAux.Aplicada = 'M';
                    operacionAux.Marca = new Marca(cadenaCambios.CodigoOperacion);
                }
                else if (cadenaCambios.TipoCambio.Equals("P"))
                {
                    operacionAux.Aplicada = 'P';
                    operacionAux.Patente = new Patente(cadenaCambios.CodigoOperacion);
                }

                IList<Operacion> operaciones = this._operacionServicios.ObtenerOperacionFiltro(operacionAux);
                if (operaciones.Count > 0)
                {
                    if (cadenaCambios.TipoCambio.Equals("M"))
                        this.Navegar(new Trascend.Bolet.Cliente.Ventanas.Marcas.ListaOperaciones(operaciones, this._ventana, true));
                    else if (cadenaCambios.TipoCambio.Equals("P"))
                        this.Navegar(new Trascend.Bolet.Cliente.Ventanas.Patentes.ListaOperaciones(operaciones,this._ventana,true));
                }
                else
                    this._ventana.Mensaje("No existen operaciones para esta Cadena de Cambios", 0);


                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, true);
            }
        }
    }
}
