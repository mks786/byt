using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorInstruccionEnvioOriginales : ControladorBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que obtiene todas las Instrucciones De Envio de Originales que tiene la tabla MYP_INSTR_EORIGINAL
        /// </summary>
        /// <returns>Lista de Instrucciones de Envios de Originales</returns>
        public static IList<InstruccionEnvioOriginales> ConsultarTodos()
        {
            IList<InstruccionEnvioOriginales> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InstruccionEnvioOriginales>> comando = FabricaComandosInstruccionEnvioOriginales.ObtenerComandoConsultarTodos();
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }


        /// <summary>
        /// Metodo que inserta o modifica una Instruccion de Envio de Originales
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public static bool InsertarOModificar(InstruccionEnvioOriginales instruccion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInstruccionEnvioOriginales.ObtenerComandoInsertarOModificar(instruccion);
                comando.Ejecutar();
                exitoso = comando.Receptor.ObjetoAlmacenado;

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
            return exitoso;
        }



        /// <summary>
        /// Metodo que obtiene la instruccion de Envio de Originales de una marca o una patente filtrado por: Codigo de la 
        /// marca o la patente, a que aplica (M de Marca o P de Patente) y que concepto posee (C de Correspondencia o F de Facturacion)
        /// </summary>
        /// <param name="instruccion">Instruccion Envio de Originales filtro</param>
        /// <returns>Retorna una entidad InstruccionEnvioOriginales; en caso contrario retorna NULL</returns>
        public static InstruccionEnvioOriginales ObtenerInstruccionEnvioOriginales(InstruccionEnvioOriginales instruccion)
        {
            InstruccionEnvioOriginales retorno = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<InstruccionEnvioOriginales> comando = FabricaComandosInstruccionEnvioOriginales.ObtenerComandoObtenerInstruccionEnvioOriginales(instruccion);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

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

            return retorno;
        }
    }
}
