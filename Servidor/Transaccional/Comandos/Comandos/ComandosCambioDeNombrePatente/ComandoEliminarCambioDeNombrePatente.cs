using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombrePatente
{
    public class ComandoEliminarCambioDeNombrePatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeNombrePatente _cambioDeNombre;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cambioDeNombre">CambioNombrePatente a eliminar</param>
        public ComandoEliminarCambioDeNombrePatente(CambioDeNombrePatente cambioDeNombre)
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