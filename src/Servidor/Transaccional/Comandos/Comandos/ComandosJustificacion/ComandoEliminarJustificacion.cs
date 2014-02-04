using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosJustificacion
{
    public class ComandoEliminarJustificacion: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Justificacion _justificacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="justificacion">Justificacion a eliminar</param>
        public ComandoEliminarJustificacion(Justificacion justificacion)
        {
            this._justificacion = justificacion;
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

                IDaoJustificacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoJustificacion();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._justificacion));

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
