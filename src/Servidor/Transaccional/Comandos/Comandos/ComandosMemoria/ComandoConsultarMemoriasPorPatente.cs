using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMemoria
{
    public class ComandoConsultarMemoriasPorPatente : ComandoBase<IList<Memoria>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="patente">Interesado para el filtrado</param>
        public ComandoConsultarMemoriasPorPatente(Patente patente)
        {
            this._patente = patente;
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

                IDaoMemoria dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMemoria();
                this.Receptor = new Receptor<IList<Memoria>>(dao.ObtenerMemoriasPorPatente(this._patente));

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
