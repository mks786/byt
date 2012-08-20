using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCesion
{
    public class ComandoInsertarOModificarCesion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Cesion _cesion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cesion">Cesion a insertar o modificar</param>
        public ComandoInsertarOModificarCesion(Cesion cesion)
        {
            this._cesion = cesion;
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

                IDaoCesion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCesion();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._cesion));

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
