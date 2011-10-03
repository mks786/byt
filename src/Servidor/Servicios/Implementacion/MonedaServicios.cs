using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MonedaServicios : MarshalByRefObject, IMonedaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene todos las monedas
        /// </summary>
        /// <returns>Lista con todos las monedas</returns>
        public IList<Moneda> ConsultarTodos()
        {
            IList<Moneda> monedas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                monedas = ControladorMoneda.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return monedas;
        }

        public Moneda ConsultarPorId(Moneda moneda)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(Moneda moneda, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Moneda moneda, int hash)
        {
            throw new NotImplementedException();
        }
    }
}
