using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoMaestroDePlantillaNHibernate: DaoBaseNHibernate<MaestroDePlantilla,int>,IDaoMaestroDePlantilla
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para obtener maestros de plantilla por filtro
        /// </summary>
        /// <param name="maestroDePlantilla">Maestro de plantilla filtro</param>
        /// <returns>Lista de maestros de plantilla obtenidas por un filtro</returns>
        public IList<MaestroDePlantilla> ObtenerMaestroPlantillaFiltro(MaestroDePlantilla maestroDePlantilla)
        {
            IList<MaestroDePlantilla> maestrosPlantilla;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerMaestroPlantilla);


                if ((null != maestroDePlantilla.Plantilla) && (maestroDePlantilla.Plantilla.Id != int.MinValue))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaPlantilla, maestroDePlantilla.Plantilla.Id);
                    variosFiltros = true;
                }

                if ((null != maestroDePlantilla.Idioma) && (!maestroDePlantilla.Idioma.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaIdioma, maestroDePlantilla.Idioma.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(maestroDePlantilla.Referido))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaReferido, maestroDePlantilla.Referido);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(maestroDePlantilla.Criterio))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaCriterio, maestroDePlantilla.Criterio);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(maestroDePlantilla.SQL_Encabezado))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaEncabezado, maestroDePlantilla.SQL_Encabezado);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(maestroDePlantilla.SQL_Detalle))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMaestroPlantillaDetalle, maestroDePlantilla.SQL_Detalle);
                    variosFiltros = true;
                }


                IQuery query = Session.CreateQuery(cabecera + filtro + " order by mp.Id asc");
                maestrosPlantilla = query.List<MaestroDePlantilla>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInfobolesPorMarca);
            }
            finally
            {
                Session.Close();
            }

            return maestrosPlantilla;
        }
    }
}
