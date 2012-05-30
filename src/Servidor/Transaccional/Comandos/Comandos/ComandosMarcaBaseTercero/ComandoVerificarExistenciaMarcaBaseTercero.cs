using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarcaBaseTercero
{
    public class ComandoVerificarExistenciaMarcaBaseTercero : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MarcaBaseTercero _marcaBaseTercero;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="marcaBaseTercero">marcaBaseTercero a verificar</param>
        public ComandoVerificarExistenciaMarcaBaseTercero(MarcaBaseTercero marcaBaseTercero)
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
            // OJO!!!!DESCOMENTAR    this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._marcaBaseTercero.Id));

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