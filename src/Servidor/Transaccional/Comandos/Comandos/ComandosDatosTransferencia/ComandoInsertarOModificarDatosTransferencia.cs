using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.Comandos.Comandos.ComandosDatosTransferencia
{
    public class ComandoInsertarOModificarDatosTransferencia : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private DatosTransferencia _datosTransferencia;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="datosTransferencia">DatosTransferencia a insertar o modificar</param>
        public ComandoInsertarOModificarDatosTransferencia(DatosTransferencia datosTransferencia)
        {
            this._datosTransferencia = datosTransferencia;
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

                IDaoDatosTransferencia dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoDatosTransferencia();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._datosTransferencia));

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
