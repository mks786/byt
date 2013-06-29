using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoCajaNHibernate : DaoBaseNHibernate<TipoCaja, string>, IDaoTipoCaja
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para obtener los tipos de cajas ya sea por Marca o por Patente
        /// </summary>
        /// <param name="paramtroNacional">Marca o Patente Nacional</param>
        /// <param name="parametroInternacional">Marca o Patente Internacional</param>
        /// <returns>Lista de Tipos de Documentos por Marca o Por Patente</returns>
        public IList<TipoCaja> ObtenerTiposCajasMarcaOPatente(string parametroFiltro)
        {
            IList<TipoCaja> retorno = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método1 {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerTipoCajaPorMarcaOPorPatente, parametroFiltro));
                retorno = query.List<TipoCaja>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método1 {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Hubo un error obteniendo los Tipos de Documentos");
                //throw new ApplicationException(Recursos.Errores.exObtenerMarcasPorFechaRenovacion);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }
    }
}
