using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInstruccionDeRenovacionNHibernate : DaoBaseNHibernate<InstruccionDeRenovacion, int>, IDaoInstruccionDeRenovacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// metodo que consulta las busquedas que tiene una marca
        /// </summary>
        /// <param name="marca">Marca a consultar las busquedas</param>
        /// <returns>Lista de busquedas de la marca solicitada</returns>
        public IList<InstruccionDeRenovacion> ObtenerInstruccionesDeRenovacionPorMarca(Marca marca)
        {
            IList<InstruccionDeRenovacion> busquedas;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerBusquedasPorMarca, marca.Id));
                busquedas = query.List<InstruccionDeRenovacion>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerBusquedasPorMarca);
            }
            finally
            {
                Session.Close();
            }

            return busquedas;
        }
    }
}
