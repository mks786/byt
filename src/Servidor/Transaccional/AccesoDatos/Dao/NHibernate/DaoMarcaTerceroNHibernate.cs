﻿using System.Collections.Generic;
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
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroId,marcaTercero.Id);
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
                if ((null != marcaTercero.FechaPublicacion) && (!marcaTercero.FechaPublicacion.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", marcaTercero.FechaPublicacion);
                    string fecha2 = String.Format("{0:dd/MM/yy}", marcaTercero.FechaPublicacion.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaTerceroFechas, fecha, fecha2);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                MarcasTercero = query.List<MarcaTercero>();

                //Busca la lista de marcaBaseTercero por cada marcaTercero
                foreach (MarcaTercero aux in MarcasTercero)
                {
                    string CabeceraBase = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarcaBaseTercero, aux.Id, aux.Anexo);
                    IQuery query2 = Session.CreateQuery(CabeceraBase);
                    aux.MarcasBaseTercero=query2.List<MarcaBaseTercero>();

                }

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
            return MarcasTercero;
        }



        public string ObtenerMaxId(string maxId)
        {
            string idConsultado;
            string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxIdMarcaTercero, maxId);
            IQuery query = Session.CreateQuery(consulta);
            idConsultado = query.UniqueResult<string>();


            return idConsultado;
        }

        public int ObtenerMaxAnexo(string maxAnexo)
        {
            int idConsultado;
            string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxAnexoMarcaTercero, maxAnexo);
            IQuery query = Session.CreateQuery(consulta);
            idConsultado = query.UniqueResult<int>();


            return idConsultado;
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