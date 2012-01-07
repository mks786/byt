using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosResolucion
{
    public class ComandoVerificarExistenciaResolucion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Resolucion _resolucion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="resolucion">Resolucion a verificar</param>
        public ComandoVerificarExistenciaResolucion(Resolucion resolucion)
        {
            this._resolucion = resolucion;
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

                IDaoResolucion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoResolucion();
                this.Receptor = new Receptor<bool>(dao.VerificarExistenciaResolucion(this._resolucion));

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