using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPlanillaNHibernate : DaoBaseNHibernate<Planilla, int>, IDaoPlanilla
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public bool EjecutarProcedimientoP1(Marca marca, Usuario usuario, int way)
        {
            bool retorno = true;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                
                IQuery query = Session.GetNamedQuery("ProcedimientoP1");
                query.SetParameter<string>("usr", usuario.Iniciales);
                query.SetParameter<int>("way", way);
                query.SetParameter<int>("cod", marca.Id);

                query.UniqueResult();




                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                retorno = false;
                logger.Error(ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }


        public Planilla EjecutarProcedimientoPID(Usuario usuario)
        {
            Planilla retorno = new Planilla();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.GetNamedQuery("ProcedimientoPID");
                query.SetParameter<string>("s", usuario.Iniciales);

                retorno.Id = query.UniqueResult<int>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException();
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }
    }
}
