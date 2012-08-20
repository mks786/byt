using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoResolucionNHibernate : DaoBaseNHibernate<Resolucion, string>, IDaoResolucion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que verifica la existencia de una resolución
        /// </summary>
        /// <param name="resolucion">Resolucion a buscar</param>
        /// <returns>true en caso de que exista, false en lo contrario</returns>
        public bool VerificarExistenciaResolucion(Resolucion resolucion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.VerificarExistenciaResolucion, resolucion.Id, resolucion.FechaResolucion.ToShortDateString(), resolucion.Boletin.Id));
                Resolucion resolucionExistente = query.UniqueResult<Resolucion>();

                if (resolucionExistente != null)
                    existe = true;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExErrorVerificarExistenciaResolucion);
            }
            finally
            {
                Session.Close();
            }

            return existe;
        }                
    }
}
