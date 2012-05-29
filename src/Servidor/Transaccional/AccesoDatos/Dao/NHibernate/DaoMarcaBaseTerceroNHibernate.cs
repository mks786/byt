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


        /// <summary>
        /// Metodo que consulta las MarcaBaseTercero dado unos parametros
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero </param>
        /// <returns>Todas las MarcaBaseTercero solicitados</returns>
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
                throw new ApplicationException(Recursos.Errores.exObtenerMarcaBaseTerceroFiltro);
            }
            finally
            {
                Session.Close();
            }
            return MarcasTercero;
        }


        /// <summary>
        /// metodo que obtiene el ultimo insert de la secuencia insertada
        /// </summary>
        /// <returns>El numero mayor en la base de datos</returns>
        public int ObtenerMaxSecuencia()
        {
            int idConsultado;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string consulta = string.Format(Recursos.ConsultasHQL.ObtenerMaxSecuenciaMarcaBaseTercero);
                IQuery query = Session.CreateQuery(consulta);
                idConsultado = query.UniqueResult<int>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMaxSecuenciaMarcaBaseTercero);
            }
            finally
            {
                Session.Close();
            }

            return idConsultado;
        }


        /// <summary>
        /// Metodo que consulta las MarcaBaseTercero de una MarcaTercero
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero con los Datos de MarcaTercero</param>
        /// <returns>Todas las MarcaBaseTercero de la MarcaTercero</returns>
        public List<MarcaBaseTercero> ObtenerTodosPorId(MarcaBaseTercero marcaBaseTercero)
        {

            List<MarcaBaseTercero> MarcasBaseTercero = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                string consulta = string.Format(Recursos.ConsultasHQL.ObtenerTodosMarcaBaseTerceroPorId, marcaBaseTercero.MarcaTercero.Id, marcaBaseTercero.MarcaTercero.Anexo);
                IQuery query = Session.CreateQuery(consulta);
                MarcasBaseTercero = (List<MarcaBaseTercero>)query.List<MarcaBaseTercero>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerTodosMarcaBaseTercero);
            }
            finally
            {
                Session.Close();
            }
            return MarcasBaseTercero;


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
