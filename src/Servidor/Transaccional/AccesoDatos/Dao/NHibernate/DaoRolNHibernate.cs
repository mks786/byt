using System;
using System.Collections.Generic;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoRolNHibernate : DaoBaseNHibernate<Rol, string>, IDaoRol
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public IList<Rol> ObteneRolesYObjetos()
        {
            IList<Rol> roles;

            try
            {
                IQuery query = Session.CreateQuery(Recursos.ConsultasHQL.ObtenerRolesYObjetos);
                roles = query.List<Rol>();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return roles;
        }
        
    }
}
