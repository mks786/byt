﻿using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMaestroDePlantilla
{
    class ComandoObtenerMaestrosDePlantillaFiltro : ComandoBase<IList<MaestroDePlantilla>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MaestroDePlantilla _maestroPlantilla;


        public ComandoObtenerMaestrosDePlantillaFiltro(MaestroDePlantilla maestroPlantilla)
        {
            this._maestroPlantilla = maestroPlantilla;
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

                IDaoMaestroDePlantilla dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMaestroDePlantilla();
                this.Receptor = new Receptor<IList<MaestroDePlantilla>>(dao.ObtenerMaestroPlantillaFiltro(this._maestroPlantilla));

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
