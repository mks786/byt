using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class InstruccionEnvioOriginalesServicios : MarshalByRefObject, IInstruccionEnvioOriginalesServicios
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que obtiene todos las Instrucciones De Envio de Originales que tiene la tabla MYP_INSTR_EORIGINAL
        /// </summary>
        /// <returns>Lista de Instrucciones de Envios de Originales</returns>
        public IList<InstruccionEnvioOriginales> ConsultarTodos()
        {
            IList<InstruccionEnvioOriginales> instruccionesEnvioOriginales;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                instruccionesEnvioOriginales = ControladorInstruccionEnvioOriginales.ConsultarTodos();

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
            return instruccionesEnvioOriginales;
        }


        /// <summary>
        /// Serivicio que inserta o modifica una Instruccion de Envio de Originales
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public bool InsertarOModificar(InstruccionEnvioOriginales instruccion, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorInstruccionEnvioOriginales.InsertarOModificar(instruccion, hash);

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
        /// Servicio que obtiene la instruccion de Envio de Originales de una marca o una patente filtrado por: Codigo de la 
        /// marca o la patente, a que aplica (M de Marca o P de Patente) y que concepto posee (C de Correspondencia o F de Facturacion)
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales filtro</param>
        /// <returns>Instruccion de Envio de Originales; en caso contrario retorna NULL</returns>
        public InstruccionEnvioOriginales ObtenerInstruccionEnvioOriginales(InstruccionEnvioOriginales instruccion)
        {
            InstruccionEnvioOriginales retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                retorno = ControladorInstruccionEnvioOriginales.ObtenerInstruccionEnvioOriginales(instruccion);

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






        public IList<InstruccionEnvioOriginales> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(InstruccionEnvioOriginales entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(InstruccionEnvioOriginales entidad)
        {
            throw new NotImplementedException();
        }


        public InstruccionEnvioOriginales ConsultarPorId(InstruccionEnvioOriginales archivo)
        {
            throw new NotImplementedException();
        }

    }
}
