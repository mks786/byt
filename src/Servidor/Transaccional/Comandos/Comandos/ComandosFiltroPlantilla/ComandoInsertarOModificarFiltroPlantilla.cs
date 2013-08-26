using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFiltroPlantilla
{
    public class ComandoInsertarOModificarFiltroPlantilla : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        FiltroPlantilla _filtro;

        /// <summary>
        /// Constructor predeterminado que recibe un filtro de plantilla para insertar y/o actualizar
        /// </summary>
        /// <param name="fechaMarca">Fecha de marca a insertar o actualizar</param>
        public ComandoInsertarOModificarFiltroPlantilla(FiltroPlantilla filtro)
        {
            this._filtro = filtro;
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

                IDaoFiltroPlantilla dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFiltroPlantilla();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._filtro));

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
