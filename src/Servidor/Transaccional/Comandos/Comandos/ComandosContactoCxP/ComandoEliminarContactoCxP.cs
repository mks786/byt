using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContactoCxP
{
    public class ComandoEliminarContactoCxP : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ContactoCxP _contactoCxP;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="contactoCxP">Contacto a eliminar</param>
        public ComandoEliminarContactoCxP(ContactoCxP contactoCxP)
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
                 this.Receptor = new Receptor<bool>(dao.Eliminar(this._contactoCxP));

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
