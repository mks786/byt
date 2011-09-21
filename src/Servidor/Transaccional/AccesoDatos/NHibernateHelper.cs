using System;
using NHibernate;
using NHibernate.Cfg;

namespace Trascend.Bolet.AccesoDatos
{
    public class NHibernateHelper
    {
        private static readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Constructor estático en donde se inicializa la sesion de NHibernate
        /// </summary>
        static NHibernateHelper()
        {
            try
            {
                Configuration cfg = new Configuration();
                _sessionFactory = cfg.Configure().BuildSessionFactory();
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Metodo estático para solicitar la sesion de NHibernate
        /// </summary>
        /// <returns>retorna la sesion abierta</returns>
        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        /// <summary>
        /// Metodo que retorna la sesion abierta
        /// </summary>
        /// <returns>la sesion</returns>
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }
    }
}
