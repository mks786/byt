using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInteresadoMultipleNHibernate : DaoBaseNHibernate<InteresadoMultiple, int>, IDaoInteresadoMultiple
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que obtiene los interesados de una patente especifica
        /// </summary>
        /// <param name="patente">Patente a consultar</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        public IList<InteresadoMultiple> ObtenerInteresadosPorPatente(Patente patente)
        {
            IList<InteresadoMultiple> interesados;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInteresadosPorPatente, patente.Id));
                interesados = query.List<InteresadoMultiple>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInventoresPorPatente);
            }
            finally
            {
                Session.Close();
            }

            return interesados;
        }


        /// <summary>
        /// Metodo que obtiene los interesados de una marca especifica
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Lista de interesados asociados a una marca especifica</returns>
        public IList<InteresadoMultiple> ObtenerInteresadosPorMarca(Marca marca)
        {
            IList<InteresadoMultiple> interesados;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInteresadosPorMarca, marca.Id));
                interesados = query.List<InteresadoMultiple>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInventoresPorPatente);
            }
            finally
            {
                Session.Close();
            }

            return interesados;
        }
    }
}
