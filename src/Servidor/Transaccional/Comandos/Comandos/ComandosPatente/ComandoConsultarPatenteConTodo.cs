using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPatente
{
    class ComandoConsultarPatenteConTodo : ComandoBase<Patente>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _Patente;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="Patente">Patente a consultar</param>
        public ComandoConsultarPatenteConTodo(Patente Patente)
        {
            this._Patente = Patente;
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
                this.Receptor = new Receptor<Patente>(dao.ObtenerPatenteConTodo(this._Patente));

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
