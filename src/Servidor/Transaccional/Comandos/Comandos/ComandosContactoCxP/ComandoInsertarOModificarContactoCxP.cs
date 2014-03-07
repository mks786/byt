using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContactoCxP
{
    public class ComandoInsertarOModificarContactoCxP : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        ContactoCxP _contactoCxP;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP a insertar o modificar</param>
        public ComandoInsertarOModificarContactoCxP(ContactoCxP contactoCxP)
        {
            this._contactoCxP = contactoCxP;
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

                IDaoContactoCxP dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContactoCxP();
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._contactoCxP));

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
