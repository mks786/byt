using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoEmailAsociado
{
    public class ComandoVerificarExistenciaTipoEmailAsociado : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TipoEmailAsociado _TipoEmailAsociado;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="TipoEmailAsociado">TipoEmailAsociado a consultar</param>
        public ComandoVerificarExistenciaTipoEmailAsociado(TipoEmailAsociado TipoEmailAsociado)
        {
            this._TipoEmailAsociado = TipoEmailAsociado;
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

                IDaoTipoEmailAsociado dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoTipoEmailAsociado();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._TipoEmailAsociado.Id));

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