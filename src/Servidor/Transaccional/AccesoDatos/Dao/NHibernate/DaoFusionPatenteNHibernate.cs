using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoFusionPatenteNHibernate : DaoBaseNHibernate<FusionPatente, int>, IDaoFusionPatente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta las fusionePatente dado unos parametros
        /// </summary>
        /// <param name="FusionPatente">FusionPatente con parametros</param>
        /// <returns>Lista de FusionesPatente solicitados</returns>
        public IList<FusionPatente> ObtenerFusionesPatenteFiltro(FusionPatente fusion)
        {
            IList<FusionPatente> FusionPatentees = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerFusionPatente);
                if ((null != fusion) && (fusion.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerFusionId, fusion.Id);
                    variosFiltros = true;
                }
                if ((null != fusion.Patente) && (!fusion.Patente.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerFusionIdPatente, fusion.Patente.Id);
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
                if ((null != fusion.Fecha) && (!fusion.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yyyy}", fusion.Fecha);
                    //string fecha2 = String.Format("{0:dd/MM/yy}", fusion.Fecha.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerFusionFecha, fecha);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                FusionPatentees = query.List<FusionPatente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerFusionPorPatente);
            }
            finally
            {
                Session.Close();
            }
            return FusionPatentees;
        }

    }
}
