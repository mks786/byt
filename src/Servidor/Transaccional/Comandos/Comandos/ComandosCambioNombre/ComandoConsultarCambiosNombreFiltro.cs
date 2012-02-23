using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioNombre
{
    class ComandoConsultarCambiosNombreFiltro : ComandoBase<IList<CambioNombre>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioNombre _cambioNombre;


        public ComandoConsultarCambiosNombreFiltro(CambioNombre cambioNombre)
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

                IDaoCambioNombre dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoCambioNombre();
                this.Receptor = new Receptor<IList<CambioNombre>>(dao.ObtenerCambiosDeNombreFiltro(this._cambioNombre));

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
