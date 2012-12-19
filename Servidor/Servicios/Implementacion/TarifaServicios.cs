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
        /// Servicio que obtiene todos las Tarifas
        /// </summary>
        /// <returns>Lista con todos las Tarifas</returns>
        public IList<Tarifa> ConsultarTodos()
        {
            IList<Tarifa> tarifas;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                tarifas = ControladorTarifa.ConsultarTodos();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
            return tarifas;
        }



        /// <summary>
        /// Servicio que consultar por Id un Tarifa
        /// </summary>
        /// <param name="entidad">Entidad a buscar</param>
        /// <returns>Tarifa que cumpla con el Id</returns>
        public Tarifa ConsultarPorId(Tarifa tarifa)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que inserta o modifica un Tarifa
        /// </summary>
        /// <param name="tipoEstado">Tarifa a insertar o modificar</param>
        /// <param name="hash">hash del usuario logerad</param>
        /// <returns></returns>
        public bool InsertarOModificar(Tarifa tarifa, int hash)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que elimina un tarifa
        /// </summary>
        /// <param name="tipoEstado">tarifa a eliminar</param>
        /// <param name="hash">Hash del usuario logeado</param>
        /// <returns></returns>
        public bool Eliminar(Tarifa tarifa, int hash)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Servicio que verifica la existencia de un Tarifa
        /// </summary>
        /// <param name="tipoEstado">Tarifa a verificar</param>
        /// <returns>true en caso de existir, false en caso contrario</returns>
        public bool VerificarExistencia(Tarifa entidad)
        {
            throw new NotImplementedException();
        }
    }
}
