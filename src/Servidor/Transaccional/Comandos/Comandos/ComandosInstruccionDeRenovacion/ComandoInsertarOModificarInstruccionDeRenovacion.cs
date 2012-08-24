using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInstruccionDeRenovacion
{
    public class ComandoInsertarOModificarInstruccionDeRenovacion : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InstruccionDeRenovacion _instruccionDeRenovacion;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="_instruccionDeRenovacion">InstruccionDeRenovacion a _instruccionDeRenovacion o modificar</param>
        public ComandoInsertarOModificarInstruccionDeRenovacion(InstruccionDeRenovacion _instruccionDeRenovacion)
        {
            this._instruccionDeRenovacion = _instruccionDeRenovacion;
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._instruccionDeRenovacion));

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
