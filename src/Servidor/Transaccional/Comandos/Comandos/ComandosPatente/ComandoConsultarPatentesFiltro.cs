using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPatente
{
    class ComandoConsultarPatentesFiltro : ComandoBase<IList<Patente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;


        /// <summary>
        /// Metodo Comando que consulta las patentes dado unos parametros
        /// </summary>
        /// <param name="patente">Patente con parametros a consultar</param>
        public ComandoConsultarPatentesFiltro(Patente patente)
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

                IDaoPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPatente();
                this.Receptor = new Receptor<IList<Patente>>(dao.ObtenerPatentesFiltro(this._patente));

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
