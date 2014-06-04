using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCasoBaseNHibernate : DaoBaseNHibernate<CasoBase,string>, IDaoCasoBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene los Casos Base de un Caso
        /// </summary>
        /// <param name="casoBase">Caso Base usado como filtro</param>
        /// <returns>Lista de Casos Base de un Caso</returns>
        public IList<CasoBase> ObtenerCasosBaseDeCaso(CasoBase casoBase)
        {
            IList<CasoBase> casosBase = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCasoBaseCaso);

                if ((null != casoBase) && (null != casoBase.Caso) && (casoBase.Caso.Id != 0) && (casoBase.Caso.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoBaseIdCaso, casoBase.Caso.Id.ToString());
                    variosFiltros = true;
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                casosBase = query.List<CasoBase>();

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
            return casosBase;
        }
       
    }
}
