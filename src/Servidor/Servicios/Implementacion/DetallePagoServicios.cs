using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class DetallePagoServicios : MarshalByRefObject, IDetallePagoServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos los detalles de pago
        /// </summary>
        /// <returns>Lista con todos los detalles de pago</returns>
        public IList<DetallePago> ConsultarTodos()
        {
            IList<DetallePago> detallesPagos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                detallesPagos = ControladorDetallePago.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return detallesPagos;
        }

        public DetallePago ConsultarPorId(DetallePago detallePago)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(DetallePago detallePago, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(DetallePago detallePago, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(DetallePago entidad)
        {
            throw new NotImplementedException();
        }
    }
}
