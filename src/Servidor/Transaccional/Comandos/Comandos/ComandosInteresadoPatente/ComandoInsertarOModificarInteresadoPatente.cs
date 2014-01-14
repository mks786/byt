using System;
using System.Configuration;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.AccesoDatos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Comandos.ComandosInteresadoPatente
{
    public class ComandoInsertarOModificarInteresadoPatente : ComandoBase<bool>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        InteresadoPatente _interesadoPatente;

        /// <summary>
        /// Constructor predeterminado que recibe un objeto InteresadoPatente
        /// </summary>
        /// <param name="interesadoPatente">Objeto InteresadoPatente a insertar o actualizar</param>
        public ComandoInsertarOModificarInteresadoPatente(InteresadoPatente interesadoPatente)
        {
            this._interesadoPatente = interesadoPatente;
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
                this.Receptor = new Receptor<bool>(dao.InsertarOModificar(this._interesadoPatente));

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
