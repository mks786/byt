using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAsociado
{
    class ComandoConsultarContactosCxPAsociado : ComandoBase<IList<ContactoCxP>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private DateTime[] _fechas;
        private Asociado _asociado;

        /// <summary>
        /// Constructor predeterminado que recibe el Asociado consultado
        /// </summary>
        /// <param name="asociado">Asociado Consultado</param>
        public ComandoConsultarContactosCxPAsociado(Asociado asociado)
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
                this.Receptor = new Receptor<IList<ContactoCxP>>(dao.ObtenerContactosCxPDelAsociado(this._asociado));

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
