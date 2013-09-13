using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosReporte
{
    class ComandoObtenerReporteFiltro : ComandoBase<IList<Reporte>>
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        Reporte _reporte;

        /// <summary>
        /// Constructor que recibe un Reporte filtro
        /// </summary>
        /// <param name="reporte">Reporte filtro</param>
        public ComandoObtenerReporteFiltro(Reporte reporte)
        {
            this._reporte = reporte;
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

                IDaoReporte dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoReporte();
                this.Receptor = new Receptor<IList<Reporte>>(dao.ObtenerReporteFiltro(this._reporte));

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
