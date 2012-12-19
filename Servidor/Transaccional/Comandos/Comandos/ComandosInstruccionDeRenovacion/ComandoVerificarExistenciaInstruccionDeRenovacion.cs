using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion
{
    public class ComandoVerificarExistenciaInstruccionDeRenovacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private InstruccionDeRenovacion _instruccionDeRenovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="instruccionDeRenovacion">instruccionDeRenovacion a verificar</param>
        public ComandoVerificarExistenciaInstruccionDeRenovacion(InstruccionDeRenovacion instruccionDeRenovacion)
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

                //IDaoInstruccionDeRenovacion dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInstruccionDeRenovacion();
                //this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._instruccionDeRenovacion.Id));

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