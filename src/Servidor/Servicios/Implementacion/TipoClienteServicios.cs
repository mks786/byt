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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tipoCliente = ControladorTipoCliente.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return tipoCliente;
        }

        public TipoCliente ConsultarPorId(TipoCliente tipoCliente)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(TipoCliente tipoCliente, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(TipoCliente tipoCliente, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(TipoCliente entidad)
        {
            throw new NotImplementedException();
        }
    }
}
