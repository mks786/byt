using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCaso
{
    public class ComandoConsultarCasosFiltro : ComandoBase<IList<Caso>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Caso _caso;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="caso"></param>
        public ComandoConsultarCasosFiltro(Caso caso)
        {
            this._caso = caso;
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

                IDaoCaso dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCaso();
                this.Receptor = new Receptor<IList<Caso>>(dao.ObtenerCasosFiltro(this._caso));

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
