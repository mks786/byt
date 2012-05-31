using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Runtime.Remoting;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class UsuarioServicios: MarshalByRefObject, IUsuarioServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); 

        /// <summary>
        /// Servicio que obtiene todos los Usuarios
        /// </summary>
        /// <returns>Lista con todos los usuarios</returns>
        public IList<Usuario> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Usuario> usuarios = ControladorUsuario.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return usuarios;
        }


        /// <summary>
        /// Servicio que consulta una entidad por su Id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Usuario ConsultarPorId(Usuario usuario)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que insertar o modifica un usuario
        /// </summary>
        /// <param name="usuarioDT">Usuario que se va a insertar o modificar</param>
        /// <returns>True si la inserción o modificación fue exitosa, en caso contrario False</returns>
        public bool InsertarOModificar(Usuario usuario, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorUsuario.InsertarOModificar(usuario, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que autentica un usuario
        /// </summary>
        /// <param name="usuario">Usuario que se va a autenticar</param>
        /// <returns>True: si la atenticacion fue exitosa; False: en caso contrario</returns>
        public Usuario Autenticar(Usuario usuario)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            Usuario usuarioAutenticado = ControladorUsuario.Autenticar(usuario);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return usuarioAutenticado;
        }


        /// <summary>
        /// Método que cierra la sesión del cliente.
        /// </summary>
        /// <param name="hash">Hash del cliente</param>
        public void CerrarSession(int hash)
        {
            ControladorUsuario.CerrarSesion(hash);
        }


        /// <summary>
        /// Servicio que elimina un usuario
        /// </summary>
        /// <param name="usuario">Usuario que se va a eliminar</param>
        /// <param name="hash">Hash del usuario que realiza la operacion</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(Usuario usuario, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorUsuario.Eliminar(usuario,hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de una Entidad
        /// </summary>
        /// <param name="entidad">Entidad a verificar existencia</param>
        /// <returns>True en caso de ser exitoso, false en caso contrario</returns>
        public bool VerificarExistencia(Usuario usuario)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorUsuario.VerificarExistencia(usuario);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
