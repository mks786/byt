using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCambioNombreNHibernate : DaoBaseNHibernate<CambioNombre, int>, IDaoCambioNombre
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<CambioNombre> ObtenerCambiosDeNombreFiltro(CambioNombre cambioDeNombre)
        {
            IList<CambioNombre> CambioDeNombres = null;

            try
            {
                bool variosFiltros = false;
                string filtro = "";
                string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCambioDeNombre);
                if ((null != cambioDeNombre) && (cambioDeNombre.Id != 0))
                {
                    filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeNombreId, cambioDeNombre.Id);
                    variosFiltros = true;
                }
                if ((null != cambioDeNombre.Marca) && (!cambioDeNombre.Marca.Id.Equals("")))
                {
                    if (variosFiltros)
                        filtro += " and ";
                    filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCambioDeNombreIdMarca, cambioDeNombre.Marca.Id);
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
                //}C:\Users\Shachuel\Desktop\Bolet\trunk\src\Servidor\Transaccional\AccesoDatos\Mapeado\ContadorAsignacion.hbm.xml
                IQuery query = Session.CreateQuery(cabecera + filtro);
                CambioDeNombres = query.List<CambioNombre>();
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
            return CambioDeNombres;
        }

    }

    

}
