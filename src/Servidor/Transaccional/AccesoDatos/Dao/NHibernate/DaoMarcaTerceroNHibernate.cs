using System.Collections.Generic;
using NLog;
using System;
using NHibernate;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoMarcaTerceroNHibernate : DaoBaseNHibernate<MarcaTercero, int>, IDaoMarcaTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las MarcaTercero dado unos parametros
        /// </summary>
        /// <param name="marcaTercero">MarcaTercero </param>
        /// <returns>Todas las MarcaTercero solicitados</returns>
        public IList<MarcaTercero> ObtenerMarcaTerceroFiltro(MarcaTercero marcaTercero)
        {
            IList<MarcaTercero> MarcasTercero = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarcaTercero);
                if ((null != marcaTercero) && (marcaTercero.Id != null))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroId, marcaTercero.Id);
                    variosFiltros = true;
                }
                if (null != marcaTercero.CodigoInscripcion)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroSolicitud, marcaTercero.CodigoInscripcion);
                    variosFiltros = true;
                }
                if ((null != marcaTercero.Asociado) && (!marcaTercero.Asociado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroAsociadoId, marcaTercero.Asociado.Id);
                    variosFiltros = true;
                }
                if ((null != marcaTercero.Interesado) && (!marcaTercero.Interesado.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroInteresadoId, marcaTercero.Interesado.Id);
                    variosFiltros = true;
                }
                if (null != marcaTercero.Internacional)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaInternacional, marcaTercero.Internacional.Id);
                    variosFiltros = true;
                }
                if (null != marcaTercero.Nacional)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaNacional, marcaTercero.Nacional.Id);
                    variosFiltros = true;
                }
                if (!string.IsNullOrEmpty(marcaTercero.Fichas))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroFichas, marcaTercero.Fichas);
                }
                if (!string.IsNullOrEmpty(marcaTercero.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDescripcion, marcaTercero.Descripcion);
                }
                if (null != marcaTercero.Distingue)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDistingue, marcaTercero.Distingue);
                    variosFiltros = true;
                }
                if (null != marcaTercero.CasoT)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTipoDeCaso, marcaTercero.CasoT);
                    variosFiltros = true;
                }
                if (null != marcaTercero.EstadoMarca)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaEstadoMarca, marcaTercero.EstadoMarca);
                    variosFiltros = true;
                }
                if ((null != marcaTercero.FechaPublicacion) && (!marcaTercero.FechaPublicacion.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", marcaTercero.FechaPublicacion);
                    string fecha2 = String.Format("{0:dd/MM/yy}", marcaTercero.FechaPublicacion.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroFechas, fecha, fecha2);
                }
                if (null != marcaTercero.Servicio)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaServicio, marcaTercero.Servicio.Id);
                    variosFiltros = true;
                }
                #region Filtros Marca Boletines

                if ((null != marcaTercero.BoletinPublicacion) && (!marcaTercero.BoletinPublicacion.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBoletinPublicacion, marcaTercero.BoletinPublicacion.Id);
                    variosFiltros = true;
                }

                if ((null != marcaTercero.BoletinConcesion) && (!marcaTercero.BoletinConcesion.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBoletinConcesion, marcaTercero.BoletinConcesion.Id);
                    variosFiltros = true;
                }


                #endregion


                IQuery query = Session.CreateQuery(cabecera + filtro);
                MarcasTercero = query.List<MarcaTercero>();

                //Busca la lista de marcaBaseTercero por cada marcaTercero
                //foreach (MarcaTercero aux in MarcasTercero)
                //{
                //    string CabeceraBase = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarcaBaseTercero, aux.Id, aux.Anexo);
                //    IQuery query2 = Session.CreateQuery(CabeceraBase);
                //    aux.MarcasBaseTercero = query2.List<MarcaBaseTercero>();

                //}

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcaTerceroFiltro);
            }
            finally
            {
                Session.Close();
            }
            return MarcasTercero;
        }


        /// <summary>
        /// Metodo que obtiene el ultimo id de una marca tercero
        /// </summary>
        /// <param name="maxId">id de la marcatercero</param>
        /// <returns>El id a utilizar</returns>
        public string ObtenerMaxId(string maxId)
        {
            string idConsultado;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxIdMarcaTercero, maxId);
                IQuery query = Session.CreateQuery(consulta);
                idConsultado = query.UniqueResult<string>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMaxIdMarcaTercero);
            }
            finally
            {
                Session.Close();
            }

            return idConsultado;
        }


        /// <summary>
        /// Metodo queobtiene el ultimo anexo de la MarcaTercero
        /// </summary>
        /// <param name="maxAnexo">El ultimo Anexo</param>
        /// <returns>El ultimo anexo de la marcatercero</returns>
        public int ObtenerMaxAnexo(string maxAnexo)
        {
            int idConsultado;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxAnexoMarcaTercero, maxAnexo);
                IQuery query = Session.CreateQuery(consulta);
                idConsultado = query.UniqueResult<int>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMaxAnexoMarcaTercero);
            }
            finally
            {
                Session.Close();
            }

            return idConsultado;
        }

        /// <summary>
        /// Metodo queobtiene el ultimo anexo de la MarcaTercero
        /// </summary>
        /// <param name="maxAnexo">El ultimo Anexo</param>
        /// <returns>El ultimo anexo de la marcatercero</returns>
        public bool ObtenerClaseInternacionalMarcaTercero(int ClaseInt,string idMarcaT,int anexo)
        {
            bool retorno = true;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                IList<MarcaTercero> MarcasTercero = null;
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarcaTercero);
                string filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroId, idMarcaT);
                IQuery query = Session.CreateQuery(cabecera + filtro);
                MarcasTercero = query.List<MarcaTercero>();

                foreach (MarcaTercero aux in MarcasTercero)
                {
                   
                        if (aux.Internacional.Id == ClaseInt)
                            retorno = false;
                    
                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMaxAnexoMarcaTercero);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }



        //public Marca ObtenerMarcaConTodo(Marca marca)
        //{
        //    Marca retorno;
        //    try
        //    {
        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion

        //        IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerMarcaConTodo, marca.Id));
        //        retorno = query.UniqueResult<Marca>();

        //        #region trace
        //        if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
        //            logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        throw new ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario);
        //    }
        //    finally
        //    {
        //        Session.Close();
        //    }

        //    return retorno;
        //}

    }
}
