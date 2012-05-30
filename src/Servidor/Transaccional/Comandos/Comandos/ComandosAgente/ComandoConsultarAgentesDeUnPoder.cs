using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAgente
{
    public class ComandoConsultarAgentesDeUnPoder : ComandoBase<IList<Agente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Poder _poder;

        /// <summary>
        /// Metodo del Comando que consulta los Agentes pertenecientes a un poder
        /// </summary>
        /// <param name="poder">Poder que comparte con los agentes</param>
        public ComandoConsultarAgentesDeUnPoder(Poder poder)
        {
            this._poder = poder;
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

                IDaoAgente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAgente();
                this.Receptor = new Receptor<IList<Agente>>(dao.ObtenerAgentesDeUnPoder(this._poder));

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
