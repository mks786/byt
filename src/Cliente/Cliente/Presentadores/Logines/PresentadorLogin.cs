using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.Remoting;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Logines;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Trascend.Bolet.Cliente.Presentadores.Logines
{
    class PresentadorLogin : PresentadorBase
    {
        private ILogin _ventana;
        private IUsuarioServicios _usuarioServicios;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Ventana que implementa la interfaz</param>
        public PresentadorLogin(ILogin ventana)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana = ventana;
                this._ventana.FocoPredeterminado();
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Método que autentica a un usuario
        /// </summary>
        public void Autenticacion()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                
                if (!string.IsNullOrEmpty(this._ventana.Id) && !string.IsNullOrEmpty(this._ventana.Password))
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = this._ventana.Id;
                    usuario.Password = this._ventana.Password.ToUpper();

                    usuario = this._usuarioServicios.Autenticar(usuario);

                    if (usuario != null)
                    {
                        UsuarioLogeado = usuario;
                        VentanaPrincipal ventanaPrincipal = VentanaPrincipal.ObtenerInstancia;
                        ventanaPrincipal.AplicarPermisologia();
                        ventanaPrincipal.Show();
                        this._ventana.MensajeError = "";
                    }
                    else
                    {
                        this._ventana.MensajeError = Recursos.MensajesConElUsuario.IdOClaveIncorrectos;
                    }
                }
                else
                {

                    this._ventana.MensajeError = Recursos.MensajesConElUsuario.IdOClaveObligatorio;
                }

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                this._ventana.MensajeError = ex.Message;
                logger.Error(ex.Message);
            }
            catch (RemotingException ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorRemoting;
                logger.Error(ex.Message);
            }
            catch (SocketException ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorConexionServidor;
                logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                this._ventana.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Método que cierra la aplicación
        /// </summary>
        public void Salir()
        {
            App.Current.Shutdown(); 
        }
    }
}
