using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosStatusWeb
{
    public class ComandoEliminarStatusWeb : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private StatusWeb _status;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="status">StatusWeb a eliminar</param>
        public ComandoEliminarStatusWeb(StatusWeb status)
        {
            this._status = status;
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

                IDaoStatusWeb dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoStatusWeb();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._status));

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