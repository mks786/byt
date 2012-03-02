using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPlanilla
{
    public class ComandoEjecutarProcedimientoP1 : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;
        private Usuario _usuario;
        private int _way;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="marca">Marca a verificar</param>
        public ComandoEjecutarProcedimientoP1(Marca marca, Usuario usuario, int way)
        {
            this._marca = marca;
            this._usuario = usuario;
            this._way = way;
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
                this.Receptor = new Receptor<bool>(dao.EjecutarProcedimientoP1(this._marca,this._usuario,this._way));

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