using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCamposReporteRelacion
{
    class ComandoInsertarOModificarCamposReporte: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CamposReporteRelacion _campoReporte;

        /// <summary>
        /// Constructor predeterminado que recibe el Campo del Reporte de Marca
        /// </summary>
        /// <param name="campoReporteDeMarca">Campo del Reporte de Marca a insertar o actualizar</param>
        public ComandoInsertarOModificarCamposReporte(CamposReporteRelacion campoReporte)
        {
            this._campoReporte = campoReporte;
        }


        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //IDaoReporteDeMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoReporteDeMarca();
                IDaoCamposReporteRelacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCamposReporteRelacion();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._campoReporte));

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
        }
    }
}
