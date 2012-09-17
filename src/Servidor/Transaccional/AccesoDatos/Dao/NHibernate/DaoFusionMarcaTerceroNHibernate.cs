using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;
using System.Configuration;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoFusionMarcaTerceroNHibernate : DaoBaseNHibernate<FusionMarcaTercero, int>, IDaoFusionMarcaTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public FusionMarcaTercero FusionConsultarFusionMarcaTerceroPorFusion(Fusion fusion)
        {
            FusionMarcaTercero retorno = new FusionMarcaTercero();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFusionMarcaTercero, fusion.Id));
                retorno = query.UniqueResult<FusionMarcaTercero>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcaConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }

    }
}
