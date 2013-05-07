using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosDepartamento
{
    class ComandoConsultarDepartamentoPorOtroCampo : ComandoBase<IList<Departamento>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string campoConsultar;
        private string tipoOrdenamiento;

        public ComandoConsultarDepartamentoPorOtroCampo(string campoConsultar, string tipoOrdenamiento)
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

                IDaoDepartamento dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoDepartamento();
                this.Receptor = new Receptor<IList<Departamento>>(dao.ObtenerTodos(this.campoConsultar,this.tipoOrdenamiento));

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
