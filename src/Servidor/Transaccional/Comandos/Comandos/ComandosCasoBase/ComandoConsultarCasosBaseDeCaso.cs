using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCasoBase
{
    class ComandoConsultarCasosBaseDeCaso : ComandoBase<IList<CasoBase>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CasoBase _casoBase;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="casoBase">Caso Base usado como filtro</param>
        public ComandoConsultarCasosBaseDeCaso(CasoBase casoBase)
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
                this.Receptor = new Receptor<IList<CasoBase>>(dao.ObtenerCasosBaseDeCaso(this._casoBase));

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
