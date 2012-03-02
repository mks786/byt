using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosCambioDeNombre
{
    class ComandoConsultarCambiosDeNombreFiltro : ComandoBase<IList<CambioDeNombre>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CambioDeNombre _cambioDeNombre;


        public ComandoConsultarCambiosDeNombreFiltro(CambioDeNombre cambioDeNombre)
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
                this.Receptor = new Receptor<IList<CambioDeNombre>>(dao.ObtenerCambiosDeNombreFiltro(this._cambioDeNombre));

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
