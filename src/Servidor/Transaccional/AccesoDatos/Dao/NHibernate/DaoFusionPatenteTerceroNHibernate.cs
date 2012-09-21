using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoFusionPatenteTerceroNHibernate : DaoBaseNHibernate<FusionPatenteTercero, int>, IDaoFusionPatenteTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public FusionPatenteTercero ConsultarFusionPatenteTerceroPorFusion(FusionPatente fusion)
        {
            FusionPatenteTercero retorno = new FusionPatenteTercero();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFusionPatenteTercero, fusion.Id));
                retorno = query.UniqueResult<FusionPatenteTercero>();

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
