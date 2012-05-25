using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAnualidad
{
    class ComandoConsultarAnualidadesPorPatente : ComandoBase<IList<Anualidad>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;


        public ComandoConsultarAnualidadesPorPatente(Patente patente)
        {
            this._patente = patente;
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

                IDaoAnualidad dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAnualidad();
                this.Receptor = new Receptor<IList<Anualidad>>(dao.ObtenerAnualidadesPorPatente(this._patente.Id));

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
