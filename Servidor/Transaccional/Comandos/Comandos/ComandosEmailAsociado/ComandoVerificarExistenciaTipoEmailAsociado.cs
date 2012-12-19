using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosEmailAsociado
{
    public class ComandoVerificarExistenciaEmailAsociado : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private EmailAsociado _EmailAsociado;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="EmailAsociado">EmailAsociado a consultar</param>
        public ComandoVerificarExistenciaEmailAsociado(EmailAsociado EmailAsociado)
        {
            this._EmailAsociado = EmailAsociado;
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

                IDaoEmailAsociado dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoEmailAsociado();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._EmailAsociado.Id));

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