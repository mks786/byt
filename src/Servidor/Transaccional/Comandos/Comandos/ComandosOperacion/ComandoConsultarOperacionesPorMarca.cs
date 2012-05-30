using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosOperacion
{
    class ComandoConsultarOperacionesPorMarca : ComandoBase<IList<Operacion>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// Metoco Comando que consulta las operaciones por una marca
        /// </summary>
        /// <param name="marca">Marca a consultar las operaciones</param>
        public ComandoConsultarOperacionesPorMarca(Marca marca)
        {
            this._marca = marca;

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
                this.Receptor = new Receptor<IList<Operacion>>(dao.ObtenerOperacionesPorMarca(this._marca));

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
