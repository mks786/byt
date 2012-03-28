using System.Collections.Generic;
using NLog;
using System;
using NHibernate;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoMarcaBaseTerceroNHibernate : DaoBaseNHibernate<MarcaBaseTercero, int>, IDaoMarcaBaseTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<MarcaBaseTercero> ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero)
        {
            IList<MarcaBaseTercero> MarcasTercero = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMarcaBaseTercero);
                if ((null != marcaBaseTercero) && (marcaBaseTercero.Id != null))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaBaseTerceroId, "'" + marcaBaseTercero.Id + "'");
                    variosFiltros = true;
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                MarcasTercero = query.List<MarcaBaseTercero>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
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


        public int ObtenerMaxSecuencia()
        {
            int idConsultado;
            string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxSecuenciaMarcaBaseTercero);
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
