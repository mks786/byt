using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBol
{
    class ComandoConsultarInfoBolesPorMarca : ComandoBase<IList<InfoBol>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// Metodo Comando que consulta los InfoBoles de una marca
        /// </summary>
        /// <param name="marca">Marca a consultar InfoBoles</param>
        public ComandoConsultarInfoBolesPorMarca(Marca marca)
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

                IDaoInfoBol dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBol();
                this.Receptor = new Receptor<IList<InfoBol>>(dao.ObtenerInfoBolesPorMarca(this._marca));

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
