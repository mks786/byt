using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFechaMarca
{
    class ComandoConsultarFechasPorMarca : ComandoBase<IList<FechaMarca>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// Metodo Comando que consulta los InfoBoles de una marca
        /// </summary>
        /// <param name="marca">Marca a consultar InfoBoles</param>
        public ComandoConsultarFechasPorMarca(Marca marca)
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
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoFechaMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFechaMarca();
                this.Receptor = new Receptor<IList<FechaMarca>>(dao.ObtenerFechasPorMarca(this._marca));

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
