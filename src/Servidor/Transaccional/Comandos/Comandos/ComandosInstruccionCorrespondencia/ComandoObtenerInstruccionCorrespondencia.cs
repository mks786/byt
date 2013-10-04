using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionCorrespondencia
{
    class ComandoObtenerInstruccionCorrespondencia : ComandoBase<InstruccionCorrespondencia>
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InstruccionCorrespondencia _instruccion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia a obtener</param>
        public ComandoObtenerInstruccionCorrespondencia(InstruccionCorrespondencia instruccion)
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

                IDaoInstruccionCorrespondencia dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionCorrespondencia();
                this.Receptor = new Receptor<InstruccionCorrespondencia>(dao.ObtenerInstruccionDeCorrespondencia(this._instruccion));

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
