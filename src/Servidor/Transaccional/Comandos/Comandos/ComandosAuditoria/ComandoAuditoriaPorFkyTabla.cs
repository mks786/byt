using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAuditoria
{
    public class ComandoAuditoriaPorFkyTabla : ComandoBase<IList<Auditoria>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Auditoria _auditoria;

        public ComandoAuditoriaPorFkyTabla(Auditoria auditoria)
        {
            this._auditoria = auditoria;
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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoAuditoria dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAuditoria();
                this.Receptor = new Receptor<IList<Auditoria>>(dao.AuditoriaPorFkYTabla(this._auditoria)); ;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
