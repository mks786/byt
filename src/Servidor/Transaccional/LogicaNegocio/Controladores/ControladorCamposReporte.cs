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
    public class ControladorCamposReporte: ControladorBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para consultar todos los campos para los reportes de marcas y patentes
        /// En este metodo se recupera todo el contenido de la tabla
        /// </summary>
        /// <returns>Lista de todos los campos para los reportes de marcas y patentes</returns>
        public static IList<CamposReporte> ConsultarTodos()
        {
            IList<CamposReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CamposReporte>> comando = FabricaComandosCamposReporte.ObtenerComandoConsultarTodos();
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
        /// Metodo que obtiene los campos para el Reporte de Marca
        /// </summary>
        /// <returns>Lista de campos para el reporte de marca</returns>
        public static IList<CamposReporte> ObtenerCamposReporteDeMarca()
        {
            IList<CamposReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CamposReporte>> comando = FabricaComandosCamposReporte.ObtenerComandoObtenerCamposReporteDeMarca();
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
        /// Metodo que obtiene los campos para el Reporte de Patente
        /// </summary>
        /// <returns>Lista de campos para el reporte de patente</returns>
        public static IList<CamposReporte> ObtenerCamposReporteDePatente()
        {
            IList<CamposReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CamposReporte>> comando = FabricaComandosCamposReporte.ObtenerComandoObtenerCamposReporteDePatente();
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
