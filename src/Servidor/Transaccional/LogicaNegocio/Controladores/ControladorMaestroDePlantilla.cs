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
    public class ControladorMaestroDePlantilla : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta todos los registros que se encuentran en la tabla ENV_MAESTRO_PLANT
        /// </summary>
        /// <returns>Lista de todos los maestros de plantilla que se encuentran en la tabla</returns>
        public static IList<MaestroDePlantilla> ConsultarTodos()
        {
            IList<MaestroDePlantilla> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<MaestroDePlantilla>> comando = FabricaComandosMaestroDePlantilla.ObtenerComandoConsultarTodos();
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
        /// Metodo para insertar o modificar un maestro de plantilla en base de datos
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de PLantilla a insertar y/o modificar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; false en caso contrario.</returns>
        public static bool InsertarOModificar(MaestroDePlantilla maestroPlantilla, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosMaestroDePlantilla.ObtenerComandoInsertarOModificar(maestroPlantilla);
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
        /// Metodo que obtiene maestros de plantilla a partir de un maestro de plantilla como filtro
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de Plantilla usado como filtro</param>
        /// <returns>Lista de Maestros de Plantilla resultantes de la consulta</returns>
        public static IList<MaestroDePlantilla> ObtenerMaestroDePlantillaFiltro(MaestroDePlantilla maestroPlantilla)
        {
            IList<MaestroDePlantilla> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<MaestroDePlantilla>> 
                    comando = FabricaComandosMaestroDePlantilla.ObtenerComandoObtenerMaestroDePlantillaFiltro(maestroPlantilla);

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
