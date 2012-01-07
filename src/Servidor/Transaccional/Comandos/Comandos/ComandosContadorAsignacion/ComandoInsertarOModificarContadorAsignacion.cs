using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContadorAsignacion
{
    class ComandoInsertarOModificarContadorAsignacion: ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ContadorAsignacion _contadorAsignacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="contadorAsignacion">Contador a insertar o modificar</param>
        public ComandoInsertarOModificarContadorAsignacion(ContadorAsignacion contadorAsignacion)
        {
            this._contadorAsignacion = contadorAsignacion;
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

                IDaoContadorAsignacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContadorAsignacion();
                dao.InsertarOModificar(this._contadorAsignacion);

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
