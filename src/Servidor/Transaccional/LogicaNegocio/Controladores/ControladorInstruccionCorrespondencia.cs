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
    public class ControladorInstruccionCorrespondencia : ControladorBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Metodo que obtiene todas las Instrucciones De Correspondencia que tiene la tabla MYP_INSTR_EMAIL
        /// </summary>
        /// <returns>Lista de Instrucciones De Correspondencia</returns>
        public static IList<InstruccionCorrespondencia> ConsultarTodos()
        {
            IList<InstruccionCorrespondencia> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InstruccionCorrespondencia>> comando = FabricaComandosInstruccionCorrespondencia.ObtenerComandoConsultarTodos();
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
        /// Metodo que inserta o modifica una Instruccion de Correspondencia
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public static bool InsertarOModificar(InstruccionCorrespondencia instruccion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInstruccionCorrespondencia.ObtenerComandoInsertarOModificar(instruccion);
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
        /// Metodo para obtener la instruccion de correspondencia tomando en cuenta el codigo de la Marca o Patente, 
        /// a que se aplica y el concepto de la instruccion
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a consultar</param>
        /// <returns>Instruccion de Correspondencia de la marca o la patente segun la aplicacion y el concepto; NULL en caso contrario</returns>
        public static InstruccionCorrespondencia ObtenerInstruccionCorrespondencia(InstruccionCorrespondencia instruccion)
        {
            InstruccionCorrespondencia retorno = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<InstruccionCorrespondencia> comando = FabricaComandosInstruccionCorrespondencia.ObtenerComandoObtenerInstruccionCorrespondencia(instruccion);
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
