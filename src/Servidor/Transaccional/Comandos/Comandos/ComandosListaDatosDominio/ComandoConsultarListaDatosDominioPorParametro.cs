using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Comandos.ComandosListaDatosDominio
{
    public class ComandoConsultarListaDatosDominioPorParametro : ComandoBase<IList<ListaDatosDominio>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ListaDatosDominio _parametro;

        public ComandoConsultarListaDatosDominioPorParametro(ListaDatosDominio parametro)
        {
            this._parametro = parametro;
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

                IDaoListaDatosDominio dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoListaDatosDominio();
                this.Receptor = new Receptor<IList<ListaDatosDominio>>(dao.ObtenerListaDatosDominioPorParametro(this._parametro));

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
