using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioPeticionario
{
    class ComandoConsultarCambiosPeticionarioFiltro : ComandoBase<IList<CambioPeticionario>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioPeticionario _cambioPeticionario;


        /// <summary>
        /// Metodo Comando que Consulta los CambioDePeticionar dado unos parametros
        /// </summary>
        /// <param name="cambioPeticionario">Cambio Peticionario a consultar</param>
        public ComandoConsultarCambiosPeticionarioFiltro(CambioPeticionario cambioPeticionario)
        {
            this._cambioPeticionario = cambioPeticionario;
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

                IDaoCambioPeticionario dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioPeticionario();
                this.Receptor = new Receptor<IList<CambioPeticionario>>(dao.ObtenerCambiosPeticionarioFiltro(this._cambioPeticionario));

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
