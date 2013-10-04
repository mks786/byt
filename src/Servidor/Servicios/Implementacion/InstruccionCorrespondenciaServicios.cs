using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class InstruccionCorrespondenciaServicios : MarshalByRefObject, IInstruccionCorrespondenciaServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todos las Instrucciones De Correspondencia que tiene la tabla MYP_INSTR_EMAIL
        /// </summary>
        /// <returns>Lista de Instrucciones De Correspondencia</returns>
        public IList<InstruccionCorrespondencia> ConsultarTodos()
        {
            IList<InstruccionCorrespondencia> instrucciones;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instrucciones = ControladorInstruccionCorrespondencia.ConsultarTodos();

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
        /// Serivicio que inserta o modifica una Instruccion de Correspondencia
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public bool InsertarOModificar(InstruccionCorrespondencia instruccion, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorInstruccionCorrespondencia.InsertarOModificar(instruccion, hash);

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
        /// Servicio para obtener la instruccion de correspondencia tomando en cuenta el codigo de la Marca o Patente, 
        /// a que se aplica y el concepto de la instruccion
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia que sirve como filtro</param>
        /// <returns>Instruccion de Correspondencia buscada; en caso contrario retorna NULL</returns>
        public InstruccionCorrespondencia ObtenerInstruccionCorrespondencia(InstruccionCorrespondencia instruccion)
        {
            InstruccionCorrespondencia retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                retorno = ControladorInstruccionCorrespondencia.ObtenerInstruccionCorrespondencia(instruccion);

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

            return retorno;
        }

        
        public bool Eliminar(InstruccionCorrespondencia entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(InstruccionCorrespondencia entidad)
        {
            throw new NotImplementedException();
        }

        public IList<InstruccionCorrespondencia> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public InstruccionCorrespondencia ConsultarPorId(InstruccionCorrespondencia archivo)
        {
            throw new NotImplementedException();
        }
    }
}
