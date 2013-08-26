using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFiltroPlantilla
{
    class ComandoConsultarFiltrosEncabezadoPlantilla: ComandoBase<IList<FiltroPlantilla>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Plantilla _plantilla;

        /// <summary>
        /// Constructor por defecto que recibe la plantilla a consultar
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar</param>
        public ComandoConsultarFiltrosEncabezadoPlantilla(Plantilla plantilla)
        {
            this._plantilla = plantilla;
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

                //IDaoFechaMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFechaMarca();
                IDaoFiltroPlantilla dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFiltroPlantilla();
                //this.Receptor = new Receptor<IList<FechaMarca>>(dao.ObtenerFechasPorMarca(this._marca));
                this.Receptor = new Receptor<IList<FiltroPlantilla>>(dao.ObtenerFiltrosEncabezadoPlantilla(this._plantilla));

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
