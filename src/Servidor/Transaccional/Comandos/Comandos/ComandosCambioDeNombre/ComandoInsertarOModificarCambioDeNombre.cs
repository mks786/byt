using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombre
{
    public class ComandoInsertarOModificarCambioDeNombre : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CambioDeNombre _cambioNombre;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioNombre">cambioNombre a insertar o modificar</param>
        public ComandoInsertarOModificarCambioDeNombre(CambioDeNombre cambioNombre)
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

                IDaoCambioDeNombre dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeNombre();
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
