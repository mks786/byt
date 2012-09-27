using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoInfobol
{
    public class ComandoInsertarOModificarTipoInfobol : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TipoInfobol _tipoInfobol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="tipoInfobol">TipoInfobol a insertar o modificar</param>
        public ComandoInsertarOModificarTipoInfobol(TipoInfobol tipoInfobol)
        {
            this._tipoInfobol = tipoInfobol;
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

                IDaoTipoInfobol dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoTipoInfobol();
                dao.InsertarOModificar(this._tipoInfobol);

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
