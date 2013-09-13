using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFiltroReporte
{
    class ComandoInsertarOModificarFiltroReporte : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        FiltroReporte _filtroReporte;

        /// <summary>
        /// Contructor predeterminado que recibe un filtro de reporte de marca
        /// </summary>
        /// <param name="filtroReporteDeMarca">Filtro de reporte de marca a insertar o actualizar</param>
        public ComandoInsertarOModificarFiltroReporte(FiltroReporte filtroReporte)
        {
            this._filtroReporte = filtroReporte;
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._filtroReporte));

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
