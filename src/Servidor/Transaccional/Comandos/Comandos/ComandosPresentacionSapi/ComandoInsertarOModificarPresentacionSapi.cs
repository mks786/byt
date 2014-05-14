using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPresentacionSapi
{
    class ComandoInsertarOModificarPresentacionSapi : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        PresentacionSapi _presentacionSapi;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="compra">Encabezado de Presentacion Sapi a insertar y/o actualizar</param>
        public ComandoInsertarOModificarPresentacionSapi(PresentacionSapi presentacionSapi)
        {
            this._presentacionSapi = presentacionSapi;
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

                //IDaoCompraSapi dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCompraSapi();
                IDaoPresentacionSapi dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPresentacionSapi();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._presentacionSapi));

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
