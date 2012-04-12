using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCesionPatenteNHibernate : DaoBaseNHibernate<CesionPatente, int>, IDaoCesionPatente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<CesionPatente> ObtenerCesionesPatenteFiltro(CesionPatente cesion)
        {
            IList<CesionPatente> CesionesPatente = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCesionPatente);
                if ((null != cesion) && (cesion.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCesionId, cesion.Id);
                    variosFiltros = true;
                }
                if ((null != cesion.Patente) && (!cesion.Patente.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCesiondPatente, cesion.Patente.Id);
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
                if ((null != cesion.FechaCesion) && (!cesion.FechaCesion.Equals(DateTime.MinValue)))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    string fecha = String.Format("{0:dd/MM/yy}", cesion.FechaCesion);
                    string fecha2 = String.Format("{0:dd/MM/yy}", cesion.FechaCesion.Value.AddDays(1));
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCesionFecha, fecha, fecha2);
                }
                IQuery query = Session.CreateQuery(cabecera + filtro);
                CesionesPatente = query.List<CesionPatente>();

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

            return CesionesPatente;
        }
            
    }
}
