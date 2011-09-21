using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosDepartamento
{
    public class ComandoConsultarDepartamentoPorId : ComandoBase<Departamento>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Departamento _departamento;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="departamento"></param>
        public ComandoConsultarDepartamentoPorId(Departamento departamento)
        {
            this._departamento = departamento;
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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoDepartamento dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoDepartamento();
                this.Receptor = new Receptor<Departamento>(dao.ObtenerPorId(this._departamento.Id));

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
