using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPlanilla
{
    public class ComandoConsultarPlanillaPorUsuario : ComandoBase<Planilla>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Usuario _usuario;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">usuario a verificar</param>
        public ComandoConsultarPlanillaPorUsuario(Usuario usuario)
        {
            this._usuario = usuario;
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

                //IDaoStoredProcedureP1 dao = FabricaDaoStoredProcedureBase.ObtenerFabricaDaoStoredProcedures().ObtenerDaoStoredProcedureP1();
                //dao.EjecutarProcedimiento("P1");

                IDaoPlanilla dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPlanilla();
                this.Receptor = new Receptor<Planilla>(dao.EjecutarProcedimientoPID(this._usuario));

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