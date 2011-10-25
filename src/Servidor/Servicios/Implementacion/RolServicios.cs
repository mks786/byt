using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class RolServicios : MarshalByRefObject, IRolServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(); 

        /// <summary>
        /// Método que obtiene todos los Roles.
        /// </summary>
        /// <returns>Todos los roles</returns>
        public IList<Rol> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Rol> roles = ControladorRol.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return roles;
        }

        public Rol ConsultarPorId(Rol rol)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que insertar o modifica un rol
        /// </summary>
        /// <param name="rol">Rol que se va a insertar o modificar</param>
        /// <param name="hash">Hash de usuario que esta realizando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(Rol rol, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorRol.InsertarOModificar(rol, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;

        }

        /// <summary>
        /// Servicio que elimina un rol
        /// </summary>
        /// <param name="rol">Rol que se va a eliminar</param>
        /// <param name="hash">Hash de usuario que esta realizando la operacion</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(Rol rol, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorRol.Eliminar(rol, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        public IList<Rol> ConsultarRolesYObjetos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Rol> roles = ControladorRol.ConsultarRolesYObjetos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return roles;
        }


        public bool VerificarExistencia(Rol rol)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorRol.VerificarExistencia(rol);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
