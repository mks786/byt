using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilioPatente
{
    class ComandoConsultarCambiosDeDomicilioPatenteFiltro : ComandoBase<IList<CambioDeDomicilioPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeDomicilioPatente _cambioDeDomicilio;

        
        /// <summary>
        /// Metodo Comando que consulta los CambiosDeDomicilioPatentes por parametros definidos
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a consultar</param>
        public ComandoConsultarCambiosDeDomicilioPatenteFiltro(CambioDeDomicilioPatente cambioDeDomicilio)
        {
            this._cambioDeDomicilio = cambioDeDomicilio;
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

                IDaoCambioDeDomicilioPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeDomicilioPatente();
                this.Receptor = new Receptor<IList<CambioDeDomicilioPatente>>(dao.ObtenerCambiosDeDomicilioPatenteFiltro(this._cambioDeDomicilio));

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
