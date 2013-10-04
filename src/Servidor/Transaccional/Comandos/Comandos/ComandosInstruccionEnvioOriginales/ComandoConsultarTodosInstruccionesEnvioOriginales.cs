using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionEnvioOriginales
{
    class ComandoConsultarTodosInstruccionesEnvioOriginales : ComandoBase<IList<InstruccionEnvioOriginales>>
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

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

                //IDaoArchivo dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoArchivo();
                IDaoInstruccionEnvioOriginales dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionEnvioOriginales();
                this.Receptor = new Receptor<IList<InstruccionEnvioOriginales>>(dao.ObtenerTodos());

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
