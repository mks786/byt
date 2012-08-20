using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosEntradaAlterna
{
    public class ComandoEliminarEntradaAlterna : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private EntradaAlterna _entradaAlterna;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna a eliminar</param>
        public ComandoEliminarEntradaAlterna(EntradaAlterna entradaAlterna)
        {
            this._entradaAlterna = entradaAlterna;
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

                IDaoEntradaAlterna dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoEntradaAlterna();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._entradaAlterna));

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