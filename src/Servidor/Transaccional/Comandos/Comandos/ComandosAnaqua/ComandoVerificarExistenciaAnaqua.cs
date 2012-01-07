using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosAnaqua
{
    public class ComandoVerificarExistenciaAnaqua : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Anaqua _anaqua;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="anaqua">Anexo a verificar</param>
        public ComandoVerificarExistenciaAnaqua(Anaqua anaqua)
        {
            this._anaqua = anaqua;
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

                IDaoAnaqua dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoAnaqua();
                //this.Receptor = new Receptor<bool>(dao.VerificarExistencia(this._anaqua.Marca));

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