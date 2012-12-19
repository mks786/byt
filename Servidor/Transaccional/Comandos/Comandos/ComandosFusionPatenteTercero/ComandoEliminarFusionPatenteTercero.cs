using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFusionPatenteTercero
{
    public class ComandoEliminarFusionPatenteTercero : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FusionPatenteTercero _FusionPatenteTercero;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="FusionPatenteTercero">FusionPatenteTercero a eliminar</param>
        public ComandoEliminarFusionPatenteTercero(FusionPatenteTercero FusionPatenteTercero)
        {
            this._FusionPatenteTercero = FusionPatenteTercero;
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

                IDaoFusionPatenteTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFusionPatenteTercero();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._FusionPatenteTercero));

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