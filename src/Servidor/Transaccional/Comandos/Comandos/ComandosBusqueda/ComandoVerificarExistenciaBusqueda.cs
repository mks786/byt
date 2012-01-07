using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBusqueda
{
    public class ComandoVerificarExistenciaBusqueda : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Busqueda _busqueda;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="busqueda">InfoBol a verificar</param>
        public ComandoVerificarExistenciaBusqueda(Busqueda busqueda)
        {
            this._busqueda = busqueda;
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

                IDaoBusqueda dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBusqueda();
                this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._busqueda.Id));

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