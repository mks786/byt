using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPlanilla
{
    class ComandoConsultarPlanillaPorId : ComandoBase<Planilla>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private int _id;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="contador"></param>
        public ComandoConsultarPlanillaPorId(int id)
        {
            this._id = id;
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
                this.Receptor = new Receptor<Planilla>(dao.ObtenerPorId(this._id));

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
