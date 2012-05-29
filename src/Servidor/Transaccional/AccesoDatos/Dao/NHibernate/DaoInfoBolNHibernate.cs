using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInfoBolNHibernate : DaoBaseNHibernate<InfoBol, int>, IDaoInfoBol
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta todos los InfoBoles por marca
        /// </summary>
        /// <param name="marca">Marca</param>
        /// <returns>Lista de Infoboles de la marca solicitada</returns>
        public IList<InfoBol> ObtenerInfoBolesPorMarca(Marca marca)
        {
            IList<InfoBol> InfoBoles;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInfoBolesPorMarcas, marca.Id));
                InfoBoles = query.List<InfoBol>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInfobolesPorMarca);
            }
            finally
            {
                Session.Close();
            }

            return InfoBoles;
        }        
    }
}
