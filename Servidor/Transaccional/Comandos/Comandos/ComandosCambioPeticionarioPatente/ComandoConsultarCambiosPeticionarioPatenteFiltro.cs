using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioPeticionarioPatente
{
    class ComandoConsultarCambiosPeticionarioPatenteFiltro : ComandoBase<IList<CambioPeticionarioPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioPeticionarioPatente _cambioPeticionario;


        /// <summary>
        /// Metodo Comando que consulta los CambioDePeticionarioDePatentes dado unos parametros
        /// </summary>
        /// <param name="cambioPeticionario">CambioDePeticionarioPatente con parametros a consultar</param>
        public ComandoConsultarCambiosPeticionarioPatenteFiltro(CambioPeticionarioPatente cambioPeticionario)
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

                IDaoCambioPeticionarioPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioPeticionarioPatente();
                this.Receptor = new Receptor<IList<CambioPeticionarioPatente>>(dao.ObtenerCambiosPeticionarioPatenteFiltro(this._cambioPeticionario));

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
