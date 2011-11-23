using System;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using NLog;

namespace Trascend.Bolet.Comandos.Comandos.ComandosUsuario
{
    public class ComandoConsultarUsuarioPorIniciales : ComandoBase<Usuario>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string _usuarioIniciales;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario a consultar</param>
        public ComandoConsultarUsuarioPorIniciales(string usuario)
        {
            this._usuarioIniciales = usuario;
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

                IDaoUsuario dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoUsuario();
                this.Receptor = new Receptor<Usuario>(dao.ObtenerUsuarioPorIniciales(this._usuarioIniciales));

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
