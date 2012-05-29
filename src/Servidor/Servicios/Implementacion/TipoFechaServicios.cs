using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    class TipoFechaServicios : MarshalByRefObject, ITipoFechaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todos los tipoFechas
        /// </summary>
        /// <returns>Lista con todos los tipoFechas</returns>
        public IList<TipoFecha> ConsultarTodos()
        {
            IList<TipoFecha> tipoFecha;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoFecha = ControladorTipoFecha.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return tipoFecha;
        }


        /// <summary>
        /// Servicio que consulta por Id un TipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha a consultar</param>
        /// <returns>Tipo fecha que cumple con el Id</returns>
        public TipoFecha ConsultarPorId(TipoFecha tipoFecha)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que insertar o modifica un TipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha que se va a insertar o modificar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>True: si la inserción o modificación fue exitosa; False: en caso contrario</returns>
        public bool InsertarOModificar(TipoFecha tipoFecha, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoFecha.InsertarOModificar(tipoFecha, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina un TipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha que se va a eliminar</param>
        /// <returns>True: si la eliminacion fue exitosa; False: en caso contrario</returns>
        /// 

        /// <summary>
        /// Servicio que elimina un TipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha que se va a eliminar</param>
        /// <param name="hash">Hash del usuario que esta realiando la operacion</param>
        /// <returns>true en caso de que la eliminación haya sido correcta, false en caso contrario</returns>
        public bool Eliminar(TipoFecha tipoFecha, int hash)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoFecha.Eliminar(tipoFecha, hash);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }


        /// <summary>
        /// Servicio que verifica la existencia de un TipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha a verificar su existencia</param>
        /// <returns>True en caso de existir, false en caso contrario</returns>
        public bool VerificarExistencia(TipoFecha tipoFecha)
        {
            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            bool exitoso = ControladorTipoFecha.VerificarExistencia(tipoFecha);

            #region trace
            if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
            #endregion

            return exitoso;
        }
    }
}
