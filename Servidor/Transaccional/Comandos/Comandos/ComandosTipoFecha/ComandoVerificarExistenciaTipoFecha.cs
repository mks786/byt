using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoFecha
{
    public class ComandoVerificarExistenciaTipoFecha : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TipoFecha _tipoFecha;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="tipoFecha">TipoFecha a verificar</param>
        public ComandoVerificarExistenciaTipoFecha(TipoFecha tipoFecha)
        {
            this._tipoFecha = tipoFecha;
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

                IDaoTipoFecha dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoTipoFecha();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._tipoFecha.Id));

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