using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContactoCxP
{
    class ComandoConsultarContactoCxPFiltro : ComandoBase<IList<ContactoCxP>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ContactoCxP _contactoCxP;


        /// <summary>
        /// Metodo Comando que Consulta los Contactos dado un asociado
        /// </summary>
        /// <param name="asociado">Asociado a buscar Contactos</param>
        public ComandoConsultarContactoCxPFiltro(ContactoCxP contactoCxP)
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
                this.Receptor = new Receptor<IList<ContactoCxP>>(dao.ObtenerContactosCxPFiltro(this._contactoCxP));

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
