using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCamposReporteRelacion
{
    class ComandoConsultarCamposDeReporte : ComandoBase<IList<CamposReporteRelacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Reporte _reporteDeMarca;

        /// <summary>
        /// Constructor predeterminado que recibe un Reporte de Marca
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        public ComandoConsultarCamposDeReporte(Reporte reporteDeMarca)
        {
            this._reporteDeMarca = reporteDeMarca;
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

                IDaoCamposReporteRelacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCamposReporteRelacion();
                this.Receptor = new Receptor<IList<CamposReporteRelacion>>(dao.ObtenerCamposDeReporte(this._reporteDeMarca));

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
