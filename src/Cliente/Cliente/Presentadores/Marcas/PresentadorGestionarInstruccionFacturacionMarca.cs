using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Corresponsales;
using Trascend.Bolet.Cliente.Ventanas.Marcas;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Renovaciones;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using Trascend.Bolet.ControlesByT.Ventanas;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Interesados;

namespace Trascend.Bolet.Cliente.Presentadores.Marcas
{
    class PresentadorGestionarInstruccionFacturacionMarca : PresentadorBase
    {
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IGestionarInstruccionFacturacionMarca _ventana;
        private object _ventanaConsultarMarcas;
        private Marca _marca;
        private Asociado _asociado;
        private IList<Asociado> _asociados = new List<Asociado>();
        private Interesado _interesado;
        private IList<Interesado> _interesados = new List<Interesado>();
        private Carta _cartaAsociado;
        private Carta _cartaInteresado;
        private IList<ListaDatosValores> _tiposDeInstruccion;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IListaDatosValoresServicios _listaDatosValoresServicios;
        private IInstruccionCorrespondenciaServicios _instruccionCorrespondenciaServicios;
        private IInstruccionEnvioOriginalesServicios _instruccionEnvioOriginalesServicios;
        private IAsociadoServicios _asociadoServicios;
        private IInteresadoServicios _interesadoServicios;
        private ICartaServicios _cartaServicios;

