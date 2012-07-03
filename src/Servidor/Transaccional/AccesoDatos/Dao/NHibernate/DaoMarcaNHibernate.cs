using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;
using System.Configuration;
using NHibernate.Criterion;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoMarcaNHibernate : DaoBaseNHibernate<Marca, int>, IDaoMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las marcas dado unos parametros
        /// </summary>
        /// <param name="marca">marca con parametros</param>
        /// <returns>Lista de marcas solicitadas</returns>
        public IList<Marca> ObtenerMarcasFiltro(Marca marca)
        {
            IList<Marca> Marcas = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarca);

                #region Filtros de MarcaNacional

                if ((null != marca) && (marca.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaId, marca.Id);
                    variosFiltros = true;
                }

                if ((null != marca.Asociado) && (!marca.Asociado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaIdAsociado, marca.Asociado.Id);
                    variosFiltros = true;
                }

                if ((null != marca.Interesado) && (!marca.Interesado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaIdInteresado, marca.Interesado.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(marca.Fichas))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFichas, marca.Fichas);
                }

                if (!string.IsNullOrEmpty(marca.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDescripcion, marca.Descripcion);
                }

                if ((null != marca.FechaPublicacion) && (!marca.FechaPublicacion.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", marca.FechaPublicacion);
                    string fecha2 = String.Format("{0:dd/MM/yy}", marca.FechaPublicacion.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFecha, fecha, fecha2);
                }

                if (null != marca.Recordatorio)
                {
                    if (variosFiltros)
                        filtro += " and ";

                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaRecordatorio, marca.Recordatorio);
                }

                if ((null != marca.Nacional) && (marca.Nacional.Id != 0))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaNacional, marca.Nacional.Id);
                    variosFiltros = true;
                }

                if ((null != marca.Internacional) && (marca.Internacional.Id != 0))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaInternacional, marca.Internacional.Id);
                    variosFiltros = true;
                }

                if ((null != marca.Servicio))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaServicio, marca.Servicio.Id);
                    variosFiltros = true;
                }

                if ((null != marca.TipoEstado))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTipoEstado, marca.TipoEstado.Id);
                    variosFiltros = true;
                }

                if ((null != marca.Corresponsal))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaCorresponsal, marca.Corresponsal.Id);
                    variosFiltros = true;
                }

                if ((null != marca.CodigoInscripcion ))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaSolicitud, marca.CodigoInscripcion);
                    variosFiltros = true;
                }

                if ((null != marca.Distingue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDistingue, marca.Distingue);
                    variosFiltros = true;
                }

                #endregion

                #region Filtros Marca TYR

                if ((null != marca.CodigoRegistro) && (!marca.CodigoRegistro.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaRegistro, marca.CodigoRegistro);
                    variosFiltros = true;
                }

                if ((null != marca.FechaRegistro) && (!marca.FechaRegistro.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", marca.FechaRegistro);
                    string fecha2 = String.Format("{0:dd/MM/yy}", marca.FechaRegistro.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFechaRegistro, fecha, fecha2);
                }

                if ((null != marca.NumeroCondiciones) && (!marca.NumeroCondiciones.Equals(int.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaNumeroCondiciones, marca.NumeroCondiciones);
                    variosFiltros = true;
                }

                if (marca.BInstruccionesRenovacion)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaInstruccionesRenovacion, marca.Rev);
                    variosFiltros = true;
                }

                if (marca.BRenovacionOtroTramitante)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaRenovacionOtroTramitante, marca.Ter);
                    variosFiltros = true;
                }

                #endregion

                #region Filtros Marca Boletines

                if ((null != marca.BoletinPublicacion) && (!marca.BoletinPublicacion.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBoletinPublicacion, marca.BoletinPublicacion.Id);
                    variosFiltros = true;
                }

                if ((null != marca.BoletinConcesion) && (!marca.BoletinConcesion.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBoletinConcesion, marca.BoletinConcesion.Id);
                    variosFiltros = true;
                }

                if ((null != marca.BoletinOrdenPublicacion) && (!marca.BoletinOrdenPublicacion.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBoletinOrdenPub, marca.BoletinOrdenPublicacion.Id);
                    variosFiltros = true;
                }

                #endregion

                #region Filtro Marca Prioridad

                if ((null != marca.CPrioridad) && (!marca.CPrioridad.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaPrioridad, marca.CPrioridad);
                    variosFiltros = true;
                }

                if ((null != marca.FechaPrioridad) && (!marca.FechaPrioridad.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", marca.FechaPrioridad);
                    string fecha2 = String.Format("{0:dd/MM/yy}", marca.FechaPrioridad.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFechaPrioridad, fecha, fecha2);
                }

                if ((null != marca.Pais) && (marca.Pais.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaPais, marca.Pais.Id);
                    variosFiltros = true;
                }

                #endregion

                IQuery query = Session.CreateQuery(cabecera + filtro);
                Marcas = query.List<Marca>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcasFiltro);
            }
            finally
            {
                Session.Close();
            }
            return Marcas;
        }


        /// <summary>
        /// Metodo que Obtiene la marca con todos sus datos
        /// </summary>
        /// <param name="marca">marca</param>
        /// <returns>Marca con toda su informacion</returns>
        public Marca ObtenerMarcaConTodo(Marca marca)
        {
            Marca retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerMarcaConTodo, marca.Id));
                retorno = query.UniqueResult<Marca>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcaConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que obtine las marcas dada una fecha de renovacion
        /// </summary>
        /// <param name="marca">marca con parametros</param>
        /// <param name="fechas">fecha como parametro</param>
        /// <returns>la lista de marcas con esa fecha de renovacion</returns>
        public IList<Marca> ObtenerMarcasPorFechaRenovacion(Marca marca, DateTime[] fechas)
        {
            IList<Marca> Marcas = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarca);

               
                string fecha = String.Format("{0:dd/MM/yy}", fechas[0]);
                string fecha2 = String.Format("{0:dd/MM/yy}", fechas[1]);
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFechaRenovacion, fecha, fecha2);

                variosFiltros = true;
               

                if (null != marca.Recordatorio)
                {
                    if (variosFiltros)
                        filtro += " or ";

                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaRecordatorio, marca.Recordatorio);
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                Marcas = query.List<Marca>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcasPorFechaRenovacion);
            }
            finally
            {
                Session.Close();
            }
            return Marcas;
        }


        /// <summary>
        /// Metodo que obtine las marcas dada una fecha de renovacion
        /// </summary>
        /// <param name="RecordatorioVista">recordatorio con parametros</param>
        /// <returns>la lista de recordatorios</returns>
        public IList<RecordatorioVista> ObtenerRecordatoriosVista(RecordatorioVista recordatorio, DateTime[] fechas)
        {
            IList<RecordatorioVista> recordatorios = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                //recordatorio.Mes = "09";
                //recordatorio.Ano = "2012";
                //Marca marca = new Marca();
                //marca.Recordatorio = 1;
                //recordatorio.Marca = marca;
                

                //bool variosFiltros = false;
                //string filtro = "";


                recordatorios = Session.CreateCriteria(typeof(RecordatorioVista))
                       .CreateAlias("Marca", "m")
                       .SetFetchMode("Asociado", FetchMode.Join)
                       .SetFetchMode("Marca", FetchMode.Join)
                       .Add(Restrictions.Between("m.FechaRenovacion",fechas[0],fechas[1]))
                       .List<RecordatorioVista>();

                //string fecha = String.Format("{0:dd/MM/yy}", fechas[0]);
                //string fecha2 = String.Format("{0:dd/MM/yy}", fechas[1]);
                //filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFechaRenovacion, fecha, fecha2);

                //variosFiltros = true;


                //if (null != marca.Recordatorio)
                //{
                //    if (variosFiltros)
                //        filtro += " or ";

                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaRecordatorio, marca.Recordatorio);
                //}

                //IQuery query = Session.CreateSQLQuery(cabecera + filtro);
                //recordatorios = query.List<RecordatorioVista>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcasPorFechaRenovacion);
            }
            finally
            {
                Session.Close();
            }
            return recordatorios;
        }
    }
}
