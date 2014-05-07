using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCompraSapi
{
    class ComandoInsertarOModificarCompra : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CompraSapi _compra;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="compra">Compra Sapi a insertar y/o actualizar</param>
        public ComandoInsertarOModificarCompra(CompraSapi compra)
        {
            this._compra = compra;
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

                IDaoCompraSapi dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCompraSapi();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._compra));

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
