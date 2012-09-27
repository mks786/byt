using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFusionMarcaTercero
{
    public class ComandoVerificarExistenciaFusionMarcaTercero : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FusionMarcaTercero _FusionMarcaTercero;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="FusionMarcaTercero">FusionMarcaTercero a verificar</param>
        public ComandoVerificarExistenciaFusionMarcaTercero(FusionMarcaTercero FusionMarcaTercero)
        {
            this._FusionMarcaTercero = FusionMarcaTercero;
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

                IDaoFusionMarcaTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFusionMarcaTercero();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._FusionMarcaTercero.Id));

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