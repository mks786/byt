using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.FechasPatente;
using Trascend.Bolet.Cliente.Ventanas.Patentes;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Presentadores.FechasPatente
{
    class PresentadorAgregarFecha: PresentadorBase
    {
        private IAgregarFecha _ventana;

        private IPatenteServicios _marcaServicios;
        private ICartaServicios _cartaServicios;
        private ITipoFechaServicios _tipoFechaServicios;
        private IFechaServicios _fechaServicios;
        private Fecha _fecha;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarFecha(IAgregarFecha ventana, object fecha)
        {
            try
            {
                this._ventana = ventana;
                this._fecha = (Fecha)fecha;
                this._ventana.FechaPatente = new Inventor();

                this._marcaServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["PatenteServicios"]);
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
            try
            {                            
                #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

                Mouse.OverrideCursor = Cursors.Wait;

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarInventor,
                        "");
                this._ventana.Tipos = null;
                this._ventana.Correspondencias = null;

                Carta cartaAux = new Carta(this._fecha.Correspondencia);

                IList<Carta> cartas = this._cartaServicios.ObtenerCartasFiltro(cartaAux);
                cartas.Insert(0, new Carta(int.MinValue));
                this._ventana.Correspondencias = cartas;

                IList<TipoFecha> tiposFecha = this._tipoFechaServicios.ConsultarTodos();
                tiposFecha.Insert(0, new TipoFecha(""));
                this._ventana.Tipos = tiposFecha;


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
        /// Método que realiza toda la lógica para agregar al contacto dentro de la base de datos
        /// </summary>
        public void Aceptar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = false;

                Fecha fecha = (Fecha)this._ventana.FechaPatente;

                if (true)
                {
                    fecha.Tipo = this._ventana.Tipo.ToString() == null ? null : this._ventana.Tipo.ToString();
                    fecha.Correspondencia = int.Parse(this._ventana.Correspondencia.ToString()) == (int.MinValue) ? 0 : int.Parse(this._ventana.Correspondencia.ToString());
                    fecha.Id = this._fecha.Id;

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
    }
}
