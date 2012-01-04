using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBol
{
    public class ComandoInsertarOModificarInfoBol : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InfoBol _infoBol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="infoBol">InfoBol a insertar o modificar</param>
        public ComandoInsertarOModificarInfoBol(InfoBol infoBol)
        {
            this._infoBol = infoBol;
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

                IDaoInfoBol dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBol();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._infoBol));

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
