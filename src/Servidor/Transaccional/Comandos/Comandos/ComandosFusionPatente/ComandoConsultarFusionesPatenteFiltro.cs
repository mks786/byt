using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFusionPatente
{
    class ComandoConsultarFusionesPatenteFiltro : ComandoBase<IList<FusionPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FusionPatente _fusion;


        /// <summary>
        /// Metodo comando que consulta las Fusiones de Patentes dado unos parametros
        /// </summary>
        /// <param name="fusion">FusionPatente con parametros a cosultar</param>
        public ComandoConsultarFusionesPatenteFiltro(FusionPatente fusion)
        {
            this._fusion = fusion;
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

                IDaoFusionPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFusionPatente();
                this.Receptor = new Receptor<IList<FusionPatente>>(dao.ObtenerFusionesPatenteFiltro(this._fusion));

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
