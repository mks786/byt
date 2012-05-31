using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    class TipoBaseServicios : MarshalByRefObject, ITipoBaseServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todos los TiposBase
        /// </summary>
        /// <returns>Lista con todos los TipoBase</returns>
        public IList<TipoBase> ConsultarTodos()
        {
            IList<TipoBase> tipoBase;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoBase = ControladorTipoBase.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return tipoBase;
        }


        /// <summary>
        /// Servicio que consultar por Id un TipoBase
        /// </summary>
        /// <param name="entidad">Entidad a buscar</param>
        /// <returns>TipoBase que cumpla con el Id</returns>
        public TipoBase ConsultarPorId(TipoBase tipobase)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que insertar o modifica un tipoBase
        /// </summary>
        /// <param name="TipoBase">TipoBase que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(TipoBase tipobase, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoBase.InsertarOModificar(tipobase, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina un TipoBase
        /// </summary>
        /// <param name="TipoBase">TipoBase que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        public bool Eliminar(TipoBase tipoBase, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoBase.Eliminar(tipoBase, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de un TipoBase
        /// </summary>
        /// <param name="tipoEstado">TipoBase a verificar</param>
        /// <returns>true en caso de existir, false en caso contrario</returns>
        public bool VerificarExistencia(TipoBase tipoBase)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoBase.VerificarExistencia(tipoBase);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
