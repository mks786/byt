using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionOtros
{
    class ComandoConsultarInstruccionesNoTipificadasPorCodigo : ComandoBase<IList<InstruccionOtros>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private InstruccionOtros _instruccionNoTipificada;


        public ComandoConsultarInstruccionesNoTipificadasPorCodigo(InstruccionOtros instruccionNoTipificada)
        {
            this._instruccionNoTipificada = instruccionNoTipificada;
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

                IDaoInstruccionOtros dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionOtros();
                this.Receptor = new Receptor<IList<InstruccionOtros>>(dao.ObtenerInstruccionesNoTipificadasPorCodigo(this._instruccionNoTipificada));

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
