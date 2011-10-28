using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosMedio
{
    public class ComandoInsertarOModificarMedio : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Medio _medio;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Medio a insertar o modificar</param>
        public ComandoInsertarOModificarMedio(Medio medio)
        {
            this._medio = medio;
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

                IDaoMedio dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoMedio();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._medio));

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
