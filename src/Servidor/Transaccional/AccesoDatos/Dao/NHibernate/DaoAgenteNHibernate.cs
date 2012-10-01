using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAgenteNHibernate : DaoBaseNHibernate<Agente, string>, IDaoAgente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary> Metodo que consulta los agentes con sus poderes </summary>
        /// <returns>Lista de agente</returns>
        public IList<Agente> ObtenerAgentesYPoderes()
        {
            IList<Agente> agentes;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(Recursos.ConsultasHQL.ObtenerAgentesYPoderes);
                agentes = query.List<Agente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAgentesYPoderes);
            }
            finally
            {
                Session.Close();
            }

            return agentes;
        }


        /// <summary>
        /// Metodo que Consulta los agentes dado unos parametros en especificos
        /// </summary>
        /// <param name="agente">Agente con Filtros</param>
        /// <returns>Lista de Agentes</returns>
        public IList<Agente> ObtenerAgentesFiltro(Agente agente)
        {
            IList<Agente> agentes = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAgente);
                if ((null != agente) && (!string.IsNullOrEmpty(agente.Id)))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteId, agente.Id);
                    variosFiltros = true;
                }
                if (!string.IsNullOrEmpty(agente.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteNombre, agente.Nombre);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                agentes = query.List<Agente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAgentesFiltro);
            }
            finally
            {
                Session.Close();
            }
            return agentes;
        }


        /// <summary>
        /// metodo que consulta todos los agentes que no tienen poder asignado
        /// </summary>
        /// <param name="agente">Agente con Filtro</param>
        /// <returns>Lista de Agentes</returns>
        public IList<Agente> ObtenerAgentesSinPoderesFiltro(Agente agente)
        {
            IList<Agente> agentes = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAgenteDesinflados);
                if ((null != agente) && (!string.IsNullOrEmpty(agente.Id)))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteId, agente.Id);
                    variosFiltros = true;
                }
                if (!string.IsNullOrEmpty(agente.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteNombre, agente.Nombre);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                agentes = query.List<Agente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAgentesSinPoderesFiltro);
            }
            finally
            {
                Session.Close();
            }
            return agentes;
        }


        /// <summary>
        /// Metodo que consulta los agentes de un poder
        /// </summary>
        /// <param name="poder">Poder solicitado</param>
        /// <returns>Lista de Agentes</returns>
        public IList<Agente> ObtenerAgentesDeUnPoder(Poder poder)
        {
            IList<Agente> agentes;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAgentesDeUnPoder, poder.Id));
                Poder poderAux = query.UniqueResult<Poder>();

                agentes = poderAux.Agentes;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAgentesDeUnPoder);
            }
            finally
            {
                Session.Close();
            }

            return agentes;
        }


        /// <summary>
        /// Metodo que regresa los Agentes que estan Vacios
        /// </summary>
        /// <param name="agente">Agente solicitado</param>
        /// <returns>Lista de agentes</returns>
        public IList<Agente> ObtenerAgentesVacios(Agente agente)
        {
            IList<Agente> agentes = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAgenteVacio);
                if ((null != agente) && (!string.IsNullOrEmpty(agente.Id)))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteId, agente.Id);
                    variosFiltros = true;
                }
                if (!string.IsNullOrEmpty(agente.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteNombre, agente.Nombre);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                agentes = query.List<Agente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAgentesVacios);
            }
            finally
            {
                Session.Close();
            }
            return agentes;
        }
    }
}
