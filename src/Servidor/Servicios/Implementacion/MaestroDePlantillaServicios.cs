using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class MaestroDePlantillaServicios: MarshalByRefObject, IMaestroDePlantillaServicios
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que consulta todos los registros que se encuentran en la tabla ENV_MAESTRO_PLANT
        /// </summary>
        /// <returns>Lista de todos los maestros de plantilla que se encuentran en la tabla</returns>
        public IList<MaestroDePlantilla> ConsultarTodos()
        {
            IList<MaestroDePlantilla> maestrosPlantilla;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                maestrosPlantilla = ControladorMaestroDePlantilla.ConsultarTodos();

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

            return maestrosPlantilla;
        }

        
        /// <summary>
        /// Servicio que Inserta o Modifica los valores de un Maestro de Plantilla 
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de plantilla a insertar o modificar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si el proceso se completa correctamente; false, en caso contrario.</returns>
        public bool InsertarOModificar(MaestroDePlantilla maestroPlantilla, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorMaestroDePlantilla.InsertarOModificar(maestroPlantilla, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
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
        }


        /// <summary>
        /// Servicio que obtiene los maestros de plantillas mediante una consulta por un filtro
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de Plantilla usado como filtro para la consulta</param>
        /// <returns>Lista de Maestros de Plantilla resultantes de la consulta</returns>
        public IList<MaestroDePlantilla> ObtenerMaestroDePlantillaFiltro(MaestroDePlantilla maestroPlantilla)
        {
            IList<MaestroDePlantilla> maestrosPlantilla;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                maestrosPlantilla = ControladorMaestroDePlantilla.ObtenerMaestroDePlantillaFiltro(maestroPlantilla);

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

            return maestrosPlantilla;
        }


        

        public MaestroDePlantilla ConsultarPorId(MaestroDePlantilla entidad)
        {
            throw new NotImplementedException();
        }


        public IList<MaestroDePlantilla> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }


        public bool Eliminar(MaestroDePlantilla entidad, int hash)
        {
            throw new NotImplementedException();
        }


        public bool VerificarExistencia(MaestroDePlantilla entidad)
        {
            throw new NotImplementedException();
        }




    }
}
