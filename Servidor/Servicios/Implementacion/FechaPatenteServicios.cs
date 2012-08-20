using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class FechaPatenteServicios : MarshalByRefObject, IFechaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene todos los Inventors
        /// </summary>
        /// <returns>Todos los Inventors</returns>
        public IList<Fecha> ConsultarTodos()
        {
            throw new NotImplementedException();

        }


        public Fecha ConsultarPorId(Fecha entidad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que inserta o modifica un Fecha
        /// </summary>
        /// <param name="fecha">Fecha a insertar o modificar</param>
        /// <param name="hash">hash del usuario loggeado</param>
        /// <returns></returns>
        public bool InsertarOModificar(Fecha fecha, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorFecha.InsertarOModificar(fecha, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que elimina un Fecha
        /// </summary>
        /// <param name="fecha">Fecha a eliminar</param>
        /// <param name="hash">Hash del usuario loggeado</param>
        /// <returns></returns>
        public bool Eliminar(Fecha fecha, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorFecha.Eliminar(fecha, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }

        /// <summary>
        /// Método que verifica si un anexo ya existe en el sistema
        /// </summary>
        /// <param name="fecha">Fecha a buscar</param>
        /// <returns>true si lo encontro, false en lo contrario</returns>
        public bool VerificarExistencia(Fecha fecha)
        {
            throw new NotImplementedException();
        }
    }
}
