using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombrePatente
{
    public class ComandoVerificarExistenciaCambioDeNombrePatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeNombrePatente _cambioDeNombre;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeNombre">CambioNombrePatente a verificar</param>
        public ComandoVerificarExistenciaCambioDeNombrePatente(CambioDeNombrePatente cambioDeNombre)
        {
            this._cambioDeNombre = cambioDeNombre;
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

                IDaoCambioDeNombrePatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeNombrePatente();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._cambioDeNombre.Id));

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