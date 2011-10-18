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
using System.ComponentModel;

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
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="asociado">Asociado a mostrar</param>
        public PresentadorConsultarAsociado(IConsultarAsociado ventana, object asociado)
        {
            try
            {

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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
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

                this._ventana.SetTipoPersona = BuscarTipoPersona(asociado.TipoPersona);

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
                primeraEtiqueta.Id = "NGN";
                etiquetas.Insert(0, primeraEtiqueta);
                this._ventana.Etiquetas = etiquetas;
                this._ventana.Etiqueta = this.BuscarEtiqueta(etiquetas, asociado.Etiqueta);

                IList<DetallePago> detallesPagos = this._detallePagoServicios.ConsultarTodos();
                DetallePago primerDetallePago = new DetallePago();
                primerDetallePago.Id = "NGN";
                detallesPagos.Insert(0, primerDetallePago);
                this._ventana.DetallesPagos = detallesPagos;
                this._ventana.DetallePago = this.BuscarDetallePago(detallesPagos, asociado.DetallePago);

                this._ventana.FocoPredeterminado();

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado,true);
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
                    asociado.TipoPersona = this._ventana.GetTipoPersona;
                    asociado.Pais = (Pais)this._ventana.Pais;
                    asociado.Idioma = (Idioma)this._ventana.Idioma;
                    asociado.Moneda = (Moneda)this._ventana.Moneda;
                    asociado.TipoCliente = (TipoCliente)this._ventana.TipoCliente;

                    if ((Tarifa)this._ventana.Tarifa != null)
                        asociado.Tarifa = !((Tarifa)this._ventana.Tarifa).Id.Equals("NGN")  ? (Tarifa)this._ventana.Tarifa : null;

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

        public void IrListaContactos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            this.Navegar(new ListaContactos(this._ventana.Asociado));

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion
        }

        

        public void Auditoria()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                Auditoria auditoria = new Auditoria();
                auditoria.Fk = ((Asociado)this._ventana.Asociado).Id;
                auditoria.Tabla = "FAC_ASOCIADOS";

                IList<Auditoria> auditorias = this._asociadoServicios.AuditoriaPorFkyTabla(auditoria);
                _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.PoderEliminado;
                this.Navegar(new ListaAuditorias(auditorias));


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
    }
}
