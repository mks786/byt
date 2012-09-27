using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosFecha
{
    public class ComandoInsertarOModificarFecha : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Fecha _fecha;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="fecha">Fecha a insertar o modificar</param>
        public ComandoInsertarOModificarFecha(Fecha fecha)
        {
            this._fecha = fecha;
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

                IDaoFecha dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoFecha();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._fecha));

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
