using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAgente
{
    class ComandoConsultarAgentesSinPoderesFiltro : ComandoBase<IList<Agente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Agente _agente;


        /// <summary>
        /// Metodo Comando que Consulta todoos los agentes sin poderes dado unos parametros
        /// </summary>
        /// <param name="agente">agente con parametros a consultar</param>
        public ComandoConsultarAgentesSinPoderesFiltro(Agente agente)
        {
            this._agente = agente;
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
                this.Receptor = new Receptor<IList<Agente>>(dao.ObtenerAgentesSinPoderesFiltro(this._agente));

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
