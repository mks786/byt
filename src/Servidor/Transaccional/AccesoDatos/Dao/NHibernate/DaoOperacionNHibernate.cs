using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoOperacionNHibernate : DaoBaseNHibernate<Operacion, int>, IDaoOperacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Operacion> ObtenerOperacionesPorMarca(Marca marca)
        {
            IList<Operacion> Operaciones;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerOperacionesPorMarcas, marca.Id));
                Operaciones = query.List<Operacion>();
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

            return Operaciones;
        }

        public IList<Operacion> ObtenerOperacionesPorMarcaYServicio(Operacion operacion)
        {
            IList<Operacion> Operaciones;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerOperacionesPorMarcasYServicio, operacion.Marca.Id, operacion.Servicio.Id));
                Operaciones = query.List<Operacion>();
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

            return Operaciones;
        }

        public IList<Operacion> ObtenerOperacionesFiltro(Operacion operacion)
        {
            IList<Operacion> operaciones = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerOperacion);

            if ((null != operacion) && (operacion.Id != 0))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerOperacionId, operacion.Id);
                variosFiltros = true;
            }

            if ((null != operacion) && !(operacion.Aplicada.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerOperacionMarca, operacion.Aplicada);
                variosFiltros = true;
            }

            if ((null != operacion) && !(operacion.Servicio.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerOperacionServicio, operacion.Servicio.Id);
                variosFiltros = true;
            }

            if ((null != operacion.Marca) && (!operacion.Marca.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerOperacionIdMarca, operacion.Marca.Id);
                variosFiltros = true;
            }

            if ((null != operacion.Fecha) && (!operacion.Fecha.Equals(DateTime.MinValue)))
            {
                if (variosFiltros)
                    filtro += " and ";
                string fecha = String.Format("{0:dd/MM/yy}", operacion.Fecha);
                string fecha2 = String.Format("{0:dd/MM/yy}", operacion.Fecha.Value.AddDays(1));
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerOperacionFecha, fecha, fecha2);
            }



            IQuery query = Session.CreateQuery(cabecera + filtro);
            operaciones = query.List<Operacion>();
            return operaciones;
        }
    }
}
