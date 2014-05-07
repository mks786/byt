using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCompraSapiDetalle
{
    class ComandoConsultarCompraSapiDetalleFiltro : ComandoBase<IList<CompraSapiDetalle>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CompraSapiDetalle _compraDetalle;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="compraDetalle"></param>
        public ComandoConsultarCompraSapiDetalleFiltro(CompraSapiDetalle compraDetalle)
        {
            this._compraDetalle = compraDetalle;
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

                IDaoCompraSapiDetalle dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCompraSapiDetalle();
                this.Receptor = new Receptor<IList<CompraSapiDetalle>>(dao.ObtenerCompraSapiDetalleFiltro(this._compraDetalle));

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
