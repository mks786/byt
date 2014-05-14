using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPresentacionSapiDetalleNHibernate : DaoBaseNHibernate<PresentacionSapiDetalle,int>, IDaoPresentacionSapiDetalle
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene Solicitudes de Presentacion Sapi por filtro
        /// </summary>
        /// <param name="presentacionSapiDetalle">Presentacion filtro</param>
        /// <returns>Lista de Solicitudes de Presentacion Sapi resultantes de la consulta</returns>
        public IList<PresentacionSapiDetalle> ObtenerPresentacionesSapiDetalleFiltro(PresentacionSapiDetalle presentacionSapiDetalle)
        {
            IList<PresentacionSapiDetalle> presentacionesConDetalle = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerPresentacionSapiDetalle);

                //Por código de Compra SAPI
                if ((presentacionSapiDetalle.Presentacion_Enc != null) && (presentacionSapiDetalle.Presentacion_Enc.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiIdEnc, presentacionSapiDetalle.Presentacion_Enc.Id);
                    variosFiltros = true;
                }


                if ((presentacionSapiDetalle.Presentacion_Enc != null) && (presentacionSapiDetalle.Presentacion_Enc.Fecha != null))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", presentacionSapiDetalle.Presentacion_Enc.Fecha);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiFecha, fecha);
                    variosFiltros = true;
                }

                if ((presentacionSapiDetalle.Presentacion_Enc.Departamento != null) && (!presentacionSapiDetalle.Presentacion_Enc.Departamento.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiDpto, presentacionSapiDetalle.Presentacion_Enc.Departamento.Id);
                    variosFiltros = true;
                }

                if ((presentacionSapiDetalle.Presentacion_Enc != null) && (!string.IsNullOrEmpty(presentacionSapiDetalle.Presentacion_Enc.Iniciales)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiIniciales, presentacionSapiDetalle.Presentacion_Enc.Iniciales);
                    variosFiltros = true;
                }

                if ((presentacionSapiDetalle.Material != null) && (!presentacionSapiDetalle.Material.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiDocumento, presentacionSapiDetalle.Material.Id);
                    variosFiltros = true;
                }

                if(!string.IsNullOrEmpty(presentacionSapiDetalle.CodExpediente))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiExpediente, presentacionSapiDetalle.CodExpediente);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(presentacionSapiDetalle.StatusDocumento))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiStatusDoc, presentacionSapiDetalle.StatusDocumento);
                    variosFiltros = true;
                }

                if (presentacionSapiDetalle.FechaPres_Gestor2 != null)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", presentacionSapiDetalle.FechaPres_Gestor2);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiFechaPresentASapi, fecha);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(presentacionSapiDetalle.ReceptorMatPresent))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerDetallePresentacionSapiGestor1, presentacionSapiDetalle.ReceptorMatPresent);
                    variosFiltros = true;
                }

                if (!filtro.Equals(String.Empty))
                {
                    filtro += " order by encabezado.Fecha DESC";
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                presentacionesConDetalle = query.List<PresentacionSapiDetalle>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar Solicitudes de Presentacion SAPI por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return presentacionesConDetalle;
        }
    }
}
