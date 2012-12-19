using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFusionPatente
{
    public class ComandoVerificarExistenciaFusionPatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FusionPatente _fusion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="fusion">FusionPatente a verificar</param>
        public ComandoVerificarExistenciaFusionPatente(FusionPatente fusion)
        {
            this._fusion = fusion;
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

                IDaoFusionPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFusionPatente();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._fusion.Id));

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