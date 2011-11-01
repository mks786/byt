using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosRemitente
{
    public class ComandoInsertarOModificarRemitente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Remitente _remitente;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="remitente">Remitente a insertar o modificar</param>
        public ComandoInsertarOModificarRemitente(Remitente remitente)
        {
            this._remitente = remitente;
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

                IDaoRemitente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoRemitente();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._remitente));

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
