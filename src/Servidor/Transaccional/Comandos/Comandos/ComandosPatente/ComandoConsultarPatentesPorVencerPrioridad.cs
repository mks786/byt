using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPatente
{
    class ComandoConsultarPatentesPorVencerPrioridad : ComandoBase<IList<VencimientoPrioridadPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private int _cantidadDiasRecordatorio;

        /// <summary>
        /// Constructor predeterminado que recibe una cantidad de dias para el recordatorio
        /// </summary>
        /// <param name="cantidadDiasRecordatorio">Cantidad de dias usadas para el recordatorio</param>
        public ComandoConsultarPatentesPorVencerPrioridad(int cantidadDiasRecordatorio)
        {
            this._cantidadDiasRecordatorio = cantidadDiasRecordatorio;
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

                IDaoPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPatente();
                this.Receptor = 
                    new Receptor<IList<VencimientoPrioridadPatente>>(dao.ObtenerPatentesPorVencerPrioridad(this._cantidadDiasRecordatorio));

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
