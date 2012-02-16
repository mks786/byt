using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInteresado
{
    public class ComandoConsultarInteresadosDeUnPoder : ComandoBase<Interesado>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Poder _poder;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="poder">InfoBol a verificar</param>
        public ComandoConsultarInteresadosDeUnPoder(Poder poder)
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

                IDaoInteresado dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInteresado();
                this.Receptor = new Receptor<Interesado>(dao.ObtenerInteresadosDeUnPoder(this._poder));

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