using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContadorAuditoria
{
    class ComandoConsultarPorIdContadorAuditoria: ComandoBase<ContadorAuditoria>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string _id;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="id"></param>
        public ComandoConsultarPorIdContadorAuditoria(string id)
        {
            this._id = id;
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

                IDaoContadorAuditoria dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContadorAuditoria();
                this.Receptor = new Receptor<ContadorAuditoria>(dao.ObtenerPorId(this._id));

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
