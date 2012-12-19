using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoLicenciaPatenteNHibernate : DaoBaseNHibernate<LicenciaPatente, int>, IDaoLicenciaPatente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las LicenciaPatentes dado unos parametros
        /// </summary>
        /// <param name="licencia">LicenciaPatentes con parametros</param>
        /// <returns>Lista de LicenciaPatentes con datos solicitados</returns>
        public IList<LicenciaPatente> ObtenerLicenciasPatenteFiltro(LicenciaPatente licencia)
        {
            IList<LicenciaPatente> LicenciaPatentes = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerLicenciaPatente);
                if ((null != licencia) && (licencia.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerLicenciaId, licencia.Id);
                    variosFiltros = true;
                }
                if ((null != licencia.Patente) && (!licencia.Patente.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerLicenciaIdPatente, licencia.Patente.Id);
                    variosFiltros = true;
                }
                //if ((null != fusion.Interesado) && (!fusion.Interesado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteIdInteresado, fusion.Interesado.Id);
                //    variosFiltros = true;
                //}
                //if (!string.IsNullOrEmpty(fusion.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteFichas, fusion.Fichas);
                //}
                //if (!string.IsNullOrEmpty(fusion.Descripcion))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerPatenteDescripcion, fusion.Descripcion);
                //}
                if ((null != licencia.Fecha) && (!licencia.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", licencia.Fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", licencia.Fecha.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerLicenciaFecha, fecha, fecha2);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                LicenciaPatentes = query.List<LicenciaPatente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerLicenciaPatente);
            }
            finally
            {
                Session.Close();
            }
            return LicenciaPatentes;
        }
    }
}
