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
    public class ControladorInstruccionOtros : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene todas las instrucciones no tipificadas de la tabla
        /// </summary>
        /// <returns>Lista de instrucciones no tipificadas</returns>
        public static IList<InstruccionOtros> ConsultarTodos()
        {
            IList<InstruccionOtros> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InstruccionOtros>> comando = FabricaComandosInstruccionOtros.ObtenerComandoConsultarTodos();
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
        /// Metodo que obtiene las instrucciones no tipificadas de una marca o de una patente segun el codigo 
        /// </summary>
        /// <param name="instruccionNoTipificada">Lista de instrucciones no tipificadas de una marca o de una patente</param>
        /// <returns></returns>
        public static IList<InstruccionOtros> ObtenerInstruccionesNoTipificadasPorFiltro(InstruccionOtros instruccionNoTipificada)
        {
            IList<InstruccionOtros> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<InstruccionOtros>> comando = 
                    FabricaComandosInstruccionOtros.ObtenerComandoConsultarInstruccionesNoTipificadasPorFiltro(instruccionNoTipificada);

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
        /// Metodo que inserta o actualiza una instruccion no tipificada de marca o de patente
        /// </summary>
        /// <param name="instruccion">Instruccion no tipificada a actualizar o insertar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True en caso de realizarse correctamente; False en caso contrario</returns>
        public static bool InsertarOModificar(InstruccionOtros instruccion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosInstruccionOtros.ObtenerComandoInsertarOModificar(instruccion);
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
    }
}
