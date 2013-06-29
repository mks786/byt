using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoCaja
{
    class ComandoObtenerTipoCajaMarcaOPatente : ComandoBase<IList<TipoCaja>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string _parametroFiltro;

        /// <summary>
        /// Constructor por defecto de la clase
        /// </summary>
        /// <param name="parametro1">Filtro para obtener los tipos de cajas de Marca o Patente</param>
        public ComandoObtenerTipoCajaMarcaOPatente(String parametro1)
        {
            this._parametroFiltro = parametro1;
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

                IDaoTipoCaja dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoTipoCaja();
                this.Receptor = new Receptor<IList<TipoCaja>>(dao.ObtenerTiposCajasMarcaOPatente(this._parametroFiltro));

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
