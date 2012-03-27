using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoLicenciaNHibernate : DaoBaseNHibernate<Licencia, int>, IDaoLicencia
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licencia"></param>
        /// <returns></returns>
        public IList<Licencia> ObtenerLicenciasFiltro(Licencia licencia)
        {
            IList<Licencia> Licencias = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerLicencia);
                if ((null != licencia) && (licencia.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerLicenciaId, licencia.Id);
                    variosFiltros = true;
                }
                if ((null != licencia.Marca) && (!licencia.Marca.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerLicenciaIdMarca, licencia.Marca.Id);
                    variosFiltros = true;
                }
                //if ((null != fusion.Interesado) && (!fusion.Interesado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaIdInteresado, fusion.Interesado.Id);
                //    variosFiltros = true;
                //}
                //if (!string.IsNullOrEmpty(fusion.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFichas, fusion.Fichas);
                //}
                //if (!string.IsNullOrEmpty(fusion.Descripcion))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDescripcion, fusion.Descripcion);
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
                Licencias = query.List<Licencia>();

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
            return Licencias;
        }
    }
}
