using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoEstadoServicios : MarshalByRefObject, ITipoEstadoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Método que obtiene todos los TipoEstados
        /// </summary>
        /// <returns>Todos los TipoEstados</returns>
        public IList<TipoEstado> ConsultarTodos()
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            IList<TipoEstado> tipoEstados = ControladorTipoEstado.ConsultarTodos();

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return tipoEstados;
        }


        /// <summary>
        /// Servicio que consultar por Id un TipoEstado
        /// </summary>
        /// <param name="entidad">Entidad a buscar</param>
        /// <returns>TipoEstado que cumpla con el Id</returns>
        public TipoEstado ConsultarPorId(TipoEstado entidad)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que inserta o modifica un TipoEstado
        /// </summary>
        /// <param name="tipoEstado">TipoEstado a insertar o modificar</param>
        /// <param name="hash">hash del usuario logerad</param>
        /// <returns></returns>
        public bool InsertarOModificar(TipoEstado tipoEstado, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoEstado.InsertarOModificar(tipoEstado, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Método que elimina un TipoEstado
        /// </summary>
        /// <param name="tipoEstado">TipoEstado a eliminar</param>
        /// <param name="hash">Hash del usuario logeado</param>
        /// <returns></returns>
        public bool Eliminar(TipoEstado tipoEstado, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoEstado.Eliminar(tipoEstado, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de un TipoEstado
        /// </summary>
        /// <param name="tipoEstado">TipoEstado a verificar</param>
        /// <returns>true en caso de existir, false en caso contrario</returns>
        public bool VerificarExistencia(TipoEstado tipoEstado)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoEstado.VerificarExistencia(tipoEstado);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
