using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosDatosTransferencia
{
    class ComandoConsultarDatosTransferenciaPorAsociado : ComandoBase<IList<DatosTransferencia>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Asociado _asociado;


        /// <summary>
        /// Metodo Comando que Consulta los Datos de transferencia por un Asociado
        /// </summary>
        /// <param name="asociado">Asociado a consultar los Datos Transferencia</param>
        public ComandoConsultarDatosTransferenciaPorAsociado(Asociado asociado)
        {
            this._asociado = asociado;

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

                IDaoDatosTransferencia dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoDatosTransferencia();
                this.Receptor = new Receptor<IList<DatosTransferencia>>(dao.ObtenerDatosTransferenciaPorAsociado(this._asociado));

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
