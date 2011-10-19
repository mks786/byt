using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class TarifaServicios : MarshalByRefObject, ITarifaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos las etiquetas
        /// </summary>
        /// <returns>Lista con todos las etiquetas</returns>
        public IList<Tarifa> ConsultarTodos()
        {
            IList<Tarifa> tarifas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tarifas = ControladorTarifa.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return tarifas;
        }

        public Tarifa ConsultarPorId(Tarifa tarifa)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Tarifa tarifa, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Tarifa tarifa, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(Tarifa entidad)
        {
            throw new NotImplementedException();
        }
    }
}
