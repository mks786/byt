using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPoderNHibernate : DaoBaseNHibernate<Poder, int>, IDaoPoder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene los poderes por un interesado
        /// </summary>
        /// <param name="interesado">Interesado a consultar los poderes</param>
        /// <returns>Lista de poderes pertenecientes al interesado</returns>
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
                throw new ApplicationException(Recursos.Errores.ExObtenerPoderesPorInteresado);
            }
            finally
            {
                Session.Close();
            }

            return poderes;
        }

        /// <summary>
        /// Método que obtiene los Poderes por un Agente
        /// </summary>
        /// <param name="agente">Agente a consultar sus poderes</param>
        /// <returns>Poderes del Agente</returns>
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
                throw new ApplicationException(Recursos.Errores.ExObtenerPoderesPorAgente);
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
                if ((null != poder) && (poder.Id != 0) && (poder.Id != null))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderId, poder.Id);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.NumPoder) && (!poder.NumPoder.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorNumPoder, poder.NumPoder);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.Boletin))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorBoletin, poder.Boletin.Id);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.Interesado))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorInteresado, poder.Interesado.Id);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.Facultad) && (!poder.Facultad.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorFacultad, poder.Facultad);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.Observaciones) && (!poder.Observaciones.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorObservacion, poder.Observaciones);
                    variosFiltros = true;
                }

                if ((null != poder) && (null != poder.Anexo) && (!poder.Anexo.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPoderPorAnexo, poder.Anexo);
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

                filtro += " order by p.Id desc";

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
                throw new ApplicationException(Recursos.Errores.ExObtenerPoderesFiltro);
            }
            finally
            {
                Session.Close();
            }
            return poderes;
        }


        public IList<Poder> ObtenerPoderesEntreAgenteEInteresado(Agente agente, Interesado interesado)
        {
            IList<Poder> poderes = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                poderes = Session.CreateCriteria(typeof(Poder))
                        .CreateAlias("Agentes", "a")
                        .SetFetchMode("Agentes", FetchMode.Join)
                        .SetFetchMode("Interesado", FetchMode.Join)
                        .Add(Restrictions.Eq("a.Id", agente.Id))
                        .Add(Restrictions.Eq("Interesado.Id", interesado.Id))
                        .List<Poder>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerPoderesFiltro);
            }
            finally
            {
                Session.Close();
            }
            return poderes;
        }
    }
}
