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
    public class ControladorTipoCaja : ControladorBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// Método que obtiene todos los registros de la entidad TipoCaja
        /// </summary>
        /// <returns>Lista con todos los tipoCaja</returns>
        public static IList<TipoCaja> ConsultarTodos()
        {
            IList<TipoCaja> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<TipoCaja>> comando = FabricaComandosTipoCaja.ObtenerComandoConsultarTodos();
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
        /// Metodo que obtiene el tipo de caja del archivo ya sea por marca o por patente
        /// </summary>
        /// <param name="parametro1">Parametro que indica el tipo de caja</param>
        /// <returns>Lista de Tipos de Cajas de Archivo por Marca o por Patente</returns>
        public static IList<TipoCaja> ObtenerTipoCajaMarcaOPatente(String parametro1)
        {
            IList<TipoCaja> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<TipoCaja>> comando = FabricaComandosTipoCaja.ObtenerComandoObtenerTipoCajaMarcaOPatente(parametro1);
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
