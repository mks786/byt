using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoAnualidadNHibernate : DaoBaseNHibernate<Anualidad, int>, IDaoAnualidad
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Anualidad> ObtenerAnualidadesFiltro(int idAnualidad)
        {
            IList<Anualidad> Anualidads = null;
            bool variosFiltros = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAnualidad);

                if ((null != idAnualidad) && (idAnualidad != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadId, idAnualidad);
                    variosFiltros = true;
                }

                //if ((null != Anualidad.Asociado) && (!Anualidad.Asociado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadIdAsociado, Anualidad.Asociado.Id);
                //    variosFiltros = true;
                //}

        

                //if (!string.IsNullOrEmpty(Anualidad.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadFichas, Anualidad.Fichas);
                //}

                //if (!string.IsNullOrEmpty(Anualidad.Descripcion))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadDescripcion, Anualidad.Descripcion);
                //}

                //if ((null != Anualidad.FechaAnualidad) && (!Anualidad.FechaAnualidad.Equals(DateTime.MinValue)))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    string fecha = String.Format("{0:dd/MM/yy}", Anualidad.FechaAnualidad);
                //    string fecha2 = String.Format("{0:dd/MM/yy}", Anualidad.FechaAnualidad.Value.AddDays(1));
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadFecha, fecha, fecha2);
                //}

                //if (null != Anualidad.Recordatorio)
                //{
                //    if (variosFiltros)
                //        filtro += " and ";

                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAnualidadRecordatorio, Anualidad.Recordatorio);
                //}


                IQuery query = Session.CreateQuery(cabecera + filtro);
                Anualidads = query.List<Anualidad>();

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
            return Anualidads;
        }

        public IList<Anualidad> ObtenerAnualidadesPorPatente(int idpatente)
        {
            IList<Anualidad> Anualidads = null;
            bool variosFiltros = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAnualidadesPorPatente,idpatente);


                IQuery query = Session.CreateQuery(cabecera + filtro);
                Anualidads = query.List<Anualidad>();

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
            return Anualidads;
        }


        public int ObtenerMaxIdAnualidad()
        {
            int idConsultado = 0;
            string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxAnualidad);
            IQuery query = Session.CreateQuery(consulta);
            idConsultado = query.UniqueResult<int>();


            return idConsultado;
        }

        public Anualidad ObtenerAnualidadConTodo(Anualidad Anualidad)
        {
            Anualidad retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAnualidadConTodo, Anualidad.Id));
                retorno = query.UniqueResult<Anualidad>();

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

            return retorno;
        }

    }
}
