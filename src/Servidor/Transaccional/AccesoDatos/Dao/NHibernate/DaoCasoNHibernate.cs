using System.Collections.Generic;
using NLog;
using System;
using NHibernate;
using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCasoNHibernate : DaoBaseNHibernate<Caso,int>, IDaoCaso
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que consulta Casos por medio de un filtro determinado
        /// </summary>
        /// <param name="caso">Caso usado como filtro</param>
        /// <returns>Lista de casos que cumplen con el filtro determinado</returns>
        public IList<Caso> ObtenerCasosFiltro(Caso caso)
        {
            IList<Caso> casos = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCasos);

                if ((null != caso) && (caso.Id != 0) && (caso.Id != int.MinValue))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoId, caso.Id.ToString());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(caso.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoDescripcion, caso.Descripcion);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(caso.Origen))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoOrigenDeCaso, caso.Origen);
                    variosFiltros = true;
                }

                if ((null != caso.Fecha) && (!caso.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", caso.Fecha);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoFechaApertura, fecha);
                    variosFiltros = true;
                }

                if ((null != caso.Asociado) && (caso.Asociado.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoAsociado, caso.Asociado.Id.ToString());
                    variosFiltros = true;
                }

                if ((null != caso.Interesado) && (caso.Interesado.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoInteresado, caso.Interesado.Id.ToString());
                    variosFiltros = true;
                }

                if ((null != caso.Servicio) && (!caso.Servicio.Id.Equals("NGN")) && (!caso.Servicio.Id.Equals(String.Empty)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCasoServicio, caso.Servicio.Id);
                    variosFiltros = true;
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                
                casos = query.List<Caso>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exConsultarCasosFiltro + " " + ex.Message);
            }
            finally
            {
                Session.Close();
            }

            return casos;
        }
    }
}
