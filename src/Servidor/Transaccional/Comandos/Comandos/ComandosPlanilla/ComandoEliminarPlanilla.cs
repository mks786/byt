using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPlanilla
{
    public class ComandoEliminarPlanilla : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Planilla _planilla;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="planilla">Planilla a eliminar</param>
        public ComandoEliminarPlanilla(Planilla planilla)
        {
            this._planilla = planilla;
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

                IDaoPlanilla dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPlanilla();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._planilla));

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