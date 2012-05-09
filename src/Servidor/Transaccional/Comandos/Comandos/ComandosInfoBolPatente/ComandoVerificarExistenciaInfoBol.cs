using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBolPatente
{
    public class ComandoVerificarExistenciaInfoBol : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private InfoBolPatente _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBolPatente a verificar</param>
        public ComandoVerificarExistenciaInfoBol(InfoBolPatente infoBol)
        {
            this._infoBol = infoBol;
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

                IDaoInfoBolPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBolPatente();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._infoBol.Id));

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