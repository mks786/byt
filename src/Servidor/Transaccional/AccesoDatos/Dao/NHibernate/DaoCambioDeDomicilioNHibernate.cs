using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCambioDeDomicilioNHibernate : DaoBaseNHibernate<CambioDeDomicilio, int>, IDaoCambioDeDomicilio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Metodo que consulta los Cambios de Domicilios dado unos parametros
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio con parametros</param>
        /// <returns>una lista de CambioDeDomicilio</returns>
        public IList<CambioDeDomicilio> ObtenerCambiosDeDomicilioFiltro(CambioDeDomicilio cambioDeDomicilio)
        {
            IList<CambioDeDomicilio> CambioDeDomicilios = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCambioDeDomicilio);

                if ((null != cambioDeDomicilio) && (cambioDeDomicilio.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeDomicilioId, cambioDeDomicilio.Id);
                    variosFiltros = true;
                }
                if ((null != cambioDeDomicilio.Marca) && (!cambioDeDomicilio.Marca.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeDomicilioIdMarca, cambioDeDomicilio.Marca.Id);
                    variosFiltros = true;
                }
                if (null != cambioDeDomicilio.CadenaDeCambios)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeDomicilioCadenaDeCambios, cambioDeDomicilio.CadenaDeCambios);
                    variosFiltros = true;
                }
                //if ((null != cambioDeDomicilio.Interesado) && (!cambioDeDomicilio.Interesado.Id.Equals("")))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaIdInteresado, cambioDeDomicilio.Interesado.Id);
                //    variosFiltros = true;
                //}
                //if (!string.IsNullOrEmpty(cambioDeDomicilio.Fichas))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaFichas, cambioDeDomicilio.Fichas);
                //}
                //if (!string.IsNullOrEmpty(cambioDeDomicilio.Descripcion))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerMarcaDescripcion, cambioDeDomicilio.Descripcion);
                //}
                //if ((null != cambioDeDomicilio.Fecha) && (!cambioDeDomicilio.Fecha.Equals(DateTime.MinValue)))
                //{
                //    if (variosFiltros)
                //        filtro += " and ";
                //    string fecha = String.Format("{0:dd/MM/yy}", cambioDeDomicilio.Fecha);
                //    string fecha2 = String.Format("{0:dd/MM/yy}", cambioDeDomicilio.Fecha.Value.AddDays(1));
                //    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeDomicilioFecha, fecha, fecha2);
                //}
                IQuery query = Session.CreateQuery(cabecera + filtro);
                CambioDeDomicilios = query.List<CambioDeDomicilio>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerCambioDeDomicilioMarca);
            }
            finally
            {
                Session.Close();
            }
            return CambioDeDomicilios;
        }
    }
}
