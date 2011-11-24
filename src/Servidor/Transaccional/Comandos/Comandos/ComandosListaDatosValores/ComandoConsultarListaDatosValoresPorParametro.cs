using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosListaDatosValores
{
    class ComandoConsultarListaDatosValoresPorParametro: ComandoBase<IList<ListaDatosValores>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ListaDatosValores _ListaDatosValores;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="ListaDatosValores"></param>
        public ComandoConsultarListaDatosValoresPorParametro(ListaDatosValores ListaDatosValores)
        {
            this._ListaDatosValores = ListaDatosValores;
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

                IDaoListaDatosValores dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoListaDatosValores();
                this.Receptor = new Receptor<IList<ListaDatosValores>>(dao.ObtenerListaDatosValoresPorParametro(this._ListaDatosValores));

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
