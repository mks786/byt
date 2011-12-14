using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosTipoEstado
{
    public class ComandoVerificarExistenciaTipoEstado : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TipoEstado _tipoEstado;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="pais">Pais a verificar</param>
        public ComandoVerificarExistenciaTipoEstado(TipoEstado tipoEstado)
        {
            this._tipoEstado = tipoEstado;
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        //public override void Ejecutar()
        //{
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        //IDaoPais dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPais();
        //        //this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._tipoEstado.Id));

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw ex;
        //    }
        //}

        public override void Ejecutar()
        {
            throw new NotImplementedException();
        }
    }
}