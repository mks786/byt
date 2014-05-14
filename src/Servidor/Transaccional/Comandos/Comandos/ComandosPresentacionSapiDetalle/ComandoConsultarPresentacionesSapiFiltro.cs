using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPresentacionSapiDetalle
{
    class ComandoConsultarPresentacionesSapiFiltro : ComandoBase<IList<PresentacionSapiDetalle>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PresentacionSapiDetalle _presentacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="presentacion"></param>
        public ComandoConsultarPresentacionesSapiFiltro(PresentacionSapiDetalle presentacion)
        {
            this._presentacion = presentacion;
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

                IDaoPresentacionSapiDetalle dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPresentacionSapiDetalle();
                this.Receptor = new Receptor<IList<PresentacionSapiDetalle>>(dao.ObtenerPresentacionesSapiDetalleFiltro(this._presentacion));

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
