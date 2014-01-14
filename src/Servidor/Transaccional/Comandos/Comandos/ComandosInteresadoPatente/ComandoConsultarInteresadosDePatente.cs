using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.Comandos.Comandos.ComandosInteresadoPatente
{
    class ComandoConsultarInteresadosDePatente : ComandoBase<IList<InteresadoPatente>>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Patente _patente;


        /// <summary>
        /// Constructor por defecto que carga la patente que se usa como filtro
        /// </summary>
        /// <param name="patente">Patente a consultar interesados</param>
        public ComandoConsultarInteresadosDePatente(Patente patente)
        {
            this._patente = patente;
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

                IDaoInteresadoPatente dao = FabricaDaoBase.ObtenerFabricaDao().ObtenerDaoInteresadoPatente();
                this.Receptor = new Receptor<IList<InteresadoPatente>>(dao.ObtenerInteresadosPorPatente(this._patente));

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
