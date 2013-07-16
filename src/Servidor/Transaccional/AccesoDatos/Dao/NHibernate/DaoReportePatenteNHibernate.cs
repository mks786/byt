using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoReportePatenteNHibernate : DaoBaseNHibernate<ReportePatente, int>, IDaoReportePatente
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que ejecuta el procedimiento
        /// </summary>
        /// <param name="parametro">Parametro a ejectura</param>
        /// <returns>true si se ejecto, de lo contrario false</returns>
        public bool EjecutarProcedimiento(ParametroProcedimiento parametro)
        {
            bool retorno = true;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.GetNamedQuery(parametro.PaqueteProcedimiento + parametro.NombreProcedimiento);
                query.SetParameter<int>("pcpatente", parametro.Id);

                query.UniqueResult();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                retorno = false;
                logger.Error(ex.Message  + ex.InnerException.Message);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }


        /// <summary>
        /// Método que ejecuta un procedimiento en base de datos
        /// </summary>
        /// <param name="usuario">parámetro que contiene todos los datos necesarios para ejecutar el procedimiento</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        public ReportePatente EjecutarProcedimientoPID(Usuario usuario)
        {
            ReportePatente retorno = new ReportePatente();
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
                throw new ApplicationException(Recursos.Errores.ExEjecutarProcedimientoBD);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }


        public ReportePatente ObtenerPorCodigoPatente(int idPatente)
        {
            ReportePatente retorno = new ReportePatente();
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerReportePatentePorCodigoPatente, idPatente));

                retorno = query.UniqueResult<ReportePatente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExEjecutarProcedimientoBD);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }


        public bool EliminarPorCodigoPatente(int idPatente)
        {
            bool retorno = true;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.EliminarReportePatentePorCodigoPatente, idPatente));

                int a = query.ExecuteUpdate();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExEjecutarProcedimientoBD);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }
    }
}
