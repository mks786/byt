using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAsociado
{
    class ComandoConsultarAsociadosFiltro : ComandoBase<IList<Asociado>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Asociado _Asociado;


        /// <summary>
        /// Metodo que consulta a los asociados dado unos parametros
        /// </summary>
        /// <param name="Asociado">Asociado con parametros a consultar</param>
        public ComandoConsultarAsociadosFiltro(Asociado Asociado)
        {
            this._Asociado = Asociado;
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
                this.Receptor = new Receptor<IList<Asociado>>(dao.ObtenerAsociadosFiltro(this._Asociado));

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
