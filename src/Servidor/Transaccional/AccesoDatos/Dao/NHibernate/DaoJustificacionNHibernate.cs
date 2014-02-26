using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoJustificacionNHibernate : DaoBaseNHibernate<Justificacion, int>, IDaoJustificacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene una lista de justificaciones filtradas por Concepto
        /// </summary>
        /// <param name="justificacion">Justificacion filtro</param>
        /// <returns>Lista de justificaciones filtradas por Concepto</returns>
        public IList<Justificacion> ObtenerJustificacionesPorConcepto(Justificacion justificacion)
        {
            IList<Justificacion> justificaciones = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerJustificacionesPorConcepto, justificacion.Concepto.Id));
                justificaciones = query.List<Justificacion>();

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
            return justificaciones;
        }
    }
}
