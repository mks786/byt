using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombrePatente
{
    public class ComandoInsertarOModificarCambioDeNombrePatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CambioDeNombrePatente _cambioNombre;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioNombre">CambioNombrePatente a insertar o modificar</param>
        public ComandoInsertarOModificarCambioDeNombrePatente(CambioDeNombrePatente cambioNombre)
        {
            this._cambioNombre = cambioNombre;
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._cambioNombre));

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
