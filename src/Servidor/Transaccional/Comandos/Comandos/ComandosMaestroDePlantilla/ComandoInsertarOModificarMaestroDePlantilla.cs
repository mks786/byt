using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMaestroDePlantilla
{
    class ComandoInsertarOModificarMaestroDePlantilla: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        MaestroDePlantilla _maestroPlantilla;

        /// <summary>
        /// Constructor predeterminado que recibe un Maestro de Plantilla a insertar o modificar
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de Plantilla para insertar o modificar</param>
        public ComandoInsertarOModificarMaestroDePlantilla(MaestroDePlantilla maestroPlantilla)
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._maestroPlantilla));

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
