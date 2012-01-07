using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCartaOut
{
    class ComandoConsultarCartasOutsFiltro : ComandoBase<IList<CartaOut>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CartaOut _carta;


        public ComandoConsultarCartasOutsFiltro(CartaOut carta)
        {
            this._carta = carta;
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

                IDaoCartaOut dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCartaOut();
                this.Receptor = new Receptor<IList<CartaOut>>(dao.ObtenerCartasOutsFiltro(this._carta));

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
