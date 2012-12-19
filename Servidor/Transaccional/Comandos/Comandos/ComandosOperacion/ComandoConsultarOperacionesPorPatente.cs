using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosOperacion
{
    class ComandoConsultarOperacionesPorPatente : ComandoBase<IList<Operacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;


        /// <summary>
        /// Metodo Comando a Consultar las operaciones por patente
        /// </summary>
        /// <param name="patente">Patente a consultar las operaciones</param>
        public ComandoConsultarOperacionesPorPatente(Patente patente)
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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoOperacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoOperacion();
                this.Receptor = new Receptor<IList<Operacion>>(dao.ObtenerOperacionesPorPatente(this._patente));

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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
