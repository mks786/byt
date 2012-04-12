using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilioPatente
{
    public class ComandoEliminarCambioDeDomicilioPatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeDomicilioPatente _cambioDeDomicilio;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilioPatente a eliminar</param>
        public ComandoEliminarCambioDeDomicilioPatente(CambioDeDomicilioPatente cambioDeDomicilio)
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
                 this.Receptor = new Receptor<bool>(dao.Eliminar(this._cambioDeDomicilio));

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