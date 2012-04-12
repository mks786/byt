using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioPeticionarioPatente
{
    public class ComandoVerificarExistenciaCambioPeticionarioPatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioPeticionarioPatente _cambioPeticionario;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a verificar</param>
        public ComandoVerificarExistenciaCambioPeticionarioPatente(CambioPeticionarioPatente cambioPeticionario)
        {
            this._cambioPeticionario = cambioPeticionario;
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

                IDaoCambioPeticionarioPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioPeticionarioPatente();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._cambioPeticionario.Id));

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