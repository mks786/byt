using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCamposReporteRelacion
{
    public class ComandoEliminarCampoRelacion: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CamposReporteRelacion _campo;

        public ComandoEliminarCampoRelacion(CamposReporteRelacion campo)
        {
            this._campo = campo;
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

                //IDaoFechaMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFechaMarca();
                IDaoCamposReporteRelacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCamposReporteRelacion();
                dao.Eliminar(this._campo);

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
