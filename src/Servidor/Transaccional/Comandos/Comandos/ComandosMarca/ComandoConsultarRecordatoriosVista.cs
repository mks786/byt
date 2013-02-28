using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarca
{
    class ComandoConsultarRecordatoriosVista : ComandoBase<IList<RecordatorioVista>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private RecordatorioVista _recordatorio;
        private DateTime[] _fechas;
        private string _localidad;


        /// <summary>
        /// Metodo Comando que consulta los recordatorios de marcas
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio con parametros</param>
        /// /// <param name="Fechas">fecha de renovación de marcas a filtrar</param>
        public ComandoConsultarRecordatoriosVista(RecordatorioVista recordatorio, DateTime[] fechas, string localidad)
        {
            this._recordatorio = recordatorio;
            this._fechas = fechas;
            this._localidad = localidad;
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

                IDaoMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarca();
                this.Receptor = new Receptor<IList<RecordatorioVista>>(dao.ObtenerRecordatoriosVista(this._recordatorio, this._fechas, this._localidad));

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
