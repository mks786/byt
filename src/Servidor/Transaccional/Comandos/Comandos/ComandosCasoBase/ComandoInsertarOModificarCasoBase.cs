using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCasoBase
{
    public class ComandoInsertarOModificarCasoBase : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CasoBase _casoBase;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="casoBase"></param>
        public ComandoInsertarOModificarCasoBase(CasoBase casoBase)
        {
            this._casoBase = casoBase;
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

                IDaoCasoBase dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCasoBase();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._casoBase));

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
