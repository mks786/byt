using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TipoClienteServicios : MarshalByRefObject, ITipoClienteServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los tipos de clientes
        /// </summary>
        /// <returns>Lista con todos los tipos de clientes</returns>
        public IList<TipoCliente> ConsultarTodos()
        {
            IList<TipoCliente> tipoCliente;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoCliente = ControladorTipoCliente.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return tipoCliente;
        }


        /// <summary>
        /// Servicio que consultar por Id un TipoCliente
        /// </summary>
        /// <param name="entidad">Entidad a buscar</param>
        /// <returns>TipoCliente que cumpla con el Id</returns>
        public TipoCliente ConsultarPorId(TipoCliente tipoCliente)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que inserta o modifica un TipoCliente
        /// </summary>
        /// <param name="tipoEstado">TipoCliente a insertar o modificar</param>
        /// <param name="hash">hash del usuario logerad</param>
        /// <returns></returns>
        public bool InsertarOModificar(TipoCliente tipoCliente, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que elimina un TipoCliente
        /// </summary>
        /// <param name="tipoEstado">TipoCliente a eliminar</param>
        /// <param name="hash">Hash del usuario logeado</param>
        /// <returns></returns>
        public bool Eliminar(TipoCliente tipoCliente, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Servicio que verifica la existencia de un TipoCliente
        /// </summary>
        /// <param name="tipoEstado">TipoCliente a verificar</param>
        /// <returns>true en caso de existir, false en caso contrario</returns>
        public bool VerificarExistencia(TipoCliente entidad)
        {
            throw new NotImplementedException();
        }
    }
}
