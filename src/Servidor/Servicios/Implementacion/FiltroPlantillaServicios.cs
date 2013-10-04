using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class FiltroPlantillaServicios: MarshalByRefObject, IFiltroPlantillaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que Consulta todos los filtros de plantilla que existen 
        /// </summary>
        /// <returns>Lista de todos los filtros de plantilla</returns>
        public IList<FiltroPlantilla> ConsultarTodos()
        {

            IList<FiltroPlantilla> filtros;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                filtros = ControladorFiltroPlantilla.ConsultarTodos();

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
            return filtros;
        }



        /// <summary>
        /// Servicio que consulta los filtros de encabezado de una plantilla especifica
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar</param>
        /// <returns>Lista de filtros de encabezado de una plantilla especifica</returns>
        public IList<FiltroPlantilla> ObtenerFiltrosEncabezadoPlantilla(MaestroDePlantilla plantilla)
        {
            IList<FiltroPlantilla> filtros;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                filtros = ControladorFiltroPlantilla.ConsultarFiltrosEncabezadoPlantilla(plantilla);

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
            return filtros;
        }


        /// <summary>
        /// Servicio que consulta los filtros del detalle de una plantilla determinada
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar sus filtros de detalle</param>
        /// <returns>Lista de filtros de detalle de la plantilla seleccionada</returns>
        public IList<FiltroPlantilla> ObtenerFiltrosDetallePlantilla(MaestroDePlantilla plantilla)
        {
            IList<FiltroPlantilla> filtros;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                filtros = ControladorFiltroPlantilla.ConsultarFiltrosDetallePlantilla(plantilla);

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
            return filtros;
        }



        public FiltroPlantilla ConsultarPorId(FiltroPlantilla entidad)
        {
            throw new NotImplementedException();
        }

        public IList<FiltroPlantilla> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que inserta o actualiza un filtro de una Plantilla
        /// </summary>
        /// <param name="entidad">Filtro de Plantilla a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si actualiza o insertar correctamente, falso en caso contrario</returns>
        public bool InsertarOModificar(FiltroPlantilla filtro, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorFiltroPlantilla.InsertarOModificar(filtro, hash);

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
        /// Servicio que borra de base de datos un filtro de una plantilla especifica
        /// </summary>
        /// <param name="filtro">Filtro a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; false en caso contrario</returns>
        public bool Eliminar(FiltroPlantilla filtro, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //bool exitoso = ControladorFechaMarca.Eliminar(fechaMarca, hash);
                bool exitoso = ControladorFiltroPlantilla.Eliminar(filtro, hash);

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

        public bool VerificarExistencia(FiltroPlantilla entidad)
        {
            throw new NotImplementedException();
        }


    }
}
