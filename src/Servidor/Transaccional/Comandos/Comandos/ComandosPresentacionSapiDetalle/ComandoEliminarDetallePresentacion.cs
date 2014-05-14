using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPresentacionSapiDetalle
{
    public class ComandoEliminarDetallePresentacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PresentacionSapiDetalle _detallePresentacion;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="detalleCompra"></param>
        public ComandoEliminarDetallePresentacion(PresentacionSapiDetalle detallePresentacion)
        {
            this._detallePresentacion = detallePresentacion;
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

                IDaoPresentacionSapiDetalle dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPresentacionSapiDetalle();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._detallePresentacion));

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
