using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoAsignacionNHibernate : DaoBaseNHibernate<Asignacion, int>, IDaoAsignacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Obtiene todas las asignaciones que tiene una carta
        /// </summary>
        /// <param name="carta">La Carta</param>
        /// <returns>Lista de Asignaciones</returns>
        public IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta)
        {
            IList<Asignacion> asignaciones = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAsignacionesPorCarta, carta.Id));
                asignaciones = query.List<Asignacion>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsignacionesPorCarta);
            }
            finally
            {
                Session.Close();
            }
            return asignaciones;
        }
    }
}
