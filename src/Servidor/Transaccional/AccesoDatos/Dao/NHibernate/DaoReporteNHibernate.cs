using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoReporteNHibernate : DaoBaseNHibernate<Reporte, int>, IDaoReporte
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene un conjunto de Reportes por medio de un filtro
        /// </summary>
        /// <param name="reporteDeMarca">Reporte usado como filtro</param>
        /// <returns>Lista de reportes por filtro</returns>
        public IList<Reporte> ObtenerReporteFiltro(Reporte reporte)
        {
            IList<Reporte> reportes = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerReporte);

                
                if ((null != reporte) && (reporte.Id != int.MinValue))
                {

                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteId, reporte.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(reporte.Descripcion))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteDescripcion, reporte.Descripcion.ToUpper());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(reporte.TituloEspanol))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteTituloEspanol, reporte.TituloEspanol.ToUpper());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(reporte.TituloIngles))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteTituloIngles, reporte.TituloIngles.ToUpper());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(reporte.Usuario))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteUsuario, reporte.Usuario);
                    variosFiltros = true;
                }

                if ((null != reporte.Idioma) && (!reporte.Idioma.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteIdioma, reporte.Idioma.Id);
                    variosFiltros = true;
                }

                if ((null != reporte.VistaReporte) && (reporte.VistaReporte.Id != 0) && (reporte.VistaReporte.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerReporteVistaReporte, reporte.VistaReporte.Id);
                    variosFiltros = true;
                }
                
                IQuery query = Session.CreateQuery(cabecera + filtro + " order by r.Id asc");
                reportes = query.List<Reporte>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerAsociadoConTodo);
            }
            finally
            {
                Session.Close();
            }

            return reportes;
        }



        public Reporte ObtenerReporteConTodo(Reporte reporte)
        {
            Reporte retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerReporteConTodo, reporte.Id));
                retorno = query.UniqueResult<Reporte>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerMarcaConTodo);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        
    }
}
