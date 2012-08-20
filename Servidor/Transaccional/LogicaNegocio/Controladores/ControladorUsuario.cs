using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorUsuario : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public static IList<Usuario> ConsultarTodos()
        {
            IList<Usuario> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Usuario>> comando = FabricaComandosUsuario.ObtenerComandoConsultarTodos();
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return retorno;
        }

        /// <summary>
        /// Método que modifica un los datos de un Usuario
        /// </summary>
        /// <param name="usuario">Usuario a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Usuario usuario, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //Si el usuario no tiene contraseña (Agregación), se coloca como contraseña el Id
                if (string.IsNullOrEmpty(usuario.Password))
                    usuario.Password = usuario.Id;

                ComandoBase<bool> comando = FabricaComandosUsuario.ObtenerComandoInsertarOModificar(usuario);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return exitoso;
        }


        /// <summary>
        /// Método que elimina un usuario
        /// </summary>
        /// <param name="usuario">Usuario a eliminar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Usuario usuario, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosUsuario.ObtenerComandoEliminarUsuario(usuario);
                comando.Ejecutar();
                exitoso = true;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return exitoso;
        }

        /// <summary>
        /// Método que autentica un usuario
        /// </summary>
        /// <param name="usuario">Usuario a atuenticar</param>
        /// <returns>True si la autenticacion fue exitosa, en caso contrario False</returns>
        public static Usuario Autenticar(Usuario usuario)
        {
            Usuario usuarioAutenticado;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Usuario> comando = FabricaComandosUsuario.ObtenerComandoAutenticarUsuario(usuario);
                comando.Ejecutar();
                usuarioAutenticado = comando.Receptor.ObjetoAlmacenado;
                if (usuarioAutenticado != null)
                    usuarioAutenticado = CrearSesion(usuarioAutenticado);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return usuarioAutenticado;
        }

        /// <summary>
        /// Verifica si el usuario existe
        /// </summary>
        /// <param name="usuario">Usuario a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(Usuario usuario)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosUsuario.ObtenerComandoVerificarExistenciaUsuario(usuario);
                comando.Ejecutar();
                existe = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return existe;
        }
    }
}
