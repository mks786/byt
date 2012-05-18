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

        private IPatenteServicios _patenteServicios;
        private ICartaServicios _cartaServicios;
        private ITipoFechaServicios _tipoFechaServicios;
        private IFechaServicios _fechaServicios;
        private Fecha _fecha;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        public PresentadorAgregarFecha(IAgregarFecha ventana, object patente)
        {
            try
            {
                this._ventana = ventana;
                this._fecha = new Fecha();
                this._fecha.Patente = (Patente)patente;
                this._ventana.FechaRegistro = DateTime.Now.ToShortDateString().ToString();

                this._patenteServicios = (IPatenteServicios)Activator.GetObject(typeof(IPatenteServicios),
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleAgregarFecha,
                        "");
                this._ventana.Tipos = null;
                
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

                if (true)
                {

                    Fecha fecha = this.CargarFecha();

                    if (this.VerificarCorresponsal())
                    {
                        exitoso = this._fechaServicios.InsertarOModificar(fecha, UsuarioLogeado.Hash);

                        Patente patenteAux = new Patente(this._fecha.Patente.Id);
                        if (exitoso)
                            this.Navegar(new ListaFechas(patenteAux));
                    }
                    else
                    {
                        this._ventana.mensaje(Recursos.MensajesConElUsuario.ErrorCorrespondenciaNoEncontrada);
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

        public bool VerificarCorresponsal()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool retorno = false;

            IList<Carta> carta = this._cartaServicios.ObtenerCartasFiltro(new Carta(int.Parse(this._ventana.Correspondencia)));

            retorno = carta.Count != 0 ? true : false;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return retorno;
        }

        /// <summary>
        /// Método que busca los valores en la ventana y los almacena en una variable
        /// </summary>
        /// <returns>Fecha Cargada</returns>
        public Fecha CargarFecha()
        {
            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Fecha fecha = new Fecha();

            fecha.Tipo = this._ventana.Tipo == null ? null : (TipoFecha)this._ventana.Tipo;
            fecha.TimeStamp = this._ventana.TimeStamp != null ? Convert.ToDateTime(this._ventana.TimeStamp) : DateTime.MinValue;
            fecha.Usuario = UsuarioLogeado.Iniciales;
            fecha.Comentario = this._ventana.Comentario != null ? this._ventana.Comentario : "";
            fecha.FechaRegistro = Convert.ToDateTime(this._ventana.FechaRegistro);
            fecha.Correspondencia = new Carta(int.Parse(this._ventana.Correspondencia));
            fecha.Patente = this._fecha.Patente;

            #region trace
            if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return fecha;
        }
    }
}
