using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoContactoCxPNHibernate : DaoBaseNHibernate<ContactoCxP, int>, IDaoContactoCxP 
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene una lista de ContactosCxP de acuerdo a un filtro dado
        /// </summary>
        /// <param name="contactoCxP">ContactoCxP usado como filtro</param>
        /// <returns>Lista de Contactos CxP encontrados</returns>
        public IList<ContactoCxP> ObtenerContactosCxPFiltro(ContactoCxP contactoCxP)
        {
            IList<ContactoCxP> contactosCxP = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerContactoCxP);

                //Por código
                if ((null != contactoCxP) && (contactoCxP.Id != 0) && (contactoCxP.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroIdContactoCxP, contactoCxP.Id);
                    variosFiltros = true;
                }

                //Por Asociado
                if (contactoCxP.Asociado != null)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroAsociadoContactoCxP, contactoCxP.Asociado.Id);
                    variosFiltros = true;
                }

                 
                filtro += " order by c.Asociado DESC, c.Id DESC";

                IQuery query = Session.CreateQuery(cabecera + filtro);
                contactosCxP = query.List<ContactoCxP>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar ContactoCxP por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return contactosCxP;
        }

    }
}
