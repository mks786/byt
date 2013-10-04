using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoFiltroPlantillaNHibernate: DaoBaseNHibernate<FiltroPlantilla,int>, IDaoFiltroPlantilla
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene las variables de encabezado de un maestro de plantilla determinado
        /// </summary>
        /// <param name="plantilla">Maestro de Plantilla a consultar</param>
        /// <returns>Lista de variables de encabezado del maestro de plantilla seleccionado; en caso contrario devuelve NULL</returns>
        public IList<FiltroPlantilla> ObtenerFiltrosEncabezadoPlantilla(MaestroDePlantilla plantilla)
        {
            IList<FiltroPlantilla> Encabezados;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFiltroEncabezadoPlantilla, plantilla.Id));
                Encabezados = query.List<FiltroPlantilla>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerFiltroEncabezadoPlantilla + ": " + ex.Message);
            }
            finally
            {
                Session.Close();
            }

            return Encabezados;
        }


        /// <summary>
        /// Metodo que obtiene las variables del detalle de un maestro de plantilla determinado
        /// </summary>
        /// <param name="plantilla">Maestro de plantilla seleccionado</param>
        /// <returns>Lista de variables de detalle del maestro de plantilla consultado; en caso contrario devuelve NULL</returns>
        public IList<FiltroPlantilla> ObtenerFiltrosDetallePlantilla(MaestroDePlantilla plantilla)
        {
            IList<FiltroPlantilla> Encabezados;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFiltroDetallePlantilla, plantilla.Id));
                Encabezados = query.List<FiltroPlantilla>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerFiltroDetallePlantilla + ": " + ex.Message);
            }
            finally
            {
                Session.Close();
            }

            return Encabezados;
        }

    }
}
