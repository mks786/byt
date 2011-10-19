using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContacto
{
    class ComandoConsultarContactosPorAsociado : ComandoBase<IList<Contacto>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Asociado _asociado;

        public ComandoConsultarContactosPorAsociado(Asociado asociado)
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
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoContacto dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContacto();
                this.Receptor = new Receptor<IList<Contacto>>(dao.ObtenerContactosPorAsociado(this._asociado));

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
