using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPoderNHibernate : DaoBaseNHibernate<Poder, int>, IDaoPoder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Poder> ObtenerPoderesPorInteresado(Interesado interesado)
        {

            IList<Poder> poderes;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerPoderesPorInteresado, interesado.Id));
                poderes = query.List<Poder>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return poderes;
        }

        public IList<Poder> ObtenerPoderesPorAgente(Agente agente)
        {
            IList<Poder> poderes;

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerPoderesPorAgente, agente.Id));

                agente = query.UniqueResult<Agente>();
                poderes = agente.Poderes;


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return poderes;
        }

        /// <summary>
        /// Método que obtiene un poder con uno o mas filtros
        /// </summary>
        /// <param name="poder">filtros de poder</param>
        /// <returns>poder filtrado</returns>
        public IList<Poder> ObtenerPoderesFiltro(Poder poder)
        {
            IList<Poder> poderes = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerPoder);
                if ((null != poder) && (poder.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderId, poder.Id);
                    variosFiltros = true;
                }
                if ((null != poder.Fecha) && (!poder.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", poder.Fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", poder.Fecha.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderFecha, fecha, fecha2);
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                poderes = query.List<Poder>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }
            return poderes;
        }
    }
}
