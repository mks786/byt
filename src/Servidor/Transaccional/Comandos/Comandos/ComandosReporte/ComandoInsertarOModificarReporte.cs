using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Comandos.Comandos.ComandosReporte
{
    class ComandoInsertarOModificarReporte: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Reporte _reporte;

        /// <summary>
        /// Constructor predeterminado que recibe un Reporte de Marca par a insertar o modificar
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca a insertar o actualizar</param>
        public ComandoInsertarOModificarReporte(Reporte reporte)
        {
            this._reporte = reporte;
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

                IDaoReporte dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoReporte();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._reporte));

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
