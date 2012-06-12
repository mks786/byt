using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPoder
{
    class ComandoConsultarPoderesEntreAgentesEInteresado : ComandoBase<IList<Poder>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Agente _agente;
        private Interesado _interesado;


        public ComandoConsultarPoderesEntreAgentesEInteresado(Agente agente, Interesado interesado)
        {
            this._interesado = interesado;
            this._agente =  agente;
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

                IDaoPoder dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPoder();
                this.Receptor = new Receptor<IList<Poder>>(dao.ObtenerPoderesEntreAgenteEInteresado(_agente, _interesado));

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
