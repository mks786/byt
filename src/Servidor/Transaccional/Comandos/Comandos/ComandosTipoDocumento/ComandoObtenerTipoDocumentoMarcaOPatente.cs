using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoDocumento
{
    class ComandoObtenerTipoDocumentoMarcaOPatente : ComandoBase<IList<TipoDocumento>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        private string _parametroNacional;
        private string _parametroInternacional;

        public ComandoObtenerTipoDocumentoMarcaOPatente(String parametro1, String parametro2)
        {
            this._parametroNacional = parametro1;
            this._parametroInternacional = parametro2;
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

                IDaoTipoDocumento dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoTipoDocumento();
                this.Receptor = new Receptor<IList<TipoDocumento>>(dao.ObtenerTiposDocumentosMarcaOPatente(this._parametroNacional, this._parametroInternacional));

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
