using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarca
{
    class ComandoConsultarMarcasFiltro : ComandoBase<IList<Marca>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Marca _marca;


        /// <summary>
        /// metodo Comando Que Consulta las Marcas dado unos parametros
        /// </summary>
        /// <param name="marca">Marca con parametros a consultar</param>
        public ComandoConsultarMarcasFiltro(Marca marca)
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

                IDaoMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarca();
                this.Receptor = new Receptor<IList<Marca>>(dao.ObtenerMarcasFiltro(this._marca));

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
