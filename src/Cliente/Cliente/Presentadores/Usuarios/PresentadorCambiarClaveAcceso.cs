using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;
using NLog;
using Trascend.Bolet.Cliente.Contratos.Usuarios;
using Trascend.Bolet.Cliente.Ventanas.Principales;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Runtime.Remoting;
using System.Net.Sockets;

namespace Trascend.Bolet.Cliente.Presentadores.Usuarios
{
    class PresentadorCambiarClaveAcceso: PresentadorBase
    {
        private ICambiarClaveAcceso _ventana;
        //private IDepartamentoServicios _departamentoServicios;
        //private IRolServicios _rolServicios;
        private IUsuarioServicios _usuarioServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado que recibe un Usuario y la ventana actual
        /// </summary>
        /// <param name="ventana">Ventana actual ICambiarClaveAcceso</param>
        /// <param name="usuario">Usuario Logueado</param>
        public PresentadorCambiarClaveAcceso(ICambiarClaveAcceso ventana, object usuario)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Usuario = usuario;
                this._usuarioServicios = (IUsuarioServicios)Activator.GetObject(typeof(IUsuarioServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["UsuarioServicios"]);
            }
            catch (Exception ex)
            {
                _paginaPrincipal.MensajeError = Recursos.MensajesConElUsuario.ErrorInesperado;
                logger.Error(ex.Message);
                this.Navegar(_paginaPrincipal);
            }
        }



        /// <summary>
        /// Metodo que carga la informacion del usuario en la ventana
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarUsuario,
                    Recursos.Ids.ConsultarUsuario);

                Usuario usuario = (Usuario)this._ventana.Usuario;
                                
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



        public void Modificar()
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                                
                Usuario usuario = (Usuario)this._ventana.Usuario;
                if (usuario.Id == UsuarioLogeado.Id)
                {
                    if (passwordValido())
                    {
                        usuario.Password = this._ventana.NuevoPassword.ToUpper();
                        UsuarioLogeado = usuario;
                        exitoso = this._usuarioServicios.InsertarOModificar(usuario, UsuarioLogeado.Hash);
                        if (exitoso)
                        {
                            PaginaPrincipal paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
                            paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.UsuarioNuevoPassword;
                            this.Navegar(paginaPrincipal);
                        }
                    }
                    else
                        this._ventana.Mensaje("La contraseña introducida no es valida, repita el proceso", 0);
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
        /// Metodo que limpia los campos para escribir la nueva contraseña de acceso al sistema
        /// </summary>
        public void LimpiarCampoNuevoPassword()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                this._ventana.NuevoPassword = String.Empty;
                this._ventana.NuevoPassword_Rep = String.Empty;

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
        /// Metodo que verifica que el nuevo password provisto por el usuario sea valido para poder hacer la actualizacion
        /// </summary>
        /// <returns>True si el password introducido es valido; False en caso contrario</returns>
        private bool passwordValido()
        {
            bool valido = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (!this._ventana.Password.Equals(this._ventana.NuevoPassword))
                {
                    if (this._ventana.NuevoPassword.Equals(this._ventana.NuevoPassword_Rep))
                        valido = true;
                    else
                        valido = false;
                }
                else
                    valido = false;

                #region trace
                if (ConfigurationManager.AppSettings["ambiente"].ToString().Equals("desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }

            return valido;
        }



        
    }
}
