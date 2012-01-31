using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeDomicilio
{
    public class ComandoInsertarOModificarCambioDeDomicilio : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CambioDeDomicilio _cambioDeDomicilio;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a insertar o modificar</param>
        public ComandoInsertarOModificarCambioDeDomicilio(CambioDeDomicilio cambioDeDomicilio)
        {
            this._cambioDeDomicilio = cambioDeDomicilio;
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

                IDaoCambioDeDomicilio dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeDomicilio();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._cambioDeDomicilio));

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
