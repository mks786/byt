using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoSolicitudSapiNHibernate : DaoBaseNHibernate<SolicitudSapi,int>, IDaoSolicitudSapi
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene Solicitudes Sapi por filtro
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi usada como filtro</param>
        /// <returns>Lista de Solicitudes Sapi que cumplen con el filtro enviado desde el cliente</returns>
        public IList<SolicitudSapi> ObtenerSolicitudesSapiFiltro(SolicitudSapi solicitudSapi)
        {
            IList<SolicitudSapi> solicitudesSapi = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerSolicitudSapi);

                if ((solicitudSapi != null) && (!solicitudSapi.Id.Equals(int.MinValue)) && (solicitudSapi.Id != 0))
                {
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiId, solicitudSapi.Id.ToString());
                    variosFiltros = true;
                }

                //Por fecha de la Solicitud
                if (solicitudSapi.FechaSolicitud != null)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", solicitudSapi.FechaSolicitud);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiFechaSolicitud, fecha);
                    variosFiltros = true;
                }


                if ((solicitudSapi.Material != null) && (!solicitudSapi.Material.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiMaterialId, solicitudSapi.Material.Id);
                    variosFiltros = true;
                }


                if ((solicitudSapi.Departamento != null) && (!solicitudSapi.Departamento.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiDptoId, solicitudSapi.Departamento.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.SolicitanteInic))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiIniciales, solicitudSapi.SolicitanteInic);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.MaterialSolicitado.ToString()))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiStatusSolicitado, solicitudSapi.MaterialSolicitado.ToString());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.MaterialEntregado.ToString()))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiStatusEntregado, solicitudSapi.MaterialEntregado.ToString());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.MaterialRecibido.ToString()))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiStatusRecibido, solicitudSapi.MaterialRecibido.ToString());
                    variosFiltros = true;
                }

                if (!filtro.Equals(String.Empty))
                {
                    filtro += " order by s.FechaSolicitud DESC";
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                solicitudesSapi = query.List<SolicitudSapi>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar Detalles de Compra SAPI por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return solicitudesSapi;
        }


        /// <summary>
        /// Metodo que obtiene solicitudes con estatus SOLICITADO para ENTREGA Y RECEPCION
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Lista de solicitudes con estatus SOLICITADO para ENTREGA Y RECEPCION</returns>
        public IList<SolicitudSapi> ObtenerSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi)
        {
            IList<SolicitudSapi> solicitudesSapi = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerSolicitudSapi);

                if ((solicitudSapi != null) && (!solicitudSapi.Id.Equals(int.MinValue)) && (solicitudSapi.Id != 0))
                {
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiId, solicitudSapi.Id.ToString());
                    variosFiltros = true;
                }

                //Por fecha de la Solicitud
                if (solicitudSapi.FechaSolicitud != null)
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", solicitudSapi.FechaSolicitud);
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiFechaSolicitud, fecha);
                    variosFiltros = true;
                }

                
                if ((solicitudSapi.Departamento != null) && (!solicitudSapi.Departamento.Id.Equals("NGN")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiDptoId, solicitudSapi.Departamento.Id);
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.SolicitanteInic))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiIniciales, solicitudSapi.SolicitanteInic);
                    variosFiltros = true;
                }
                
                if (!string.IsNullOrEmpty(solicitudSapi.MaterialEntregado.ToString()))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiStatusEntregado, solicitudSapi.MaterialEntregado.ToString());
                    variosFiltros = true;
                }

                if (!string.IsNullOrEmpty(solicitudSapi.MaterialRecibido.ToString()))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerSolicitudSapiStatusRecibido, solicitudSapi.MaterialRecibido.ToString());
                    variosFiltros = true;
                }

                if (!filtro.Equals(String.Empty))
                {
                    filtro += " and s.MaterialSolicitado = 'T' order by s.FechaSolicitud DESC";
                }
                else
                {
                    filtro += " s.MaterialSolicitado = 'T' order by s.FechaSolicitud DESC"; 
                }

                IQuery query = Session.CreateQuery(cabecera + filtro);
                solicitudesSapi = query.List<SolicitudSapi>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error al consultar Detalles de Compra SAPI por filtro: " + ex.Message);
            }
            finally
            {
                Session.Close();
            }
            return solicitudesSapi;
        }

    }
}
