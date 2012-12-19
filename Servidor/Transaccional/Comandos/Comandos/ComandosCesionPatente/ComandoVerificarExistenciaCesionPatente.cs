using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCesionPatente
{
    public class ComandoVerificarExistenciaCesionPatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CesionPatente _cesion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cesion">CesionPatente a verificar</param>
        public ComandoVerificarExistenciaCesionPatente(CesionPatente cesion)
        {
            this._cesion = cesion;
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

                IDaoCesionPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCesionPatente();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._cesion.Id));

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