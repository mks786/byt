using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosRenovacion
{
    public class ComandoInsertarOModificarRenovacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Renovacion _renovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="renovacion">Renovacion a insertar o modificar</param>
        public ComandoInsertarOModificarRenovacion(Renovacion renovacion)
        {
            this._renovacion = renovacion;
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

                IDaoRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoRenovacion();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._renovacion));

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
