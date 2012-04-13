using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCambioDeNombrePatenteNHibernate : DaoBaseNHibernate<CambioDeNombrePatente, int>, IDaoCambioDeNombrePatente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<CambioDeNombrePatente> ObtenerCambiosDeNombrePatenteFiltro(CambioDeNombrePatente cambioDeNombre)
        {
            IList<CambioDeNombrePatente> CambioDeNombre = null;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCambioDeNombrePatente);
                
                if ((null != cambioDeNombre) && (cambioDeNombre.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeNombrePatenteId, cambioDeNombre.Id);
                    variosFiltros = true;
                }
                if ((null != cambioDeNombre.Patente) && (!cambioDeNombre.Patente.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeNombrePatenetIdPatente, cambioDeNombre.Patente.Id);
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

                if ((null != cambioDeNombre.Fecha) && (!cambioDeNombre.Fecha.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", cambioDeNombre.Fecha);
                    string fecha2 = String.Format("{0:dd/MM/yy}", cambioDeNombre.Fecha.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeNombrePatenteFecha, fecha, fecha2);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                CambioDeNombre = query.List<CambioDeNombrePatente>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodosUsuariosPorUsuario);
            }
            finally
            {
                Session.Close();
            }
            return CambioDeNombre;
        }

    }

    

}
