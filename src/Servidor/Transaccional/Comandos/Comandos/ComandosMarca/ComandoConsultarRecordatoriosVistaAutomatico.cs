using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarca
{
    class ComandoConsultarRecordatoriosVistaNoAutomatico : ComandoBase<IList<RecordatorioVista>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private RecordatorioVista _recordatorio;
        private DateTime?[] _fechas;
        private string _ano;
        private string _mes;
        


        /// <summary>
        /// Metodo Comando que consulta los recordatorios de marcas
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio a consultar</param>
        /// <param name="ano">Ano de fecha renovacion a filtrar</param>
        /// <param name="mes">mes de fecha renovación a filtrar</param>
        /// <param name="fechas">fecha desde y hasta de renovación a filtrar</param>
        public ComandoConsultarRecordatoriosVistaNoAutomatico(RecordatorioVista recordatorio, string ano, string mes, DateTime?[] fechas)
        {
            this._recordatorio = recordatorio;
            this._fechas = fechas;
            this._ano = ano;
            this._mes = mes;
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
                this.Receptor = new Receptor<IList<RecordatorioVista>>(dao.ObtenerRecordatoriosVistaNoAutomatico(this._recordatorio, this._ano, this._mes, this._fechas));

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
