using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoPatenteNHibernate : DaoBaseNHibernate<Patente, int>, IDaoPatente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que obtiene las patentes que cumplan con un filtro determinado
        /// </summary>
        /// <param name="Patente">Patente filtro</param>
        /// <returns>Lista de patentes que cumplan con el filtro</returns>
        public IList<Patente> ObtenerPatentesFiltro(Patente Patente)
        {
            IList<Patente> Patentes = null;
            bool variosFiltros = false;
            int[] consultaGrid; ;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerPatente);

                if ((null != Patente) && (Patente.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteId, Patente.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(Patente.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteDescripcion, Patente.Descripcion);
                }

                if ((null != Patente.Asociado) && (!Patente.Asociado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteIdAsociado, Patente.Asociado.Id);
                    variosFiltros = true;
                }

                if ((null != Patente.Interesado) && (!Patente.Interesado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteIdInteresado, Patente.Interesado.Id);
                    variosFiltros = true;
                }

                //if (!string.IsNullOrEmpty(Patente.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteFichas, Patente.Fichas);
                //}

                if ((null != Patente.FechaPublicacion) && (!Patente.FechaPublicacion.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", Patente.FechaPublicacion);
                    string fecha2 = String.Format("{0:dd/MM/yy}", Patente.FechaPublicacion.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteFecha, fecha, fecha2);
                }

                //if (null != Patente.Recordatorio)
                //{
                //    if (variosFiltros)
                //        filtro += " and ";

                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteRecordatorio, Patente.Recordatorio);
                //}


                IQuery query = Session.CreateQuery(cabecera + filtro);
                Patentes = query.List<Patente>();

                ////Busca la lista de Anualidad por cada Patente
                //foreach (Patente aux in Patentes)
                //{

                //    string CabeceraBase = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAnualidadPorIdPatente, aux.Id);
                //    IQuery query2 = Session.CreateQuery(CabeceraBase);
                //    aux.Anualidades = query2.List<Anualidad>();


                //}

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExPatentesFiltro);
            }
            finally
            {
                Session.Close();
            }
            return Patentes;
        }

        /// <summary>
        /// Método que obtiene una patente con todos sus objetos
        /// </summary>
        /// <param name="Patente">Patente a consultar</param>
        /// <returns>Patente con todos los objetos que la componen</returns>
        public Patente ObtenerPatenteConTodo(Patente Patente)
        {
            Patente retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerPatenteConTodo, Patente.Id));
                retorno = query.UniqueResult<Patente>();

                string CabeceraBase = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAnualidadPorIdPatente, retorno.Id);
                IQuery query2 = Session.CreateQuery(CabeceraBase);
                retorno.Anualidades = query2.List<Anualidad>();



                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerPatenteConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }

        /// <summary>
        /// Método que obtiene las fechas de una patente
        /// </summary>
        /// <param name="Patente">Patente a consultarle las fechas</param>
        /// <returns>Lista de fechas de la patente</returns>
        public IList<Fecha> ObtenerFechasPatente(Patente Patente)
        {
            IList<Fecha> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFechaPatente, Patente.Id));
                retorno = query.List<Fecha>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExObtenerFechasPatente);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }

    }
}
