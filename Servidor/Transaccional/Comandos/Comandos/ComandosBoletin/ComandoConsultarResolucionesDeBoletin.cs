using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBoletin
{
    public class ComandoConsultarResolucionesDeBoletin : ComandoBase<IList<Resolucion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Boletin _boletin;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="boletin">Boletin a verificar</param>
        public ComandoConsultarResolucionesDeBoletin(Boletin boletin)
        {
            this._boletin = boletin;
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

                IDaoBoletin dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBoletin();
                this.Receptor = new Receptor<IList<Resolucion>>(dao.ObtenerResolucionesDeBoletin(this._boletin.Id));

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