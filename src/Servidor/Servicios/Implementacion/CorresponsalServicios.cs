using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CorresponsalServicios : MarshalByRefObject, ICorresponsalServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los Corresponsals
        /// </summary>
        /// <returns>Todos los Corresponsals</returns>
        public IList<Corresponsal> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<Corresponsal> corresponsals = ControladorCorresponsal.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return corresponsals;
        }


        public Corresponsal ConsultarPorId(Corresponsal entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica un Corresponsal
        /// </summary>
        /// <param name="corresponsal">Corresponsal a insertar o modificar</param>
        /// <param name="hash">hash del usuario loggeado</param>
        /// <returns></returns>
        public bool InsertarOModificar(Corresponsal corresponsal, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCorresponsal.InsertarOModificar(corresponsal, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que elimina un Corresponsal
        /// </summary>
        /// <param name="corresponsal">Corresponsal a eliminar</param>
        /// <param name="hash">Hash del usuario loggeado</param>
        /// <returns></returns>
        public bool Eliminar(Corresponsal corresponsal, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCorresponsal.Eliminar(corresponsal, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Metodo que verifica si un Corresponsal ya existe en el sistema
        /// </summary>
        /// <param name="corresponsal">Corresponsal a buscar</param>
        /// <returns>true si lo encontro, false en lo contrario</returns>
        public bool VerificarExistencia(Corresponsal corresponsal)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorCorresponsal.VerificarExistencia(corresponsal);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