        /// <summary>
        /// Constructor predeterminado que recibe una marca y una ventana padre
        /// </summary>
        /// <param name="ventana">Ventana actual</param>
        /// <param name="instruccionEnvioEmails">Instruccion de Facturacion de Envio de Emails</param>
        /// <param name="instruccionEnvioOriginales">Instruccion de Facturacion de Envio de Originales</param>
        /// <param name="marca">Marca seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="ventanaConsultarMarcas">Ventana padre de la ventana ConsultarMarca</param>
        public PresentadorGestionarInstruccionFacturacionMarca (IGestionarInstruccionFacturacionMarca ventana, 
                                                                object instruccionEnvioEmails, 
                                                                object instruccionEnvioOriginales,
                                                                object marca, 
                                                                object ventanaPadre,
                                                                object ventanaConsultarMarcas)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventanaPadre = ventanaPadre;
                this._ventanaConsultarMarcas = ventanaConsultarMarcas;
                this._marca = (Marca)marca;
                this._ventana.Marca = marca;
                this._ventana.InstruccionEnvioEmail = (InstruccionCorrespondencia)instruccionEnvioEmails;
                this._ventana.InstruccionEnvioOriginales = (InstruccionEnvioOriginales)instruccionEnvioOriginales;
                this._ventana.AlertaEOriginales = (InstruccionEnvioOriginales)instruccionEnvioOriginales;

                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._listaDatosValoresServicios = (IListaDatosValoresServicios)Activator.GetObject(typeof(IListaDatosValoresServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosValoresServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._instruccionCorrespondenciaServicios = (IInstruccionCorrespondenciaServicios)Activator.GetObject(typeof(IInstruccionCorrespondenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InstruccionCorrespondenciaServicios"]);
                this._instruccionEnvioOriginalesServicios = (IInstruccionEnvioOriginalesServicios)Activator.GetObject(typeof(IInstruccionEnvioOriginalesServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InstruccionEnvioOriginalesServicios"]);
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._interesadoServicios = (IInteresadoServicios)Activator.GetObject(typeof(IInteresadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["InteresadoServicios"]);

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

                this._ventana.TipoDeMarca = CargarCategoriaDeMarca();

                CargarDatosInstruccionEnvioEmails();

                CargarDatosInstruccionEnvioOriginales();

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
        /// Metodo que carga los datos iniciales de la instruccion de facturacion por envio de originales
        /// </summary>
        private void CargarDatosInstruccionEnvioOriginales()
        {
            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado primerAsociado = new Asociado();
                Interesado primerInteresado = new Interesado();
                primerAsociado.Id = int.MinValue;
                primerInteresado.Id = int.MinValue;

                Asociado asociadoEO = ((InstruccionEnvioOriginales)this._ventana.InstruccionEnvioOriginales).Asociado;
                Interesado interesadoEO = ((InstruccionEnvioOriginales)this._ventana.InstruccionEnvioOriginales).Interesado;

                this._asociados.Add(primerAsociado);
                if (asociadoEO != null)
                    this._asociados.Add(asociadoEO);

                this._ventana.Asociados = this._asociados;
                if (asociadoEO != null)
                    this._ventana.Asociado = this.BuscarAsociado(_asociados, asociadoEO);

                this._interesados.Add(primerInteresado);
                if (interesadoEO != null)
                    this._interesados.Add(interesadoEO);

                this._ventana.Interesados = this._interesados;
                if (interesadoEO != null)
                    this._ventana.Interesado = this.BuscarInteresado(_interesados, interesadoEO);

                this._ventana.ConvertirEnteroMinimoABlanco();

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
        /// Metodo que permite cargar los datos iniciales de una instruccion de Correspondencia de Envio de Emails
        /// </summary>
        private void CargarDatosInstruccionEnvioEmails()
        {

            IList<ListaDatosValores> tiposDeInstruccion = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                InstruccionCorrespondencia instruccion = (InstruccionCorrespondencia)this._ventana.InstruccionEnvioEmail;

                tiposDeInstruccion = this._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro
                    (new ListaDatosValores(Recursos.Etiquetas.cbiTipoInstruccion));

                this._ventana.TiposDeInstruccionEnvioEmails = tiposDeInstruccion;

                if (instruccion.TipoInstruccion != null)
                {
                    ListaDatosValores tipoInstruccion = new ListaDatosValores();
                    tipoInstruccion.Valor = instruccion.TipoInstruccion;
                    this._ventana.TipoInstruccionEnvioEmails = this.BuscarTipoInstruccion(tiposDeInstruccion, tipoInstruccion);
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
        }


        /// <summary>
        /// Metodo que determina la descripcion del tipo de Marca para colocarlo en el Textbox de la Ventana
        /// </summary>
        private String CargarCategoriaDeMarca()
        {
            String retorno = String.Empty;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ListaDatosDominio tipoMarcaActual = new ListaDatosDominio();
                tipoMarcaActual.Id = ((Marca)this._ventana.Marca).Tipo;

                IList<ListaDatosDominio> tiposMarca = this._listaDatosDominioServicios.
                        ConsultarListaDatosDominioPorParametro(new ListaDatosDominio(Recursos.Etiquetas.cbiCategoriaMarca));

                ListaDatosDominio tipoAMostar = this.BuscarTipoMarca(tiposMarca, ((Marca)this._ventana.Marca).Tipo);

                if (tipoAMostar != null)
                    retorno = tipoAMostar.Descripcion;
                else
                    retorno = null;


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


        public void ActualizarTitulo()
        {
            this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleGestionarInstruccionCorrespondencia,
                       Recursos.Ids.GestionarInstruccionCorrespondencia);
        }


        /// <summary>
        /// Metodo que obtiene el Id de la marca a la que se le esta modificando la Instruccion por Correspondencia
        /// </summary>
        /// <returns></returns>
        public string ObtenerIdMarca()
        {
            return ((Marca)this._marca).Id.ToString();
        }


        /// <summary>
        /// Metodo que realizar el proceso de insertar o modificar una Instruccion de Correspondencia
        /// </summary>
        /// <returns>True si fue realizado con exito; False en caso contrario</returns>
        public bool Modificar(String botonPresionado)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (botonPresionado.Equals("_btnModificar"))
                {
                    InstruccionCorrespondencia instruccion = CargarInstruccionDeCorrespondenciaDeLaPantalla();
                    if ((instruccion.TipoInstruccion != null) && (!instruccion.TipoInstruccion.Equals("")))
                    {
                        exitoso = this._instruccionCorrespondenciaServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash);
                        if (exitoso)
                            this._ventana.Mensaje("La instruccion de Facturación de Envio de Email fue guardada exitosamente", 2);

                    }
                    else
                        this._ventana.Mensaje("La Instruccion por Facturación de Envio de Email debe tener un Tipo", 0);
                }
                else if (botonPresionado.Equals("_btnModificarInstEnvioOriginales"))
                {
                    InstruccionEnvioOriginales instruccion = CargarInstruccionEnvioOriginalesDeLaPantalla();

                    if (instruccion.NombreInstruccion != null)
                    {
                        if (ValidarCorrespondencias(instruccion))
                        {
                            exitoso = this._instruccionEnvioOriginalesServicios.InsertarOModificar(instruccion, UsuarioLogeado.Hash); 
                        }
                        else
                            this._ventana.Mensaje("El Asociado o Interesado ingresado no tiene Correspondencia asociada", 0);
                    }
                    else
                        this._ventana.Mensaje("La Instrucción de Facturacion de Envío de Originales debe tener un Destinatario", 0);

                    if (exitoso)
                        this._ventana.Mensaje("La instruccion de Facturación de Envio de Originales fue guardada exitosamente", 2);

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

            return exitoso;
        }


        /// <summary>
        /// Metodo que valida que el Asociado o el Interesado que este ingresado tenga su numero de correspondencia asociado
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales de Facturacion a insertar o modificar</param>
        /// <returns>True si el Asociado o Interesado tiene su numero de correspondencia asociado; False en caso contrario</returns>
        public bool ValidarCorrespondencias(InstruccionEnvioOriginales instruccion)
        {
            bool retorno = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (((instruccion.Asociado != null) && (instruccion.CorrespAsociado == null)) ||
                    ((instruccion.Interesado != null) && (instruccion.CorrespInteresado == null)))
                {
                    retorno = false;
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



        /// <summary>
        /// Metodo que devuelve la instruccion de envios de originales de la pantalla
        /// </summary>
        /// <returns>Instruccion de Envios Originales de la pantalla</returns>
        private InstruccionEnvioOriginales CargarInstruccionEnvioOriginalesDeLaPantalla()
        {

            bool existeCarta = false;
            InstruccionEnvioOriginales instruccion = new InstruccionEnvioOriginales();

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                instruccion.Id = ((InstruccionEnvioOriginales)this._ventana.InstruccionEnvioOriginales).Id;
                instruccion.AplicadaA = ((InstruccionEnvioOriginales)this._ventana.InstruccionEnvioOriginales).AplicadaA;
                instruccion.Concepto = ((InstruccionEnvioOriginales)this._ventana.InstruccionEnvioOriginales).Concepto;

                instruccion.NombreInstruccion = !this._ventana.NombreInstruccionEnvioOriginales.Equals("") ?
                    this._ventana.NombreInstruccionEnvioOriginales : null;

                instruccion.Asociado = (this._ventana.Asociado != null) && (((Asociado)this._ventana.Asociado).Id != int.MinValue) ?
                    (Asociado)this._ventana.Asociado : null;

                if ((this._ventana.IdCorrespondencia_Asociado != null) && (!this._ventana.IdCorrespondencia_Asociado.Equals(String.Empty)))
                {
                    Carta cartaAsocBuscar = new Carta();
                    cartaAsocBuscar.Id = int.Parse(this._ventana.IdCorrespondencia_Asociado);
                    existeCarta = this._cartaServicios.VerificarExistencia(cartaAsocBuscar);
                    if (existeCarta)
                    {
                        IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaAsocBuscar);
                        Carta carta = cartas[0];
                        instruccion.CorrespAsociado = carta;
                        existeCarta = false;
                    }
                }
                else
                    instruccion.CorrespAsociado = null;


                instruccion.Interesado = (this._ventana.Interesado != null) && (((Interesado)this._ventana.Interesado).Id != int.MinValue) ?
                    (Interesado)this._ventana.Interesado : null;

                if ((this._ventana.IdCorrespondencia_Interesado != null) && (!this._ventana.IdCorrespondencia_Interesado.Equals(String.Empty)))
                {
                    Carta cartaIntBuscar = new Carta();
                    cartaIntBuscar.Id = int.Parse(this._ventana.IdCorrespondencia_Interesado);
                    existeCarta = this._cartaServicios.VerificarExistencia(cartaIntBuscar);
                    if (existeCarta)
                    {
                        IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaIntBuscar);
                        Carta carta = cartas[0];
                        instruccion.CorrespInteresado = carta;
                    }
                }
                else
                    instruccion.CorrespInteresado = null;


                instruccion.DireccionInstruccion = this._ventana.OtraDireccion != null ? this._ventana.OtraDireccion : null;

                instruccion.Alerta = this._ventana.AlertaEnvioOriginales != null ? this._ventana.AlertaEnvioOriginales : null;


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


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return instruccion;
        }




        /// <summary>
        /// Metodo que carga la Isntruccion de Correspondencia de la Pantalla
        /// </summary>
        /// <returns>Instruccion de Correspondencia a guardar o actualizar</returns>
        private InstruccionCorrespondencia CargarInstruccionDeCorrespondenciaDeLaPantalla()
        {
            InstruccionCorrespondencia instruccion = new InstruccionCorrespondencia();
            bool existeCarta = false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            try
            {
                instruccion.Id = ((InstruccionCorrespondencia)this._ventana.InstruccionEnvioEmail).Id;

                instruccion.AplicadaA = ((InstruccionCorrespondencia)this._ventana.InstruccionEnvioEmail).AplicadaA;

                instruccion.Concepto = ((InstruccionCorrespondencia)this._ventana.InstruccionEnvioEmail).Concepto;

                instruccion.TipoInstruccion = this._ventana.TipoInstruccionEnvioEmails != null ? ((ListaDatosValores)this._ventana.TipoInstruccionEnvioEmails).Valor : null;

                if ((this._ventana.IdCorrespondenciaEnvioEmails != null) && (!this._ventana.IdCorrespondenciaEnvioEmails.Equals(String.Empty)))
                {
                    Carta cartaABuscar = new Carta();
                    cartaABuscar.Id = int.Parse(this._ventana.IdCorrespondenciaEnvioEmails);
                    existeCarta = this._cartaServicios.VerificarExistencia(cartaABuscar);
                    if (existeCarta)
                    {
                        IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaABuscar);
                        Carta carta = cartas[0];
                        instruccion.Correspondencia = carta;
                    }
                }
                else
                    instruccion.Correspondencia = null;

                instruccion.NombreEmail = this._ventana.NombreEmailEnvioEmails != null ? this._ventana.NombreEmailEnvioEmails : null;

                instruccion.ParaEmail = this._ventana.EmailParaEnvioEmails != null ? this._ventana.EmailParaEnvioEmails : null;

                instruccion.CCEmail = this._ventana.EmailCCEnvioEmails != null ? this._ventana.EmailCCEnvioEmails : null;

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


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return instruccion;
        }

        /// <summary>
        /// Metodo que consulta una correspondencia que se le asigna a una Instruccion de Correspondencia
        /// <param name="botonPresionado">Boton que presiona el usuario al momento de consultar una correspondencia</param>
        /// </summary>
        public void ConsultarCorrespondencia(String botonPresionado)
        {
            Carta cartaAConsultar = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                switch (botonPresionado)
                {
                    case "_btnConsultarCorrespondencia":
                        if ((this._ventana.IdCorrespondenciaEnvioEmails != null) && (!this._ventana.IdCorrespondenciaEnvioEmails.Equals("")))
                        {
                            cartaAConsultar = new Carta();
                            cartaAConsultar.Id = int.Parse(this._ventana.IdCorrespondenciaEnvioEmails);
                        }
                        else
                        {
                            this._ventana.Mensaje("Escriba el Código de la Correspondencia a consultar", 0);
                        }
                        break;

                    case "_btnConsultarCorrespondenciaAsociado":
                        if ((this._ventana.IdCorrespondencia_Asociado != null) && (!this._ventana.IdCorrespondencia_Asociado.Equals("")))
                        {
                            cartaAConsultar = new Carta();
                            cartaAConsultar.Id = int.Parse(this._ventana.IdCorrespondencia_Asociado);
                        }
                        else
                        {
                            this._ventana.Mensaje("Escriba el Código de la Correspondencia del Asociado", 0);
                        }
                        break;

                    case "_btnConsultarCorrespondenciaInteresado":
                        if ((this._ventana.IdCorrespondencia_Interesado != null) && (!this._ventana.IdCorrespondencia_Interesado.Equals("")))
                        {
                            cartaAConsultar = new Carta();
                            cartaAConsultar.Id = int.Parse(this._ventana.IdCorrespondencia_Interesado);
                        }
                        else
                        {
                            this._ventana.Mensaje("Escriba el Código de la Correspondencia del Interesado", 0);
                        }
                        break;

                }

                if (cartaAConsultar != null)
                {
                    IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaAConsultar);
                    if (cartas != null)
                    {
                        if (cartas.Count > 0)
                        {
                            this.Navegar(new ConsultarCarta(cartas[0], this._ventana));
                        }
                        else
                        {
                            this._ventana.Mensaje("La Correspondencia consultada no existe", 0);
                            switch (botonPresionado)
                            {
                                case "_btnConsultarCorrespondencia":
                                    this._ventana.IdCorrespondenciaEnvioEmails = String.Empty;
                                    break;

                                case "_btnConsultarCorrespondenciaAsociado":
                                    this._ventana.IdCorrespondencia_Asociado = String.Empty;
                                    break;

                                case "_btn_ConsultarCorrespondenciaInteresado":
                                    this._ventana.IdCorrespondencia_Interesado = String.Empty;
                                    break;
                            }
                        }

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
        /// Metodo que vuelve a consultar la marca despues que agrega o actualiza una Instruccion de Correspondencia
        /// </summary>
        public void IrConsultarMarca()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this.Navegar(new ConsultarMarca(this._marca, this._ventanaConsultarMarcas));

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
        /// Metodo que consulta un asociado y presenta una lista de resultados
        /// </summary>
        public void BuscarAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Asociado primerAsociado = new Asociado(int.MinValue);


                Asociado asociado = new Asociado();
                IList<Asociado> asociadosFiltrados;

                asociado.Nombre = this._ventana.NombreAsociadoFiltrar.ToUpper();
                asociado.Id = this._ventana.IdAsociadoFiltrar.Equals("") ? 0
                                      : int.Parse(this._ventana.IdAsociadoFiltrar);


                if ((!asociado.Nombre.Equals("")) || (asociado.Id != 0))
                    asociadosFiltrados = this._asociadoServicios.ObtenerAsociadosFiltro(asociado);
                else
                    asociadosFiltrados = new List<Asociado>();

                if (asociadosFiltrados.Count != 0)
                {
                    asociadosFiltrados.Insert(0, primerAsociado);
                    this._ventana.Asociados = asociadosFiltrados;
                }
                else
                {
                    asociadosFiltrados.Insert(0, primerAsociado);
                    this._ventana.Asociados = this._asociados;
                    this._ventana.Asociado = primerAsociado;

                    this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
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
        }

        /// <summary>
        /// Metodo que cambia el asociado al seleccionar uno de la lista de consulta 
        /// </summary>
        public void CambiarAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Asociado)this._ventana.Asociado != null)
                {
                    Asociado asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);
                    this._ventana.AsociadoEnvioOriginales = ((Asociado)this._ventana.Asociado).Nombre;
                    this._ventana.IdAsociadoEnvioOriginales = ((Asociado)this._ventana.Asociado).Id.ToString();
                    this._ventana.Domicilio_Asociado = ((Asociado)this._ventana.Asociado).Domicilio;
                    this._asociado = (Asociado)this._ventana.Asociado;

                    this._ventana.ConvertirEnteroMinimoABlanco();
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
        }


        /// <summary>
        /// Metodo que consulta un interesado para la Instruccion de Envio de Originales
        /// </summary>
        public void BuscarInteresado()
        {

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Interesado primerInteresado = new Interesado(int.MinValue);
            Interesado interesado = new Interesado();
            IList<Interesado> interesadosFiltrados;

            interesado.Nombre = this._ventana.NombreInteresadoFiltrar.ToUpper();
            interesado.Id = this._ventana.IdInteresadoFiltrar.Equals("") ? int.MinValue : int.Parse(this._ventana.IdInteresadoFiltrar);


            if ((!interesado.Nombre.Equals("")) || (interesado.Id != int.MinValue))
                interesadosFiltrados = this._interesadoServicios.ObtenerInteresadosFiltro(interesado);
            else
                interesadosFiltrados = new List<Interesado>();

            if (interesadosFiltrados.Count != 0)
            {
                interesadosFiltrados.Insert(0, primerInteresado);
                this._ventana.Interesados = interesadosFiltrados;
            }
            else
            {
                interesadosFiltrados.Add(primerInteresado);
                this._ventana.Interesados = interesadosFiltrados;
                this._ventana.Mensaje(Recursos.MensajesConElUsuario.NoHayResultados, 1);
            }


            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }



        /// <summary>
        /// Metodo que cambia el Interesado seleccionado despues de la consulta de los Interesados
        /// </summary>
        public void CambiarInteresado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if ((Interesado)this._ventana.Interesado != null)
                {
                    Interesado interesadoAux = this._interesadoServicios.ConsultarInteresadoConTodo((Interesado)this._ventana.Interesado);

                    if (null != interesadoAux)
                    {
                        this._ventana.InteresadoEnvioOriginales = ((Interesado)this._ventana.Interesado).Nombre;
                        this._ventana.IdInteresadoEnvioOriginales = ((Interesado)this._ventana.Interesado).Id.ToString();
                        this._ventana.Domicilio_Interesado = ((Interesado)this._ventana.Interesado).Domicilio;
                        this._interesado = (Interesado)this._ventana.Interesado;
                        this._ventana.ConvertirEnteroMinimoABlanco();
                    }

                    else
                    {
                        this._ventana.InteresadoEnvioOriginales = String.Empty;
                        this._ventana.IdInteresadoEnvioOriginales = String.Empty;
                        this._ventana.Domicilio_Interesado = String.Empty;
                    }


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
        }


        /// <summary>
        /// Metodo que va a la ventana ConsultarAsociado para visualizar los datos del asociado en pantalla
        /// </summary>
        public void IrVentanaAsociado()
        {
            if ((Asociado)this._ventana.Asociado != null)
            {
                Asociado asociado = ((Asociado)this._ventana.Asociado).Id != int.MinValue ? (Asociado)this._ventana.Asociado : null;
                Navegar(new ConsultarAsociado(asociado, this._ventana, false));
            }
        }



        public void IrVentanaInteresado()
        {
            if ((Interesado)this._ventana.Interesado != null)
            {
                Interesado interesado = ((Interesado)this._ventana.Interesado).Id != int.MinValue ? (Interesado)this._ventana.Interesado : null;
                Navegar(new ConsultarInteresado(interesado, this._ventana));
            }
        }

    }
}
