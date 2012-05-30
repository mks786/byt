using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInventor
{
    public class ComandoVerificarExistenciaInventor : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Inventor _inventor;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="inventor">inventor a verificar</param>
        public ComandoVerificarExistenciaInventor(Inventor inventor)
        {
            this._inventor = inventor;
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

                IDaoInventor dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInventor();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._inventor.Id));

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