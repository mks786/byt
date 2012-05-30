using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBusqueda
{
    class ComandoConsultarBusquedasPorMarca : ComandoBase<IList<Busqueda>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// Metodo Comando que Consulta las busquedas pertenecientes a una marca
        /// </summary>
        /// <param name="marca">Marca a consultar sus busquedas</param>
        public ComandoConsultarBusquedasPorMarca(Marca marca)
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

                IDaoBusqueda dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBusqueda();
                this.Receptor = new Receptor<IList<Busqueda>>(dao.ObtenerBusquedasPorMarca(this._marca));

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
