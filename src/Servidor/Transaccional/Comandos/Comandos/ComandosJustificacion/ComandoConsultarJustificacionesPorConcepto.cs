using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosJustificacion
{
    public class ComandoConsultarJustificacionesPorConcepto : ComandoBase<IList<Justificacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Justificacion _justificacion;

        
        public ComandoConsultarJustificacionesPorConcepto(Justificacion justificacion)
        {
            this._justificacion = justificacion;
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

                IDaoJustificacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoJustificacion();
                this.Receptor = new Receptor<IList<Justificacion>>(dao.ObtenerJustificacionesPorConcepto(this._justificacion));

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
