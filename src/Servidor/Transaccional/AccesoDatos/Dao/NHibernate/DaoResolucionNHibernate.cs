using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Configuration;
using NHibernate;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoResolucionNHibernate : DaoBaseNHibernate<Resolucion, string>, IDaoResolucion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool VerificarExistenciaResolucion(Resolucion resolucion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.VerificarExistenciaResolucion, resolucion.Id, resolucion.FechaResolucion.ToShortDateString(), resolucion.Boletin.Id));
                Resolucion resolucionExistente = query.UniqueResult<Resolucion>();


                if (resolucionExistente != null)
                    existe = true;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del metodo {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario);
            }
            finally
            {
                Session.Close();
            }

            return existe;
        }
        
    }
}
