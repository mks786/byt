using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Collections;
using NHibernate.Criterion;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAsociadoNHibernate : DaoBaseNHibernate<Asociado, int>, IDaoAsociado
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Consulta un Asociado coon toda su informacion
        /// </summary>
        /// <param name="asociado">Asociado con parametros</param>
        /// <returns>Asociado </returns>
        public Asociado ObtenerAsociadoConTodo(Asociado asociado)
        {
            Asociado retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAsociadoConTodo, asociado.Id));
                retorno = query.UniqueResult<Asociado>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadoConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        /// <summary>
        /// Consulta los asociados dado unos parametros determinados
        /// </summary>
        /// <param name="asociado">asociado con parametros</param>
        /// <returns>lista de asociados</returns>
        public IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado)
        {

            IList<Asociado> asociados = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAsociado);
                if ((null != asociado) && (asociado.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoId, asociado.Id);
                    variosFiltros = true;
                }
                if (!string.IsNullOrEmpty(asociado.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAsociadoNombre, asociado.Nombre);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                asociados = query.List<Asociado>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadosFiltro);
            }
            finally
            {
                Session.Close();
            }
            return asociados;
        }
    }
}
