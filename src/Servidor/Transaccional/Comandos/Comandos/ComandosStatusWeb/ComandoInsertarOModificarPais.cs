using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosStatusWeb
{
    public class ComandoInsertarOModificarStatusWeb : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        StatusWeb _status;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario a insertar o modificar</param>
        public ComandoInsertarOModificarStatusWeb(StatusWeb status)
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._status));

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
