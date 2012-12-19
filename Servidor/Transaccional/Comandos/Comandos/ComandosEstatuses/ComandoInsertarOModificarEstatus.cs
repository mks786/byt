using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosEstatuses
{
    public class ComandoInsertarOModificarEstatus : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Estatus _estatus;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="estatus">estatus a insertar o modificar</param>
        public ComandoInsertarOModificarEstatus(Estatus estatus)
        {
            this._estatus = estatus;
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

                IDaoEstatus dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoEstatus();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._estatus));

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
