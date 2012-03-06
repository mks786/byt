using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosEstadoMarca
{
    public class ComandoEliminarEstadoMarca : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private EstadoMarca _estadoMarca;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="anexo">Pais a eliminar</param>
        public ComandoEliminarEstadoMarca(EstadoMarca estadoMarca)
        {
            this._estadoMarca = estadoMarca;
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

                IDaoEstadoMarca dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoEstadoMarca();
                this.Receptor = new Receptor<bool>(dao.Eliminar(this._estadoMarca));

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