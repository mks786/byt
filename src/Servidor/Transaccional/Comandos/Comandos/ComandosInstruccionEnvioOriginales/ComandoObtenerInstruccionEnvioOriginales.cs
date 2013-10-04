using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionEnvioOriginales
{
    class ComandoObtenerInstruccionEnvioOriginales : ComandoBase<InstruccionEnvioOriginales>
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InstruccionEnvioOriginales _instruccion;

        /// <summary>
        /// Constructor por defecto que recibe una instruccion a buscar
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales a buscar</param>
        public ComandoObtenerInstruccionEnvioOriginales(InstruccionEnvioOriginales instruccion)
        {
            this._instruccion = instruccion;
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

                IDaoInstruccionEnvioOriginales dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionEnvioOriginales();
                this.Receptor = new Receptor<InstruccionEnvioOriginales>(dao.ObtenerInstruccionDeEnvioDeOriginales(this._instruccion));

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
