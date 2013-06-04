using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoContactoNHibernate : DaoBaseNHibernate<Contacto, int>, IDaoContacto
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que Consulta los Contactos que tiene un Asociado
        /// </summary>
        /// <param name="asociado">Asociado</param>
        /// <returns>Lista de Contactos del asociado solicitado</returns>
        public IList<Contacto> ObtenerContactosPorAsociado(Asociado asociado)
        {
            IList<Contacto> contactos;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerContactosPorAsociado, asociado.Id) + " order by c.Asociado DESC, c.Id DESC");
                contactos = query.List<Contacto>();

                foreach (Contacto contacto in contactos)
                {
                    contacto.Asociado = contacto.Asociado;

                }

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerContactosPorAsociado);
            }
            finally
            {
                Session.Close();
            }

            return contactos;
        }

        /// <summary>
        /// Método que consulta los contactos por Id
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        public Contacto ConsultarContactoPorId(Contacto contacto)
        {
            Contacto retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerContactoPorId, contacto.Id, contacto.Asociado.Id));
                retorno = query.UniqueResult<Contacto>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerContactosPorAsociado);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        public IList<Contacto> ObtenerContactosFiltro(Contacto contacto)
        {
            IList<Contacto> contactos = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerContacto);

                //Por código
                if ((null != contacto) && (contacto.Id != 0) && (contacto.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroIdContacto, contacto.Id);
                    variosFiltros = true;
                }

                //Por correo
                if (!string.IsNullOrEmpty(contacto.Email))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroCorreoContacto, contacto.Email.ToUpper());
                    variosFiltros = true;
                }

                //Por Asociado
                if (contacto.Asociado != null)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroAsociadoContacto, contacto.Asociado.Id);
                    variosFiltros = true;
                }

                //Por nombre
                if (!string.IsNullOrEmpty(contacto.Nombre))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroNombreContacto, contacto.Nombre.ToUpper());
                    variosFiltros = true;
                }

                //Por Departamento
                if (!string.IsNullOrEmpty(contacto.Departamento))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroDepartamentoContacto, contacto.Departamento.ToUpper());
                    variosFiltros = true;
                }

                //Por Telefono
                if (!string.IsNullOrEmpty(contacto.Telefono))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroTelefonoContacto, contacto.Telefono.ToUpper());
                    variosFiltros = true;
                }

                //Por Fax
                if (!string.IsNullOrEmpty(contacto.Fax))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroFaxContacto, contacto.Fax.ToUpper());
                    variosFiltros = true;
                }

                filtro += " order by c.Asociado DESC, c.Id DESC";

                IQuery query = Session.CreateQuery(cabecera + filtro);
                contactos = query.List<Contacto>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
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
            return contactos;
        }
    }
}
