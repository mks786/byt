using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class InstruccionOtrosServicios : MarshalByRefObject, IInstruccionOtrosServicios
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que consulta todos los registros de la tabla
        /// </summary>
        /// <returns>Lista de instrucciones no tipificadas</returns>
        public IList<InstruccionOtros> ConsultarTodos()
        {
            IList<InstruccionOtros> instrucciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instrucciones = ControladorInstruccionOtros.ConsultarTodos();

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
            return instrucciones;
        }


        /// <summary>
        /// Servicio que obtiene las instrucciones no tipificadas de una marca o de una patente segun el codigo 
        /// </summary>
        /// <param name="instruccionNoTipificada">Lista de instrucciones no tipificadas</param>
        /// <returns></returns>
        public IList<InstruccionOtros> ObtenerInstruccionesNoTipificadasPorFiltro(InstruccionOtros instruccionNoTipificada)
        {
            IList<InstruccionOtros> instrucciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instrucciones = ControladorInstruccionOtros.ObtenerInstruccionesNoTipificadasPorFiltro(instruccionNoTipificada);

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
            return instrucciones;
        }


        public IList<InstruccionOtros> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(InstruccionOtros entidad, int hash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Servicio que inserta o actualiza una instruccion no tipificada de marca o de patente
        /// </summary>
        /// <param name="instruccion">Instruccion no tipificada a actualizar o insertar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True en caso de realizarse correctamente; False en caso contrario</returns>
        public bool InsertarOModificar(InstruccionOtros instruccion, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorInstruccionOtros.InsertarOModificar(instruccion, hash);

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

        public bool VerificarExistencia(InstruccionOtros entidad)
        {
            throw new NotImplementedException();
        }


        public InstruccionOtros ConsultarPorId(InstruccionOtros archivo)
        {
            throw new NotImplementedException();
        }

    }
}
