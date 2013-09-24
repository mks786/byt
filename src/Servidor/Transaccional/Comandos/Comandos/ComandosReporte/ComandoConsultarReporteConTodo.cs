using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosReporte
{
    class ComandoConsultarReporteConTodo: ComandoBase<Reporte>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Reporte _reporte;

        /// <summary>
        /// Constructor por defecto que recibe un Reporte del Cliente
        /// </summary>
        /// <param name="reporte">Reporte a consultar</param>
        public ComandoConsultarReporteConTodo(Reporte reporte)
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
                this.Receptor = new Receptor<Reporte>(dao.ObtenerReporteConTodo(this._reporte));

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
