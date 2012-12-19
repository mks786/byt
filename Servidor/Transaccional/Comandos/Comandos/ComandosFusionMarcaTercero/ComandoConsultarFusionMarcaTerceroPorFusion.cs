using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFusionMarcaTercero
{
    public class ComandoConsultarFusionMarcaTerceroPorFusion : ComandoBase<FusionMarcaTercero>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Fusion _parametroEntrada;


        public ComandoConsultarFusionMarcaTerceroPorFusion(Fusion fusion)
        {
            this._parametroEntrada = fusion;
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

                IDaoFusionMarcaTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFusionMarcaTercero();
                this.Receptor = new Receptor<FusionMarcaTercero>(dao.FusionConsultarFusionMarcaTerceroPorFusion(this._parametroEntrada));

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
