﻿using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System;
using NHibernate;
using System.Configuration;

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
    }
}
