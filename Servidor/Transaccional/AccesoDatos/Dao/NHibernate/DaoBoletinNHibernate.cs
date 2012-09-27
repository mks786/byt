using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoBoletinNHibernate : DaoBaseNHibernate<Boletin, int>, IDaoBoletin
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta todas las resoluciones que tiene un boletin
        /// </summary>
        /// <param name="id">id del boletin</param>
        /// <returns>lista de resoluciones del boletin</returns>
        public IList<Resolucion> ObtenerResolucionesDeBoletin(int id)
        {
            IList<Resolucion> retorno = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerResolucionesPorBoletin, id));                
                retorno = query.List<Resolucion>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerResolucionesDeBoletin);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }
    }
}
