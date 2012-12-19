using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAsociado
{
    class ComandoConsultarEmailsDelAsociado : ComandoBase<IList<EmailAsociado>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private DateTime[] _fechas;
        private Asociado _asociado;
        


        /// <summary>
        /// Metodo Comando que consulta los recordatorios de marcas
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio con parametros</param>
        /// /// <param name="Fechas">fecha de renovación de marcas a filtrar</param>
        public ComandoConsultarEmailsDelAsociado(Asociado asociado)
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

                IDaoAsociado dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAsociado();
                this.Receptor = new Receptor<IList<EmailAsociado>>(dao.ObtenerEmailsDelAsociado(this._asociado));

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
