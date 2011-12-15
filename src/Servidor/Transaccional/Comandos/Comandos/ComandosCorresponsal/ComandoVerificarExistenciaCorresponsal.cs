using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCorresponsal
{
    public class ComandoVerificarExistenciaCorresponsal : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Corresponsal _corresponsal;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="Correspnsal">Corresponsal a verificar</param>
        public ComandoVerificarExistenciaCorresponsal(Corresponsal Correspnsal)
        {
            this._corresponsal = Correspnsal;
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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoCorresponsal dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCorresponsal();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._corresponsal.Id));

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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