using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;
using System.Configuration;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoListaDatosDominioNHibernate : DaoBaseNHibernate<ListaDatosDominio, int>, IDaoListaDatosDominio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las ListaDeDatosDeDominio dado unos parametros
        /// </summary>
        /// <param name="parametro">ListaDatosDominio conparametros</param>
        /// <returns>Lista de ListaDatosDominio solicitados</returns>
        public IList<ListaDatosDominio> ObtenerListaDatosDominioPorParametro(ListaDatosDominio parametro)
        {
            IList<ListaDatosDominio> listaDatos = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerListaDatosDominioPorParametro, parametro.Filtro));
                listaDatos = query.List<ListaDatosDominio>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerListaDatosDominio);
            }
            finally
            {
                Session.Close();
            }

            return listaDatos;
        }
    }
}
