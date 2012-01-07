using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoAdicional
{
    public class ComandoInsertarOModificarInfoAdicional : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InfoAdicional _infoAdicional;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional a insertar o modificar</param>
        public ComandoInsertarOModificarInfoAdicional(InfoAdicional infoAdicional)
        {
            this._infoAdicional = infoAdicional;
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

                IDaoInfoAdicional dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoAdicional();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._infoAdicional));

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
