using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosLicenciaPatente
{
    class ComandoConsultarLicenciasPatenteFiltro : ComandoBase<IList<LicenciaPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private LicenciaPatente _licencia;


        public ComandoConsultarLicenciasPatenteFiltro(LicenciaPatente licencia)
        {
            this._licencia = licencia;
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

                IDaoLicenciaPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoLicenciaPatente();
                this.Receptor = new Receptor<IList<LicenciaPatente>>(dao.ObtenerLicenciasPatenteFiltro(this._licencia));

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
