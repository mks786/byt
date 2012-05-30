using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCesionPatente
{
    class ComandoConsultarCesionesPatenteFiltro : ComandoBase<IList<CesionPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CesionPatente _cesion;


        /// <summary>
        /// Metodo Comando que consulta las CesionesDePatentes dado unos parametros
        /// </summary>
        /// <param name="cesion">Cesion con parametros a consultar</param>
        public ComandoConsultarCesionesPatenteFiltro(CesionPatente cesion)
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

                IDaoCesionPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCesionPatente();
                this.Receptor = new Receptor<IList<CesionPatente>>(dao.ObtenerCesionesPatenteFiltro(this._cesion));

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
