using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInternacional
{
    class ComandoConsultarPorId : ComandoBase<Internacional>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

         private Internacional _internacional;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="internacional">Internacional a insertar o modificar</param>
         public ComandoConsultarPorId(Internacional internacional)
        {
            this._internacional = internacional;
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

                IDaoInternacional dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInternacional();
                this.Receptor = new Receptor<Internacional>(dao.ObtenerPorId(_internacional.Id));

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
