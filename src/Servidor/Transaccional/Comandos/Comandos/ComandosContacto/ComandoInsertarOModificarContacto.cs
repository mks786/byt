using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContacto
{
    public class ComandoInsertarOModificarContacto : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        Contacto _contacto;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Contacto a insertar o modificar</param>
        public ComandoInsertarOModificarContacto(Contacto contacto)
        {
            this._contacto = contacto;
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._contacto));

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
