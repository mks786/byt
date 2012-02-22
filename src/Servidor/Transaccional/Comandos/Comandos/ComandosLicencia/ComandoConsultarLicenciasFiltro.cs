using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosLicencia
{
    class ComandoConsultarLicenciasFiltro : ComandoBase<IList<Licencia>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Licencia _licencia;


        public ComandoConsultarLicenciasFiltro(Licencia licencia)
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

                IDaoLicencia dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoLicencia();
                this.Receptor = new Receptor<IList<Licencia>>(dao.ObtenerLicenciasFiltro(this._licencia));

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
