using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosRol
{
    public class ComandoConsultarRolPorId : ComandoBase<Rol>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Rol _rol;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="rol">Rol que contiene el Id</param>
        public ComandoConsultarRolPorId(Rol rol)
        {
            this._rol = rol;
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

                IDaoRol dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoRol();
                this.Receptor = new Receptor<Rol>(dao.ObtenerPorId(this._rol.Id));

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
