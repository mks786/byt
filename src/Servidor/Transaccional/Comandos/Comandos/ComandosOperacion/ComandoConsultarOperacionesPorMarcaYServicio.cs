using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosOperacion
{
    class ComandoConsultarOperacionesPorMarcaYServicio : ComandoBase<IList<Operacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Operacion _operacion;

        public ComandoConsultarOperacionesPorMarcaYServicio(Operacion operacion)
        {
            this._operacion = operacion;

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
                this.Receptor = new Receptor<IList<Operacion>>(dao.ObtenerOperacionesPorMarcaYServicio(this._operacion));

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
