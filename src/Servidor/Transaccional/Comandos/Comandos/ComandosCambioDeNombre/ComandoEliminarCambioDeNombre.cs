using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombre
{
    public class ComandoEliminarCambioDeNombre : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeNombre _cambioDeNombre;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeNombre">CambioNombre a eliminar</param>
        public ComandoEliminarCambioDeNombre(CambioDeNombre cambioDeNombre)
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

                IDaoCambioDeNombre dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioDeNombre();
                 this.Receptor = new Receptor<bool>(dao.Eliminar(this._cambioDeNombre));

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