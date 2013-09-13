using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFiltroReporte
{
    class ComandoConsultarFiltrosReporte : ComandoBase<IList<FiltroReporte>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Reporte _reporte;

        /// <summary>
        /// Constructor por defecto que recibe como parametro un Reporte de Marca
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        public ComandoConsultarFiltrosReporte(Reporte reporte)
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

                IDaoFiltroReporte dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFiltroReporte();
                this.Receptor = new Receptor<IList<FiltroReporte>>(dao.ObtenerFiltrosDeReporte(this._reporte));

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
