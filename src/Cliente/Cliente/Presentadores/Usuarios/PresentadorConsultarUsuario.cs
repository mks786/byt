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
    class PresentadorConsultarUsuario : PresentadorBase
    {
        private IConsultarUsuario _ventana;
        private IDepartamentoServicios _departamentoServicios;
        private IRolServicios _rolServicios;
        private IUsuarioServicios _usuarioServicios;
        private static PaginaPrincipal _paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ventana">Página que satisface el contrato</param>
        /// <param name="usuario">Usuario a mostrar</param>
        public PresentadorConsultarUsuario(IConsultarUsuario ventana, object usuario)
        {
            try
            {
                this._ventana = ventana;
                this._ventana.Usuario = usuario;
                this._departamentoServicios = (IDepartamentoServicios)Activator.GetObject(typeof(IDepartamentoServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["DepartamentoServicios"]);
                this._rolServicios = (IRolServicios)Activator.GetObject(typeof(IRolServicios),
                    ConfigurationManager.AppSettings["RutaServidor"] + ConfigurationManager.AppSettings["RolServicios"]);
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

                this.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.titleConsultarUsuario,
                    Recursos.Ids.ConsultarUsuario);

                Usuario usuario = (Usuario)this._ventana.Usuario;

                IList<Rol> roles = this._rolServicios.ConsultarTodos();
                this._ventana.Roles = roles;
                this._ventana.Rol = this.BuscarRol(roles, usuario.Rol);

                IList<Departamento> departamentos = this._departamentoServicios.ConsultarTodos();
                this._ventana.Departamentos = departamentos;
                this._ventana.Departamento = this.BuscarDepartamento(departamentos, usuario.Departamento);
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
        /// Método que dependiendo del estado de la página, habilita los campos o 
        /// modifica los datos del usuario
        /// </summary>
        public void Modificar()
        {
            try
            {
                //Habilitar campos
                if (this._ventana.TextoBotonModificar == Recursos.Etiquetas.btnModificar)
                {
                    this._ventana.HabilitarCampos = true;
                    this._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar;
                }

                //Modifica los datos del usuario
                else
                {
                    Usuario usuario = (Usuario)this._ventana.Usuario;

                    if (this._ventana.Rol != null)
                    {
                        Rol rol = (Rol)this._ventana.Rol;
                        usuario.Rol = rol;
                    }
                    if (this._ventana.Departamento != null)
                    {
                        Departamento departamento = (Departamento)this._ventana.Departamento;
                        usuario.Departamento = departamento;
                    }
                    bool exitoso = this._usuarioServicios.InsertarOModificar(usuario, UsuarioLogeado.Hash);

                    if (exitoso)
                    {
                        PaginaPrincipal paginaPrincipal = PaginaPrincipal.ObtenerInstancia;
                        paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.UsuarioModificado;
                        this.Navegar(paginaPrincipal);
                    }
                }
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

                if (this._usuarioServicios.Eliminar((Usuario)this._ventana.Usuario, UsuarioLogeado.Hash))
                {
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.UsuarioEliminado;
                    this.Navegar(_paginaPrincipal);
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
