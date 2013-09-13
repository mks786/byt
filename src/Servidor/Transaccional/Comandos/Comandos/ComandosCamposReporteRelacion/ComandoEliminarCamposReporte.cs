using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCamposReporteRelacion
{
    public class ComandoEliminarCamposReporte : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Reporte _reporteDeMarca;

        /// <summary>
        /// Constructor por defecto que recibe un Reporte de Marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        public ComandoEliminarCamposReporte(Reporte reporteDeMarca)
        {
            this._reporteDeMarca = reporteDeMarca;
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

                IDaoCamposReporteRelacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCamposReporteRelacion();
                this.Receptor = new Receptor<bool>(dao.EliminarCamposReporte(this._reporteDeMarca));

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
