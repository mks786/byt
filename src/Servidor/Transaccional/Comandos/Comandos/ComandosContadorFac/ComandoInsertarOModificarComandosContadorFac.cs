using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContadorFac
{
    class ComandoInsertarOModificarContadorFac : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ContadorFac _contador;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="contador">Contador a insertar o modificar</param>
        public ComandoInsertarOModificarContadorFac(ContadorFac contador)
        {
            this._contador = contador;
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

                IDaoContadorFac dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContadorFac();
                dao.InsertarOModificar(this._contador);

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
