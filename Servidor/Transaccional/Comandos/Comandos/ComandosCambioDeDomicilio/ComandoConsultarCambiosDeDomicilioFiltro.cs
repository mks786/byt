using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilio
{
    class ComandoConsultarCambiosDeDomicilioFiltro : ComandoBase<IList<CambioDeDomicilio>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeDomicilio _cambioDeDomicilio;


        /// <summary>
        /// Metodo Comando que consulta los Cambios De Domicilio dado unos parametros
        /// </summary>
        /// <param name="cambioDeDomicilio">Cambio de Domicilio a consultar</param>
        public ComandoConsultarCambiosDeDomicilioFiltro(CambioDeDomicilio cambioDeDomicilio)
        {
            this._cambioDeDomicilio = cambioDeDomicilio;
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

                IDaoCambioDeDomicilio dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeDomicilio();
                this.Receptor = new Receptor<IList<CambioDeDomicilio>>(dao.ObtenerCambiosDeDomicilioFiltro(this._cambioDeDomicilio));

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
