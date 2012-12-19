using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosRenovacion
{
    public class ComandoVerificarExistenciaRenovacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Renovacion _renovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="renovacion">renovacion a verificar</param>
        public ComandoVerificarExistenciaRenovacion(Renovacion renovacion)
        {
            this._renovacion = renovacion;
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

                IDaoRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoRenovacion();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._renovacion.Id));

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