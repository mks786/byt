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

        public IList<Patente> ObtenerPatentesFiltro(Patente Patente)
        {
            IList<Patente> Patentes = null;
            bool variosFiltros = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //string filtro = "";
                //string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerPatente);

                //if ((null != Patente) && (Patente.Id != 0))
                //{
                //    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteId, Patente.Id);
                //    variosFiltros = true;
                //}

                //if ((null != Patente.Asociado) && (!Patente.Asociado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteIdAsociado, Patente.Asociado.Id);
                //    variosFiltros = true;
                //}

                //if ((null != Patente.Interesado) && (!Patente.Interesado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteIdInteresado, Patente.Interesado.Id);
                //    variosFiltros = true;
                //}

                //if (!string.IsNullOrEmpty(Patente.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteFichas, Patente.Fichas);
                //}

                //if (!string.IsNullOrEmpty(Patente.Descripcion))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteDescripcion, Patente.Descripcion);
                //}

                //if ((null != Patente.FechaPublicacion) && (!Patente.FechaPublicacion.Equals(DateTime.MinValue)))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    string fecha = String.Format("{0:dd/MM/yy}", Patente.FechaPublicacion);
                //    string fecha2 = String.Format("{0:dd/MM/yy}", Patente.FechaPublicacion.Value.AddDays(1));
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteFecha, fecha, fecha2);
                //}

                //if (null != Patente.Recordatorio)
                //{
                //    if (variosFiltros)
                //        filtro += " and ";

                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteRecordatorio, Patente.Recordatorio);
                //}


                //IQuery query = Session.CreateQuery(cabecera + filtro);
                //Patentes = query.List<Patente>();

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
            return Patentes;
        }

        public Patente ObtenerPatenteConTodo(Patente Patente)
        {
            //Patente retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                //IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerPatenteConTodo, Patente.Id));
                //retorno = query.UniqueResult<Patente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario);
            }
            finally
            {
                Session.Close();
            }

            //return retorno;
            return null;
        }

    }
}
