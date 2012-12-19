using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMemoria
{
    public class ComandoInsertarOModificarMemoria : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Memoria _memoria;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="memoria">memoria a insertar o modificar</param>
        public ComandoInsertarOModificarMemoria(Memoria memoria)
        {
            this._memoria = memoria;
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

                IDaoMemoria dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMemoria();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._memoria));

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
