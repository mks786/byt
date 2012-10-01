using System;
using System.Collections.Generic;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoRolNHibernate : DaoBaseNHibernate<Rol, string>, IDaoRol
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Método que obtiene los roles y los objetos asociados por los mismos
        /// </summary>
        /// <returns>Lista de Roles con sus objetos</returns>
        public IList<Rol> ObtenerRolesYObjetos()
        {
            IList<Rol> roles;

            try
            {

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(Recursos.ConsultasHQL.ObtenerRolesYObjetos);
                roles = query.List<Rol>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExErrorConsultandoObjetos);
            }
            finally
            {
                Session.Close();
            }

            return roles;
        }

    }
}
