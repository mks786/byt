using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarcaBaseTercero
{
    class ComandoConsultarMarcasBaseTerceroFiltro : ComandoBase<IList<MarcaBaseTercero>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MarcaBaseTercero _marcaBaseTercero;


        /// <summary>
        /// Metodo Comando que consulta las marcas base tercero dado unos parametros
        /// </summary>
        /// <param name="marcaBaseTercero">marcaBaseTercero con parametros a consultar</param>
        public ComandoConsultarMarcasBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero)
        {
            this._marcaBaseTercero = marcaBaseTercero;
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

                IDaoMarcaBaseTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarcaBaseTercero();
                this.Receptor = new Receptor<IList<MarcaBaseTercero>>(dao.ObtenerMarcaBaseTerceroFiltro(this._marcaBaseTercero));

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
