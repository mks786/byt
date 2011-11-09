using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosEntradaAlterna
{
    public class ComandoInsertarOModificarEntradaAlterna : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private EntradaAlterna _entradaAlterna;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna a insertar o modificar</param>
        public ComandoInsertarOModificarEntradaAlterna(EntradaAlterna entradaAlterna)
        {
            this._entradaAlterna = entradaAlterna;
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

                IDaoEntradaAlterna dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoEntradaAlterna();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._entradaAlterna));

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
