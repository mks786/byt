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
                query.SetParameter<string>("usr", parametro.Usuario.Iniciales);
                query.SetParameter<int>("way", parametro.Via);
                query.SetParameter<int>("cod", parametro.Id);

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


        /// <summary>
        /// Método que ejecuta un procedimiento en base de datos
        /// </summary>
        /// <param name="usuario">parámetro que contiene todos los datos necesarios para ejecutar el procedimiento</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
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
