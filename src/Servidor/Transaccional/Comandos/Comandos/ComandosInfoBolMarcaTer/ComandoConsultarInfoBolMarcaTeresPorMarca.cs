using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInfoBolMarcaTer
{
    class ComandoConsultarInfoBolMarcaTeresPorMarca : ComandoBase<IList<InfoBolMarcaTer>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MarcaTercero _marca;


        /// <summary>
        /// Metodo Comando que consulta los InfoBolMarcaTeres de una marca
        /// </summary>
        /// <param name="marca">Marca a consultar InfoBolMarcaTeres</param>
        public ComandoConsultarInfoBolMarcaTeresPorMarca(MarcaTercero marca)
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

                IDaoInfoBolMarcaTer dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInfoBolMarcaTer();
                this.Receptor = new Receptor<IList<InfoBolMarcaTer>>(dao.ObtenerInfoBolMarcaTeresPorMarca(this._marca));

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
