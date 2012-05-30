using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;

namespace Trascend.Bolet.Comandos.Comandos.ComandosPatente
{
    public class ComandoConsultarPatentePorId : ComandoBase<Patente>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="id">Patente a eliminar</param>
        public ComandoConsultarPatentePorId(int id)
        {
            _patente = new Patente();
            this._patente.Id = id;
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
                
                IDaoPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoPatente();
                this.Receptor = new Receptor<Patente>(dao.ObtenerPorId(this._patente.Id));

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