using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAsignacion
{
    public class ComandoInsertarOModificarAsignacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Asignacion _asignacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="asignacion">asignacion a insertar o modificar</param>
        public ComandoInsertarOModificarAsignacion(Asignacion asignacion)
        {
            this._asignacion = asignacion;
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

                IDaoAsignacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAsignacion();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._asignacion));

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
