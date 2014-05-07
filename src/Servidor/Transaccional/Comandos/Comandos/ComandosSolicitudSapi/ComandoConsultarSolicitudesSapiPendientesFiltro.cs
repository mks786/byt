using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosSolicitudSapi
{
    class ComandoConsultarSolicitudesSapiPendientesFiltro : ComandoBase<IList<SolicitudSapi>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private SolicitudSapi _solicitudSapi;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        public ComandoConsultarSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi)
        {
            this._solicitudSapi = solicitudSapi;
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

                IDaoSolicitudSapi dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoSolicitudSapi();
                this.Receptor = new Receptor<IList<SolicitudSapi>>(dao.ObtenerSolicitudesSapiPendientesFiltro(this._solicitudSapi));

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
