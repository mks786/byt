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
    public class ControladorCamposReporteRelacion: ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        
        /// <summary>
        /// Metodo que inserta o actualiza un Campo que pertenece al Reporte en la tabla que relaciona a dichas entidades
        /// </summary>
        /// <param name="campoReporteDeMarca">Campo del Reporte a insertar o actualizar</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; false en caso contrario</returns>
        public static bool InsertarOModificar(CamposReporteRelacion campoReporte, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCamposReporteRelacion.ObtenerComandoInsertarOModificar(campoReporte);
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
        /// Metodo que sirve para consultar los campos de un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte seleccionado</param>
        /// <returns>Lista de campos de un reporte especifico</returns>
        public static IList<CamposReporteRelacion> ConsultarCamposDeReporte(Reporte reporte)
        {

            IList<CamposReporteRelacion> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CamposReporteRelacion>> comando = 
                    FabricaComandosCamposReporteRelacion.ObtenerComandoConsultarCamposDeReporte(reporte);
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
        /// Metodo que borra los campos definidos para un Reporte seleccionado
        /// </summary>
        /// <param name="reporteDeMarca">Reporte seleccionado</param>
        /// <returns>True si el proceso se realiza exitosamente; False en caso contrario</returns>
        public static bool EliminarCamposReporteDeMarca(Reporte reporte)
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCamposReporteRelacion.ObtenerComandoEliminarCamposReporte(reporte);
                comando.Ejecutar();
                exitoso = true;

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
        /// Metodo para eliminar un Campo Relacion de un Reporte especifico
        /// </summary>
        /// <param name="campo">Campo Relacion a borrar</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; False en caso contrario</returns>
        public static bool Eliminar(CamposReporteRelacion campo)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCamposReporteRelacion.ObtenerComandoEliminarCampoRelacion(campo);
                comando.Ejecutar();
                exitoso = true;

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
