using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosContacto
{
    class ComandoConsultarContactoPorId : ComandoBase<Contacto>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Contacto _contacto;


        /// <summary>
        /// Metodo Comando que Consulta los Contactos dado un asociado
        /// </summary>
        /// <param name="asociado">Asociado a buscar Contactos</param>
        public ComandoConsultarContactoPorId(Contacto contacto)
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
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IDaoContacto dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoContacto();
                this.Receptor = new Receptor<Contacto>(dao.ConsultarContactoPorId(this._contacto));

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
