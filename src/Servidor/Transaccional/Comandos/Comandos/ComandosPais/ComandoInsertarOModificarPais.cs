using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPais
{
    public class ComandoInsertarOModificarPais : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Pais _pais;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario a insertar o modificar</param>
        public ComandoInsertarOModificarPais(Pais pais)
        {
            this._pais = pais;
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

                IDaoPais dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPais();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._pais));

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
