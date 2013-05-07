using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMedio
{
    class ComandoConsultarMedioPorOtroCampo : ComandoBase<IList<Medio>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string campoConsultar;
        private string tipoOrdenamiento;

        public ComandoConsultarMedioPorOtroCampo(string campoConsultar, string tipoOrdenamiento)
        {
            // TODO: Complete member initialization
            this.campoConsultar = campoConsultar;
            this.tipoOrdenamiento = tipoOrdenamiento;
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

                IDaoMedio dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMedio();
                this.Receptor = new Receptor<IList<Medio>>(dao.ObtenerTodos(this.campoConsultar, this.tipoOrdenamiento));

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
