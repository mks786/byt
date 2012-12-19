using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion
{
    public class ComandoEliminarInstruccionDeRenovacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private InstruccionDeRenovacion _instruccionDeRenovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="instruccionDeRenovacion">instruccionDeRenovacion a eliminar</param>
        public ComandoEliminarInstruccionDeRenovacion(InstruccionDeRenovacion instruccionDeRenovacion)
        {
            this._instruccionDeRenovacion = instruccionDeRenovacion;
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

                IDaoInstruccionDeRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionDeRenovacion();
                dao.Eliminar(this._instruccionDeRenovacion);

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