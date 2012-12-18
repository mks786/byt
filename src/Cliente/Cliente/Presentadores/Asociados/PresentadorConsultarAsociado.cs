using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.Cliente.Ventanas.Asociados;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ventanas.Auditorias;
using Trascend.Bolet.Cliente.Ventanas.Cartas;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using Trascend.Bolet.Cliente.Ayuda;
using System.Text.RegularExpressions;
using Trascend.Bolet.ControlesByT.Ventanas;
using Trascend.Bolet.Cliente.Ventanas.EmailsAsociado;

namespace Trascend.Bolet.Cliente.Presentadores.Asociados
{
    class PresentadorConsultarAsociado : PresentadorBase
    {

        private IConsultarAsociado _ventana;
        private IAsociadoServicios _asociadoServicios;
        private IDetallePagoServicios _detallePagoServicios;
        private IEtiquetaServicios _etiquetaServicios;
        private IIdiomaServicios _idiomaServicios;
        private IMonedaServicios _monedaServicios;
        private ITarifaServicios _tarifaServicios;
        private ITipoClienteServicios _tipoClienteServicios;
        private IPaisServicios _paisServicios;
        private IContactoServicios _contactoServicios;
        private ITipoEmailAsociadoServicios _tipoEmailAsociadoServicios;
        private IEmailAsociadoServicios _emailAsociadoServicios;
        private ICartaServicios _cartaServicios;
        private IListaDatosDominioServicios _listaDatosDominioServicios;
        private IDatosTransferenciaServicios _datosTransferenciaServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private IList<Auditoria> _auditorias;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="asociado">Asociado a mostrar</param>
        public PresentadorConsultarAsociado(IConsultarAsociado ventana, object asociado, object ventanaPadre)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventanaPadre = ventanaPadre;
                this._ventana = ventana;
                this._ventana.Asociado = asociado;
                this._asociadoServicios = (IAsociadoServicios)Activator.GetObject(typeof(IAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["AsociadoServicios"]);
                this._detallePagoServicios = (IDetallePagoServicios)Activator.GetObject(typeof(IDetallePagoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DetallePagoServicios"]);
                this._etiquetaServicios = (IEtiquetaServicios)Activator.GetObject(typeof(IEtiquetaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["EtiquetaServicios"]);
                this._idiomaServicios = (IIdiomaServicios)Activator.GetObject(typeof(IIdiomaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["IdiomaServicios"]);
                this._monedaServicios = (IMonedaServicios)Activator.GetObject(typeof(IMonedaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["MonedaServicios"]);
                this._tarifaServicios = (ITarifaServicios)Activator.GetObject(typeof(ITarifaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TarifaServicios"]);
                this._tipoClienteServicios = (ITipoClienteServicios)Activator.GetObject(typeof(ITipoClienteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoClienteServicios"]);
                this._paisServicios = (IPaisServicios)Activator.GetObject(typeof(IPaisServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PaisServicios"]);
                this._contactoServicios = (IContactoServicios)Activator.GetObject(typeof(IContactoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ContactoServicios"]);
                this._datosTransferenciaServicios = (IDatosTransferenciaServicios)Activator.GetObject(typeof(IDatosTransferenciaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DatosTransferenciaServicios"]);
                this._listaDatosDominioServicios = (IListaDatosDominioServicios)Activator.GetObject(typeof(IListaDatosDominioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["ListaDatosDominioServicios"]);
                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._tipoEmailAsociadoServicios = (ITipoEmailAsociadoServicios)Activator.GetObject(typeof(ITipoEmailAsociadoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoEmailAsociadoServicios"]);

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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarAsociado,
                    Recursos.Ids.ConsultarAsociado);

                this._ventana.Asociado = this._asociadoServicios.ConsultarAsociadoConTodo((Asociado)this._ventana.Asociado);

                Asociado asociado = (Asociado)this._ventana.Asociado;

                asociado.Contactos = this._contactoServicios.ConsultarContactosPorAsociado(asociado);
                //asociado.Emails = FiltrarEmailsPorDepartamento(UsuarioLogeado.Departamento, this._asociadoServicios.ConsultarEmailsDelAsociado(asociado));
                asociado.Emails = this._asociadoServicios.ConsultarEmailsDelAsociado(asociado);
                asociado.DatosTransferencias = this._datosTransferenciaServicios.ConsultarDatosTransferenciaPorAsociado(asociado);



                IList<Pais> paises = this._paisServicios.ConsultarTodos();

                this._ventana.Paises = paises;
                this._ventana.Pais = this.BuscarPais(paises, asociado.Pais);

                IList<Idioma> idiomas = this._idiomaServicios.ConsultarTodos();
                this._ventana.Idiomas = idiomas;
                this._ventana.Idioma = this.BuscarIdioma(idiomas, asociado.Idioma);

                IList<Moneda> monedas = this._monedaServicios.ConsultarTodos();
                this._ventana.Monedas = monedas;
                this._ventana.Moneda = this.BuscarMoneda(monedas, asociado.Moneda);

                IList<TipoCliente> tiposClientes = this._tipoClienteServicios.ConsultarTodos();
                this._ventana.TiposClientes = tiposClientes;
                this._ventana.TipoCliente = this.BuscarTipoCliente(tiposClientes, asociado.TipoCliente);

                IList<Tarifa> tarifas = this._tarifaServicios.ConsultarTodos();
                Tarifa primeraTarifa = new Tarifa();
                primeraTarifa.Id = "NGN";
                tarifas.Insert(0, primeraTarifa);
                this._ventana.Tarifas = tarifas;
                this._ventana.Tarifa = this.BuscarTarifa(tarifas, asociado.Tarifa);

                IList<Etiqueta> etiquetas = this._etiquetaServicios.ConsultarTodos();
                Etiqueta primeraEtiqueta = new Etiqueta();
                primeraEtiqueta.Id = string.Empty;
                etiquetas.Insert(0, primeraEtiqueta);
                this._ventana.Etiquetas = etiquetas;
                this._ventana.Etiqueta = this.BuscarEtiqueta(etiquetas, asociado.Etiqueta);

                IList<DetallePago> detallesPagos = this._detallePagoServicios.ConsultarTodos();
                DetallePago primerDetallePago = new DetallePago();
                primerDetallePago.Id = "NGN";
                detallesPagos.Insert(0, primerDetallePago);
                this._ventana.DetallesPagos = detallesPagos;
                this._ventana.DetallePago = this.BuscarDetallePago(detallesPagos, asociado.DetallePago);

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Asociado)this._ventana.Asociado).Id;
                auditoria.Tabla = "FAC_ASOCIADOS";

                IList<ListaDatosDominio> tiposPersona = this._listaDatosDominioServicios.ConsultarListaDatosDominioPorParametro(new ListaDatosDominio("PERSONA"));
                this._ventana.TipoPersonas = tiposPersona;

                this._ventana.TipoPersona = BuscarTipoPersona(asociado.TipoPersona, (IList<ListaDatosDominio>)this._ventana.TipoPersonas);

                _auditorias = this._asociadoServicios.AuditoriaPorFkyTabla(auditoria);

                if (asociado.Justificaciones.Count > 0)
                    this._ventana.pintarJustificacion();
                if (asociado.Contactos.Count > 0)
                    this._ventana.pintarContacto();
                if (asociado.DatosTransferencias.Count > 0)
                    this._ventana.pintarDatosTransferencia();
                if (_auditorias.Count > 0)
                    this._ventana.pintarAuditoria();
                if (asociado.Emails.Count > 0)
                    this._ventana.pintarEmails();
                if (this._asociadoServicios.VerificarCartasPorAsociado(asociado))
                    this._ventana.pintarCorrespondencia();

                IList<ContactosDelAsociadoVista> listaContactos = this._asociadoServicios.ConsultarContactosDelAsociado(asociado, false);
                this._ventana.ListaContactos = listaContactos;

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

                //Modifica los datos del Agente
                else
                {
                    Asociado asociado = (Asociado)this._ventana.Asociado;

                    asociado.Operacion = "MODIFY";
                    asociado.TipoPersona = ((ListaDatosDominio)this._ventana.TipoPersona).Id[0];
                    asociado.Pais = (Pais)this._ventana.Pais;
                    asociado.Idioma = (Idioma)this._ventana.Idioma;
                    asociado.Moneda = (Moneda)this._ventana.Moneda;
                    asociado.TipoCliente = (TipoCliente)this._ventana.TipoCliente;

                    if ((Tarifa)this._ventana.Tarifa != null)
                        asociado.Tarifa = !((Tarifa)this._ventana.Tarifa).Id.Equals("NGN") ? (Tarifa)this._ventana.Tarifa : null;

                    if ((Etiqueta)this._ventana.Etiqueta != null)
                        asociado.Etiqueta = !((Etiqueta)this._ventana.Etiqueta).Id.Equals("NGN") ? (Etiqueta)this._ventana.Etiqueta : null;

                    if ((DetallePago)this._ventana.DetallePago != null)
                        asociado.DetallePago = !((DetallePago)this._ventana.DetallePago).Id.Equals("NGN") ? (DetallePago)this._ventana.DetallePago : null;

                    bool exitoso = this._asociadoServicios.InsertarOModificar(asociado, UsuarioLogeado.Hash);

                    if (exitoso)
                        this.Navegar(Recursos.MensajesConElUsuario.AsociadoModificado, false);
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
        /// Método que elimina un asociado
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._asociadoServicios.Eliminar((Asociado)this._ventana.Asociado, UsuarioLogeado.Hash))
                {
                    this.Navegar(Recursos.MensajesConElUsuario.AsociadoEliminado, false);
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
        /// Método que muestra la ventana de Justificaciones de un Asociado
        /// </summary>
        public void IrListaJustificaciones()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaJustificaciones(this._ventana.Asociado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

        }


        /// <summary>
        /// Método que muestra la ventana de Contactos de un Asociado
        /// </summary>
        public void IrListaContactos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaContactos(this._ventana.Asociado, this._ventana));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que muestra la ventana de transferencia de un Asociado
        /// </summary>
        public void IrListaDatosTransferencia()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaDatosTransferencias(this._ventana.Asociado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        /// <summary>
        /// Método que muestra la ventana de Auditoría de un Asociado
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
        /// Metodo que muestra el expediente de un asociado en PDF
        /// </summary>
        public void AbrirExpediente()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["rutaAsociados"].ToString() + ((Asociado)this._ventana.Asociado).Id + ".pdf");

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



        public void IrACorrespondencia()
        {
            Navegar(new ConsultarCartas(this._ventana, this._ventana.Asociado));
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
                ((ListView)this._ventana.ListaContactos).Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (this._ventana.CurSortCol == column && this._ventana.CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            this._ventana.CurSortCol = column;
            this._ventana.CurAdorner = new SortAdorner(this._ventana.CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(this._ventana.CurSortCol).Add(this._ventana.CurAdorner);
            ((ListView)this._ventana.ListaContactos).Items.SortDescriptions.Add(
                new SortDescription(field, newDir));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }


        public void ConsultarUltimaCorrespondenciaEnviada()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaSalida != 0)
                {
                    Carta ultimaCorrespondenciaEnviada = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaSalida))[0];
                    Navegar(new ConsultarCarta(ultimaCorrespondenciaEnviada, this._ventana));
                }
            }
        }


        public void ConsultarCorrespondenciaCreacion()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).CartaCreacion != 0)
                {
                    Carta correspondenciaCreacion = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).CartaCreacion))[0];
                    Navegar(new ConsultarCarta(correspondenciaCreacion, this._ventana));
                }
            }
        }


        public void ConsultarUltimaCorrespondenciaEntrada()
        {
            if ((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado != null)
            {
                if (((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaEntrada != 0)
                {
                    Carta ultimaCorrespondenciaEntrada = this._cartaServicios.ObtenerCartasFiltro(
                        new Carta(((ContactosDelAsociadoVista)this._ventana.ContactoSeleccionado).UltimaCartaEntrada))[0];
                    Navegar(new ConsultarCarta(ultimaCorrespondenciaEntrada, this._ventana));
                }
            }
        }


        public void IrWebAsociado()
        {
            if (!string.IsNullOrEmpty(((Asociado)this._ventana.Asociado).Web))
            {
                Match match = Regex.Match(((Asociado)this._ventana.Asociado).Web, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$",
        RegexOptions.IgnoreCase);
                if (match.Success)
                    IrURL(((Asociado)this._ventana.Asociado).Web);
                else
                    this._ventana.Mensaje("Disculpe, URL errónea");
            }
        }


        public void VerEtiqueta()
        {
            if (((Etiqueta)this._ventana.Etiqueta).Id != string.Empty)
            {
                string stringAMostrar = "Español:" +
                                        Environment.NewLine +
                                        ((Etiqueta)this._ventana.Etiqueta).Descripcion1 +
                                        Environment.NewLine + "Inglés: " +
                                        Environment.NewLine +
                                        ((Etiqueta)this._ventana.Etiqueta).Descripcion2;

                ChildWindow detalle = new ChildWindow(stringAMostrar);
                detalle.ShowDialog();
            }

        }


        public void RefrescarVentanaPadre()
        {
            ((ConsultarContactosPorAsociado)_ventanaPadre).Refrescar();
        }


        public void VerEmailsAsociado()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ((Asociado)this._ventana.Asociado).Emails = this._asociadoServicios.ConsultarEmailsDelAsociado((Asociado)this._ventana.Asociado);
                Navegar(new ListaEmails(this._ventana.Asociado));

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
    }
}
