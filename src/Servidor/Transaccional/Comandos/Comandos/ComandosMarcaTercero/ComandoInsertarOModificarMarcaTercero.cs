using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMarcaTercero
{
    public class ComandoInsertarOModificarMarcaTercero : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        MarcaTercero _marcaTercero;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario a insertar o modificar</param>
        public ComandoInsertarOModificarMarcaTercero(MarcaTercero marcaTercero)
        {
            this._marcaTercero = marcaTercero;
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

                IDaoMarcaTercero dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMarcaTercero();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._marcaTercero));

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
