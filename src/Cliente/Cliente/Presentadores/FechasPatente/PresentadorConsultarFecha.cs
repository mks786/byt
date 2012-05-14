using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.FechasPatente;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.FechasPatente
{
    class PresentadorConsultarFecha : PresentadorBase
    {
        private IConsultarFecha _ventana;
        private ICartaServicios _cartaServicios;
        private ITipoFechaServicios _tipoFechaServicios;
        private IFechaServicios _fechaServicios;

        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Fecha _fecha;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="contacto">Inventor a mostrar</param>
        public PresentadorConsultarFecha(IConsultarFecha ventana, object fecha)
        {
            try
            {
                this._ventana = ventana;
                this._fecha = (Fecha)fecha;
                this._ventana.FechaPatente = this._fecha;
                this._ventana.Tipos = null;
                this._ventana.Correspondencias = null;

                this._cartaServicios = (ICartaServicios)Activator.GetObject(typeof(ICartaServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["CartaServicios"]);
                this._tipoFechaServicios = (ITipoFechaServicios)Activator.GetObject(typeof(ITipoFechaServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["TipoFechaServicios"]);
                this._fechaServicios = (IFechaServicios)Activator.GetObject(typeof(IFechaServicios),
                     ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["FechaServicios"]);
                
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarInventor,"");

                Carta cartaAux = new Carta(this._fecha.Correspondencia);

                IList<Carta> cartas = this._cartaServicios.ConsultarTodos();
                cartas.Insert(0, new Carta(int.MinValue));
                this._ventana.Correspondencias = cartas;
                this._ventana.Correspondencia = this.BuscarCarta((IList<Carta>)this._ventana.Correspondencias, cartaAux);

                IList<TipoFecha> tiposFecha = this._tipoFechaServicios.ConsultarTodos();
                tiposFecha.Insert(0, new TipoFecha(""));
                this._ventana.Tipos = tiposFecha;
                this._ventana.Tipo = this.BuscarTipoFecha((IList<TipoFecha>)this._ventana.Tipos, this._fecha.Tipo);

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
        /// modifica los datos del contacto
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
                //Modifica los datos del Inventor
                else
                {
                    bool exitoso = false;
                    Fecha fecha = (Fecha)this._ventana.FechaPatente;

                    fecha.Tipo = this._ventana.Tipo == null ? null : this._ventana.Tipo.ToString();
                    fecha.Correspondencia = int.Parse(this._ventana.Correspondencia.ToString()).Equals(int.MinValue) ? 0 : int.Parse(this._ventana.Correspondencia.ToString());

                    exitoso = this._fechaServicios.InsertarOModificar(fecha, UsuarioLogeado.Hash);
                    
                    if (exitoso)
                        this.Navegar(new ListaFechas(this._fecha));
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
        /// Método que se activa al presionar el boton de eliminar al contacto. Es el encargado de eliminar el
        /// contacto
        /// </summary>
        public void Eliminar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (this._fechaServicios.Eliminar((Fecha)this._ventana.FechaPatente, UsuarioLogeado.Hash))
                {
                    Patente patente = new Patente(this._fecha.Id);
                    this.Navegar(new ListaInventores(patente));
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
