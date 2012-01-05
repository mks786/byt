using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosBusqueda
{
    public class ComandoInsertarOModificarBusqueda : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Busqueda _busqueda;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="_busqueda">InfoBol a insertar o modificar</param>
        public ComandoInsertarOModificarBusqueda(Busqueda _busqueda)
        {
            this._busqueda = _busqueda;
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

                IDaoBusqueda dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoBusqueda();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._busqueda));

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
