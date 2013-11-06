using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCamposReporte
{
    class ComandoObtenerCamposPorVista : ComandoBase<IList<CamposReporte>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        String _nombreVista;

        /// <summary>
        /// Constructor por defecto que recibe el nombre de la vista a extraer los campos
        /// </summary>
        /// <param name="nombreVista">Nombre de la vista a consultar</param>
        public ComandoObtenerCamposPorVista(String nombreVista)
        {
            this._nombreVista = nombreVista;
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

                IDaoCamposReporte dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCamposReporte();
                this.Receptor = new Receptor<IList<CamposReporte>>(dao.ObtenerCamposPorVista(this._nombreVista));

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
