using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBoletin
{
    public class ComandoInsertarOModificarBoletin : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Boletin _boletin;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="estado">estado a insertar o modificar</param>
        public ComandoInsertarOModificarBoletin(Boletin estado)
        {
            this._boletin = estado;
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

                IDaoBoletin dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBoletin();
                dao.InsertarOModificar(this._boletin);

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
